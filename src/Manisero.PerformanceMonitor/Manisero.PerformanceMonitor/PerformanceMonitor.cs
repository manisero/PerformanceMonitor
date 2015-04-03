using System;
using System.Collections.Generic;

namespace Manisero.PerformanceMonitor
{
    public class PerformanceMonitor<TTask>
    {
		public void EnterTaskScope(TTask task)
		{
			
		}

		public void LeaveTaskScope(TTask task)
		{
			
		}

		public IDictionary<TTask, TimeSpan> GetResults()
		{
			return null;
		}
    }
}
