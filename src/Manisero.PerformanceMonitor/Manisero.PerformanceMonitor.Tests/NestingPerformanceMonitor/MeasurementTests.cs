using System.Threading;
using Manisero.PerformanceMonitor._Impl;
using NUnit.Framework;

namespace Manisero.PerformanceMonitor.Tests.NestingPerformanceMonitor
{
	public class MeasurementTests
	{
		[Test]
		public void started_task_is_monitored()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.Greater(result[task].Duration.Ticks, 0);
		}

		[Test]
		public void result_for_stopped_task_is_correct()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);
			Thread.Sleep(20);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(18, 24));
		}

		[Test]
		public void result_for_task_started_twice_is_correct()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);

			Thread.Sleep(10);

			monitor.StartTask(task);
			Thread.Sleep(20);
			monitor.StopTask(task);
			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(38, 44));
		}

		[Test]
		public void result_for_task_with_subtask_is_correct()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;
			var subtask = 2;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(10);

			monitor.StartTask(subtask);
			Thread.Sleep(20);
			monitor.StopTask(subtask);

			Thread.Sleep(10);
			monitor.StopTask(task);

			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(38, 44));
			Assert.That(result[task].SubtasksDurations[subtask].Duration.TotalMilliseconds, Is.InRange(18, 24));
		}

		[Test]
		public void result_for_task_with_two_subtasks_is_correct()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;
			var subtask1 = 2;
			var subtask2 = 3;

			// Act
			monitor.StartTask(task);
			Thread.Sleep(10);

			monitor.StartTask(subtask1);
			Thread.Sleep(20);
			monitor.StopTask(subtask1);

			monitor.StartTask(subtask2);
			Thread.Sleep(20);
			monitor.StopTask(subtask2);

			Thread.Sleep(10);
			monitor.StopTask(task);

			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(58, 64));
			Assert.That(result[task].SubtasksDurations[subtask1].Duration.TotalMilliseconds, Is.InRange(18, 24));
			Assert.That(result[task].SubtasksDurations[subtask2].Duration.TotalMilliseconds, Is.InRange(18, 24));
		}

		[Test]
		public void result_for_two_tasks_with_two_subtasks_is_correct()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task1 = 1;
			var task2 = 2;
			var subtask1 = 11;
			var subtask2 = 22;
			var subtaskOfBothTasks = 33;

			// Act - task1:
			monitor.StartTask(task1);
			Thread.Sleep(10);

			monitor.StartTask(subtask1);
			Thread.Sleep(20);
			monitor.StopTask(subtask1);

			monitor.StartTask(subtaskOfBothTasks);
			Thread.Sleep(20);
			monitor.StopTask(subtaskOfBothTasks);

			Thread.Sleep(10);
			monitor.StopTask(task1);

			Thread.Sleep(10);

			// Act - task2:
			monitor.StartTask(task2);
			Thread.Sleep(10);

			monitor.StartTask(subtask2);
			Thread.Sleep(20);
			monitor.StopTask(subtask2);

			monitor.StartTask(subtaskOfBothTasks);
			Thread.Sleep(20);
			monitor.StopTask(subtaskOfBothTasks);

			Thread.Sleep(10);
			monitor.StopTask(task2);

			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task1].Duration.TotalMilliseconds, Is.InRange(58, 64));
			Assert.That(result[task1].SubtasksDurations[subtask1].Duration.TotalMilliseconds, Is.InRange(18, 24));
			Assert.That(result[task1].SubtasksDurations[subtaskOfBothTasks].Duration.TotalMilliseconds, Is.InRange(18, 24));

			Assert.That(result[task2].Duration.TotalMilliseconds, Is.InRange(58, 64));
			Assert.That(result[task2].SubtasksDurations[subtask2].Duration.TotalMilliseconds, Is.InRange(18, 24));
			Assert.That(result[task2].SubtasksDurations[subtaskOfBothTasks].Duration.TotalMilliseconds, Is.InRange(18, 24));
		}

		[Test]
		public void result_for_task_with_subsubtask_is_correct()
		{
			// Arrange
			var monitor = new NestingPerformanceMonitor<int>();
			var task = 1;
			var subtask = 2;
			var subsubtask = 3;

			// Act:
			monitor.StartTask(task);
			Thread.Sleep(10);

			monitor.StartTask(subtask);
			Thread.Sleep(10);

			monitor.StartTask(subsubtask);
			Thread.Sleep(20);
			monitor.StopTask(subsubtask);

			Thread.Sleep(10);
			monitor.StopTask(subtask);

			Thread.Sleep(10);
			monitor.StopTask(task);

			Thread.Sleep(10);

			// Assert
			var result = monitor.GetResult();
			Assert.That(result[task].Duration.TotalMilliseconds, Is.InRange(58, 64));
			Assert.That(result[task].SubtasksDurations[subtask].Duration.TotalMilliseconds, Is.InRange(38, 44));
			Assert.That(result[task].SubtasksDurations[subtask].SubtasksDurations[subsubtask].Duration.TotalMilliseconds, Is.InRange(18, 24));
		}
	}
}
