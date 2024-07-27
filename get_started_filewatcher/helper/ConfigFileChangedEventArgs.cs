/*
* Copyright (c) 2021-2024 Siemens AG. All rights reserved.
* This software is the confidential and proprietary information of Siemens AG.
* This file is part of Trusted Traceability.
*/
namespace PublisherEDCAdapter.Helper
{
    /// <summary>
    /// Represents the event arguments for a configuration file change event.
    /// </summary>
    /// <typeparam name="T">The type of the configuration data.</typeparam>
    internal class ConfigFileChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets the configuration data associated with the event.
        /// </summary>
        public T ConfigData { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFileChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="configData">The configuration data.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="configData"/> is null.</exception>
        public ConfigFileChangedEventArgs(T configData)
        {
            ConfigData = configData ?? throw new ArgumentNullException(nameof(configData));
        }
    }
}
