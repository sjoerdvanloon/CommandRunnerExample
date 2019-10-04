using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandRunner.Commands
{
    class ReadJSONFileCommand : CommandBase, ICommand, INeedsInput
    {
        string ICommand.FriendlyName => "Read state from JSON file";
        public string FilePath { get; set; }

        public ReadJSONFileCommand(CommandContext context) : base(context) { }

        public bool Go()
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(FilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                Context.Cases =  (List<Case>)serializer.Deserialize(file, typeof(List<Case>));
            }
            return true;
        }

        public bool Input()
        {
            FilePath = Context.InputFilePath;

            if (string.IsNullOrWhiteSpace(FilePath))
            {
                Console.Write("Input file path: ");
                FilePath = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(FilePath))
                {
                    Console.WriteLine("No input");
                    return false;
                }
            }

            if (!System.IO.File.Exists(FilePath))
            {
                Console.WriteLine("File does not exist");
                return false;
            }

            return true;
        }
    }
}
