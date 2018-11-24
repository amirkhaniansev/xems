using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Xems.ViewModels
{
    public class XemsViewModelBase : ViewModelBase
    {
        private Brush _themeColor;
        
        public Brush ThemeColor
        {
            get => this._themeColor;

            set => this.Set("ThemeColor", ref this._themeColor, value);
        }

        public XemsViewModelBase()
        {
            this._themeColor = (Brush)new BrushConverter().ConvertFrom("#FF3580BF");
        }
    }
}