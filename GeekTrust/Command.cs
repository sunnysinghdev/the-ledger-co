namespace GeekTrust
{
    public class Command
    {
        //LOAN BANK_NAME BORROWER_NAME PRINCIPAL NO_OF_YEARS RATE_OF_INTEREST
        public string Name { get; set; }
        public string BankName { get; set; }
        public string BorrowerName { get; set; }
       
    }
    public class LoanCommand : Command
    {
        //string command = "LOAN IDIDI Dale 10000 5 4";//loav
         public int Principal { get; set; }
        public int Years { get; set; }
        public int Rate { get; set; }
        public LoanCommand(string bankName, string borrowerName, int principal, int years, int rate)
        {
            this.Name = Constants.LOAN;
            this.BankName = bankName;
            this.BorrowerName = borrowerName;
            this.Principal = principal;
            this.Years = years;
            this.Rate = rate;
        }
    }
    public class PaymentCommand : Command
    {
        //string command="PAYMENT MBI Dale 1000 5";//payement
        public int Amount = 0;
        public int EMICount = 0;
        public PaymentCommand(string bankName, string borrowerName, int amount, int emiCount)
        {
            this.Name=Constants.PAYMENT;
            this.BankName=bankName;
            this.BorrowerName=borrowerName;
            Amount = amount;
            EMICount = emiCount;//after
            
        }

    }
    public class BalanceCommand : Command
    {
        //string command="MBI Harry 1250 43";//output format
        public int EMICount = 0;
        public BalanceCommand(string bankName, string borrowerName, int emiCount)
        {
            this.Name = Constants.BALANCE;
            this.BankName = bankName;
            this.BorrowerName=borrowerName;
            EMICount = emiCount;//including
        }
    }

}