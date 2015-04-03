using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Manisero.PerformanceMonitor._Impl
{
	public class NestingPerformanceMonitor<TTask> : IPerformanceMonitor<TTask>
	{
		private class TaskData
		{
			public TTask Task { get; set; }
			public Stopwatch Stopwatch { get; set; }
			public IDictionary<TTask, TaskData> Subtasks { get; set; }
		}

		private readonly IDictionary<TTask, TaskData> _tasks = new Dictionary<TTask, TaskData>();
		private TaskData _currentTask;

		public void StartTask(TTask task)
		{
			if (_currentTask == null)
			{
				StartTask(task, _tasks);
			}
			else if (_currentTask.Task.Equals(task))
			{
				_currentTask.Stopwatch.Start();
			}
			else
			{
				StartTask(task, _currentTask.Subtasks);
			}
		}

		private void StartTask(TTask task, IDictionary<TTask, TaskData> tasks)
		{
			TaskData taskData;

			if (!tasks.TryGetValue(task, out taskData))
			{
				taskData = new TaskData
				{
					Task = task,
					Stopwatch = new Stopwatch(),
					Subtasks = new Dictionary<TTask, TaskData>()
				};

				tasks.Add(task, taskData);
			}

			_currentTask = taskData;
			taskData.Stopwatch.Start();
		}

		public void StopTask(TTask task)
		{
			throw new NotImplementedException();
		}

		public TasksDurations<TTask> GetResult()
		{
			return _tasks.Values.ToTasksDurations(x => x.Task, MapToTaskInfo);
		}

		private TaskInfo<TTask> MapToTaskInfo(TaskData taskData)
		{
			return new TaskInfo<TTask>
				{
					Duration = taskData.Stopwatch.Elapsed,
					SubtasksDurations = taskData.Subtasks != null
											? taskData.Subtasks.ToTasksDurations(x => x.Key, x => MapToTaskInfo(x.Value))
											: null
				};
		}

		public string GetReport(bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null)
		{
			throw new NotImplementedException();
		}
	}
}
