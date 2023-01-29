using System.Collections.Generic;

namespace GeekTrust
{
    public class Bank
    {
        public string Name {get; set;}
        public List<Customer> Customers = new List<Customer>();
        public Bank(string bankName)
        {
            this.Name = bankName;
        }
        public void ProvideLoan(string customerName, int principal, int years, int rate){
            Customer customer = GetCustomer(customerName);
            customer.AddLoan(principal, years, rate);
        }
        public void Payement(string customerName, int principal, int years, int rate){
            Customer customer = GetCustomer(customerName);
            customer.AddLoan(principal, years, rate);
        }
        
        public Customer GetCustomer(string customerName)
        {
            Customer customer = Customers.Find(b=>b.Name==customerName);
            if(customer == null)
            {
                customer = new Customer(customerName);
                Customers.Add(customer);
            }
            return customer;
        }
    }
}