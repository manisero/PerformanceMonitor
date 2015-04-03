using System;

namespace Manisero.PerformanceMonitor._Impl
{
	public class NestingPerformanceMonitor<TTask> : IPerformanceMonitor<TTask>
	{
		public void StartTask(TTask task)
		{
			throw new NotImplementedException();
		}

		public void StopTask(TTask task)
		{
			throw new NotImplementedException();
		}

		public TasksDurations<TTask> GetResult()
		{
			throw new NotImplementedException();
		}

		public string GetReport(bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null)
		{
			throw new NotImplementedException();
		}
	}
}
