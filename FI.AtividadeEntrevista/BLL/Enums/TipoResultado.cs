using System.ComponentModel;

namespace FI.AtividadeEntrevista.BLL.Enums
{
    public enum TipoResultado
    {
        ErroGenerico, 
        Excecao, 
        CpfInvalido, 
        CpfDuplicado, 
        Sucesso

        //[Description("Erro ao salvar cliente.")]
        //ErroGenerico,
        //Excecao,
        //[Description("Erro ao salvar, forneça um CPF válido.")]
        //CpfInvalido,
        //[Description("Erro ao salvar, este CPF está vinculado a outro cliente.")]
        //CpfDuplicado,
        //[Description("Cadastro efetuado com sucesso.")]
        //SucessoInclusao,
        //[Description("Cadastro alterado com sucesso.")]
        //SucessoAlteracao
    }
}
