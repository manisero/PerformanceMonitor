using System.Collections.Generic;

namespace Manisero.PerformanceMonitor
{
	public class TasksDurations<TTask> : Dictionary<TTask, TaskInfo<TTask>>
	{
		public TasksDurations()
		{
		}

		public TasksDurations(IDictionary<TTask, TaskInfo<TTask>> dictionary)
			: base(dictionary)
		{
		}
	}
}
