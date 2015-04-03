using System.Threading;
using Manisero.PerformanceMonitor.Monitors;
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
			var result = monitor.GetResult();
			Assert.Greater(result[task].Duration.Ticks, 0);
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
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(18, 22));
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
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(18, 22));
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
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(38, 42));
		}

		[Test]
		public void result_for_two_tasks_is_correct()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task1 = 1;
			var task2 = 2;

			// Act
			monitor.StartTask(task1);
			Thread.Sleep(20);
			monitor.StopTask(task1);

			Thread.Sleep(10);

			monitor.StartTask(task2);
			Thread.Sleep(20);
			monitor.StopTask(task2);
			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task1].Duration.TotalMilliseconds, Is.InRange(18, 22));
			Assert.That(result[task2].Duration.TotalMilliseconds, Is.InRange(18, 22));
		}

		[Test]
		public void result_for_two_tasks_stopped_with_StopCurrentTask_is_correct()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task1 = 1;
			var task2 = 2;

			// Act
			monitor.StartTask(task1);
			Thread.Sleep(20);
			monitor.StopCurrentTask();

			Thread.Sleep(10);

			monitor.StartTask(task2);
			Thread.Sleep(20);
			monitor.StopCurrentTask();
			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task1].Duration.TotalMilliseconds, Is.InRange(18, 22));
			Assert.That(result[task2].Duration.TotalMilliseconds, Is.InRange(18, 22));
		}
	}
}
