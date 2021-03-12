using System.Threading.Tasks;

namespace BO.Interfaces
{
    public interface IErgClient
    {
        Task UpdateDisplay(PerformanceMonitor performanceMonitor);
    }
}
