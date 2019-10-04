using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandRunner.Commands
{
    class RandomPreviewCommand : CommandBase, ICommand
    {
        string ICommand.FriendlyName => "Preview a random selection of cases";

        public RandomPreviewCommand(CommandContext context) : base(context) { }

        public bool Go()
        {
            Console.WriteLine($"There are {Context.Cases.Count()} cases");
            foreach (var c in Context.Cases)
            {
                c.Preview();
            }

            return true;
        }
    }
}
