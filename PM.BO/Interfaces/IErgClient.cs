using System.Threading.Tasks;

namespace PM.BO.Interfaces
{
    public interface IErgClient
    {
        Task BroadcastWorkoutStatistics(IErg performanceMonitor);

        Task BroadcastHeartbeat(string serialNumber);

        Task BroadcastStatus(string serialNumber);
    }
}
