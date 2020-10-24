
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Utilitarios;
using LogicaNegocio;


namespace hccauchosAPI.Controllers
{
    //required token and establish role
    [Authorize (Roles ="1")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {

        [HttpGet]
        [Route("conf")]
        public UEncapUsuario confiadmin()
        {
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var value = claimsIdentity.FindFirst(ClaimTypes.Email).ToString();

            UEncapUsuario usu = new UEncapUsuario();
            usu.Correo = value.ToString();

            var usua = new LAdministrador().obtenerAdmin(usu);
            
            return usua;
           
        }
    }
}
