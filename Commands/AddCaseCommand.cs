using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandRunner.Commands
{
    class AddCaseCommand : CommandBase, ICommand, INeedsInput
    {
        string ICommand.FriendlyName => "Add a case to the list";
        public int Number { get; set; } 

        public AddCaseCommand(CommandContext context) : base(context) { }

        public bool Go()
        {
            Context.Cases.Add(new Case() { Number = Number });

            return true;
        }

        public bool Input()
        {
            Console.Write("Number: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input");
                return false;
            }

            var number = 0;
            if (!int.TryParse(input, out number))
            {
                Console.WriteLine("Not a valid number");
                return false;
            }

            Number = number;

            return true;
        }
    }
}
