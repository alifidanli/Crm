using AutoMapper;
using Crm.Core.Dto;
using Crm.Core.Models;
using Crm.Core.Services;
using Crm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Crm.Models;

namespace Crm.Controllers
{
    public class HomeController : BaseController
    {
                private readonly ICrmRepository _repository ;
        public HomeController()
        {
            _repository = new CrmRepository(this.ConnectionString);
        }

        public ActionResult Index(string language)
        {
         var   viewModel = new LoginViewModel { language = language };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UserDto user, string language)
        {
            var viewModel = new LoginViewModel();
            var userInDb = _repository.GetUser(user.UserName);

            if (userInDb != null)
            {
                if (userInDb.Password == user.Password)
                {
                    viewModel = new LoginViewModel { language = language, user = Mapper.Map<UserDto>(userInDb) };
                    Session["permission"] = userInDb.Permission.Id;
                    Session["username"] = userInDb.UserName;
                }
            }



            return View(viewModel);
        }
        public ActionResult LogOut(string language)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll(); //Removes all session variables
            return RedirectToAction("Index", "Home", new { language = language });
        }

        public ActionResult ManageCustomers(string language)
        {
            var viewModel = new LoginViewModel { language = language };
            return View(viewModel);
        }

        public ActionResult ManageCustomerCalls(string language)
        {
            var viewModel = new LoginViewModel { language = language };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ManageCustomers(string language, string operationType)
        {
            if (operationType == null)
                operationType = Request["operationType"];


            if (operationType == "edit")
            {
                var customer = new Customer
                {
                    CustomerNo = Convert.ToInt32(Request["customerNo"]),
                    Address = Request["address"],
                    Country = Request["country"],
                    CustomerName = Request["customerName"],
                    CustomerSurname = Request["customerSurname"],
                    DateofBirth = Convert.ToDateTime(Request["dateofBirth"]),
                    Enabled = true,
                    PostCode = Request["postCode"]
                };

                var isok = _repository.UpdateCustomer(customer);
            }
            else if (operationType == "create")
            {
                var customer = new Customer
                {
                    Address = Request["iaddress"],
                    Country = Request["icountry"],
                    CustomerName = Request["icustomerName"],
                    CustomerSurname = Request["icustomerSurname"],
                    DateofBirth = Convert.ToDateTime(Request["idateofBirth"]),
                    Enabled = true,
                    PostCode = Request["ipostCode"]
                };

                _repository.InsertCustomer(customer);


            }


            var viewModel = new LoginViewModel { language = language };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ManageCustomerCalls(string language, string operationType)
        {
            if (operationType == null)
                operationType = Request["operationType"];


            if (operationType == "edit")
            {
                var customerCall = new CustomerCall
                {
                    Id = new Guid (Request["id"]),
                    Customer = _repository.GetCustomer(Convert.ToInt32(Request["customerNo"])),
                    DateOfCall = Convert.ToDateTime(Request["dateOfCall"]),
                    Enabled = true,
                    Subject = Request["subject"],
                    Description = Request["description"],
                    TimeOfCall = TimeSpan.Parse(Request["timeOfCall"])
                };

                var isok = _repository.UpdateCustomerCall(customerCall);
            }
            else if (operationType == "create")
            {
                var customerCall = new CustomerCall
                {
                    Customer = _repository.GetCustomer(Convert.ToInt32(Request["icustomerNo"])),
                    DateOfCall = Convert.ToDateTime(Request["idateOfCall"]),
                    Enabled = true,
                    Subject = Request["isubject"],
                    Description = Request["idescription"],
                    TimeOfCall = TimeSpan.Parse(Request["itimeOfCall"])
                };

                _repository.InsertCustomerCall(customerCall);


            }


            var viewModel = new LoginViewModel { language = language };
            return View(viewModel);
        }

        public ActionResult ManageReports()
        {
            return View();
        }

        public ActionResult ShowCustomerList()
        {
            var db = new Crm.Models.CrmEntities1();
            //CrMVCApp.Models.Customer c;
            var c = (from b in db.CustomerReport select b).ToList();

            var dto = Mapper.Map<IList<Report>>(c);

            CrystalReportCustomers rpt = new CrystalReportCustomers();

            rpt.Load();
            rpt.SetDataSource(dto);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
    }
}
