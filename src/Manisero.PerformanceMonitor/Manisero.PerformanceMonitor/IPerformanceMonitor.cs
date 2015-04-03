namespace Manisero.PerformanceMonitor
{
	public interface IPerformanceMonitor<TTask>
	{
		void StartTask(TTask task);

		void StopCurrentTask();

		void StopTask(TTask task);

		TasksDurations<TTask> GetResult();
	}
}
