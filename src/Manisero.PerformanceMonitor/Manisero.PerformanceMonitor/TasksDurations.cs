using System;
using System.Collections.Generic;
using System.Linq;

namespace Manisero.PerformanceMonitor
{
	public class TasksDurations<TTask> : Dictionary<TTask, TaskDuration<TTask>>
	{
		public TasksDurations()
		{
		}

		public TasksDurations(IDictionary<TTask, TaskDuration<TTask>> dictionary)
			: base(dictionary)
		{
		}
	}

	public static class TasksDurationsExtensions
	{
		public static TasksDurations<TTask> ToTasksDurations<TTask>(this IDictionary<TTask, TaskDuration<TTask>> dictionary)
		{
			return new TasksDurations<TTask>(dictionary);
		}

		public static TasksDurations<TTask> ToTasksDurations<TSource, TTask>(this IEnumerable<TSource> source, Func<TSource, TTask> keySelector, Func<TSource, TaskDuration<TTask>> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector).ToTasksDurations();
		}
	}
}
