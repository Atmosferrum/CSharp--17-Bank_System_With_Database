using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Independent
{
    class Silver<T> : Department<Client>
        where T : Aristocrat
    {
        public Silver(string Name)
               : base(Name)
        { }
    }
}
