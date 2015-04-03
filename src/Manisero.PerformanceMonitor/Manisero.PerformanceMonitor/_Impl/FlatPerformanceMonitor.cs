using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Manisero.PerformanceMonitor._Impl
{
	public class FlatPerformanceMonitor<TTask> : IPerformanceMonitor<TTask>
	{
	    private readonly IDictionary<TTask, Stopwatch> _tasks = new Dictionary<TTask, Stopwatch>();

		public void StartTask(TTask task)
		{
			Stopwatch stopwatch;

			if (!_tasks.TryGetValue(task, out stopwatch))
			{
				stopwatch = new Stopwatch();
				_tasks.Add(task, stopwatch);
			}

			stopwatch.Start();
		}

		public void StopTask(TTask task)
		{
			_tasks[task].Stop();
		}

		public TasksDurations<TTask> GetResults()
		{
			var taskInfos = _tasks.ToDictionary(x => x.Key,
												x => new TaskInfo<TTask>
													{
														Duration = x.Value.Elapsed
													});

			return new TasksDurations<TTask>(taskInfos);
		}

		public string GetReport(bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null)
		{
			if (taskNameFormatter == null)
			{
				taskNameFormatter = x => x.ToString();
			}

			if (taskDurationFormatter == null)
			{
				taskDurationFormatter = x => x.TotalMilliseconds.ToString();
			}

			var report = GetResults().Select(x => string.Format("{0}: {1}",
																taskNameFormatter(x.Key),
																taskDurationFormatter(x.Value.Duration)))
									 .ToList();

			if (includeHeader)
			{
				var header = typeof(TTask).Name + " performance report:";
				report.Insert(0, header);
			}

			return string.Join(Environment.NewLine, report);
		}
    }
}
