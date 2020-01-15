using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController
    {
        private readonly IConfiguration configuration;

        public ValuesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string[] Get()
        {
            string connectionData = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionData);
            connection.Open();
            SqlCommand AllMenuesCommand = new SqlCommand("SELECT COUNT(Menu.Description)FROM Customer INNER JOIN Menu ON Menu.CustomerId = Customer.id",
                                                         connection);
            int count = (int)AllMenuesCommand.ExecuteScalar();
            connection.Close();


            return new[] { count.ToString(), "From", "Pluralsight" };
        }
    }
}
