using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    class MyIncorrectDataException : Exception
    {
        public MyIncorrectDataException(string Msg) : base(Msg) {}
    }
}
