using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AndroidToolkit.Infrastructure.Utilities
{
    public interface ICommandExecutor 
    {

        Task Execute(Command cmd, TextBlock context, bool createNoWindow = true);

        Task<string> Execute(Command cmd, bool createNoWindow = true);

        Task Execute(IEnumerable<Command> cmds, TextBlock context, bool createNoWindow = true);

        Task<string> Execute(IEnumerable<Command> cmds, bool createNoWindow = true);

    }
}
