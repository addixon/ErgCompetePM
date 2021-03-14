namespace PM.BO.Interfaces
{
    public interface IPMConnectionFactory
    {
        IConnection Create(ushort port);
    }
}
