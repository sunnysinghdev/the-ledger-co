using System;
using System.Collections.Generic;

namespace GeekTrust
{
    public class Customer
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        int TotalAmount { get; set; }
        int EMIAmount { get; set; }
        int principal = 0;
        int years = 0;
        int rate = 0;
        Dictionary<int, int> paymentHistory = new Dictionary<int, int>();
        public Customer(string borrowerName)
        {
            this.Name = borrowerName;
        }
        public void AddLoan(int principal, int years, int rate)
        {
            this.principal = principal;
            this.years = years;
            this.rate = rate;
            Amount =  principal + (int)Math.Ceiling((double)(principal * years * rate / 100));
            EMIAmount = (int)Math.Ceiling((double)(Amount / (double)(years * Constants.TOTAL_MONTH)));
            //TotalAmount = EMIAmount * this.years * Constants.TOTAL_MONTH;
        }
        public void AddPayment(int lumpSumAmount, int emiCount)
        {
            if (!paymentHistory.ContainsKey(emiCount))
            {
                paymentHistory.Add(emiCount, lumpSumAmount);
            }
        }
        private int GetBalance(int emiCount)
        {
            int balance = 0;
            balance = Amount - (EMIAmount * emiCount);
            return balance > 0 ? balance : 0;
        }
        public int GetAmountPaid(int emiCount)
        {
            int remainingEmiCount = GetRemainingEMI(emiCount);
            if (remainingEmiCount <= 0)
            {
                return Amount;
            }
            return EMIAmount * emiCount + GetLumpSumAmountPaid(emiCount);
        }
        private int GetLumpSumAmountPaid(int emiCount)
        {
            int paidAmount = 0;
            foreach (var kv in paymentHistory)
            {
                if (kv.Key <= emiCount)
                {
                    paidAmount += kv.Value;
                }
            }
            return paidAmount;
        }
        public int GetEMI()
        {
            return EMIAmount;
        }
        public int GetRemainingEMI(int emiCount)
        {
            //int total = (years * Constants.TOTAL_MONTH) - emiCount - GetLumpSumAmountPaid(emiCount) / EMIAmount);
            int total = (int)Math.Ceiling((double)(Amount - GetLumpSumAmountPaid(emiCount) - (EMIAmount * emiCount)) / (double)EMIAmount);
            return total > -1 ? total : 0;
        }
        private int GetTotalEMICount(int emiCount)
        {
            return (Amount - GetLumpSumAmountPaid(emiCount)) / EMIAmount;
        }

    }
}