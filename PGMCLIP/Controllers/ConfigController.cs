
using PGMCLIP.Configuracion;
using PGMCLIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGMCLIP.Controllers
{
    public class ConfigController : Controller
    {
        // GET: Config
        public ActionResult Configuracion()
        {
            List<tablaConfig> listaConfig = pdfConfig.Configurar();

            return View(listaConfig);

        }

        public ActionResult Modificar(int id_config)
        {
            tablaConfig resultado = pdfConfig.updateConfig(id_config);
            return View(resultado);
        }
        // Post: Update
        [HttpPost]
        public ActionResult Modificar(tablaConfig model)
        {

            //  Console.WriteLine(model.font.ToString());
            //   Console.WriteLine(model.color.ToString());
            //  Console.WriteLine(model.font.ToString());
            bool resultado = pdfConfig.ActualizarConfig(model);
            if (resultado)
          { 

    
            return RedirectToAction("Configuracion", "Config", new { @id_config = 9 });
         }
          else
            {

                return View(model);
            }
        }
    }
}
