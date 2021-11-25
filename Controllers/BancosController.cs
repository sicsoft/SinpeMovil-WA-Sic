using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SinpeMovilWA.Models;


namespace SinpeMovilWA.Controllers
{
    // seguridad por tokens en proyectos framework, para que funcione se debe incluir en
    // App_Start/WebApiConfig.cs, la linea   config.MessageHandlers.Add(new TokenValidationHandler());
    // debajo de  config.MapHttpAttributeRoutes();

    [Authorize]
    [RoutePrefix("api/bancos")]
    public class BancosController : ApiController
    {
        // GET api/<controller>
        SinpeMovilPruebasEntities db = new SinpeMovilPruebasEntities();
       
        public async Task<IHttpActionResult> Get()
        {
            var Respuesta =  db.Bancos.Select(a => new {
                a.CodBanco,
                a.NombreBanco,
                a.ActivaPrincipal
            }).ToList();

            return Ok(Respuesta);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }  
    
        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}