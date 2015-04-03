using System;

namespace Manisero.PerformanceMonitor
{
	public class TaskInfo<TTask>
	{
		public TimeSpan Duration { get; set; }

		public TasksDurations<TTask> SubtasksDurations { get; set; }
	}
}
