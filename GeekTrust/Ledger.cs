using System.Collections.Generic;

namespace GeekTrust
{
    public class Ledger
    {
        public List<Bank> Banks = new List<Bank>();
        public Ledger()
        {
           
        }
        public string Process(Command cmd){
            string output = "";
            switch(cmd){
                case LoanCommand l:
                    SanctionLoan(l.BankName, l.BorrowerName, l.Principal, l.Years, l.Rate);
                    break;
                case BalanceCommand b:
                    output = GetBalance(b.BankName, b.BorrowerName, b.EMICount);
                    break;
                    case PaymentCommand p:
                    MakePayment(p.BankName, p.BorrowerName, p.Amount, p.EMICount);
                    break;
            }
            return output;
        }
        private void MakePayment(string bankName, string customerName, int lumpSumAmount,  int emiCount)
        {
            Bank bank = GetBank(bankName);
            Customer customer = bank.GetCustomer(customerName);
            customer.AddPayment(lumpSumAmount, emiCount);
        }
        private string GetBalance(string bankName, string customerName, int emiCount)
        {
            Bank bank = GetBank(bankName);
            Customer customer = bank.GetCustomer(customerName);
            int amount = customer.GetAmountPaid(emiCount);
            int emi = customer.GetRemainingEMI(emiCount);
            return string.Format("{0} {1} {2} {3}", bankName, customerName, amount, emi);
        }
        private  void SanctionLoan(string bankName, string customerName, int principal, int years, int rate)
        {
            Bank bank = GetBank(bankName);
            bank.ProvideLoan(customerName, principal, years, rate);
        }
        private Bank GetBank(string bankName)
        {
            Bank bank = Banks.Find(b=>b.Name==bankName);
            if(bank == null)
            {
                bank = new Bank(bankName);
                Banks.Add(bank);
            }
            return bank;
        }
    }
}