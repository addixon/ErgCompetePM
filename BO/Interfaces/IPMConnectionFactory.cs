namespace BO.Interfaces
{
    public interface IPMConnectionFactory
    {
        IConnection Create(ushort port);
    }
}
