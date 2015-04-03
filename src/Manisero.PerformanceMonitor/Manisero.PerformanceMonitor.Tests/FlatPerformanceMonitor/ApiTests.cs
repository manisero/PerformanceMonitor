using Manisero.PerformanceMonitor.Monitors;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.FlatPerformanceMonitor
{
	public class ApiTests
    {
		[Test]
		public void result_contains_started_task()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			var result = monitor.GetResult();

			// Assert
			Assert.AreEqual(1, result.Count);
			Assert.True(result.ContainsKey(task));
		}

		[Test]
		public void result_contains_started_tasks()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var tasks = new[] { 1, 2, 3 };

			// Act
			foreach (var task in tasks)
			{
				monitor.StartTask(task);
			}
			
			var result = monitor.GetResult();

			// Assert
			Assert.AreEqual(3, result.Count);

			foreach (var task in tasks)
			{
				Assert.True(result.ContainsKey(task));
			}
		}

		[Test]
		public void can_start_same_task_twice()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			
			// Assert
			monitor.StartTask(task);
		}

		[Test]
		public void stopping_never_started_task_fails()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Assert
			Assert.Catch(() => monitor.StopTask(task));
		}

		[Test]
		public void stopping_stopped_task_does_not_fail()
		{
			// Arrange
			var monitor = new FlatPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			monitor.StopTask(task);

			// Assert
			monitor.StopTask(task);
		}
    }
}
