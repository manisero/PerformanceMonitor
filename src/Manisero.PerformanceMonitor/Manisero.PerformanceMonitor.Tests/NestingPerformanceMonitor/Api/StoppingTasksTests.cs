using Manisero.PerformanceMonitor._Impl;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.NestingPerformanceMonitor.Api
{
	public class StoppingTasksTests
	{
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
	}
}
