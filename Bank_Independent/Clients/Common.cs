using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Independent
{
    class Common : Client
    {

        #region Constructor;

        public Common(string Name,
                      string LastName,
                      int Deposit,
                      float Percent,
                      DateTime DateOfDeposit)
            : base(Name,
                   LastName,
                   Deposit,
                   Percent,
                   DateOfDeposit)
        { }

        #endregion Constructor
    }
}
