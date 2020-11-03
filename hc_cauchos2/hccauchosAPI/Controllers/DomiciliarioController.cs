using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilitarios;
using LogicaNegocio;
using System;
using System.Security.Claims;
using System.Threading;

namespace hccauchosAPI.Controllers
{
    //[Authorize(Roles = "3")]
    [RoutePrefix("api/domiciliario")]
    public class DomiciliarioController : ApiController
    {
        //editar correo del domiciliario
        [HttpPut]
        [Route("editarcorreodom")]
        public string editarcorreo(UEncapUsuario correo)
        {
            string mensaje = "";
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            UEncapUsuario user = new UEncapUsuario();
            user.User_id = Int32.Parse(valor.Value);
            user.Correo = correo.Correo;

            user = new LDomiciliario().verificarCorreo(user);
            if (user != null)
            {
                mensaje = "el correo ya existe encuentra asociado a una cuenta";
            }
            else
            {
                if (correo == null)
                {
                    mensaje = "debe ingresar un correo";
                }
                else
                {
                    new LDomiciliario().actualizaruser(user);
                    mensaje = "correo actualizado satisfactoriamente";
                }
            }
            return mensaje;
        }
        //metodo para actualizar contraseña del domiciiliario
        [HttpPut]
        [Route("modificarclavedom")]
        public string modificarclave(UEncapUsuario usuario)
        {
            string mensaje = "";
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            UEncapUsuario user = new UEncapUsuario();
            user.User_id = Int32.Parse(valor.Value);
            user.Clave = usuario.Clave;

            if (usuario == null)
            {
                mensaje = "debe ingresar la nueva clave";
            }
            else
            {
                new LDomiciliario().actualizaruser(user);
                mensaje = "contraseña actualizada satisfactoriamente";
            }
            return mensaje;
        }
        [HttpGet]
        [Route("pedidos")]
        public List<UEncapPedido> obtenerpedidos(int user)
        {
            return new LDomiciliario().pedidosdomiciliario(user);
        }
        [HttpPost]
        [Route("entrega")]
        public void entregapedido(int id)
        {
            UEncapPedido entrega = new UEncapPedido();
            entrega.Id = id;
            entrega.Fecha_pedido_fin = DateTime.Now;
            new LDomiciliario().actualizarnovedad(entrega);

        }

    }
}

