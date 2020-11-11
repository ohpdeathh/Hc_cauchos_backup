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
    [Authorize(Roles = "4")]
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        //metodo para eliminar cuenta
        [HttpDelete]
        [Route("eliminarcuenta")]
        public void eliminar()
        {
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            UEncapUsuario user = new UEncapUsuario();
            user.User_id = Int32.Parse(valor.Value);

            new LUsuario().EliminarUsuario(user);
        }
        [HttpGet]
        [Route("editarcorreouser")]
        public string editarcorreo(UEncapUsuario correo)
        {
            string mensaje = "";
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            UEncapUsuario user = new UEncapUsuario();
            user.User_id = Int32.Parse(valor.Value);
            user.Correo = correo.Correo;

            bool verificar = new LAdministrador().verifcarCorreo(user);
            if (verificar != false)
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
                    new LAdministrador().actualizarCorreo(user);
                    mensaje = "correo actualizado satisfactoriamente";
                }
            }
            return mensaje;
        }
        //metodo para actualizar contraseña del user
        [HttpGet]
        [Route("modificarclave")]
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
                new LAdministrador().actualizarContraseña(user);
                mensaje = "contraseña actuañizada satisfactoriamente";
            }
            return mensaje;
        }
        //metodo para llamar pedidos
        [HttpGet]
        [Route("obtenerpedidos")]
        public List<UEncapPedido> obtenerpedidos()
        {
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userid = Int32.Parse(valor.Value);

            return new LUsuario().ObtenerPedidosActivo(userid);

        }
        //metodo para obtener pedidos finalizados
        [HttpGet]
        [Route("pedidosfinalizados")]
        public List<UEncapPedido> pedidosfinalizados()
        {

            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var valor = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userid = Int32.Parse(valor.Value);

            return new LUsuario().ObtenerPedidosFin(userid);
        }

        //metodo para mostrar catalogo
        [HttpGet]
        [Route("catalogo")]
        public List<UEncapInventario> catalogo()
        {
            return new LUsuario().ConsultarInventario();
        }
        //metodo para mostrar catalogo por categoria
        [HttpGet]
        [Route("catalogocategoria")]
        public List<UEncapInventario> catalogocategoria(catalogocat categ)
        {

            return new LUsuario().ConsultarInventarioCategoria(categ.categoria);
        }
        //metodo para mostrar catalogo por marca
        [HttpGet]
        [Route("catalogomarca")]
        public List<UEncapInventario> catalogomarca(catalogomarca marca)
        {
            return new LUsuario().ConsultariMarca(marca.marca);
        }
        //metodo para mostrar catalogo combinado
        [HttpGet]
        [Route("catalogocombinado")]
        public List<UEncapInventario> catalogocombinado(catalogocombinado combinado)
        {
            return new LUsuario().ConsultarInventariCombinado(combinado.marca, combinado.categoria);
        }

        //metodo para mostrar catalogo precio
        [HttpGet]
        [Route("catalogoprecio")]
        public List<UEncapInventario> catalogoprecio(catalogoprecio valor)
        {
            return new LUsuario().ConsultarInventaroiPrecio(valor.precio);
        }
        //metodo para mostrar catalogo precio y categoria
        [HttpGet]
        [Route("catalogopreciocategoria")]
        public List<UEncapInventario> catalogopreciocate(catalogopreciocategoria preciocateg)
        {
            return new LUsuario().ConsultarInventaroiPrecioCategoria(preciocateg.precio, preciocateg.categoria);
        }
        //metodo para mostrar catalogo precio y marca
        [HttpGet]
        [Route("catalogopreciomarca")]
        public List<UEncapInventario> catalogopreciomarca(catalogopreciomarca preciomarca)
        {

            return new LUsuario().ConsultarInventaroiPrecioMarca(preciomarca.precio, preciomarca.marca);
        }
        //metodo para mostrar catalogo precio,marca ycategoria
        [HttpGet]
        [Route("catalogopreciocombinado")]
        public List<UEncapInventario> catalogopreciocombinado(combinadorequest requeridos)
        {
            return new LUsuario().ConsultarInventaroiPrecioCombinado(requeridos.precio, requeridos.marca, requeridos.categoria);
        }

    }
}
