using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeekTrust.Tests
{
    public class ProgramTests
    {
        [Test]
        public void TestProcessing()
        {
            //LOAN BANK_NAME BORROWER_NAME PRINCIPAL NO_OF_YEARS RATE_OF_INTEREST
        }
        [Test]
        [TestCase("LOAN IDIDI Dale 10000 5 4")]
        [TestCase("PAYMENT MBI Dale 1000 5")]
        [TestCase("BALANCE MBI Harry 12")]
        public void ReadCommands(string command)
        {
            Command cmd = CommandFactory.GetCommandType(command);
            switch(cmd)
            {
                case LoanCommand c:
                    Assert.AreEqual(cmd.Name, "LOAN");
                break;
                case PaymentCommand c:
                    Assert.AreEqual(cmd.Name, "PAYMENT");
                break;
                case BalanceCommand c:
                    Assert.AreEqual(cmd.Name, "BALANCE");
                break;

            }
        }

        [Test]
        [TestCase("LOAN IDIDI Dale 10000 5 4")]
        public void TestLoanSanctioned(string command)
        {
            Command cmd = CommandFactory.GetCommandType(command);
            Ledger ledger  = new Ledger();
            string output = ledger.Process(cmd);
            Assert.AreEqual("IDIDI", ledger.Banks[0].Name);
            Assert.AreEqual("Dale", ledger.Banks[0].Customers[0].Name);
            Assert.AreEqual(10000+(10000*5*4/100), ledger.Banks[0].Customers[0].Amount);
            Assert.AreEqual("", output);
        }
        [Test]
        public void TestBalance()
        {
            List<Command> cmdList = new List<Command>();
            cmdList.Add(CommandFactory.GetCommandType("LOAN IDIDI Dale 10000 5 4"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE IDIDI Dale 5"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE IDIDI Dale 40"));
            Ledger ledger  = new Ledger();
            ledger.Process(cmdList[0]);
            
            ledger.Process(cmdList[1]);
            Assert.AreEqual("IDIDI Dale 1000 55", ledger.Process(cmdList[1]));

            ledger.Process(cmdList[2]);
            Assert.AreEqual("IDIDI Dale 8000 20", ledger.Process(cmdList[2]));
            
        }
        [Test]
        public void TestPayment()
        {
            List<Command> cmdList = new List<Command>();
            cmdList.Add(CommandFactory.GetCommandType("LOAN IDIDI Dale 5000 1 6"));
            cmdList.Add(CommandFactory.GetCommandType("LOAN MBI Harry 10000 3 7"));
            cmdList.Add(CommandFactory.GetCommandType("LOAN UON Shelly 15000 2 9"));
            cmdList.Add(CommandFactory.GetCommandType("PAYMENT IDIDI Dale 1000 5"));
            cmdList.Add(CommandFactory.GetCommandType("PAYMENT MBI Harry 5000 10"));
            cmdList.Add(CommandFactory.GetCommandType("PAYMENT UON Shelly 7000 12"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE IDIDI Dale 3"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE IDIDI Dale 6"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE UON Shelly 12"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE MBI Harry 12"));
            Ledger ledger  = new Ledger();
            ledger.Process(cmdList[0]);
           // Assert.AreEqual(442, ledger.Banks[0].Customers[0].GetEMI());
            ledger.Process(cmdList[1]);
            ledger.Process(cmdList[2]);
            ledger.Process(cmdList[3]);
            ledger.Process(cmdList[4]);
            ledger.Process(cmdList[5]);
            
            Assert.AreEqual("IDIDI Dale 1326 9", ledger.Process(cmdList[6]));
            Assert.AreEqual("IDIDI Dale 3652 4", ledger.Process(cmdList[7]));
            Assert.AreEqual("UON Shelly 15856 3", ledger.Process(cmdList[8]));
            Assert.AreEqual("MBI Harry 9044 10", ledger.Process(cmdList[9]));
            
        }
        [Test]
        public void TestUONPayment()
        {
            List<Command> cmdList = new List<Command>();
            cmdList.Add(CommandFactory.GetCommandType("LOAN UON Shelly 15000 2 9"));
            cmdList.Add(CommandFactory.GetCommandType("PAYMENT UON Shelly 7000 12"));
            cmdList.Add(CommandFactory.GetCommandType("BALANCE UON Shelly 12"));
            Ledger ledger  = new Ledger();
            ledger.Process(cmdList[0]);
            ledger.Process(cmdList[1]);
            Assert.AreEqual("UON Shelly 15856 3", ledger.Process(cmdList[2]));
            
        }
    }
}
