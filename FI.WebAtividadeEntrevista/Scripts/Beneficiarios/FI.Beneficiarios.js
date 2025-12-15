var beneficiarios = [];

$(document).on('click', '#btnIncluirBeneficiario', function () {
    var cpf = $('#CpfBeneficiario').val();
    var nome = $('#NomeBeneficiario').val();
    var idCliente = 0; 

    if (obj) {
        idCliente = obj.Id;
    }

    var beneficiario = {
        CPF: cpf,
        Nome: nome,
        IdCliente: idCliente
    };

    beneficiarios.push(beneficiario);
    $('#CpfBeneficiario').val('');
    $('#NomeBeneficiario').val('');
});

    //var dados = {
    //    CPF: cpf,
    //    Nome: nome,
    //    IdCliente: idCliente
    //};
    //$.ajax({
    //    url: '/Beneficiario/Incluir',
    //    type: 'POST',
    //    data: dados,
    //    error:
    //        function (r) {
    //            if (r.status == 400)
    //                ModalDialog("Ocorreu um erro", r.responseJSON);
    //            else if (r.status == 500)
    //                ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
    //        },
    //    success:
    //        function (r) {
    //            ModalDialog("Sucesso!", r)
    //            $("#formCadastro")[0].reset();
    //        }
    //});

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
