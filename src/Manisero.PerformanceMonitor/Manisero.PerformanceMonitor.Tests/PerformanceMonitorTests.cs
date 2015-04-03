using Xunit;

namespace Manisero.PerformanceMonitor.Tests
{
    public class PerformanceMonitorTests
    {
		[Fact]
		public void results_contain_started_task()
		{
			// Arrange
			var monitor = new PerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.EnterTaskScope(task);
			var results = monitor.GetResults();

			// Assert
			Assert.Equal(1, results.Count);
			Assert.True(results.ContainsKey(task));
		}
    }
}
