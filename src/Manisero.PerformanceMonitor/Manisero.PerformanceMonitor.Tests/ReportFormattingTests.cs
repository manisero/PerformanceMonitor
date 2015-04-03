using System;
using System.Diagnostics;
using Xunit;

namespace Manisero.PerformanceMonitor.Tests
{
	public class ReportFormattingTests
	{
		[Fact]
		public void formats_default_report()
		{
			// Arrange
			var monitor = new PerformanceMonitor<string>();
			var task1 = "task1";
			var task2 = "task2";

			// Act
			monitor.StartTask(task1);
			monitor.StartTask(task2);

			// Assert
			var report = monitor.GetReport();
		}
	}
}
