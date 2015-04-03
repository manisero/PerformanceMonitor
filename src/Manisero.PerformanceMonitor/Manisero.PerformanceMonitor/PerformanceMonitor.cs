using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Manisero.PerformanceMonitor
{
    public class PerformanceMonitor<TTask>
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

		public IDictionary<TTask, TimeSpan> GetResults()
		{
			return _tasks.ToDictionary(x => x.Key, x => x.Value.Elapsed);
		}
    }
}
