using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListV1.Exceptions
{
    class ListeTachesException : Exception
    {
        public ListeTachesException(string msg) : base(msg) { }
    }
}
