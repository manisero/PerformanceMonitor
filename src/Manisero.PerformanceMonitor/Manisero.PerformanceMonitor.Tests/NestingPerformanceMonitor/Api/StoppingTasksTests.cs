using Manisero.PerformanceMonitor.Monitors;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.NestingPerformanceMonitor.Api
{
	public class StoppingTasksTests
	{
		[Test]
		public void stopping_never_started_task_fails()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Assert
			Assert.Catch(() => monitor.StopTask(task));
		}

		[Test]
		public void stopping_stopped_task_fails()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			monitor.StopTask(task);

			// Assert
			Assert.Catch(() => monitor.StopTask(task));
		}

		[Test]
		public void result_contains_stopped_task()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			monitor.StopTask(task);
			var results = monitor.GetResult();

			// Assert
			Assert.AreEqual(1, results.Count);
			Assert.True(results.ContainsKey(task));
		}

		[Test]
		public void starting_task_after_stopping_another_task_does_not_result_in_nesting()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task1 = 1;
			var task2 = 2;

			// Act
			monitor.StartTask(task1);
			monitor.StopTask(task1);
			monitor.StartTask(task2);
			var results = monitor.GetResult();

			// Assert
			Assert.AreEqual(2, results.Count);
			Assert.True(results.ContainsKey(task1));
			Assert.True(results.ContainsKey(task2));
			Assert.Null(results[task1].SubtasksDurations);
		}

		[Test]
		public void subtask_gets_nested_in_each_task_it_is_started_in()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task1 = 1;
			var task2 = 2;
			var subtask = 3;

			// Act
			monitor.StartTask(task1);
			monitor.StartTask(subtask);
			monitor.StopTask(task1);

			monitor.StartTask(task2);
			monitor.StartTask(subtask);

			var results = monitor.GetResult();

			// Assert
			Assert.AreEqual(2, results.Count);

			Assert.True(results.ContainsKey(task1));
			Assert.NotNull(results[task1].SubtasksDurations);
			Assert.True(results[task1].SubtasksDurations.ContainsKey(subtask));

			Assert.True(results.ContainsKey(task2));
			Assert.NotNull(results[task2].SubtasksDurations);
			Assert.True(results[task2].SubtasksDurations.ContainsKey(subtask));
		}
	}
}
