namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        public long IdCliente { get; set; }
    }
}
