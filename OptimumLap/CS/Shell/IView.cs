namespace MobileRibbonMVVMSample
{
    /// <summary>
    /// Базовый интерфейс для представлений
    /// </summary>
    public interface IView
    {
        object DataContext { get; set; }
    }
}
