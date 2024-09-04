﻿

using System.IO;
using System.Text.Json;

namespace UEMM.Core.Settings
{
    internal class JsonData
    {
        /// <summary>
        /// Reads a JSON file and tries to cast it to a object.
        /// </summary>
        /// <param name="filePath">The absolute path to the file containing the JSON text string</param>
        public static T Read<T>(string filePath) where T : new()
        {
            try
            {
                return JsonSerializer.Deserialize<T>(
                    File.ReadAllText(filePath)
                ) ?? new T();
            }
            catch
            {
                return new T();
            }
        }

        /// <summary>
        /// Writes an object to a file in JSON format
        /// </summary>
        /// <param name="filePath">The absolute path where the file will be saved</param>
        /// <param name="objectToWrite">A C# object to be cast to a text string as JSON</param>
        public static void Write<T>(string filePath, T objectToWrite)
        {
            File.WriteAllText(
                filePath,
                JsonSerializer.Serialize(
                    objectToWrite,
                    typeof(T),
                    new JsonSerializerOptions { WriteIndented = true }
                )
            );
        }
    }
}