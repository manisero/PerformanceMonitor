using System;
using System.Threading;
using Manisero.PerformanceMonitor._Impl;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.NestingPerformanceMonitor
{
	public class ReportFormattingTests
	{
		private NestingPerformanceMonitor<string> Execute()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<string>();
			var task1 = "task1";
			var subtask1_1 = "sub-task 1.1";
			var subsubtask1_1_1 = "sub-sub-task 1.1.1";
			var subsubtask1_1_2 = "sub-sub-task 1.1.2";
			var subtask1_2 = "sub-task 1.2";
			var subsubtask1_2_1 = "sub-sub-task 1.2.1";
			var subsubtask1_2_2 = "sub-sub-task 1.2.2";

			var task2 = "task2";
			var subtask2_1 = "sub-task 2.1";
			var subsubtask2_1_1 = "sub-sub-task 2.1.1";
			var subsubtask2_1_2 = "sub-sub-task 2.1.2";
			var subtask2_2 = "sub-task 2.2";
			var subsubtask2_2_1 = "sub-sub-task 2.2.1";
			var subsubtask2_2_2 = "sub-sub-task 2.2.2";

			// Act - task1:
			monitor.StartTask(task1);
			monitor.StartTask(subtask1_1); // subtask1_1
			monitor.StartTask(subsubtask1_1_1);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask1_1_1);
			monitor.StartTask(subsubtask1_1_2);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask1_1_2);
			monitor.StopTask(subtask1_1);
			monitor.StartTask(subtask1_2); // subtask1_2
			monitor.StartTask(subsubtask1_2_1);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask1_2_1);
			monitor.StartTask(subsubtask1_2_2);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask1_2_2);
			monitor.StopTask(subtask1_2);
			monitor.StopTask(task1);

			// Act - task2:
			monitor.StartTask(task2);
			monitor.StartTask(subtask2_1); // subtask2_1
			monitor.StartTask(subsubtask2_1_1);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask2_1_1);
			monitor.StartTask(subsubtask2_1_2);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask2_1_2);
			monitor.StopTask(subtask2_1);
			monitor.StartTask(subtask2_2); // subtask2_2
			monitor.StartTask(subsubtask2_2_1);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask2_2_1);
			monitor.StartTask(subsubtask2_2_2);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask2_2_2);
			monitor.StopTask(subtask2_2);
			monitor.StopTask(task2);

			return monitor;
		}

		[Test]
		public void formats_default_report()
		{
			var monitor = Execute();

			// Assert
			var report = monitor.GetResult().FormatReport();
			Console.Write(report);
		}

		[Test]
		public void formats_report_without_header()
		{
			var monitor = Execute();

			// Assert
			var report = monitor.GetResult().FormatReport(false);
			Console.Write(report);
		}

		[Test]
		public void formats_report_with_custom_task_name_formatter()
		{
			var monitor = Execute();

			// Assert
			var report = monitor.GetResult().FormatReport(taskNameFormatter: x => x.ToUpper());
			Console.Write(report);
		}

		[Test]
		public void formats_report_with_custom_task_duration_formatter()
		{
			var monitor = Execute();

			// Assert
			var report = monitor.GetResult().FormatReport(taskDurationFormatter: x => x.ToString());
			Console.Write(report);
		}
	}
}
