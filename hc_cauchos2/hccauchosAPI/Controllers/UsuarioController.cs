using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilitarios;
using LogicaNegocio;
using System;

namespace hccauchosAPI.Controllers
{
    [Authorize(Roles = "4")]
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
    }
}
