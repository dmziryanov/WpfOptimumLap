using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace OptimumLap
{
    /// <summary>
    /// Extends RichTextBox with properties that can be bound to a view model.
    /// </summary>
    public class FlowDocumentEditor : RichTextBox
    {
        static FlowDocumentEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlowDocumentEditor), new FrameworkPropertyMetadata(typeof(FlowDocumentEditor)));    
        }

        public FlowDocumentEditor()
        {
            Loaded += delegate { UpdateProperties(); };
        }

        #region Dependency Properties

        /// <Summary>
        /// Backing store for ParagraphAlignment.
        /// </Summary>
        public static readonly DependencyProperty ParagraphAlignmentProperty =
            DependencyProperty.Register("ParagraphAlignment", typeof(TextAlignment), typeof(FlowDocumentEditor), new PropertyMetadata(TextAlignment.Left,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).SetPropertyOnSelectedParagraphs(Block.TextAlignmentProperty, (TextAlignment)e.NewValue);                    
                }));
        /// <summary>
        /// Get or sets the alignment of current paragraph.
        /// </summary>
        public TextAlignment ParagraphAlignment
        {
            get { return (TextAlignment)GetValue(ParagraphAlignmentProperty); }
            set { SetCurrentValue(ParagraphAlignmentProperty, value); }
        }
        
        /// <summary>
        /// Backing store for ParagraphListDescription.
        /// </summary>
        public static readonly DependencyProperty ParagraphListDescriptionProperty = 
            DependencyProperty.Register("ParagraphListDescription", typeof(ListDescription), typeof(FlowDocumentEditor), new PropertyMetadata(ListDescription.None,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).UpdateParagraphList((ListDescription)e.OldValue, (ListDescription)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets the list description of the current paragraph.
        /// </summary>
        public ListDescription ParagraphListDescription
        {
            get { return (ListDescription) GetValue(ParagraphListDescriptionProperty); }
            set { SetValue(ParagraphListDescriptionProperty, value); }
        }

        /// <Summary>
        /// Backing store for ParagraphLineSpacing.
        /// </Summary>
        public static readonly DependencyProperty ParagraphLineSpacingProperty =
            DependencyProperty.Register("ParagraphLineSpacing", typeof(double), typeof(FlowDocumentEditor), new PropertyMetadata(1.0,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).SetParagraphLineHeight((double)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets the spacing between lines of the current paragraph.
        /// </summary>
        public double ParagraphLineSpacing
        {
            get { return (double)GetValue(ParagraphLineSpacingProperty); }
            set { SetCurrentValue(ParagraphLineSpacingProperty, value); }
        }

        /// <Summary>
        /// Backing store for ParagraphStyle.
        /// </Summary>
        public static readonly DependencyProperty ParagraphStyleProperty =
            DependencyProperty.Register("ParagraphStyle", typeof(ParagraphStyle), typeof(FlowDocumentEditor), new PropertyMetadata(null,
                (s, e) =>
                {
                    var editor = (FlowDocumentEditor)s;
                    editor.SetPropertyOnSelectedParagraphs(FrameworkContentElement.StyleProperty, (Style)e.NewValue);
                    editor.UpdateProperties();
                }));
        /// <summary>
        /// Gets or sets the ParagraphStyle of the current paragraph.
        /// </summary>
        public ParagraphStyle ParagraphStyle
        {
            get { return (ParagraphStyle)GetValue(ParagraphStyleProperty); }
            set { SetCurrentValue(ParagraphStyleProperty, value); }
        }
        
        /// <summary>
        /// Backing store for SelectionBackground.
        /// </summary>
        public static readonly DependencyProperty SelectionBackgroundProperty = 
            DependencyProperty.Register("SelectionBackground", typeof(Brush), typeof(FlowDocumentEditor), new PropertyMetadata(null,
                (s, e) =>
                {
                   ((FlowDocumentEditor)s).ApplySelectionPropertyValue(TextElement.BackgroundProperty, (Brush)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets the background of the selection.
        /// </summary>
        public Brush SelectionBackground
        {
            get { return (Brush) GetValue(SelectionBackgroundProperty); }
            set { SetValue(SelectionBackgroundProperty, value); }
        }

        /// <Summary>
        /// Backing store for IsCurrentBold.
        /// </Summary>
        public static readonly DependencyProperty SelectionIsBoldProperty =
            DependencyProperty.Register("SelectionIsBold", typeof(bool), typeof(FlowDocumentEditor), new PropertyMetadata(false,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).ApplySelectionPropertyValue(TextElement.FontWeightProperty, (bool)e.NewValue ? FontWeights.Bold : FontWeights.Normal);
                }));
        /// <summary>
        /// Gets or sets whether the current selection is bold.
        /// </summary>
        public bool SelectionIsBold
        {
            get { return (bool)GetValue(SelectionIsBoldProperty); }
            set { SetCurrentValue(SelectionIsBoldProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionIsItalic.
        /// </Summary>
        public static readonly DependencyProperty SelectionIsItalicProperty =
            DependencyProperty.Register("SelectionIsItalic", typeof(bool), typeof(FlowDocumentEditor), new PropertyMetadata(false,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).ApplySelectionPropertyValue(TextElement.FontStyleProperty, (bool)e.NewValue ? FontStyles.Italic : FontStyles.Normal);
                }));
        /// <summary>
        /// Gets or sets whether the font at current selection is italic.
        /// </summary>
        public bool SelectionIsItalic
        {
            get { return (bool)GetValue(SelectionIsItalicProperty); }
            set { SetCurrentValue(SelectionIsItalicProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionIsStrikeThrough.
        /// </Summary>
        public static readonly DependencyProperty SelectionIsStrikeThroughProperty =
            DependencyProperty.Register("SelectionIsStrikeThrough", typeof(bool), typeof(FlowDocumentEditor), new PropertyMetadata(false,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).UpdateDocumentTextDecorations();
                }));
        /// <summary>
        /// Gets or sets whether the font at current selection has strikethrough.
        /// </summary>
        public bool SelectionIsStrikeThrough
        {
            get { return (bool)GetValue(SelectionIsStrikeThroughProperty); }
            set { SetCurrentValue(SelectionIsStrikeThroughProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionIsSubscript.
        /// </Summary>
        public static readonly DependencyProperty SelectionIsSubscriptProperty =
            DependencyProperty.Register("SelectionIsSubscript", typeof(bool), typeof(FlowDocumentEditor), new PropertyMetadata(false,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).ApplySelectionPropertyValue(Typography.VariantsProperty, (bool)e.NewValue ? FontVariants.Subscript : FontVariants.Normal);
                }));
        /// <summary>
        /// Gets or sets whether the current selection is superscript.
        /// </summary>
        public bool SelectionIsSubscript
        {
            get { return (bool)GetValue(SelectionIsSubscriptProperty); }
            set { SetCurrentValue(SelectionIsSubscriptProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionIsSuperscript.
        /// </Summary>
        public static readonly DependencyProperty SelectionIsSuperscriptProperty =
            DependencyProperty.Register("SelectionIsSuperscript", typeof(bool), typeof(FlowDocumentEditor), new PropertyMetadata(false,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).ApplySelectionPropertyValue(Typography.VariantsProperty, (bool)e.NewValue ? FontVariants.Superscript : FontVariants.Normal);
                }));
        /// <summary>
        /// Gets or sets whether the current selection is superscript.
        /// </summary>
        public bool SelectionIsSuperscript
        {
            get { return (bool)GetValue(SelectionIsSuperscriptProperty); }
            set { SetCurrentValue(SelectionIsSuperscriptProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionIsUnderline.
        /// </Summary>
        public static readonly DependencyProperty SelectionIsUnderlineProperty =
            DependencyProperty.Register("SelectionIsUnderline", typeof(bool), typeof(FlowDocumentEditor), new PropertyMetadata(false,
                (s, e) =>
                {
                    ((FlowDocumentEditor)s).UpdateDocumentTextDecorations();
                }));
        /// <summary>
        /// Gets or sets whether the current selection is underlined.
        /// </summary>
        public bool SelectionIsUnderline
        {
            get { return (bool)GetValue(SelectionIsUnderlineProperty); }
            set { SetCurrentValue(SelectionIsUnderlineProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionFontFamilyName.
        /// </Summary>
        public static readonly DependencyProperty SelectionFontFamilyNameProperty =
            DependencyProperty.Register("SelectionFontFamilyName", typeof(string), typeof(FlowDocumentEditor), new PropertyMetadata(null,
                (s, e) =>
                {
                    if(e.NewValue != null)
                        ((FlowDocumentEditor)s).ApplySelectionPropertyValue(TextElement.FontFamilyProperty, (string)e.NewValue);                    
                }));
        /// <summary>
        /// Get or set the font family of the current selection.
        /// </summary>
        public string SelectionFontFamilyName
        {
            get { return (string)GetValue(SelectionFontFamilyNameProperty); }
            set { SetCurrentValue(SelectionFontFamilyNameProperty, value); }
        }

        /// <Summary>
        /// Backing store for SelectionFontSize.
        /// </Summary>
        public static readonly DependencyProperty SelectionFontSizeProperty =
            DependencyProperty.Register("SelectionFontSize", typeof(double), typeof(FlowDocumentEditor), new PropertyMetadata(16d,
                (s, e) =>
                {
                    var value = (double)e.NewValue;
                    if(!double.IsNaN(value))
                        ((FlowDocumentEditor)s).ApplySelectionPropertyValue(TextElement.FontSizeProperty, value);
                }));
        /// <summary>
        /// Get or set the font size of the current selection.
        /// </summary>
        public double SelectionFontSize
        {
            get { return (double)GetValue(SelectionFontSizeProperty); }
            set { SetCurrentValue(SelectionFontSizeProperty, value); }
        }
        
        /// <summary>
        /// Backing store for SelectionForeground.
        /// </summary>
        public static readonly DependencyProperty SelectionForegroundProperty = 
            DependencyProperty.Register("SelectionForeground", typeof(Brush), typeof(FlowDocumentEditor), new PropertyMetadata(null,
                (s, e) =>
                {                    
                    ((FlowDocumentEditor)s).ApplySelectionPropertyValue(TextElement.ForegroundProperty, (Brush)e.NewValue);
                }));
        /// <summary>
        /// Gets or sets the foreground of the current selection.
        /// </summary>
        public Brush SelectionForeground
        {
            get { return (Brush) GetValue(SelectionForegroundProperty); }
            set { SetValue(SelectionForegroundProperty, value); }
        }

        #endregion // Dependency Properties

        #region Overrides

        protected override void OnInitialized(EventArgs e)
        {            
            AcceptsTab = true;
            AcceptsReturn = true;
            base.OnInitialized(e);
        }

        private bool _TextChanging;

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            _TextChanging = true;
            base.OnTextChanged(e);
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            UpdateProperties();
        }

        protected override void OnSelectionChanged(RoutedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (!_TextChanging && !IsMouseCaptured)
                UpdateProperties();
            _TextChanging = false;
        }

        #endregion // Overrides

        #region Public Methods

        public void ClearSelectionFormatting()
        {
            Selection.ClearAllProperties();
            UpdateProperties();
        }

        public void HighlightSelection()
        {
            Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
        }

        public void SetSelectionForeground(Color color)
        {
            Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        public void IncreaseIndentation()
        {
            EditingCommands.IncreaseIndentation.Execute(null, this);
        }

        public void DecreaseIndentation()
        {
            EditingCommands.DecreaseIndentation.Execute(null, this);
        }

        public bool IncrementSpacingAfterParagraph(double increment, bool testWithoutChanging = false)
        {
            var canChange = false;
            var current = Selection.Start;
            while (current != null && Selection.End.CompareTo(current) >= 0)
            {
                var p = current.Paragraph;
                if (p == null)
                {
                    current = current.GetNextInsertionPosition(LogicalDirection.Forward);
                    continue;
                }

                current = p.ContentEnd.GetNextInsertionPosition(LogicalDirection.Forward);

                var margin = p.Margin;
                margin.Bottom += increment;
                var localCanChange = margin.Bottom >= 0;
                canChange |= localCanChange;

                if (testWithoutChanging || !localCanChange)
                    continue;
                
                p.Margin = margin;
            }

            return canChange;
        }

        #endregion // Public Methods

        #region Private

        private bool _UpdatingProperties;
        
        private void ApplySelectionPropertyValue(DependencyProperty property, object value)
        {
            if(_UpdatingProperties)
                return;

            Selection.ApplyPropertyValue(property, value);
            if(property == FontSizeProperty)
                SetParagraphLineHeight(ParagraphLineSpacing);
        }
    
        private void UpdateProperties()
        {
            if (_UpdatingProperties)
                return;
            _UpdatingProperties = true;

            SelectionForeground = Selection.GetPropertyValue(TextElement.ForegroundProperty) as Brush;
            SelectionBackground = Selection.GetPropertyValue(TextElement.BackgroundProperty) as Brush;

            var fontWeight = Selection.GetPropertyValue(FontWeightProperty);
            SelectionIsBold = fontWeight != DependencyProperty.UnsetValue && (FontWeight)fontWeight == FontWeights.Bold;

            var fontStyle = Selection.GetPropertyValue(FontStyleProperty);
            SelectionIsItalic = fontStyle != DependencyProperty.UnsetValue && (FontStyle)fontStyle == FontStyles.Italic;

            var fontSize = Selection.GetPropertyValue(TextElement.FontSizeProperty);
            SelectionFontSize = fontSize != DependencyProperty.UnsetValue ? (double)fontSize : double.NaN;

            var fontVariant = Selection.GetPropertyValue(Typography.VariantsProperty);
            SelectionIsSubscript = fontVariant != DependencyProperty.UnsetValue && (FontVariants)fontVariant == FontVariants.Subscript;
            SelectionIsSuperscript = fontVariant != DependencyProperty.UnsetValue && (FontVariants)fontVariant == FontVariants.Superscript;

            var fontFamily = Selection.GetPropertyValue(TextElement.FontFamilyProperty) as FontFamily;
            if (fontFamily != null)
                SelectionFontFamilyName = fontFamily.Source;

            var alignment = GetPropertyValueOfSelectedParagrahps(Block.TextAlignmentProperty);
            if(alignment != DependencyProperty.UnsetValue)
                ParagraphAlignment = (TextAlignment)alignment;

            UpdateTextDecorationProperties();
            UpdateParagraphLineSpacing();

            ParagraphStyle = GetPropertyValueOfSelectedParagrahps(FrameworkContentElement.StyleProperty) as ParagraphStyle;

            _UpdatingProperties = false;
        }

        private void UpdateParagraphLineSpacing()
        {
            var paragraphLineHeight = (double)GetPropertyValueOfSelectedParagrahps(Block.LineHeightProperty);
            if(double.IsNaN(paragraphLineHeight))
                ParagraphLineSpacing = 1.15;
            else
            {
                var fontSize = GetMaxFontSizeInSelectedParagraphs();
                ParagraphLineSpacing = paragraphLineHeight/fontSize;
            }
        }

        private void SetParagraphLineHeight(double spacing)
        {
            var lineHeight = spacing*GetMaxFontSizeInSelectedParagraphs();
            SetPropertyOnSelectedParagraphs(Block.LineHeightProperty, lineHeight);
        }
        
        private void UpdateTextDecorationProperties()
        {
            var decorations = (TextDecorationCollection)Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            SelectionIsUnderline = decorations.Contains(TextDecorations.Underline[0]);
            SelectionIsStrikeThrough = decorations.Contains(TextDecorations.Strikethrough[0]);
        }

        private void UpdateDocumentTextDecorations()
        {
            var decorations = new TextDecorationCollection();
            if (SelectionIsStrikeThrough)
                decorations.Add(TextDecorations.Strikethrough[0]);
            if(SelectionIsUnderline)
                decorations.Add(TextDecorations.Underline[0]);
            Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, decorations);
        }
        
        private void UpdateParagraphList(ListDescription oldList, ListDescription newList)
        {
            if(newList.Equals(ListDescription.None))
            {
                if(oldList.IsBullet)
                    EditingCommands.ToggleBullets.Execute(null, this);
                else
                    EditingCommands.ToggleNumbering.Execute(null, this);
            }
            else
            {
                if(newList.IsBullet)
                    EditingCommands.ToggleBullets.Execute(null, this);
                else
                    EditingCommands.ToggleNumbering.Execute(null, this);
            }
        }

        private double GetMaxFontSizeInSelectedParagraphs()
        {
            double size = 0;

            foreach(var paragraph in GetParagraphsInCurrentSelection())
            {
                if(paragraph.Inlines.Count == 0)
                    size = Math.Max(size, paragraph.FontSize);
                else
                {
                    foreach(var inline in paragraph.Inlines)
                        size = Math.Max(size, inline.FontSize);
                }
            }

            return size;
        }
        
        private void SetPropertyOnSelectedParagraphs(DependencyProperty property, object value)
        {
            foreach (var paragraph in GetParagraphsInCurrentSelection())
            {
                if (value != null)
                    paragraph.SetValue(property, value);
                else
                    paragraph.ClearValue(property);
            }

            if(property == StyleProperty)
            {
                SetParagraphLineHeight(ParagraphLineSpacing);
                UpdateProperties();
            }
        }

        private object GetPropertyValueOfSelectedParagrahps(DependencyProperty property)
        {
            object value = null;
            foreach(var paragraph in GetParagraphsInCurrentSelection())
            {
                var v = paragraph.GetValue(property);
                if (value == null)
                    value = v;
                else if (!Equals(value, v))
                    return DependencyProperty.UnsetValue;
            }

            return value;
        }

        private IEnumerable<Paragraph> GetParagraphsInCurrentSelection()
        {
            var current = Selection.Start;
            while (current != null && Selection.End.CompareTo(current) >= 0)
            {
                var p = current.Paragraph;
                if (p != null)
                {
                    yield return p;                   
                    current = p.ContentEnd.GetNextInsertionPosition(LogicalDirection.Forward);
                }
                else
                    current = current.GetNextInsertionPosition(LogicalDirection.Forward);
            }
        }

        #endregion 
    }
}
