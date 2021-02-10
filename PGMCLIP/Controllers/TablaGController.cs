using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGMCLIP.Models
{
    public class TablaGController : Controller
    {
        // GET: Registro
        public ActionResult TablaG()
        {
            return View();

        }
    }
}

      /*  [HttpPost]//mando los valores
        public ActionResult Registro(User x)
        {
            if (ModelState.IsValid)
            {
                bool resultado = UsuarioAD.Registrar(x);

                if (resultado)
                {
                    return RedirectToAction("Login", "User");


                }
                else
                {
                    return View();
                }
            }
            else { return View(); }
        }
    }
      */