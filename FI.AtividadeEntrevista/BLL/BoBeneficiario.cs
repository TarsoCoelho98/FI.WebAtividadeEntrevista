using FI.AtividadeEntrevista.BLL.Enums;
using FI.AtividadeEntrevista.BLL.Resultados;
using FI.AtividadeEntrevista.BLL.Util;
using FI.AtividadeEntrevista.DML;
using System;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto beneficiário</param>
        public ResultadoCliente Incluir(Beneficiario beneficiario)
        {
            if (!ValidadorCpf.CpfValido(beneficiario.CPF))
                return new ResultadoCliente(TipoResultado.CpfInvalido, "Erro ao salvar, forneça um CPF válido.", false);
            else if (VerificarDuplicidadeCpfPorCliente(beneficiario.CPF, beneficiario.Id, beneficiario.IdCliente))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, este CPF já existe vinculado a esse cliente.", false);

            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();

                return dao.Incluir(beneficiario) != 0 
                    ? new ResultadoCliente(TipoResultado.Sucesso,"Beneficiário incluído com sucesso.",true)
                    : new ResultadoCliente(TipoResultado.ErroGenerico,"Falha ao incluir beneficiário.",false);
            }
            catch (Exception e)
            {
                return new ResultadoCliente(TipoResultado.Excecao, e.Message, false);
            }
        }


        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto beneficiário</param>
        public ResultadoCliente Alterar(Beneficiario beneficiario)
        {
            if (!ValidadorCpf.CpfValido(beneficiario.CPF))
                return new ResultadoCliente(
                    TipoResultado.CpfInvalido,
                    "Erro ao salvar, forneça um CPF válido.",
                    false
                );

            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                dao.Alterar(beneficiario);

                return new ResultadoCliente(
                    TipoResultado.Sucesso,
                    "Beneficiário alterado com sucesso.",
                    true
                );
            }
            catch (Exception e)
            {
                return new ResultadoCliente(
                    TipoResultado.Excecao,
                    e.Message,
                    false
                );
            }
        }

        /// <summary>
        /// Excluir beneficiário
        /// </summary>
        /// <param name="id">Id do beneficiário</param>
        public ResultadoCliente Excluir(long id)
        {
            try
            {
                DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
                dao.Excluir(id);

                return new ResultadoCliente(TipoResultado.Sucesso, "Beneficiário excluído com sucesso.", true);
            }
            catch (Exception e)
            {
                return new ResultadoCliente(TipoResultado.Excecao, e.Message, false);
            }
        }


        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="idBeneficiario"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public bool VerificarDuplicidadeCpfPorCliente(string CPF, long idBeneficiario, long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarDuplicidadeCpfPorCliente(CPF, idBeneficiario, idCliente);
        }
    }
}
