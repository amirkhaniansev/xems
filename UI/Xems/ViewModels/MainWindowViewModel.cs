namespace Xems.ViewModels
{
    public class MainWindowViewModel : XemsViewModelBase
    {
        private string _name;

        private string _profilePhotoUrl;

        private string _currentProfileType;

        public string Name
        {
            get => this._name;

            set => this.Set("Name", ref this._name, value);
        }

        public string ProfilePhotoUrl
        {
            get => this._profilePhotoUrl;

            set => this.Set("ProfilePhotoUrl", ref this._profilePhotoUrl, value);
        }

        public string CurrentProfileType
        {
            get => this._currentProfileType;

            set => this.Set("CurrentProfileType", ref this._currentProfileType, value);
        }

        public MainWindowViewModel()
        {
            this.Name = "Sevak John Amirkhanian";
            this._currentProfileType = "Student";
        }
    }
}