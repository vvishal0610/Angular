using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCRUDusingAngular.Models;

namespace WebApiCRUDusingAngular.Controllers
{
    [RoutePrefix("Api/CustomerAPI")]
    public class CustomerAPIController : ApiController
    {
        AzureCustomerEntities customerEntities = new AzureCustomerEntities();

        [HttpGet]
        [Route("AllCustomerDetails")]
        public IQueryable<Customer> GetCustomers()
        {
            try
            {
                return customerEntities.Customers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetCustomerDetailsById/{customerId}")]
        public IHttpActionResult GetCustomerById(string customerId)
        {
            Customer objEmp = new Customer();
            int ID = Convert.ToInt32(customerId);
            try
            {
                objEmp = customerEntities.Customers.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertCustomerDetails")]
        public IHttpActionResult PostEmaployee(Customer data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                customerEntities.Customers.Add(data);
                customerEntities.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateCustomerDetails")]
        public IHttpActionResult PutEmaployeeMaster(Customer cust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer objcust = new Customer();
                objcust = customerEntities.Customers.Find(cust.CustomerID);
                if (objcust != null)
                {
                    objcust.CustomerName = cust.CustomerName;
                    objcust.Address = cust.Address;
                    objcust.ContactName = cust.ContactName;
                    objcust.City = cust.City;
                    objcust.Country = cust.Country;
                    objcust.PostalCode = cust.PostalCode;

                }
                int i = this.customerEntities.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(cust);
        }

        [HttpDelete]
        [Route("DeleteCustomerDetails")]
        public IHttpActionResult DeleteCustomerDelete(int id)
        {
            //int empId = Convert.ToInt32(id);  
            Customer customer = customerEntities.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            customerEntities.Customers.Remove(customer);
            customerEntities.SaveChanges();

            return Ok(customer);
        }
    }
}
