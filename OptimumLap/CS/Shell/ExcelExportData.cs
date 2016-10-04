using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using ERP.Infrastructure.CrossCutting;
using ERP.Infrastructure.CrossCutting.Commands;
using ERP.Infrastructure.CrossCutting.Interop;
using ERP.Infrastructure.Data.Core.Excel;
using ERP.Infrastructure.CrossCutting.Utils;

namespace ERP.Wpf.Ui
{
    class ExcelExportData : IExcelExportData
    {
        #region Nested Classes
        private class RealCellsCollection : CellsCollection
        {
            public RealCellsCollection(string[,] cells)
            {
                Cells = cells;
            }

            public void ChangeSize(int rows, int columns, bool preserveData)
            {
                var cells = new string[rows, columns];
                if (preserveData)
                {
                    for (int i = 0; i < Math.Min(Cells.GetLength(0), rows); i++)
                        for (int j = 0; j < Math.Min(Cells.GetLength(1), columns); j++)
                            cells[i, j] = Cells[i, j];
                }
                Cells = cells;
            }
        }

        private class RealColumnHeadersCollection : ColumnHeadersCollection
        {
            public RealColumnHeadersCollection(string[] headers)
            {
                Headers = headers;
            }

            public void ChangeSize(int rows, int columns, bool preserveData)
            {
                var headers = new string[rows];
                if (preserveData)
                {
                    for (int i = 0; i < Math.Min(Headers.GetLength(0), rows); i++)
                        headers[i] = Headers[i];
                }
                Headers = headers;
            }
        }

        private class RealRowBrushCollection : RowBrushCollection
        {
            public RealRowBrushCollection(Brush[] brushes)
            {
                RowBrushes = brushes;
            }

            public void ChangeSize(int rows, bool preserveData)
            {
                var brushes = new Brush[rows];
                if (preserveData)
                {
                    for (int i = 0; i < Math.Min(RowBrushes.GetLength(0), rows); i++)
                        brushes[i] = RowBrushes[i];
                }
                RowBrushes = brushes;
            }
        }

        private class RealCellBrushCollection : CellBrushCollection
        {
            public RealCellBrushCollection(Brush[,] brushes)
            {
                CellBrushes = brushes;
            }

            public void ChangeSize(int rows, int columns, bool preserveData)
            {
                var brushes = new Brush[rows, columns];
                if (preserveData)
                {
                    for (int i = 0; i < Math.Min(CellBrushes.GetLength(0), rows); i++)
                        for (int j = 0; j < Math.Min(CellBrushes.GetLength(1), columns); j++)
                            brushes[i, j] = CellBrushes[i, j];
                }
                CellBrushes = brushes;
            }
        }
        #endregion

        private List<object> _rowData;
        public CellsCollection Cells { get; private set; }
        public ColumnHeadersCollection Headers { get; private set; }
        public RowBrushCollection RowBrushes { get; private set; }
        public CellBrushCollection CellBrushes { get; private set; }
        public Action<dynamic> AfterExport { get; set; }
        public string SheetName { get; set; }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public void ChangeSize(int rows, int columns, bool preserveData)
        {
            ((RealCellsCollection)Cells).ChangeSize(rows, columns, preserveData);
            ((RealColumnHeadersCollection)Headers).ChangeSize(rows, columns, preserveData);
            ((RealRowBrushCollection)RowBrushes).ChangeSize(rows, preserveData);
            ((RealCellBrushCollection)CellBrushes).ChangeSize(rows, columns, preserveData);
        }

        private string GetHeaderString(object o)
        {
            if (o is string)
                return (string)o;
            if (o is TextBlock)
                return ((TextBlock)o).Text;
            return o == null ? string.Empty : o.ToString();
        }

        private class ExcelExportObject
        {
            public string[,] Cells;
            public Brush[] RowBrushes;
            public Brush[,] CellBrushes;
            public int CurrentRow;
            public int PrevRow;
            public GridRowContent[] PhantomRows;
            public CellContentPresenter[,] PhantomCells;
            public List<object> RowData;
            public bool ExportColors;
            public List<GridColumn> VisibleColumns;
            public GridControl Grid;
            public ExcelExportProgressCommand ProgressCommand;
            public Action<ExcelExportData, IDocumentPresenter, ExcelExportProgressCommand> AfterExportComplete;
            public IDocumentPresenter Document;
        }


        public void PrepareExport(GridControl grid, bool exportColors, HashSet<GridColumn> excludeColumn,
                Action<ExcelExportData, IDocumentPresenter, ExcelExportProgressCommand> afterExportComplete,
                ExcelExportProgressCommand progressCommand, IDocumentPresenter document)
        {
            Rows = grid.VisibleRowCount;
            progressCommand.Rows = Rows;
            progressCommand.CurrentRow = 0;
            var tv = grid.View as TableView;
            var eo = new ExcelExportObject
            {
                VisibleColumns =
                    grid.Columns.Where(
                        c => !excludeColumn.Contains(c) && !(c is GridExpandColumn) && c.Visible)
                        .OrderBy(c => c.VisibleIndex)
                        .ToList(),
                RowBrushes = new Brush[grid.VisibleRowCount],
                CurrentRow = 0,
                PrevRow = 0,
                ExportColors = exportColors && tv != null,
                Grid = grid,
                RowData = new List<object>(),
                ProgressCommand = progressCommand,
                AfterExportComplete = afterExportComplete,
                Document = document
            };
            Headers = new RealColumnHeadersCollection(eo.VisibleColumns.Select(c => GetHeaderString(c.Header)).ToArray());
            Columns = eo.VisibleColumns.Count;
            NumberFormatting.ClearFormats();
            for (int i = 0; i < eo.VisibleColumns.Count; i++)
            {
                NumberFormatting.AddFormat(i, eo.VisibleColumns[i].FieldType);
            }
            eo.CellBrushes = new Brush[grid.VisibleRowCount, eo.VisibleColumns.Count];
            eo.Cells = new string[grid.VisibleRowCount, eo.VisibleColumns.Count];
            if (eo.ExportColors)
            {
                eo.PhantomCells = new CellContentPresenter[32, Columns];
                eo.PhantomRows = new GridRowContent[32];
                for (int i = 0; i < 32; i++)
                {
                    eo.PhantomRows[i] = new GridRowContent() { Style = tv.RowStyle };
                    for (int j = 0; j < Columns; j++)
                        eo.PhantomCells[i, j] = new CellContentPresenter { Style = eo.VisibleColumns[j].CellStyle };
                }
            }

            DoExport(eo);
        }

        private void DoExport(ExcelExportObject eo)
        {
            try
            {
                var rows = Math.Min(32, Rows - eo.CurrentRow) - 1;
                var rowIndex = 0;
                while (rows >= 0)
                {
                    var rowHandle = eo.Grid.GetRowHandleByVisibleIndex(eo.CurrentRow);
                    if (rowHandle < 0)
                    {
                        eo.CurrentRow++;
                        rows--;
                        continue;
                    }
                    var data = eo.Grid.GetRow(rowHandle);
                    eo.RowData.Add(data);
                    RowData rd = null;
                    if (eo.ExportColors)
                    {
                        rd = new RowData(new VisualDataTreeBuilder(eo.Grid.View, null, null, null)) { Row = data };
                        eo.PhantomRows[rowIndex].DataContext = rd;
                    }

                    int j = 0;
                    foreach (var column in eo.VisibleColumns)
                    {
                        try
                        {
                            eo.Cells[rowIndex, j] = eo.Grid.GetCellDisplayText(rowHandle, column);
                        }
                        catch { }

                        if (eo.ExportColors)
                        {
                            eo.PhantomCells[rowIndex, j].DataContext = new EditGridCellData(rd);
                        }
                        j++;
                    }

                    eo.CurrentRow++;
                    rowIndex++;
                    rows--;
                }

                eo.Grid.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action<ExcelExportObject>(ExportStep), eo);
            }
            catch
            {
                eo.ProgressCommand.CurrentStep = 4;
                throw;
            }

        }

        private void ExportStep(ExcelExportObject eo)
        {
            try
            {
                if (eo.ExportColors)
                {
                    for (int i = eo.PrevRow; i < eo.CurrentRow; i++)
                    {
                        eo.RowBrushes[i] = eo.PhantomRows[i - eo.PrevRow].Background;
                        for (int j = 0; j < eo.VisibleColumns.Count; j++)
                        {
                            eo.CellBrushes[i, j] = eo.PhantomCells[i - eo.PrevRow, j].Background;
                        }
                    }
                }

                eo.PrevRow = eo.CurrentRow;
                eo.ProgressCommand.CurrentRow = eo.CurrentRow;
                if (eo.CurrentRow < Rows)
                    eo.Grid.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action<ExcelExportObject>(DoExport), eo);
                else
                {
                    eo.ProgressCommand.CurrentStep = 1;
                    if (!eo.ExportColors)
                    {
                        for (int i = 0; i < Rows; i++)
                        {
                            eo.RowBrushes[i] = Brushes.Transparent;
                            for (int j = 0; j < Columns; j++)
                                eo.CellBrushes[i, j] = Brushes.Transparent;
                        }
                    }
                    RowBrushes = new RealRowBrushCollection(eo.RowBrushes);
                    CellBrushes = new RealCellBrushCollection(eo.CellBrushes);
                    Cells = new RealCellsCollection(eo.Cells);
                    _rowData = eo.RowData.ToList();

                    eo.Grid.Dispatcher.BeginInvoke(DispatcherPriority.Background, eo.AfterExportComplete, this, eo.Document, eo.ProgressCommand);
                }
            }
            catch
            {
                eo.ProgressCommand.CurrentStep = 4;
                throw;
            }

        }

        public T GetRow<T>(int row) where T : class
        {
            return _rowData[row] as T;
        }

        private Color MixColors(Color fg, Color bg)
        {
            var A = 1 - (1 - fg.A / 255.0) * (1 - bg.A / 255.0);
            var R = fg.R / 255.0 * fg.A / 255.0 / A + bg.R / 255.0 * bg.A / 255.0 * (1 - fg.A / 255.0) / A;
            var G = fg.G / 255.0 * fg.A / 255.0 / A + bg.G / 255.0 * bg.A / 255.0 * (1 - fg.A / 255.0) / A;
            var B = fg.B / 255.0 * fg.A / 255.0 / A + bg.B / 255.0 * bg.A / 255.0 * (1 - fg.A / 255.0) / A;

            return Color.FromArgb(Convert.ToByte(A * 255), Convert.ToByte(R * 255),
                                  Convert.ToByte(G * 255), Convert.ToByte(B * 255));
        }

        private class ColorArea
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public Color Color { get; private set; }

            private bool CheckHLine(Color[,] colors, int x, int y, int fX)
            {
                while (x < fX)
                {
                    if (colors[y, x] != Color)
                        return false;
                    x++;
                }
                return true;
            }

            private bool CheckVLine(Color[,] colors, int x, int y, int fY)
            {
                while (y < fY)
                {
                    if (colors[y, x] != Color)
                        return false;
                    y++;
                }
                return true;
            }

            public void Fill(Color[,] colors, int x, int y)
            {
                X = x;
                Y = y;
                Color = colors[y, x];
                Width = 1;
                Height = 1;
                var lh = y + 1 < colors.GetLength(0);
                var lv = x + 1 < colors.GetLength(1);

                while (lh || lv)
                {
                    if (lh) lh = CheckHLine(colors, X, Y + Height, X + Width);
                    if (lv) lv = CheckVLine(colors, X + Width, Y, Y + Height);
                    if (lh) Height = Height + 1;
                    if (lv) Width = Width + 1;
                    if (Y + Height >= colors.GetLength(0))
                    {
                        Height = colors.GetLength(0) - Y;
                        lh = false;
                    }
                    if (X + Width >= colors.GetLength(1))
                    {
                        Width = colors.GetLength(1) - X;
                        lv = false;
                    }
                }
            }

            public void Bleach(Color[,] colors)
            {
                for (int i = Y; i < Y + Height; i++)
                    for (int j = X; j < X + Width; j++)
                        colors[i, j] = Colors.White;
            }
        }

        public void GenerateExcelFile(ExcelExportProgressCommand progress)
        {
            var table = new object[Rows + 1, Columns];
            for (int j = 0; j < Columns; j++)
                table[0, j] = Headers[j];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    table[i + 1, j] = Cells[i, j];

            ExcelOpener.OpenInExcel(table, SheetName, AfterExport,
                worksheet => FillWorksheet(progress, worksheet, table));
        }
        /// <summary>
        /// Заполнить лист данными
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="worksheet"></param>
        /// <param name="table"></param>
        private void FillWorksheet(ExcelExportProgressCommand progress, dynamic worksheet, object[,] table)
        {
            var rangeTo = string.Format("{0}:{1}", (table.GetLength(0) + 1), (table.GetLength(1) + 1));
            var r = worksheet.Range["1:1", rangeTo];
            r.NumberFormat = "@";
            var colors = new Color[Rows, Columns];
            ThreadUtils.SafeCall(() => MixCellWithRowColors(colors));
            DecorateWorksheetCells(progress, worksheet, colors);
            progress.CurrentStep = 3;
            using (var context = new ExcelFileContext(worksheet))
                context.Sign();
        }
        /// <summary>
        /// Залить цветом ячейки в таблице
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="worksheet">Страница</param>
        /// <param name="colors">Цвета</param>
        private void DecorateWorksheetCells(ExcelExportProgressCommand progress, dynamic worksheet, Color[,] colors)
        {
            var areas = new List<ColorArea>();
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    var c = colors[i, j];
                    if (c.R < 250 || c.B < 250 || c.G < 250)
                    {
                        var a = new ColorArea();
                        a.Fill(colors, j, i);
                        a.Bleach(colors);
                        areas.Add(a);
                    }
                }

            foreach (var a in areas)
            {
                progress.Area = string.Format("{0}:{1:D2} - {2}:{3:D2}", a.Y + 2, a.X + 1, a.Y + 1 + a.Height,
                    a.X + a.Width);
                var r1 = worksheet.Cells[a.Y + 2, a.X + 1];
                var r2 = worksheet.Cells[a.Y + 1 + a.Height, a.X + a.Width];
                var r = worksheet.Range(r1, r2);
                r.Interior.Color = a.Color.ToOle();
            }
        }

        /// <summary>
        /// Смешать цвет ячеек с цветом столбца
        /// </summary>
        /// <param name="colors"></param>
        private void MixCellWithRowColors(Color[,] colors)
        {
            for (int i = 0; i < Rows; i++)
            {
                var rowBrush = RowBrushes[i] as SolidColorBrush;
                for (int j = 0; j < Columns; j++)
                {
                    var brush = CellBrushes[i, j] as SolidColorBrush;
                    if (brush != null || rowBrush != null)
                    {
                        var fg = brush == null ? Color.FromArgb(0, 0, 0, 0) : brush.Color;
                        var bg = rowBrush == null ? Color.FromArgb(0, 0, 0, 0) : rowBrush.Color;
                        colors[i, j] = MixColors(fg, MixColors(bg, Colors.White));
                    }
                }
            }
        }
    }


    class ExcelExportProgressCommand : AbstractAsyncCommand
    {
        private static readonly string[] Messages =
            new[]{
                     "Получение данных из таблицы",
                     "Обработка данных",
                     "Формирование Excel-файла",
                     "Форматирование",
                     "Финиш"
                  };

        public int CurrentStep
        {
            get { return _currentStep; }
            set
            {
                if (value != _currentStep)
                {
                    _currentStep = value;
                    if (_currentStep == 0)
                        Message = string.Format("Получение данных из таблицы, {0} из {1}", CurrentRow, Rows);
                    else
                        Message = Messages[_currentStep];
                    while (_prevStep < _currentStep)
                    {
                        _prevStep++;
                        ReportProgress();
                    }
                }
            }
        }

        public int CurrentRow
        {
            get { return _currentRow; }
            set
            {
                if (value != _currentRow)
                {
                    _currentRow = value;
                    if (_currentStep == 0)
                        Message = string.Format("Получение данных из таблицы, {0} из {1}", CurrentRow, Rows);
                }
            }
        }


        public string Area
        {
            get { return _area; }
            set
            {
                _area = value;
                if (CurrentStep == 2 && !string.IsNullOrEmpty(_area))
                {
                    Message = Messages[CurrentStep] + ", " + Environment.NewLine + "выставление цвета зоне " + _area;
                }
            }
        }

        public int Rows { get; set; }
        public ExcelExportData Data { get; set; }
        private int _prevStep;
        private int _currentStep;
        private int _currentRow;
        private string _area;
        public ExcelExportProgressCommand()
        {
            CurrentStep = 0;
            _prevStep = 0;
        }

        protected override void ExecuteInternal(object parameter)
        {
            ThreadUtils.SafeCall(() =>
            {
                Title = "Экспорт таблицы в Excel";
                StepCount = 5;
            });
            while (CurrentStep < 4)
            {
                System.Threading.Thread.Sleep(250);
                if (Data != null && CurrentStep == 2)
                {
                    try
                    {
                        Data.GenerateExcelFile(this);
                    }
                    finally
                    {
                        CurrentStep = 4;
                    }
                }
            }
        }
    }
}
