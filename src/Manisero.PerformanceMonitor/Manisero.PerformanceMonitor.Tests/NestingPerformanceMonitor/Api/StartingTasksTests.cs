using System.Linq;
using Manisero.PerformanceMonitor._Impl;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.NestingPerformanceMonitor.Api
{
	public class StartingTasksTests
	{
		[Test]
		public void result_contains_started_task()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			var results = monitor.GetResult();

			// Assert
			Assert.AreEqual(1, results.Count);
			Assert.True(results.ContainsKey(task));
		}

		[Test]
		public void task_without_subtasks_has_null_subtasks()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);

			// Assert
			var taskDuration = monitor.GetResult().Single().Value;
			Assert.Null(taskDuration.SubtasksDurations);
		}

		[Test]
		public void result_contains_subtask()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;
			var subtask = 2;

			// Act
			monitor.StartTask(task);
			monitor.StartTask(subtask);
			var result = monitor.GetResult();

			// Assert
			var subtasks = result[task].SubtasksDurations;
			Assert.NotNull(subtasks);
			Assert.AreEqual(1, subtasks.Count);
			Assert.True(subtasks.ContainsKey(subtask));
		}

		[Test]
		public void can_start_same_task_twice()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);

			// Assert
			monitor.StartTask(task);
		}

		[Test]
		public void starting_same_task_twice_does_not_result_in_nesting()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			monitor.StartTask(task);

			// Assert
			var taskDuration = monitor.GetResult().Single().Value;
			Assert.Null(taskDuration.SubtasksDurations);
		}

		[Test]
		public void can_start_same_subtask_twice()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;
			var subtask = 2;

			// Act
			monitor.StartTask(task);
			monitor.StartTask(subtask);

			// Assert
			monitor.StartTask(subtask);
		}
	}
}
