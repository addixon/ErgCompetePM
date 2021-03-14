using PM.BO;
using System.Threading.Tasks;

namespace PM.BO.Interfaces
{
    public interface IErgClient
    {
        Task UpdateDisplay(PerformanceMonitor performanceMonitor);
    }
}
