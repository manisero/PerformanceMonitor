using System;
using System.Collections.Generic;

namespace Manisero.PerformanceMonitor._Impl
{
	public class NestedPerformanceMonitor<TTask> : IPerformanceMonitor<TTask>
	{
		public void StartTask(TTask task)
		{
			throw new NotImplementedException();
		}

		public void StopTask(TTask task)
		{
			throw new NotImplementedException();
		}

		public IDictionary<TTask, TimeSpan> GetResults()
		{
			throw new NotImplementedException();
		}

		public string GetReport(bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null)
		{
			throw new NotImplementedException();
		}
	}
}
