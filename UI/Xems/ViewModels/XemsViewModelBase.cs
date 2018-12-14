using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace Xems.ViewModels
{
    public class XemsViewModelBase : ViewModelBase
    {
        private Brush _themeColor;

        private App _app;

        public Brush ThemeColor
        {
            get => this._themeColor;

            set => this.Set("ThemeColor", ref this._themeColor, value);
        }

        public App App => this._app;

        public XemsViewModelBase()
        {
            this._app = (App) App.Current;
            this._themeColor = (Brush)new BrushConverter().ConvertFrom("#FF3580BF");
        }

        public void ChangeWindows(Window from, Window to)
        {
            ((App) App.Current).MainWindow = to;

            to.Show();
            from.Close();
        }

        public string GetResource(string key)
        {
            return (string)Application.Current.Resources[key];
        }
    }
}