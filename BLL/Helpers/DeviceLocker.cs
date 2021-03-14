using System.Collections.Concurrent;

namespace BLL.Helpers
{
	internal class DeviceLocker
	{
		private readonly ConcurrentDictionary<(int, int), object> _dictionary = new ConcurrentDictionary<(int, int), object>();

		public object this[(int BusNumber, int address) hubAddress] => _dictionary.GetOrAdd(hubAddress, _ => new object());
	}
}