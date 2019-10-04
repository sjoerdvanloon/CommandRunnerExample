using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandRunner.Commands
{
    class PreviewCaseCommand : CommandBase, ICommand, INeedsInput
    {
        string ICommand.FriendlyName => "Preview a case";
        int _number = 0;

        public PreviewCaseCommand(CommandContext context) : base(context) { }

        public bool Go()
        {
            var cases = Context.Cases.Where(c => c.Number == _number).ToArray();

         if (cases.Count() == 0)
            {
                Console.WriteLine("Could not find this cases");
                return true;
            }

         if (cases.Count() > 1)
            {
                Console.WriteLine($"Found multiple cases with this number");
            }

            foreach (var c in cases)
            {
                c.Preview();
            }

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

            if (!int.TryParse(input, out _number))
            {
                Console.WriteLine("Not a valid number");
                return false;
            }

            return true;
        }
    }
}
