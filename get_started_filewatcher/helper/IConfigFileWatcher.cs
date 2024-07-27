/*
* Copyright (c) 2021-2024 Siemens AG. All rights reserved.
* This software is the confidential and proprietary information of Siemens AG.
* This file is part of Trusted Traceability.
*/
namespace PublisherEDCAdapter.Helper
{
    /// <summary>
    /// Represents an interface for a config file watcher.
    /// </summary>
    /// <typeparam name="T">The type of the config file.</typeparam>
    internal interface IConfigFileWatcher<T>
    {
        /// <summary>
        /// Event that is raised when the config file has changed.
        /// </summary>
        event EventHandler<ConfigFileChangedEventArgs<T>> ConfigFileChanged;

        /// <summary>
        /// Event that is raised when the initial config file has been read.
        /// </summary>
        event EventHandler<ConfigFileChangedEventArgs<T>> IntialConfigRead;

        /// <summary>
        /// Starts watching the config file for changes.
        /// </summary>
        void StartWatching();

        /// <summary>
        /// Stops watching the config file for changes.
        /// </summary>
        void StopWatching();
    }
}
