using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public interface IAccount
    {
        void Put(decimal val); //put money on a account
        decimal Withdraw(decimal val);//withdraw money from account
    }
}
