using System;

namespace Manisero.PerformanceMonitor
{
	public class TaskDuration<TTask>
	{
		public TimeSpan Duration { get; set; }

		public TasksDurations<TTask> SubtasksDurations { get; set; }
	}
}
