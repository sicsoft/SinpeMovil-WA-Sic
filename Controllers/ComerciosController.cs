using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SinpeMovilWA.Models;

namespace SinpeMovilWA.Controllers
{

   // [Authorize]
    public class ComerciosController : ApiController
    {
        // GET api/<controller>
        SinpeMovilPruebasEntities db = new SinpeMovilPruebasEntities();
       
        public string RandomString(int length = 8)
        {
            //Random random;
            string Respuesta = "";
            //string timeS = TimeStamp(DateTime.Now);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var randomNumber = 0;

            for (int i = 0; i < length; i++)
            {
                randomNumber = new Random().Next(0, 36);
                System.Threading.Thread.Sleep(200);
                Respuesta += chars.Substring(randomNumber, 1);
            }

            return Respuesta;
        }

        public string GeneraCodigoCliente() {            
            string codigoAutogenerado;
            int existe;
            while (true)
            {
                codigoAutogenerado = RandomString(8);
                existe = db.Comercios.Where(m => m.CodComercio == codigoAutogenerado).Count();
                if (existe == 0)
                    break;
            }

            return codigoAutogenerado;

        }

        // valida que los datos sean correctos antes de guardar, o actualizar.
        public string Validar(Comercios Comercio) {
            string Respuesta = "OK";

            //if (Token == "")
            //{
            //    Respuesta = "Token No Válido";
            //}
            //else

            if (Comercio == null) {
                Respuesta = "Faltan los datos del Comercio";
                // throw new Exception("Faltan los datos del Comercio");
            }
            else if (Comercio.NombreComercio =="") {
                Respuesta = "Debe indicar el Nombre del Comercio";
                //throw new Exception("Debe indicar el Nombre del Comercio");
            }
            //else if (Comercio.Logo == "") {
            //    throw new Exception("Debe indicar el nombre del Logo");
            //}            
            else if (Comercio.Email == "") {
                Respuesta = "Debe indicar el Email de Contacto";
                //throw new Exception("Debe indicar el Email de Contacto");
            }
            else if (Comercio.TelefonoSinpe == "") {
                Respuesta = "Debe indicar el Numero para SinpeMovil";
                //throw new Exception("Debe indicar el Numero para SinpeMovil");
            }

            return Respuesta;
        }

        // POST api/<controller>
        [Route("api/Comercios/Incluir")]
        [HttpPost]
        public async Task <IHttpActionResult> Incluir(Comercios Comercio)
        {
            try
            {
                string Validacion = Validar(Comercio);
                if (Validacion == "OK")
                {                    
                    Comercios NuevoRegistro = new Comercios();

                    NuevoRegistro.NombreComercio = Comercio.NombreComercio;
                    NuevoRegistro.Logo = Comercio.Logo;
                    NuevoRegistro.CodComercio = GeneraCodigoCliente(); // codigo de 8 caracteres aleatorio para clientes nuevos.
                    NuevoRegistro.Email = Comercio.Email;
                    NuevoRegistro.TipoComercio = Comercio.TipoComercio;
                    NuevoRegistro.TipoPlan = Comercio.TipoPlan;
                    NuevoRegistro.TelefonoSinpe = Comercio.TelefonoSinpe;
                    NuevoRegistro.FechaRegistro = DateTime.Now;

                    db.Comercios.Add(NuevoRegistro);

                    db.SaveChanges();

                    var Respuesta = new
                    {
                        success = true,
                        CodComercio = NuevoRegistro.CodComercio,
                        NuevoRegistro.NombreComercio ,
                        NuevoRegistro.TelefonoSinpe
                    };

                    return Ok(Respuesta);
                }
                else {
                    return BadRequest(Validacion);
                }               
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