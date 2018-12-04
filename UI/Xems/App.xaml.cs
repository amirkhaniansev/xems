using AuthTokenService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using UsersApiConsumer.Core;
using Xems.Resources;
using Xems.Views.Windows;

namespace Xems
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly TokenProvider _tokenProvider;

        private readonly UsersApiClient _usersApiClient;

        private readonly bool _isReadyForStartup;

        public TokenProvider TokenProvider => this._tokenProvider;

        public UsersApiClient UsersApiClient => this._usersApiClient;

        public App()
        {
            try
            {
                this._tokenProvider = new TokenProvider(
                    ConfigurationManager.AppSettings["AuthAPI"],28);

                this._usersApiClient = new UsersApiClient(ConfigurationManager.AppSettings["UsersAPI"]);

                this.ConfigureHandlers();

                this._isReadyForStartup = true;
            }
            catch (Exception ex)
            {
                this._isReadyForStartup = false;
            }
        }

        private async void App_OnStartup(object sender, StartupEventArgs e)
        {
            this.CheckBeforeStartup();

            if (!this._isReadyForStartup)
            {
                XemsMsgBox.Show(Strings.UnableToStart);
                return;
            }

            if (string.IsNullOrEmpty(XemsUser.Default.RefreshToken) ||
                string.IsNullOrEmpty(XemsUser.Default.Username))
            {
                new SignInWindow().Show();
            }
            else
            {
                try
                {
                    var status = await this._tokenProvider
                        .CheckRefreshTokenAsync(XemsUser.Default.RefreshToken);

                    if (status == TokenStatus.Error)
                    {
                        new SignInWindow().Show();
                        return;
                    }

                    var userResponse = await this._usersApiClient
                        .GetUserByUsernameAsync(XemsUser.Default.Username);

                    if (userResponse.ResponseStatus != ResponseStatus.Success || userResponse.Data == null)
                    {
                        new SignInWindow().Show();
                        return;
                    }

                    var user = userResponse.Data;

                    XemsUser.Default.Username = user.Username;
                    XemsUser.Default.CurrentProfile = user.CurrentProfileType;
                    XemsUser.Default.FirstName = user.FirstName;
                    XemsUser.Default.LastName = user.LastName;
                    XemsUser.Default.Id = user.Id;
                    XemsUser.Default.Save();

                    new MainWindow().Show();
                }
                catch (Exception)
                {
                    XemsMsgBox.Show(Strings.UnknownError);
                }
            }
        }

        private void CheckBeforeStartup()
        {
            var processes = Process.GetProcesses();

            var currentProcess = Process.GetCurrentProcess();

            var runningProcess = processes
                .FirstOrDefault(process => process.Id != currentProcess.Id &&
                 process.ProcessName.Equals(currentProcess.ProcessName, StringComparison.Ordinal));

            if (runningProcess == null)
                return;

            ShowWindow(runningProcess.MainWindowHandle, 5);

            this.Shutdown();
        }

        private void ConfigureHandlers()
        {
            this._tokenProvider.TokenUpdated += this._usersApiClient.UpdateToken;
            this._tokenProvider.TokenUpdated += this.RefreshToken;
        }

        private void RefreshToken(object sender, TokenEventArgs eventArgs)
        {
            if(eventArgs.RefreshToken == null || eventArgs.RefreshToken == XemsUser.Default.RefreshToken)
                return;

            XemsUser.Default.RefreshToken = eventArgs.RefreshToken;
            XemsUser.Default.Save();
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            XemsMsgBox.Show(Strings.UnknownError);
        }
    }
}
