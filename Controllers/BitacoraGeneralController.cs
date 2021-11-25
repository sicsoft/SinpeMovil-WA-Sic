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


    public class BitacoraGeneralController : ApiController
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

        [Route("api/GetIp")]
        [HttpGet]
        public string GetIp()
        {
            return HttpContext.Current.Request.UserHostAddress; 
        }

        // POST api/<controller>
        [Route("api/BitacoraGeneral/Incluir")]
        [HttpPost]
        public async Task <IHttpActionResult> Incluir(string Token, BitacoraGeneral Bitacora)
        {
            try
            {
                BitacoraGeneral NuevoRegistro = new BitacoraGeneral();

                NuevoRegistro.CodComercio = Bitacora.CodComercio;
                NuevoRegistro.FechaRegistro = DateTime.Now;
                NuevoRegistro.TipoBitacora = Bitacora.TipoBitacora;
                NuevoRegistro.IP = HttpContext.Current.Request.UserHostAddress;

                if (Bitacora.TipoBitacora==0) {
                    // es inicio de pagina o carga de pagina principal, se debe guardar   BitacoraIngreso
                    BitacoraIngreso BitacoraIngreso = new BitacoraIngreso();

                    BitacoraIngreso.CodComercio = Bitacora.CodComercio;
                    BitacoraIngreso.FechaHora = DateTime.Now;

                    db.BitacoraIngreso.Add(BitacoraIngreso);
                }

                db.BitacoraGeneral.Add(NuevoRegistro);

                db.SaveChanges();

                var Respuesta = new
                {
                    success = true,
                    CodComercio = NuevoRegistro.CodComercio,
                    Descripcion = "Bitacora Agregada"
                };

                return Ok(Respuesta);
                         
            }
            catch (Exception ex)
            {
                //return BadRequest("Ha ocurrido un error inesperado, estamos verificando la situación para corregirla");
                return BadRequest(ex.Message);
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