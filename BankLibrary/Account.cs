using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    //abstract class that specifies account
    public abstract class Account : IAccount
    {
        //Event that occurs when withdrawing money
        protected internal event AccountStateHandler Withdrawed;
        //Event that occurs when adding money to an account
        protected internal event AccountStateHandler Added;
        //Event that occurs when an account is opened
        protected internal event AccountStateHandler Opened;
        //Event that occurs when closing an account
        protected internal event AccountStateHandler Closed;
        //The event that occurs when interest is calculated
        protected internal event AccountStateHandler Calculated;

        protected int _id;
        static int counter = 0;
        protected decimal _val;
        protected int _percentage;
        protected int _days;

        public Account(decimal val, int percentage)
        {
            _val = val;
            _percentage = percentage;
            _id = ++counter;
        }
        public decimal CurrentVal
        {
            get { return _val; }
        }
        public int Percentage
        {
            get { return _percentage; }
        }
        public int Id
        {
            get { return _id; }
        }
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (handler != null && e != null)
                handler(this, e);
        }

        //Individual events call. Each event has own virtual method 
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put(decimal val)
        {
            _val += val;
            OnAdded(new AccountEventArgs("На счет поступило " + val,val));
        }
        public virtual decimal Withdraw(decimal val)
        {
            decimal result = 0;
            if (val <= _val)
            {
                _val -= val;
                result = val;
                OnWithdrawed(new AccountEventArgs("Сумма " + val + " снята со счета " + _id, val));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs("Недостаточно денег на счету " + _id, 0));
            }
            return result;
        }
        //Account opening
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs("Открыт новый депозитный счет!Id счета: " + this._id, this._val));
        }
        //Account closing
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Счет " + _id + " закрыт.  Итоговая сумма: " + CurrentVal, CurrentVal));
        }

        protected internal void IncrementDays()
        {
            _days++;
        }
        //Accrual of interest
        protected internal virtual void Calculate()
        {
            decimal increment = _val * _percentage / 100;
            _val = _val + increment;
            OnCalculated(new AccountEventArgs("Начислены проценты в размере: " + increment, increment));
        }
    }
}
