using System;
using System.Collections.Generic;
using System.Threading;

namespace SDM.ApplicationCore.Console
{
    /// <summary>
    /// Provides foundation to execute a long running work in background thread.
    /// </summary>
    public abstract class ConsoleRunnerBase
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets a value indicates whether the service is stopped.
        /// </summary>
        public bool IsStopped { get; private set; }

        /// <summary>
        /// Gets a value indicates whether the services run successfully.
        /// </summary>
        public bool IsSuccess { get; private set; }

        #endregion

        
        //**************************************************
        //
        // Private variables
        //
        //**************************************************

        #region Private variables

        /// <summary>
        /// Gets pending messages in this console.
        /// </summary>
        private readonly List<string> _pendingMessages = new List<string>();        

        #endregion

        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Starts given action.
        /// </summary>
        public void Start()
        {
            Thread thread = new Thread(this.ExecuteInNewThread)
                                {
                                    Name = this.GetType().Name,
                                    IsBackground = true,
                                    Priority = ThreadPriority.BelowNormal,
                                };
            thread.Start();
        }

        /// <summary>
        /// Writes given text into the console.
        /// </summary>
        public void Write(string message, params object[] args)
        {
            lock (_pendingMessages)
            {
                _pendingMessages.Add(string.Format(message, args));
            }
        }

        /// <summary>
        /// Gets all pending messages from the buffer and clears the buffer.
        /// </summary>
        public string[] GetAndClearPendingMessages()
        {
            lock (_pendingMessages)
            {
                // take the result
                string[] result = this._pendingMessages.ToArray();

                // clear buffer
                _pendingMessages.Clear();

                // return result
                return result;
            }
        }

        #endregion


        //**************************************************
        //
        // Protected methods
        //
        //**************************************************

        #region Protected methods

        /// <summary>
        /// Fired when the thread is running.
        /// </summary>
        protected abstract void OnExecute();

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Starts in new thread.
        /// </summary>
        private void ExecuteInNewThread()
        {
            try
            {
                IsSuccess = false;
                this.OnExecute();
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                this.Write(ex.ToString());
            }
            finally
            {
                IsStopped = true;
            }
        }

        #endregion

    }
}
