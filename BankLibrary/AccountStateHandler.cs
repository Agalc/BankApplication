using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        public string Message { get; private set; }//message
        public decimal Value { get; private set; }//value of transaction
        public AccountEventArgs(string _mes,decimal _val)
        {
            Message = _mes;
            Value = _val;
        }
    }
}
