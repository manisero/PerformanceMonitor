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
			public TaskData Parent { get; set; }
			public Stopwatch Stopwatch { get; set; }
			public IDictionary<TTask, TaskData> Subtasks { get; set; }
		}

		private readonly IDictionary<TTask, TaskData> _tasks = new Dictionary<TTask, TaskData>();
		private TaskData _currentTask;

		public void StartTask(TTask task)
		{
			if (_currentTask == null)
			{
				StartTaskOrSubtask(task, _tasks);
			}
			else if (_currentTask.Task.Equals(task))
			{
				_currentTask.Stopwatch.Start();
			}
			else
			{
				StartTaskOrSubtask(task, _currentTask.Subtasks);
			}
		}

		private void StartTaskOrSubtask(TTask task, IDictionary<TTask, TaskData> tasks)
		{
			TaskData taskData;

			if (!tasks.TryGetValue(task, out taskData))
			{
				taskData = new TaskData
				{
					Task = task,
					Parent = _currentTask,
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
			if (_currentTask == null)
			{
				throw new InvalidOperationException(string.Format("Task '{0}' is not started.", task));
			}
			else if (_currentTask.Task.Equals(task))
			{
				StopTaskWithSubtasks(_currentTask);
				_currentTask = _currentTask.Parent;
			}
			else
			{
				StopParentTask(task, _currentTask.Parent);
			}
		}

		private void StopTaskWithSubtasks(TaskData taskData)
		{
			foreach (var subtask in taskData.Subtasks)
			{
				StopTaskWithSubtasks(subtask.Value);
			}

			taskData.Stopwatch.Stop();
		}

		private void StopParentTask(TTask task, TaskData parentTaskData)
		{
			if (parentTaskData == null)
			{
				throw new InvalidOperationException(string.Format("Task '{0}' is not started.", task));
			}

			if (parentTaskData.Task.Equals(task))
			{
				StopTaskWithSubtasks(parentTaskData);

				_currentTask = parentTaskData.Parent;
			}
			else
			{
				StopParentTask(task, parentTaskData);
			}
		}

		public TasksDurations<TTask> GetResult()
		{
			return _tasks.Values.ToTasksDurations(x => x.Task, MapToTaskDuration);
		}

		private TaskDuration<TTask> MapToTaskDuration(TaskData taskData)
		{
			return new TaskDuration<TTask>
				{
					Duration = taskData.Stopwatch.Elapsed,
					SubtasksDurations = taskData.Subtasks.Any()
											? taskData.Subtasks.ToTasksDurations(x => x.Key, x => MapToTaskDuration(x.Value))
											: null
				};
		}

		public string GetReport(bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null)
		{
			throw new NotImplementedException();
		}
	}
}
