using System;

namespace PM.BO
{
    public struct Location : IEquatable<Location>
    {
        public int BusNumber { get; }
        public int Address { get; }

        public Location(int busNumber, int address)
        {
            BusNumber = busNumber;
            Address = address;
        }

        public bool Equals(Location otherLocation)
        {
            return BusNumber == otherLocation.BusNumber && Address == otherLocation.Address;
        }

        public override bool Equals(object? obj)
        {
            return obj != null && obj is Location location && Equals(location);
        }

        public override int GetHashCode()
        {
            var calculation = BusNumber ^ Address;
            return calculation.GetHashCode();
        }

        public static bool operator == (Location l1, Location l2)
        {
            return l1.Equals(l2);
        }

        public static bool operator != (Location l1, Location l2)
        {
            return !l1.Equals(l2);
        }

    }
}
