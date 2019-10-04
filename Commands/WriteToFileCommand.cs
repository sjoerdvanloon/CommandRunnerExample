using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandRunner.Commands
{
    internal class WriteToFileCommand : CommandBase,ICommand, INeedsInput
    {
        string ICommand.FriendlyName => "Write current state to file";
        public string FilePath { get;set; }

        public WriteToFileCommand(CommandContext context) : base(context) { }

        public bool Go()
        {
            using (StreamWriter file = File.CreateText(FilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, Context.Cases);
            }

            return true;
        }

        public bool Input()
        {
            FilePath = Context.OutputFilePath;

            if (string.IsNullOrWhiteSpace(FilePath))
            {
                Console.Write("Output file path: ");
                FilePath = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(FilePath))
                {
                    Console.Write("No input");
                    return false;
                }
            }

            return true;
        }
    }
}
