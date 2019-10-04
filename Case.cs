using System;
using System.Collections.Generic;
using System.Text;

namespace CommandRunner
{
   public class Case
    {
        public int Number { get; set; }

        public void Preview()
        {
            Console.WriteLine($"Number: {Number}");
        }
    }
}
