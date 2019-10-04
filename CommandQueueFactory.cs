using CommandRunner.Commands;

namespace CommandRunner
{
    public class CommandQueueFactory
        {
        public static ICommand[] GetProcessedAUMCQueue()
        {
            var context = new CommandContext() ;
            var q = new CommandQueue();
            q.Push(new ReadJSONFileCommand(context) { FilePath = @"C:\temp\cases.json" });
            q.Push(new AddCaseCommand(context) { Number = 666 });
            q.Push(new AddCaseCommand(context) { Number = 667 });
            q.Push(new AddCaseCommand(context) { Number = 668 });
            q.Push(new AddCaseCommand(context) { Number = 669 });
            q.Push(new RandomPreviewCommand(context));
            q.Push(new WriteToFileCommand(context) {FilePath = @"C:\temp\cases2.json" });

            return q.GetQueue();
        }
    }

}
