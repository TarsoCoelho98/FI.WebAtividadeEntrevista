using FI.AtividadeEntrevista.BLL.Enums;
using FI.AtividadeEntrevista.BLL.Resultados;
using FI.AtividadeEntrevista.BLL.Util;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Incluir novo beneficiário
        /// </summary>
        /// <param name="beneficiario"></param>
        /// <returns></returns>
        public ResultadoCliente Incluir(Beneficiario beneficiario)
        {            
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                return dao.Incluir(beneficiario) != 0 
                    ? new ResultadoCliente(TipoResultado.Sucesso,"Beneficiário incluído com sucesso.",true)
                    : new ResultadoCliente(TipoResultado.ErroGenerico,$"Falha ao incluir o beneficiário {beneficiario.Nome}.",false);
            }
            catch (Exception e)
            {
                return new ResultadoCliente(TipoResultado.Excecao, e.Message, false);
            }
        }

        /// <summary>
        /// Consultar por cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public List<DML.Beneficiario> ConsultarPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.ConsultarPorCliente(idCliente);
        }

        /// <summary>
        /// Remover por cliente
        /// </summary>
        /// <param name="idCliente"></param>
        internal void RemoverPorIdCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            dao.RemoverPorIdCliente(idCliente);
        }

        public bool ExisteDuplicidade(List<Beneficiario> beneficiarios)
        {
            var cpfs = new HashSet<string>();

            foreach (var item in beneficiarios)
            {
                if (string.IsNullOrWhiteSpace(item.CPF))
                    continue;

                if (!cpfs.Add(item.CPF))
                    return true;
            }

            return false;
        }
    }
}
