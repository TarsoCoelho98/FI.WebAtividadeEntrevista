using FI.AtividadeEntrevista.BLL.Enums;

namespace FI.AtividadeEntrevista.BLL.Resultados
{
    public class ResultadoCliente
    {
        public TipoResultado Tipo { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }

        public ResultadoCliente(TipoResultado tipo, string mensagem, bool sucesso)
        {
            Tipo = tipo;
            Mensagem = mensagem;
            Sucesso = sucesso;
        }
    }
}
