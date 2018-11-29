using System.Windows;
using Xems.Globals;

namespace Xems.ViewModels
{
    public class LoadableWindowViewModel : XemsViewModelBase
    {
        private Visibility _textVisibility;

        private Visibility _spinnerVisibility;

        private bool _isSpinning;

        public Visibility SpinnerVisibility
        {
            get => this._spinnerVisibility;

            set => this.Set(Constants.SpinnerVisibility, ref this._spinnerVisibility, value);
        }

        public Visibility TextVisibility
        {
            get => this._textVisibility;

            set => this.Set(Constants.TextVisibility, ref _textVisibility, value);
        }

        public bool IsSpinning
        {
            get => this._isSpinning;

            set => this.Set(Constants.IsSpinning, ref this._isSpinning, value);
        }

        public LoadableWindowViewModel()
        {
            this.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
        }

        public void SetVisibilities(Visibility spinnerVisibility, Visibility textVisibility, bool isSpinning)
        {
            this.SpinnerVisibility = spinnerVisibility;
            this.TextVisibility = textVisibility;
            this.IsSpinning = isSpinning;
        }
    }
}