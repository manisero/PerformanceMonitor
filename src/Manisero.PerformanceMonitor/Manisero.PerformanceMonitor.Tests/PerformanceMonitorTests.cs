using Xunit;

namespace Manisero.PerformanceMonitor.Tests
{
    public class PerformanceMonitorTests
    {
		[Fact]
		public void Passing()
		{
			Assert.Equal(3, 3);
		}

		[Fact]
		public void Failing()
		{
			Assert.Equal(3, 5);
		}
    }
}
