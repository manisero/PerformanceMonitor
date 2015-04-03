using System.Threading;
using Manisero.PerformanceMonitor._Impl;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.FlatPerformanceMonitor
{
	public class MeasurementTests
	{
		[Test]
		public void started_task_is_monitored()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(10);

			// Assert
			var results = monitor.GetResults();
			Assert.Greater(results[task].Ticks, 0);
		}

		[Test]
		public void result_for_started_task_is_correct()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);

			// Assert
			var results = monitor.GetResults();
			Assert.That(results[task].TotalMilliseconds, Is.InRange(18, 22));
		}

		[Test]
		public void result_for_stopped_task_is_correct()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);
			Thread.Sleep(20);

			// Assert
			var results = monitor.GetResults();
			Assert.That(results[task].TotalMilliseconds, Is.InRange(18, 22));
		}

		[Test]
		public void result_for_task_started_twice_is_correct()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
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
			Assert.That(results[task].TotalMilliseconds, Is.InRange(38, 42));
		}
	}
}
