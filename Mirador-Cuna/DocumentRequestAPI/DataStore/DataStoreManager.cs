using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentRequestAPI.DataStore
{
	public class DataStoreManager
	{
		/// <summary>
		/// Gets the path to the application ProgramData directory
		/// </summary>
		/// <returns></returns>
		public static string GetAppDataDirectory()
		{
			string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

			var solutionName = Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName);

			// Combine the base folder with your specific folder....
			string specificFolder = Path.Combine(appDataPath, solutionName);

			// CreateDirectory will check if folder exists and, if not, create it.
			// If folder exists then CreateDirectory will do nothing.
			Directory.CreateDirectory(specificFolder);

			return specificFolder;
		}

		/// <summary>
		/// Serialize the given object to JSON and store the result to a data store file
		/// </summary>
		/// <typeparam name="T">Type of data being serialized</typeparam>
		/// <param name="data">Object to be serialized</param>
		/// <param name="dataStoreFileName">Optional name given to the data store file. If null the data type name will be used</param>
		public static void SerializeJsonToStore<T>(object data, string dataStoreFileName = null)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (string.IsNullOrEmpty(dataStoreFileName))
			{
				dataStoreFileName = typeof(T).ToString();
			}

			string json = JsonConvert.SerializeObject(data, Formatting.Indented);
			string path = Path.Combine(GetAppDataDirectory(), dataStoreFileName + @".txt");
			File.WriteAllText(path, json);
		}

		/// <summary>
		/// Retreives deserialized data from the store as a collection of its type
		/// </summary>
		/// <typeparam name="T">Type of data being deserialized</typeparam>
		/// <param name="dataStoreFileName">Optional name given to the data store file. If null the data type name will be used</param>
		/// <returns>List of the deserialized data objects</returns>
		public static List<T> DeserializeObjectsFromStore<T>(string dataStoreFileName = null)
		{
			if (string.IsNullOrEmpty(dataStoreFileName))
			{
				dataStoreFileName = typeof(T).ToString();
			}

			string path = Path.Combine(GetAppDataDirectory(), dataStoreFileName + @".txt");

			if (!File.Exists(path))
			{
				return new List<T>();
			}

			string json = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<List<T>>(json);
		}

		/// <summary>
		/// Flushes all data from the data store
		/// </summary>
		public static void FlushDataStore()
		{
			DirectoryInfo di = new DirectoryInfo(GetAppDataDirectory());

			foreach (FileInfo file in di.GetFiles())
			{
				file.Delete();
			}

			foreach (DirectoryInfo dir in di.GetDirectories())
			{
				dir.Delete(true);
			}
		}

	}
}