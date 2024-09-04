﻿

using UEMM.Core.Common;
using System.IO;

namespace UEMM.Core.Settings
{
    /// <summary>
    /// Stores application settings and allows to save and read them.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Main settings directory.
        /// </summary>
        private readonly string _directory;

        /// <summary>
        /// All application options.
        /// </summary>
        public Options Options { get; internal set; } = new();

        /// <summary>
        /// Path to the application settings file.
        /// </summary>
        public string Path { get; }

        public Manager()
        {
            _directory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "lepo_co\\uemm"
            );

            Path = System.IO.Path.Combine(
                _directory,
                "settings.json"
            );

            Prepare();
        }

        /// <summary>
        /// Checks directory, creates it if needed. Saves default options.
        /// </summary>
        /// <returns></returns>
        private void Prepare()
        {
            IOExtensions.CreateOpenDirectory(_directory);

            if (!File.Exists(Path) || new FileInfo(Path).Length == 0)
                Save();
            else
                Read();
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(Path))
                throw new NullReferenceException(
                    $"ERROR | The write path must be defined in the {typeof(Manager)} constructor.");

            JsonData.Write(Path, Options);
        }

        public async Task SaveAsync()
        {
            await Task.Run(Save);
        }

        public void Read()
        {
            if (String.IsNullOrEmpty(Path))
                throw new NullReferenceException(
                    $"ERROR | The write path must be defined in the {typeof(Manager)} constructor.");

            Options = JsonData.Read<Options>(Path);
        }

        public async Task ReadAsync()
        {
            await Task.Run(Read);
        }
    }
}