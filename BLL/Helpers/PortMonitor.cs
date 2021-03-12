using System.Collections.Concurrent;

namespace BLL.Helpers
{
	internal class PortMonitor
	{
		private readonly ConcurrentDictionary<ushort, object> _dictionary = new ConcurrentDictionary<ushort, object>();

		public object this[ushort port] => _dictionary.GetOrAdd(port, _ => new object());
	}
}