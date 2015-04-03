using System;
using System.Collections.Generic;

namespace Manisero.PerformanceMonitor
{
	public interface IPerformanceMonitor<TTask>
	{
		void StartTask(TTask task);

		void StopTask(TTask task);

		IDictionary<TTask, TimeSpan> GetResults();

		string GetReport(bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null);
	}
}
