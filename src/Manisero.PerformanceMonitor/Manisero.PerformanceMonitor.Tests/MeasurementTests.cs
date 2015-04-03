using System.Threading;
using Xunit;

namespace Manisero.PerformanceMonitor.Tests
{
	public class MeasurementTests
	{
		[Fact]
		public void started_task_is_monitored()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(10);

			// Assert
			var results = monitor.GetResults();
			Assert.InRange(results[task].Ticks, 1, long.MaxValue);
		}

		[Fact]
		public void result_for_started_task_is_correct()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);

			// Assert
			var results = monitor.GetResults();
			Assert.InRange(results[task].Milliseconds, 18, 22);
		}

		[Fact]
		public void result_for_stopped_task_is_correct()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);
			Thread.Sleep(20);

			// Assert
			var results = monitor.GetResults();
			Assert.InRange(results[task].Milliseconds, 18, 22);
		}

		[Fact]
		public void result_for_task_started_twice_is_correct()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);

			Thread.Sleep(10);

			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);
			Thread.Sleep(10);

			// Assert
			var results = monitor.GetResults();
			Assert.InRange(results[task].Milliseconds, 38, 42);
		}
	}
}