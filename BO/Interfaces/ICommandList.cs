using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface ICommandList : IList<ICommand>
    {
        bool CanSend { get; }

        ushort ExpectedResponseSize { get; }

        uint[] Buffer { get; }

        void Reset();

        new void Add(ICommand command);

        void AddRange(IEnumerable<ICommand> commands);

        void Prepare();

        bool Read(IResponseReader reader);
    }
}
