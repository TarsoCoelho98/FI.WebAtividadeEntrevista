using System.Collections.Generic;
using System.Data;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiário
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto beneficiário</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.IdCliente));
            DataSet ds = base.Consultar("FI_SP_IncBenef", parametros);

            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario ben = new DML.Beneficiario();
                    ben.Id = row.Field<long>("ID");
                    ben.CPF = row.Field<string>("CPF");
                    ben.Nome = row.Field<string>("NOME");
                    ben.IdCliente = row.Field<long>("IDCLIENTE");
                    lista.Add(ben);
                }
            }

            return lista;
        }

        /// <summary>
        /// Consultar por id cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        internal List<Beneficiario> ConsultarPorCliente(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("idCliente", idCliente));
            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            return Converter(ds);
        }


        /// <summary>
        /// Remover por id cliente
        /// </summary>
        /// <param name="idCliente"></param>
        internal void RemoverPorIdCliente(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("idCliente", idCliente));
            base.Executar("FI_SP_DelBeneficiario", parametros);
        }
    }
}
