using FI.AtividadeEntrevista.BLL;
using System;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BeneficiariosPorCliente(long idCliente)
        {
            try
            {
                var lista = new BoBeneficiario().ConsultarPorCliente(idCliente);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}