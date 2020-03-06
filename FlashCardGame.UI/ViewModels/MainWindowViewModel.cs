using Prism.Mvvm;

namespace FlashCardGame.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _title = "Illumina Flash Card Game";
    }
}