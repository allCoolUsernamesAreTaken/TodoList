using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListV1.Exceptions
{
    internal class ListePrincipaleException : Exception
    {
        public ListePrincipaleException(string msg) : base(msg) { }
    }
}
