using System;
using System.Timers;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AuthTokenService
{
    /// <summary>
    /// Class for updating something periodically
    /// </summary>
    internal class Updater : IDisposable
    {
        /// <summary>
        /// Timer
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// Background worker
        /// </summary>
        private readonly BackgroundWorker _backgroundWorker;

        /// <summary>
        /// Interval in minutes
        /// </summary>
        private readonly int _interval;

        /// <summary>
        /// Update method
        /// </summary>
        private Func<Task> _updaterMethod;

        /// <summary>
        /// Creates new instance of <see cref="Updater"/>
        /// </summary>
        /// <param name="interval">Interval in minutes</param>
        public Updater(int interval,Func<Task> updateMethod)
        {
            // setting fields
            this._interval = interval;
            this._updaterMethod = updateMethod;

            // creating timer
            this._timer = new Timer(this._interval * 0xEA60);
            this._timer.Elapsed += this.ExecuteWhenTimerElapse;

            // creating background worker
            this._backgroundWorker = new BackgroundWorker();
            this._backgroundWorker.DoWork += this.DoBackgroundWork;
        }

        /// <summary>
        /// Starts update periodic process
        /// </summary>
        public void StartUpdatePeriod()
        {
            // start timer
            this._timer.Start();
        }

        /// <summary>
        /// Dispose updater
        /// </summary>
        public void Dispose()
        {
            this._timer.Dispose();
            this._backgroundWorker.Dispose();
        }

        /// <summary>
        /// Does background work
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private async void DoBackgroundWork(object sender, DoWorkEventArgs e)
        {
            if (this._updaterMethod != null)
                await this._updaterMethod();
        }

        /// <summary>
        /// Executes when timer is elapsed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void ExecuteWhenTimerElapse(object sender, ElapsedEventArgs e)
        {
            if (!this._backgroundWorker.IsBusy)
                this._backgroundWorker.RunWorkerAsync();
        }
    }
}
