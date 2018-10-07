using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreCodeCamp.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController
  {

    public string[] Get()
    {
      return new[] { "Hello", "From","Pluralsight" };
    }
  }
}
