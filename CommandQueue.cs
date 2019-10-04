using System;
using System.Collections.Generic;
using System.Text;

namespace CommandRunner
{
    public class CommandQueue
    {
        private List<ICommand> _commands { get; } = new List<ICommand>();

        public void Push(ICommand command)
        {
            _commands.Add(command);
        }

        public ICommand[] GetQueue()
        {
            return _commands.ToArray();
        }

    }

}
