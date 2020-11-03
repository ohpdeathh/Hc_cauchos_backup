
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Utilitarios;
using LogicaNegocio;
using System;

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
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            UEncapUsuario usu = new UEncapUsuario();
            usu.User_id = Int32.Parse(valor.Value);
            usu.Clave = newcontraseña.Clave;
           

            new LAdministrador().actualizarContraseña(usu);

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
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            UEncapUsuario usu = new UEncapUsuario();
            usu.User_id = Int32.Parse(valor.Value);
            usu.Correo = newcorreo.Correo;

            bool verificar = new LAdministrador().verifcarCorreo(usu);
            if (verificar != false)
            {
                return "el correo ya se encuentra asociado a una cuenta";
            }
            else
            {
                new LAdministrador().actualizarCorreo(usu);

                if (newcorreo == null)
                {
                    return "debe ingresar el correo a cambiar";
                }
                else
                {
                    return "el correo se ha modificado satisfactoriamente";
                }
            }

        }




    }
}
