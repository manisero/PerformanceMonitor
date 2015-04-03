using Manisero.PerformanceMonitor._Impl;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.NestingPerformanceMonitor
{
	public class ApiTests
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
		public void result_contains_nested_task()
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
			Assert.AreEqual(1, subtasks.Count);
			Assert.True(subtasks.ContainsKey(subtask));
		}
	}
}
