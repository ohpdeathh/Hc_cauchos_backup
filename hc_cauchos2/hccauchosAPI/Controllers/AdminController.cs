
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
        [Route("obtenerPerfil")]
        public UEncapUsuario obtenerAdmin()
        {
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.Email);

            UEncapUsuario usu = new UEncapUsuario();
            usu.Correo = valor.Value;

            var usua = new LAdministrador().obtenerAdmin(usu);
            
            return usua;
           
        }

        [HttpGet]
        [Route("modificarContraseña")]
        public string modificarContraseña(UEncapUsuario newcontraseña)
        {
            string mensaje = "";
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.Email);

            UEncapUsuario usu = new UEncapUsuario();
            usu.Correo = valor.Value;
            usu.Clave = newcontraseña.Clave;

            new LAdministrador().actualizarAdmin(usu);

            if (newcontraseña == null)
            {
                mensaje = "debe ingresar la contraseña a cambiar";
            }
            else
            {
                mensaje = "la contraseña se ha modificado satisfactoriamente";
            }
            return mensaje;
        }

        [HttpGet]
        [Route("modificarCorreo")]
        public string modificarCorreo(UEncapUsuario newcorreo)
        {
            string mensaje = "";
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);

            UEncapUsuario usu = new UEncapUsuario();
            usu.Identificacion = valor.Value;
            usu.Clave = newcorreo.Clave;

            new LAdministrador().actualizarAdmin(usu);

            if (newcorreo == null)
            {
                mensaje = "debe ingresar el correo a cambiar";
            }
            else
            {
                mensaje = "el correo se ha modificado satisfactoriamente";
            }
            return mensaje;
        }




    }
}
