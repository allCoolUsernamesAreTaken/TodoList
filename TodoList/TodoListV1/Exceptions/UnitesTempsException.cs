using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListV1.Exceptions
{
    internal class UnitesTempsException : Exception
    {
        public UnitesTempsException(string msg) : base(msg) { }
    }
}
