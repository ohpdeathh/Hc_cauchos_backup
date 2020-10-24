using System.Web.Http;

namespace WebApiSegura.Controllers
{
    /// <summary>
    /// customer controller class for testing security token 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake: " + id;
            return Ok(customerFake);
        }

        [HttpGet]
        [Route("obtener")]
        public IHttpActionResult GetAll()
        {
            var customersFake ="prueba";
            return Ok(customersFake);
        }
    }
}
