# PerformanceMonitor
Tiny performance monitoring tool for .NET.

Available via NuGet:
https://www.nuget.org/packages/Manisero.PerformanceMonitor

Usage:
-----

Find a feature you want to monitor...
```C#
static void Main(string[] args)
{
	// Database operations:
	OpenDatabaseConnection(); // = Thread.Sleep(50);
	ReadDatabaseData(); // = Thread.Sleep(300);
	WriteDatabaseData(); // = Thread.Sleep(500);
	CloseDatabaseConnection(); // = Thread.Sleep(20);

	// FileSystem operations:
	ReadFiles(); // = Thread.Sleep(200);
	WriteFiles(); // = Thread.Sleep(400);
}
```

...define tasks for it...
```C#
public enum DataImportTask
{
	DatabaseOperations,
	ReadData,
	WriteData,

	FileSystemOperations,
	ReadFiles,
	WriteFiles
}
```

...create PerformanceMonitor class for your tasks (just for convenience)...
```C#
public class ImportMonitor : PerformanceMonitor<DataImportTask>
{
}
```

...add monitoring to your feature...
```C#
static void Main(string[] args)
{
	// Database operations:
	ImportMonitor.StartTask(DataImportTask.DatabaseOperations);
	OpenDatabaseConnection(); // = Thread.Sleep(50);

	ImportMonitor.StartTask(DataImportTask.ReadData);
	ReadDatabaseData(); // = Thread.Sleep(300);
	ImportMonitor.StopCurrentTask();

	ImportMonitor.StartTask(DataImportTask.WriteData);
	WriteDatabaseData(); // = Thread.Sleep(500);
	ImportMonitor.StopCurrentTask();

	CloseDatabaseConnection(); // = Thread.Sleep(20);
	ImportMonitor.StopCurrentTask();

	// FileSystem operations:
	ImportMonitor.StartTask(DataImportTask.FileSystemOperations);

	ImportMonitor.StartTask(DataImportTask.ReadFiles);
	ReadFiles(); // = Thread.Sleep(200);
	ImportMonitor.StopCurrentTask();

	ImportMonitor.StartTask(DataImportTask.WriteFiles);
	WriteFiles(); // = Thread.Sleep(400);
	ImportMonitor.StopCurrentTask();

	ImportMonitor.StopCurrentTask();

	// Get monitoring results:
	var report = ImportMonitor.GetResult().FormatReport();
	Console.WriteLine(report);
}
```

...get performance report...
```
DataImportTask performance report:
DatabaseOperations: 909,7298 ms
        ReadData: 312,6394 ms
        WriteData: 514,0502 ms
FileSystemOperations: 608,3821 ms
        ReadFiles: 202,8857 ms
        WriteFiles: 405,489 ms
```

...fix any performance issues and remove monitoring from your feature to make it clean again.
