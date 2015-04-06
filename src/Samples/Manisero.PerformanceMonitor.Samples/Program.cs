using System;
using System.Threading;

namespace Manisero.PerformanceMonitor.Samples
{
	public enum DataImportTask
	{
		DatabaseOperations,
		ReadData,
		WriteData,

		FileSystemOperations,
		ReadFiles,
		WriteFiles
	}

	public class ImportMonitor : PerformanceMonitor<DataImportTask>
	{
	}

	class Program
	{
		static void Main(string[] args)
		{
			// Database operations:
			ImportMonitor.StartTask(DataImportTask.DatabaseOperations);
			OpenDatabaseConnection();

			ImportMonitor.StartTask(DataImportTask.ReadData);
			ReadDatabaseData();
			ImportMonitor.StopCurrentTask();

			ImportMonitor.StartTask(DataImportTask.WriteData);
			WriteDatabaseData();
			ImportMonitor.StopCurrentTask();

			CloseDatabaseConnection();
			ImportMonitor.StopCurrentTask();

			// FileSystem operations:
			ImportMonitor.StartTask(DataImportTask.FileSystemOperations);

			ImportMonitor.StartTask(DataImportTask.ReadFiles);
			ReadFiles();
			ImportMonitor.StopCurrentTask();

			ImportMonitor.StartTask(DataImportTask.WriteFiles);
			WriteFiles();
			ImportMonitor.StopCurrentTask();

			ImportMonitor.StopCurrentTask();

			// Get monitoring results:
			var report = ImportMonitor.GetResult().FormatReport();
			Console.WriteLine(report);
		}

		private static void OpenDatabaseConnection()
		{
			Thread.Sleep(50);
		}

		private static void CloseDatabaseConnection()
		{
			Thread.Sleep(20);
		}

		private static void ReadDatabaseData()
		{
			Thread.Sleep(300);
		}

		private static void WriteDatabaseData()
		{
			Thread.Sleep(500);
		}

		private static void ReadFiles()
		{
			Thread.Sleep(200);
		}

		private static void WriteFiles()
		{
			Thread.Sleep(400);
		}
	}
}
