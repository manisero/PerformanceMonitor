using System;
using Xunit;

namespace Manisero.PerformanceMonitor.Tests
{
	public class ApiTests
    {
		[Fact]
		public void results_contain_started_task()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			var results = monitor.GetResults();

			// Assert
			Assert.Equal(1, results.Count);
			Assert.True(results.ContainsKey(task));
		}

		[Fact]
		public void results_contain_started_tasks()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var tasks = new[] { 1, 2, 3 };

			// Act
			foreach (var task in tasks)
			{
				monitor.StartTask(task);
			}
			
			var results = monitor.GetResults();

			// Assert
			Assert.Equal(3, results.Count);

			foreach (var task in tasks)
			{
				Assert.True(results.ContainsKey(task));
			}
		}

		[Fact]
		public void can_start_same_task_twice()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			
			// Assert
			monitor.StartTask(task);
		}

		[Fact]
		public void stopping_never_started_task_fails()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Assert
			Assert.ThrowsAny<Exception>(() => monitor.StopTask(task));
		}

		[Fact]
		public void stopping_stopped_task_does_not_fail()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			monitor.StopTask(task);

			// Assert
			monitor.StopTask(task);
		}
    }
}
