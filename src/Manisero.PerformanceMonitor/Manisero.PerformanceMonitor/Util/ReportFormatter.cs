using System;
using System.Collections.Generic;
using System.Linq;

namespace Manisero.PerformanceMonitor.Util
{
	public static class ReportFormatter
	{
		public static string FormatReport<TTask>(TasksDurations<TTask> tasksDurations, bool includeHeader = true, Func<TTask, string> taskNameFormatter = null, Func<TimeSpan, string> taskDurationFormatter = null)
		{
			if (taskNameFormatter == null)
			{
				taskNameFormatter = x => x.ToString();
			}

			if (taskDurationFormatter == null)
			{
				taskDurationFormatter = x => x.TotalMilliseconds.ToString() + " ms";
			}

			var report = GetSubReport(tasksDurations, 0, taskNameFormatter, taskDurationFormatter).ToList();

			if (includeHeader)
			{
				var header = typeof(TTask).Name + " performance report:";
				report.Insert(0, header);
			}

			return string.Join(Environment.NewLine, report);
		}

		private static IEnumerable<string> GetSubReport<TTask>(TasksDurations<TTask> tasksDurations, int depth, Func<TTask, string> taskNameFormatter, Func<TimeSpan, string> taskDurationFormatter)
		{
			var subreport = new List<string>();
			var indent = string.Join(string.Empty, Enumerable.Repeat("\t", depth));

			foreach (var taskDuration in tasksDurations)
			{
				subreport.Add(string.Format("{0}{1}: {2}",
											indent,
											taskNameFormatter(taskDuration.Key),
											taskDurationFormatter(taskDuration.Value.Duration)));

				if (taskDuration.Value.SubtasksDurations != null)
				{
					subreport.AddRange(GetSubReport(taskDuration.Value.SubtasksDurations,
													depth + 1,
													taskNameFormatter,
													taskDurationFormatter));
				}
			}

			return subreport;
		}
	}
}
