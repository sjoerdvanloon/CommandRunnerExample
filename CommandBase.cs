namespace CommandRunner
{
    public abstract class CommandBase
    {
        protected CommandContext Context { get; set; }

        public CommandBase(CommandContext context)
        {
            Context = context;
        }
    }


}
