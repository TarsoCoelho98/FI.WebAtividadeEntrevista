using FI.AtividadeEntrevista.BLL.Enums;
using FI.AtividadeEntrevista.BLL.Resultados;
using FI.AtividadeEntrevista.BLL.Util;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public ResultadoCliente Incluir(Cliente cliente)
        {
            if (!ValidadorCpf.CpfValido(cliente.CPF))
                return new ResultadoCliente(TipoResultado.CpfInvalido, "Erro ao salvar, forneça um CPF válido.", false);
            else if (VerificarDuplicidadeCpf(cliente.CPF, cliente.Id))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, este CPF está vinculado a outro cliente.", false);

            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                return cli.Incluir(cliente) != 0
                ? new ResultadoCliente(TipoResultado.Sucesso, "Cadastro efetuado com sucesso.", true)
                : new ResultadoCliente(TipoResultado.ErroGenerico, "Falha ao incluir cliente.", false);
            }
            catch (Exception e)
            {
                return new ResultadoCliente(TipoResultado.Excecao, e.Message, false);
            }
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public ResultadoCliente Alterar(Cliente cliente)
        {
            if (!ValidadorCpf.CpfValido(cliente.CPF))
                return new ResultadoCliente(TipoResultado.CpfInvalido, "Erro ao salvar, forneça um CPF válido.", false);
            else if (VerificarDuplicidadeCpf(cliente.CPF, cliente.Id))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, este CPF está vinculado a outro cliente.", false);

            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                cli.Alterar(cliente);
                return new ResultadoCliente(TipoResultado.Sucesso, "Cadastro alterado com sucesso.", true);
            }
            catch (Exception e)
            {
                return new ResultadoCliente(TipoResultado.Excecao, e.Message, false);
            }
        }



        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarDuplicidadeCpf(string CPF, long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarDuplicidadeCpf(CPF, id);
        }
    }
}
