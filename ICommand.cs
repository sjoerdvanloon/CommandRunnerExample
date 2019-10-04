using System;
using System.Collections.Generic;
using System.Text;

namespace CommandRunner
{
    public  interface ICommand
    {

        string FriendlyName { get; }
        bool Go();
    }
}
