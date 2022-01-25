using Crm.Core.Models;
using Crm.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=Crm;MultipleActiveResultSets=True;User ID=AppUser;Password=Passw0rd;";
            ICrmRepository repository = new CrmRepository(connectionString);


            var c = new Customer
            {
                Address = "adress",
                Country = "GR",
                CustomerName = "costas",
                CustomerSurname = "sloukas",
                DateofBirth = new DateTime(1985, 1, 1),
                PostCode = "pob xxx",
                Enabled = true
            };

            var cc = new CustomerCall();
            cc.Customer = c;
            cc.DateOfCall = new DateTime(1980, 12, 12);
            cc.Description = "desc2222";
            cc.Enabled = true;
            cc.Subject = "subjo2222";
            cc.TimeOfCall = new TimeSpan(9, 5, 0);
            cc.Enabled = true;
            ///////////////////////////////////////////////////////////



            var p2 = repository.InsertCustomer(c);
            var p = repository.InsertCustomerCall(cc);


            cc.Subject = "updated subject";


            c.CustomerSurname = "updated spanulis";

            repository.UpdateCustomer(c);
            repository.UpdateCustomerCall(cc);


            var customer = repository.GetCustomer(1);
            var customerCall = repository.GetCustomerCall(new Guid("7C61004F-13DD-444D-976D-58B0F68F526F"));
            var user = repository.GetUser("ali.fidanli");
            var permission = repository.GetPermission(1);
            //-------------------------------------------------//
            var customers = repository.GetCustomers();
            var customerCalls = repository.GetCustomerCalls();
            var users = repository.GetUsers();
            var permissions = repository.GetPermissions();





            Console.ReadLine();
        }
    }
}
