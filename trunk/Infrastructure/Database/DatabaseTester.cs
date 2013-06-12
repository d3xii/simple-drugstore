﻿using System;
using System.Diagnostics;
using System.Linq;
using SDM.ApplicationCore.Console;
using SDM.Infrastructure.Database.Entities;

namespace SDM.Infrastructure.Database
{
    /// <summary>
    /// Provides methods to test the database.
    /// </summary>
    public class DatabaseTester : ConsoleRunnerBase
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets or sets to of this instance.
        /// </summary>
        public IDisposable Tag { get; set; }

        #endregion


        //**************************************************
        //
        // Private variables
        //
        //**************************************************

        #region Private variables

        /// <summary>
        /// Gets database context used by this instance.
        /// </summary>
        private readonly DatabaseContext _context;

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="DatabaseTester"/>.
        /// </summary>
        public DatabaseTester(DatabaseContext context)
        {
            _context = context;
        }

        #endregion



        #region Overrides of ConsoleRunnerBase

        /// <summary>
        /// Fired when the thread is running.
        /// </summary>
        protected override void OnExecute()
        {
            // define constants
            const int testCount = 1000*1000*1000;

            // warm up run
            this.Write("Warming up database...");
            // ReSharper disable UnusedVariable
            AccountEntity[] entities = _context.Accounts.ToArray();

            this.Write("Starting {0:n0} database queries...", testCount);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                AccountEntity accountEntity = _context.Accounts.FirstOrDefault(t => t.IsEnabled);
            }
            stopwatch.Stop();
            this.Write("Finished. Duration: {0}. Speed: {1:n0} queries/s.", stopwatch.Elapsed, testCount / stopwatch.Elapsed.TotalSeconds);
            // ReSharper restore UnusedVariable
        }

        #endregion
    }
}
