using AutoMapper;
using Crm.Core.Dto;
using Crm.Core.Models;
using Crm.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Crm.Api
{
    [RoutePrefix("api")]
    public class CrmApiController :  BaseApiController
    {
        private ICrmRepository _repository;
        public CrmApiController()
        {
            _repository = new CrmRepository(this.ConnectionString);
        }

        [Route("customers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var customers = _repository.GetCustomers();
            var dto = Mapper.Map<IList<CustomerDto>>(customers);
            return Ok(dto);
        }

        [Route("customercalls")]
        [HttpGet]
        public async Task<IHttpActionResult>  GetCustomerCalls()
        {
            var customerCalls =  _repository.GetCustomerCalls();
            var dto = Mapper.Map<IList<CustomerCallDto>>(customerCalls);
            return Ok(dto);
        }

        [Route("customers/{customerNo}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer(int customerNo)
        {
            var customer = _repository.GetCustomer(customerNo);
            var dto = Mapper.Map<CustomerDto>(customer);
            return Ok(dto);
        }

        [Route("customercalls/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerCalls(Guid id)
        {
            var customerCall = _repository.GetCustomerCall(id);
            var dto = Mapper.Map<CustomerCallDto>(customerCall);
            return Ok(dto);
        }

        [Route("customers/create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer(CustomerDto customerDto)
        {

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            var result = _repository.InsertCustomer(customer);
            customerDto.CustomerNo = result.CustomerNo;
            return Ok(customerDto);
        }

        [Route("customercalls/create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomerCall(CustomerCallDto customerCallDto)
        {
            var customerCall = Mapper.Map<CustomerCallDto, CustomerCall>(customerCallDto);
            var result = _repository.InsertCustomerCall(customerCall);
            customerCallDto.Id = result.Id;
            return Ok(customerCallDto);
        }


        [Route("customers/update")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer(CustomerDto customerDto)
        {
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
           var result =  _repository.UpdateCustomer(customer);
            return Ok(result);
        }

        [Route("customercalls/update")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerCall(CustomerCallDto customerCallDto)
        {
            var customer = Mapper.Map<CustomerCallDto, CustomerCall>(customerCallDto);
            var result =   _repository.UpdateCustomerCall(customer);
            return Ok(result);

        }

        [Route("customers/delete/{customerNo}")]
        [HttpPut]
        public async Task<IHttpActionResult> DeleteCustomer(int customerNo)
        {
            var customer = _repository.GetCustomer(customerNo);
            customer.Enabled = false;
            var result = _repository.UpdateCustomer(customer);
            return Ok(result);
        }
        [Route("customercalls/delete/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> DeleteCustomerCall(string id)
        {

            var customerCall = _repository.GetCustomerCall(new Guid(id));
            customerCall.Enabled = false;
            var result = _repository.UpdateCustomerCall(customerCall);
            return Ok(result);
        }

    }
}
