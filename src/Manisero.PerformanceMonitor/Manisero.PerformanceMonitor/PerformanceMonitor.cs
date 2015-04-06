using System;
using Manisero.PerformanceMonitor.Monitors;

namespace Manisero.PerformanceMonitor
{
	public class PerformanceMonitor<TTask>
	{
		public static IPerformanceMonitor<TTask> Current { get; set; }

		static PerformanceMonitor()
		{
			SetCurrentMonitor(BuiltInMonitorType.Nested);
		}

		public static void SetCurrentMonitor<TMonitor>()
			where TMonitor : IPerformanceMonitor<TTask>
		{
			Current = Activator.CreateInstance<TMonitor>();
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
	}
}
