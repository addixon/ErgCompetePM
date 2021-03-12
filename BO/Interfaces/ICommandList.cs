using System.Collections.Generic;

namespace BO.Interfaces
{
    public interface ICommandList : IList<ICommand>
    {
        uint[] Buffer { get; }
        int Size { get; }
        bool CanSend { get; }

        ushort ExpectedResponseSize { get; }

        void Reset();
        new void Add(ICommand command);

        void AddRange(IEnumerable<ICommand> commands);

        void Prepare();

        bool Read(IResponseReader reader);
    }
}
