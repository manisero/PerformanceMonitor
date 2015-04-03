using System;
using System.Collections.Generic;
using System.Linq;

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

	public static class TasksDurationsExtensions
	{
		public static TasksDurations<TTask> ToTasksDurations<TTask>(this IDictionary<TTask, TaskInfo<TTask>> dictionary)
		{
			return new TasksDurations<TTask>(dictionary);
		}

		public static TasksDurations<TTask> ToTasksDurations<TSource, TTask>(this IEnumerable<TSource> source, Func<TSource, TTask> keySelector, Func<TSource, TaskInfo<TTask>> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector).ToTasksDurations();
		}
	}
}
