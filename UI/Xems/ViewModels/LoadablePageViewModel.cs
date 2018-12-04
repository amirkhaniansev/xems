using System.Windows;
using Xems.Globals;

namespace Xems.ViewModels
{
    public class LoadablePageViewModel : XemsViewModelBase
    {
        private Visibility _spinnerVisibility;

        private bool _isLoading;

        private bool _isSpinning;

        public Visibility SpinnerVisibility
        {
            get => this._spinnerVisibility;

            set => this.Set(Constants.SpinnerVisibility, ref this._spinnerVisibility, value);
        }

        public bool IsSpinning
        {
            get => this._isSpinning;

            set => this.Set(Constants.IsSpinning, ref this._isSpinning, value);
        }

        public bool IsLoading
        {
            get => this._isLoading;

            set => this._isLoading = value;
        }

        public LoadablePageViewModel()
        {
            this.IsLoading = false;
            this.SetVisibilities(Visibility.Collapsed, false);
        }

        public void SetVisibilities(Visibility spinnerVisibility, bool isSpinning)
        {
            this.SpinnerVisibility = spinnerVisibility;
            this.IsSpinning = isSpinning;
        }
    }
}