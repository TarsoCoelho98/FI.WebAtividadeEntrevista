using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            // lógica pode ser passado pro próprio .js
            model.CPF = model.CPF.Replace(".", string.Empty).Replace("-", string.Empty).Trim();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            var resultado = bo.Incluir(new Beneficiario()
            {
                CPF = model.CPF,
                Nome = model.Nome,
                IdCliente = model.IdCliente,
            });

            if (!resultado.Sucesso)
            {
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, resultado.Mensagem));
            }

            return Json(resultado.Mensagem);
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();
            // lógica pode ser passado pro próprio .js
            model.CPF = model.CPF.Replace(".", string.Empty).Replace("-", string.Empty).Trim();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            var resultado = bo.Alterar(new Beneficiario()
            {
                Id = model.Id,
                CPF = model.CPF,
                Nome = model.Nome,
            });

            if (!resultado.Sucesso)
            {
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, resultado.Mensagem));
            }

            return Json(resultado.Mensagem);
        }
    }
}