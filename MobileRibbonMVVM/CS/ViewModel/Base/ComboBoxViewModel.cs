using DevComponents.WPF.Controls;

namespace OptimumLap.ViewModel
{
    public class ComboBoxViewModel : ItemSelectorViewModel
    {
        public string Header { get; set; }
        public bool IsEditable { get; set; }
        public bool IsReadOnly { get; set; }
        public AutoCompleteOptions AutoCompleteOption { get; set; }

        private string _Text;
        public virtual string Text
        {
            get { return _Text; }
            set { SetPropertyValue(value, ref _Text, "Text"); }
        }
    }
}
