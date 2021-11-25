using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using SinpeMovilWA.Models;

namespace SinpeMovilWA.Controllers
{
    // seguridad por tokens en proyectos framework, para que funcione se debe incluir en
    // App_Start/WebApiConfig.cs, la linea   config.MessageHandlers.Add(new TokenValidationHandler());
    // debajo de  config.MapHttpAttributeRoutes();

    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        SinpeMovilPruebasEntities db = new SinpeMovilPruebasEntities();

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(string CodComercio)
        {
            if (CodComercio == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            var Cliente = db.Comercios.Find(CodComercio);

            if (Cliente != null) {
                // Cliente encontrado, se genera el Token
                var token = TokenGenerator.GenerateTokenJwt(CodComercio);
                return Ok(token);
            }
            else {
                // Cliente NO Valido.
                return Unauthorized();
            }
            
        }
    }
}
