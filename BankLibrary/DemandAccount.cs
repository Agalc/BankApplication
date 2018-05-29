using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class DemandAccount:Account
    {
        public DemandAccount(decimal val,int percentage) : base(val, percentage) { }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs("Открыт новый счет до востребования!Id счета: " + this._id, this._val));
        }
    }
}
