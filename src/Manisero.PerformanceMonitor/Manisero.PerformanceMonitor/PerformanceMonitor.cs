using System;
using Manisero.PerformanceMonitor.Monitors;

namespace Manisero.PerformanceMonitor
{
	public class PerformanceMonitor<TTask>
	{
		private static IPerformanceMonitor<TTask> _current;

		static PerformanceMonitor()
		{
			SetCurrentMonitor(BuiltInMonitorType.Nested);
		}

		public static void SetCurrentMonitor<TMonitor>()
			where TMonitor : IPerformanceMonitor<TTask>
		{
			_current = Activator.CreateInstance<TMonitor>();
		}

		public static void SetCurrentMonitor(BuiltInMonitorType monitorType)
		{
			if (monitorType == BuiltInMonitorType.Flat)
			{
				SetCurrentMonitor<FlatPerformanceMonitor<TTask>>();
			}
			else if (monitorType == BuiltInMonitorType.Nested)
			{
				SetCurrentMonitor<NestingPerformanceMonitor<TTask>>();
			}
			else
			{
				throw new NotSupportedException("Not supported monitor type.");
			}
		}

		public static void StartTask(TTask task)
		{
			_current.StartTask(task);
		}

		public static void StopCurrentTask()
		{
			_current.StopCurrentTask();
		}

		public static void StopTask(TTask task)
		{
			_current.StopTask(task);
		}

		public static TasksDurations<TTask> GetResult()
		{
			return _current.GetResult();
		}
	}
}
