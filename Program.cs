using ConsoleTools;
using System;
using CommandRunner.Commands;
using System.Linq;

namespace CommandRunner
{
    class Program
    {

        private static void HandleCommand(ICommand command)
        {
            Console.WriteLine(command.FriendlyName);
            var needsInput = command as INeedsInput;
            if (needsInput == null || needsInput.Input())
            {
                var result = command.Go();
                if (!result)
                {
                    Console.WriteLine("Error while running command!");
                }
            }
            else
            {
                Console.WriteLine("Input was wrong!");
            }
            Console.WriteLine("Press the ANY key to return to the menu...");
            Console.ReadKey();
        }

        private static void HandleQueue(ICommand[] commands)
        {
            foreach (var command in commands)
            {
                Console.WriteLine(command.FriendlyName);
                var needsInput = command as INeedsInput;
                var result = command.Go();
                if (!result)
                {
                    Console.WriteLine("Error while running command!");
                    break;
                }
            }
            Console.WriteLine("Press the ANY key to return to the menu...");
            Console.ReadKey();

        }
        static void Main(string[] args)
        {
            try
            {
                var context = new CommandContext() { OutputFilePath = @"c:\TEMP\cases.json" };

                var interfaceType = typeof(ICommand);
                var all = AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(x => x.GetTypes())
                  .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                  .Select(x => Activator.CreateInstance(x, context));

                var menu = new ConsoleMenu(args, level: 0);
                menu.Configure(config => config.ClearConsole = true);

                foreach (var x in all)
                {
                    var command = (ICommand)x;
                    menu.Add(command.FriendlyName, () => HandleCommand(command));
                }

                menu.Add("Run processed AUMC queue", () => HandleQueue(CommandQueueFactory.GetProcessedAUMCQueue()));

                menu.Show();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Press the ANY key to continue...");
            Console.ReadKey();
            //var writeToFileCommand = new WriteToFileCommand(context);
            //var command = (ICommand)writeToFileCommand;

            //var needsInput = writeToFileCommand as INeedsInput;
            //if (needsInput == null || needsInput.Input())
            //{
            //    Console.WriteLine(command.FriendlyName);
            //    command.Go();
            //}


            //   var subMenu = new ConsoleMenu(args, level: 1)
            //.Add("Sub_One", () => SomeAction("Sub_One"))
            //.Add("Sub_Two", () => SomeAction("Sub_Two"))
            //.Add("Sub_Three", () => SomeAction("Sub_Three"))
            //.Add("Sub_Four", () => SomeAction("Sub_Four"))
            //.Add("Sub_Close", ConsoleMenu.Close)
            //.Configure(config =>
            //{
            //    config.Selector = "--> ";
            //    config.EnableFilter = true;
            //    config.Title = "Submenu";
            //    config.EnableBreadcrumb = true;
            //    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            //});

            //   var menu = new ConsoleMenu(args, level: 0)
            //     .Add("One", () => SomeAction("One"))
            //     .Add("Two", () => SomeAction("Two"))
            //     .Add("Three", () => SomeAction("Three"))
            //     .Add("Sub", subMenu.Show)
            //     .Add("Change me", (thisMenu) => thisMenu.CurrentItem.Name = "I am changed!")
            //     .Add("Close", ConsoleMenu.Close)
            //     .Add("Action then Close", (thisMenu) => { SomeAction("Close"); thisMenu.CloseMenu(); })
            //     .Add("Exit", () => Environment.Exit(0))
            //     .Configure(config =>
            //     {
            //         config.Selector = "--> ";
            //         config.EnableFilter = true;
            //         config.Title = "Main menu";
            //         config.EnableWriteTitle = false;
            //         config.EnableBreadcrumb = true;
            //     });

            //   menu.Show();
        }
    }
}
