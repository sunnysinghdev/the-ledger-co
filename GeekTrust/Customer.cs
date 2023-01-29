using System;
using System.Collections.Generic;

namespace GeekTrust
{
    public class Customer
    {
        public string Name { get; set; }
        public int Amount { get; set; }
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
            Amount = (int)Math.Ceiling((double)(principal + (principal * years * rate / 100)));
            EMIAmount = (int)Math.Ceiling((double)(Amount / (double)(years * Constants.TOTAL_MONTH)));
        }
        public void AddPayment(int lumpSumAmount, int emiCount)
        {
            if (!paymentHistory.ContainsKey(emiCount))
            {
                paymentHistory.Add(emiCount, lumpSumAmount);
            }
        }
        public int GetBalance(int emiCount)
        {
            int balance = 0;
            balance = Amount - (EMIAmount * emiCount);
            return balance > 0 ? balance : 0;
        }
        public int GetAmountPaid(int emiCount)
        {
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
            return (years * Constants.TOTAL_MONTH) - emiCount - GetLumpSumAmountPaid(emiCount) / EMIAmount;
        }
        private int GetTotalEMICount(int emiCount)
        {
            return (Amount - GetLumpSumAmountPaid(emiCount)) / EMIAmount;
        }

    }
}