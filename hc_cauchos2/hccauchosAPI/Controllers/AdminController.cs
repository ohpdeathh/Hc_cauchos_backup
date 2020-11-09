
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Utilitarios;
using LogicaNegocio;
using System;
using System.Collections.Generic;

namespace hccauchosAPI.Controllers
{
    //required token and establish role
    [Authorize(Roles = "1")]
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

        [HttpGet]
        [Route("obtenerMision")]
        public EncapMision obtenerMision()
        {
            return new LAdministrador().ObtenerMision();
        }

        [HttpPost]
        [Route("actualizarMision")]
        public void actualizarMision(EncapMision mision)
        {
            new LAdministrador().ActualizarMision(mision);
        }

        [HttpGet]
        [Route("obtenerVision")]
        public EncapVision obtenerVision()
        {
            return new LAdministrador().ObtenerVision();
        }

        [HttpPost]
        [Route("actualizarVision")]
        public void actualizarVision(EncapVision vision)
        {
            new LAdministrador().ActualizarVision(vision);
        }

        [HttpGet]
        [Route("obtenerObjetivo")]
        public EncapObjetivo obtenerObjetivo()
        {
            return new LAdministrador().ObtenerObjetivo();
        }

        [HttpPost]
        [Route("actualizarObjetivo")]
        public void actualizarObjetivo(EncapObjetivo objetivo)
        {
            new LAdministrador().ActualizarObjetivo(objetivo);
        }

        [HttpPost]
        [Route("insertarEmpleado")]
        public void insertarEmpleado (UEncapUsuario empleado)
        {
            new LAdministrador().insertarEmpleado(empleado);
        }

        [HttpGet]
        [Route("obtenerEmpleados")]
        public List<UEncapUsuario> obtenerEmpleados()
        {
            return new LAdministrador().obtenerEmpleados();
        }

        [HttpPost]
        [Route("actualizarEmpleados")]
        public void actualizarEmpleados(UEncapUsuario empleado)
        {
            new LAdministrador().actualizarEmpleado(empleado);
        }

        [HttpGet]
        [Route("obtenerInventario")]
        public List<UEncapInventario> obtenerInventario()
        {
            return new LAdministrador().ConsultarInventario();
        }

        [HttpPost]
        [Route("actualizarInventario")]
        public void actualizarInventario(UEncapInventario inventario)
        {
            new LAdministrador().ActualizarInventario(inventario);
        }

        [HttpPost]
        [Route("însertarItem")]
        public void insertarItem(UEncapInventario item)
        {
            new LAdministrador().insertarItem(item);
        }
        [HttpPost]
        [Route("actualziarTiempoCarrito")]
        public void actualizarTiempoCarrito(UEncapParametros tiempo)
        {
            new LAdministrador().ActualizarTiempoCarrito(tiempo);
        }

        [HttpGet]
        [Route("consultarMarca")]
        public List<UEncapMarca> consultarMarca()
        {
            return new LAdministrador().ConsultarMarca();
        }

        [HttpGet]
        [Route("consultarCategoria")]
        public List<UEncapCategoria> consultarCategoria()
        {
            return new LAdministrador().ConsultarCategoria();
        }

        [HttpGet]
        [Route("consultarVentas")]
        public List<UEncapPedido> consultarVentas()
        {
            return new LAdministrador().ConsultarVentas();
        }

        [HttpGet]
        [Route("consultarEmpleado")]
        public List<UEncapUsuario> consultarEmpleado()
        {
            return new LAdministrador().ConsultarEmpleado();
        }

        [HttpPost]
        [Route("consultarPedidosEstado")]
        public List<UEncapPedido> consultarPedidosEstado(int estado)
        {
            return new LAdministrador().ConsultarPedidosEstado(estado);
        }

        [HttpGet]
        [Route("consultarPedidos")]
        public List<UEncapPedido> consultarPedidos()
        {
            return new LAdministrador().ConsultarPedidos();
        }























    }
}
