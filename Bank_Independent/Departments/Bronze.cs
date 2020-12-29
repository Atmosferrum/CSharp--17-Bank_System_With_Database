using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Independent
{
    class Bronze<T> : Department<Client> 
        where T : Common
    {
        public Bronze(string Name)
               : base(Name)
        { } 
    }
}
