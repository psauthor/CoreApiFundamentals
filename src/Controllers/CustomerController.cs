using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CoreCodeCamp.Data;
using CoreCodeCamp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly CustomerRepository repository;

        public CustomerController(ICustomerRepository repository)
        {
            this.repository = (CustomerRepository) repository;
        }

        /*
        public CustomerController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /*
        // GET: api/Customer
        [HttpGet]
        public ActionResult Get()
        {
            string connectionData = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionData);
            connection.Open();
            SqlCommand AllMenuesCommand = new SqlCommand("SELECT Name FROM Customer",
                                                         connection);
            //int count = (int)AllMenuesCommand.ExecuteScalar();
            var customers = AllMenuesCommand.ExecuteScalar();
            
            connection.Close();



            return Ok(allCustomers);
        }
        */
        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<Customer[]>> Get()
        {
            try
            {
                var result = await repository.GetAllCampsAsync();
                return result;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
