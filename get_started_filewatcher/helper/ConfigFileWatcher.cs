/*
* Copyright (c) 2021-2024 Siemens AG. All rights reserved.
* This software is the confidential and proprietary information of Siemens AG.
* This file is part of Trusted Traceability.
*/
using System.Runtime.CompilerServices;
using System.Text.Json;
using PublisherEDCAdapter.Domain.Common.Constants;

[assembly: InternalsVisibleTo(InternalVisibleTestConstants.PublisherEDCAdapterTest)]
namespace PublisherEDCAdapter.Helper
{
    /// <summary>
    /// Represents a watcher for a configuration file.
    /// </summary>
    /// <typeparam name="T">The type of the configuration object.</typeparam>
    internal class ConfigFileWatcher<T> : IConfigFileWatcher<T>, IDisposable
    {
        private bool _disposed = false;
        private readonly string _filePath;
        private readonly string _directoryPath;
        private readonly FileSystemWatcher _fileWatcher;
        private EventHandler<ConfigFileChangedEventArgs<T>>? _configFileChanged;
        private EventHandler<ConfigFileChangedEventArgs<T>>? _intialConfigRead;

        /// <summary>
        /// Event that is raised when the configuration file is changed.
        /// </summary>
        /// <typeparam name="T">The type of the configuration file.</typeparam>
        event EventHandler<ConfigFileChangedEventArgs<T>>? IConfigFileWatcher<T>.ConfigFileChanged
        {
            add
            {
                _configFileChanged += value;
            }
            remove
            {
                _configFileChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the initial configuration is read.
        /// </summary>
        /// <typeparam name="T">The type of the configuration.</typeparam>
        event EventHandler<ConfigFileChangedEventArgs<T>>? IConfigFileWatcher<T>.IntialConfigRead
        {
            add
            {
                _intialConfigRead += value;
            }
            remove
            {
                _intialConfigRead -= value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFileWatcher{T}"/> class.
        /// </summary>
        /// <param name="directoryPath">The directory path where the configuration file is located.</param>
        /// <param name="filePath">The file path of the configuration file.</param>
        public ConfigFileWatcher(string directoryPath, string filePath)
        {
            _directoryPath = directoryPath ?? throw new ArgumentNullException(nameof(directoryPath));
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

            _fileWatcher = new FileSystemWatcher
            {
                Path = _directoryPath,
                Filter = Path.GetFileName(_filePath),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
            };

            _fileWatcher.Deleted += OnConfigFileDeleted;
            _fileWatcher.Renamed += OnConfigFileDeleted;
            _fileWatcher.Changed += OnConfigFileChanged;
            _fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Handles the event when the configuration file is deleted.
        /// </summary>
        private void OnConfigFileDeleted(object sender, FileSystemEventArgs e)
        {
            if ((e.ChangeType == WatcherChangeTypes.Deleted|| e.ChangeType == WatcherChangeTypes.Renamed) && !File.Exists(_filePath))
            {
                T defaultConfig = CreateDefaultConfig();
                string defaultConfigJson = JsonSerializer.Serialize(defaultConfig);
                File.WriteAllText(_filePath, defaultConfigJson);
            }
        }

        /// <summary>
        /// Handles the event when the configuration file is changed.
        /// </summary>
        private void OnConfigFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                ParseConfigAndRaiseChangedEvent();
            }
        }

        /// <summary>
        /// Creates a default configuration object.
        /// </summary>
        /// <returns>The default configuration object.</returns>
        private static T CreateDefaultConfig()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Parses the configuration file and raises the <see cref="ConfigFileChanged"/> event.
        /// </summary>
        private void ParseConfigAndRaiseEvent(StreamReader reader, EventHandler<ConfigFileChangedEventArgs<T>>? eventHandler)
        {
            string newConfigJson = reader.ReadToEnd();
            try
            {
                var config = JsonSerializer.Deserialize<T>(newConfigJson);
                if (config != null)
                {
                    eventHandler?.Invoke(this, new ConfigFileChangedEventArgs<T>(config));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }
        }

        private void ParseConfigAndRaiseChangedEvent()
        {
            using (FileStream fs = File.Open(Path.Combine(_directoryPath, _filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    ParseConfigAndRaiseEvent(reader, _configFileChanged);
                }
            }
        }

        void IConfigFileWatcher<T>.StartWatching()
        {
            using (FileStream fs = File.Open(Path.Combine(_directoryPath, _filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    ParseConfigAndRaiseEvent(reader, _intialConfigRead);
                }
            }
        }

        /// <summary>
        /// Stops watching the configuration file.
        /// </summary>
        void IConfigFileWatcher<T>.StopWatching()
        {
            _fileWatcher.Deleted -= OnConfigFileDeleted;
            _fileWatcher.Renamed -= OnConfigFileDeleted;
            _fileWatcher.Changed -= OnConfigFileChanged;
            _fileWatcher.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Releases the resources used by the <see cref="ConfigFileWatcher{T}"/> object.
        /// </summary>
        /// <param name="disposing">A value indicating whether the object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ((IConfigFileWatcher<T>)this).StopWatching();
                    _fileWatcher.Dispose();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
