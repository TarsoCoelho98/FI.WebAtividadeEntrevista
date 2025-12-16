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
            // #1. Validações de CPF cliente
            
            if (!ValidadorCpf.CpfValido(cliente.CPF))
                return new ResultadoCliente(TipoResultado.CpfInvalido, "Erro ao salvar, forneça um CPF válido.", false);

            if (ExisteDuplicidade(cliente.CPF, cliente.Id))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, este CPF está vinculado a outro cliente.", false);

            BoBeneficiario bo = new BoBeneficiario();

            // #2. Validações de CPF beneficiario

            if (bo.ExisteDuplicidade(cliente.Beneficiarios))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, há CPFs de beneficiários duplicados.", false);

            foreach (var item in cliente.Beneficiarios)
            {
                if (!ValidadorCpf.CpfValido(item.CPF))
                    return new ResultadoCliente(TipoResultado.CpfInvalido, $"Erro ao salvar, o CPF do beneficiário {item.Nome} é inválido.", false);
            }

            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                long id = cli.Incluir(cliente);
                                
                if (id == 0)
                    new ResultadoCliente(TipoResultado.ErroGenerico, "Falha ao incluir cliente.", false); ;
                
                foreach (var item in cliente.Beneficiarios)
                {
                    item.IdCliente = id;
                    var retorno = bo.Incluir(item);

                    if (!retorno.Sucesso)
                        return retorno;
                }

                return new ResultadoCliente(TipoResultado.Sucesso, "Cadastro efetuado com sucesso.", true);
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
            // #1. Validações de CPF cliente
            if (!ValidadorCpf.CpfValido(cliente.CPF))
                return new ResultadoCliente(TipoResultado.CpfInvalido, "Erro ao salvar, forneça um CPF válido.", false);
            else if (ExisteDuplicidade(cliente.CPF, cliente.Id))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, este CPF está vinculado a outro cliente.", false);

            BoBeneficiario bo = new BoBeneficiario();
            
            // #2. Validações de CPF beneficiario
            if (bo.ExisteDuplicidade(cliente.Beneficiarios))
                return new ResultadoCliente(TipoResultado.CpfDuplicado, "Erro ao salvar, há CPFs de beneficiários duplicados.", false);

            foreach (var item in cliente.Beneficiarios)
            {
                if (!ValidadorCpf.CpfValido(item.CPF))
                    return new ResultadoCliente(TipoResultado.CpfInvalido, $"Erro ao salvar, o CPF do beneficiário {item.Nome} é inválido.", false);
            }

            try
            {
                DAL.DaoCliente cli = new DAL.DaoCliente();
                cli.Alterar(cliente);
                bo.RemoverPorIdCliente(cliente.Id);

                foreach (var item in cliente.Beneficiarios)
                {
                    var retorno = bo.Incluir(item);

                    if (!retorno.Sucesso)
                        return retorno;
                }

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
        public Cliente Consultar(long id)
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
        public List<Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Consulta paginada de clientes
        /// </summary>
        /// <param name="iniciarEm"></param>
        /// <param name="quantidade"></param>
        /// <param name="campoOrdenacao"></param>
        /// <param name="crescente"></param>
        /// <param name="qtd"></param>
        /// <returns></returns>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Verifica duplicidade de cpf
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool ExisteDuplicidade(string CPF, long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.ExisteDuplicidade(CPF, id);
        }
    }
}
