
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
        //metodo para modificar la contraseña del admin
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
        //servicio para modificar el correo del admin
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
        //servicio para insertar al empleado 
        [HttpPost]
        [Route("insertarEmpleado")]
        public void insertarEmpleado(UEncapUsuario empleado)
        {
            new LAdministrador().insertarEmpleado(empleado);
        }

        //servicio para obtener empleados por nombre
        [HttpGet]
        [Route("obtenerEmpleados")]
        public List<UEncapUsuario> obtenerEmpleados()
        {
            return new LAdministrador().obtenerEmpleados();
        }

        //servicio actualizar empleado
        [HttpPost]
        [Route("modificarEmpleado")]
        public void obtenerEmpleados(UEncapUsuario empleado)
        {
            new LAdministrador().actualizarEmpleado(empleado);
        }


        //servicio para insertar a inventario
        [HttpPost]
        [Route("insertarItemInventario")]
        public void insertarInventario(UEncapInventario item)
        {
            new LAdministrador().insertarItem(item);
        }

        //servicio para obtener inventario
        [HttpPost]
        [Route("obtenerInventario")]
        public List<UEncapInventario> obtenerInventario()
        {
            return new LAdministrador().ConsultarInventario();
        }

        //servicio actualizar inventario
        [HttpPost]
        [Route("modificarInventario")]
        public void modificarInventario(UEncapInventario inventario)
        {
            new LAdministrador().ActualizarInventario(inventario);
        }

        //servicios para mision vision objetivo 
        [HttpGet]
        [Route("obtenerMision")]
        public EncapMision getMision()
        {
            return new LAdministrador().ObtenerMision();
        }

        [HttpGet]
        [Route("obtenerVision")]
        public EncapVision getVision()
        {
            return new LAdministrador().ObtenerVision();
        }
       
        [HttpGet]
        [Route("obtenerObjetivo")]
        public EncapObjetivo getObjetivo()
        {
            return new LAdministrador().ObtenerObjetivo();
        }

        //servicios para modificar mision vision objetivo
        [HttpPost]
        [Route("modificarMision")]
        public void setMision(EncapMision mision)
        {
            new LAdministrador().ActualizarMision(mision);
        }

        [HttpPost]
        [Route("modificarVision")]
        public void setVision(EncapVision vision)
        {
            new LAdministrador().ActualizarVision(vision);
        }

        [HttpPost]
        [Route("modificarObjetivo")]
        public void setObjetivo(EncapObjetivo objetivo)
        {
            new LAdministrador().ActualizarObjetivo(objetivo);
        }









    }
}
