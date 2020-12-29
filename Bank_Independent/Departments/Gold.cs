using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Independent
{
    class Gold<T> : Department<Client>
        where T : Royal
    {
        public Gold(string Name)
               : base(Name)
        { }
    }
}
