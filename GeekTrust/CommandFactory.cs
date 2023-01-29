using System;

namespace GeekTrust
{
    public class CommandFactory
    {
        public static Command GetCommandType(string command)
        {
            //string command = "LOAN IDIDI Dale 10000 5 4";//loav
            //string command="PAYMENT MBI Dale 1000 5";//payement
            //string command="MBI Harry 1250 43";//balance
            string[] param = command.Split(' ');
            Command cmd = null;
            switch (param[0])
            {
                case Constants.LOAN:
                    cmd = new LoanCommand(param[1], param[2], int.Parse(param[3]), int.Parse(param[4]), int.Parse(param[5]));
                    break;
                case Constants.PAYMENT:
                    cmd = new PaymentCommand(param[1], param[2], int.Parse(param[3]), int.Parse(param[4]));
                    break;
                case Constants.BALANCE:
                    cmd = new BalanceCommand(param[1], param[2], int.Parse(param[3]));
                    break;

            }
            return cmd;

        }
    }
}