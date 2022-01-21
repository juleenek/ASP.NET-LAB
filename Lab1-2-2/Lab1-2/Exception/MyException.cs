using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Exception
{
    public class MyException : Exception
    {
        public MyException(string? message) : base(message)
        {
        }
    }
}
