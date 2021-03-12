using BO.Interfaces;
using System;

namespace BO
{
    public class PerformanceMonitor
    {
        public PMProperties Properties { get; }

        public DateTime? LastCommunication { get; set; }

        public PMData Data { get; set; }

        public PerformanceMonitor(PMProperties properties)
        {
            Properties = properties;
            Data = new PMData();
        }

        public PerformanceMonitor()
        {
        }
    }
}