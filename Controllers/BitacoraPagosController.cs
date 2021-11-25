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

    public class BitacoraPagosController : ApiController
    {
        // GET api/<controller>
        SinpeMovilPruebasEntities db = new SinpeMovilPruebasEntities();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
      

        // POST api/<controller>
        [Route("api/BitacoraPagos/Incluir")]
        [HttpPost]
        public async Task <IHttpActionResult> Incluir(string Token, BitacoraPagos Pago)
        {
            try
            {
                BitacoraPagos NuevoRegistro = new BitacoraPagos();

                NuevoRegistro.CodComercio = Pago.CodComercio;
                NuevoRegistro.FechaHora = DateTime.Now;
                NuevoRegistro.CodBanco  = Pago.CodBanco;
                NuevoRegistro.Ip = HttpContext.Current.Request.UserHostAddress;
                NuevoRegistro.MontoPago = Pago.MontoPago;
                NuevoRegistro.Telefono = Pago.Telefono;
               
                db.BitacoraPagos.Add(NuevoRegistro);

                db.SaveChanges();

                var Respuesta = new
                {
                    success = true,
                    CodComercio = NuevoRegistro.CodComercio,
                    Descripcion = "Pago Agregado",
                    Pago.MontoPago
                };

                return Ok(Respuesta);                         
            }
            catch (Exception ex)
            {
                //return BadRequest("Ha ocurrido un error inesperado, estamos verificando la situación para corregirla");
                return BadRequest();
            }
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