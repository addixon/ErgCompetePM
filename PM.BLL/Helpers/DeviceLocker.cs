using PM.BO;
using System.Collections.Concurrent;

namespace BLL.Helpers
{
    /// <summary>
    /// A locking dictionary specifically for devices
    /// </summary>
    internal class DeviceLocker
	{
		/// <summary>
		/// The dictionary holding the lock objects
		/// </summary>
		private readonly ConcurrentDictionary<Location, object> _dictionary = new();

		/// <summary>
		/// Returns or sets the lock object
		/// </summary>
		/// <param name="location">The location of the device</param>
		/// <returns>The lock object</returns>
		public object this[Location location] => _dictionary.GetOrAdd(location, _ => new object());
	}
}