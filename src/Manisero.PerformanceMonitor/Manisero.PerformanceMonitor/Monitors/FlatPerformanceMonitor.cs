using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Manisero.PerformanceMonitor.Monitors
{
	public class FlatPerformanceMonitor<TTask> : IPerformanceMonitor<TTask>
	{
	    private readonly IDictionary<TTask, Stopwatch> _tasks = new Dictionary<TTask, Stopwatch>();
		private readonly List<TTask> _runningTasks = new List<TTask>();

		public void StartTask(TTask task)
		{
			Stopwatch stopwatch;

			if (!_tasks.TryGetValue(task, out stopwatch))
			{
				stopwatch = new Stopwatch();
				_tasks.Add(task, stopwatch);
			}

			if (!stopwatch.IsRunning)
			{
				_runningTasks.Add(task);
				stopwatch.Start();
			}
		}

		public void StopCurrentTask()
		{
			if (_runningTasks.Count == 0)
			{
				throw new InvalidOperationException("No task is running");
			}

			var taskIndex = _runningTasks.Count - 1;
			var task = _runningTasks[taskIndex];

			_tasks[task].Stop();
			_runningTasks.RemoveAt(taskIndex);
		}

		public void StopTask(TTask task)
		{
			var stopwatch = _tasks[task];
			
			if (stopwatch.IsRunning)
			{
				stopwatch.Stop();
				_runningTasks.Remove(task);
			}
		}

		public TasksDurations<TTask> GetResult()
		{
			return _tasks.ToTasksDurations(x => x.Key,
										   x => new TaskDuration<TTask>
											   {
												   Duration = x.Value.Elapsed
											   });
		}
    }
}
