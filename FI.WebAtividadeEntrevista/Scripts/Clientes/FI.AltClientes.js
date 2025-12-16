var indexEdicao = null; 

$(document).ready(function () {
    $('#btnBeneficiarios').on('click', function () {
        $('#modalBeneficiarios').modal('show');
    });

    $('#btnCancelar').on('click', function () {
        cancelarModoEdicao();
    });

    $('#btnAtualizarBeneficiario').on('click', function () {
        editarBeneficiario();
    });


    $('#CpfBeneficiario, #NomeBeneficiario').on('input', function () {
        validarBeneficiario();
    });

    $('#modalBeneficiarios').on('hidden.bs.modal', function () {
        limparCamposBeneficiario();
        cancelarModoEdicao(); 
    });

    $(document).on('click', '#btnIncluirBeneficiario', function (e) {
        e.preventDefault();
        var cpf = $('#CpfBeneficiario').val().replace(/\./g, '').replace(/-/g, '');

        var nome = $('#NomeBeneficiario').val();
        var idCliente = 0;

        if (typeof obj !== 'undefined' && obj !== null) {
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

        atualizarGridBeneficiarios();
    });

    if (obj) {
        $.ajax({
            url: urlBeneficiariosPorCliente, 
            type: 'GET',
            dataType: 'json',
            data: {
                idCliente: obj.Id
            },
            success: function (data) {
                beneficiarios = data;          
                atualizarGridBeneficiarios();  
            },
            error: function (xhr) {
                console.error('Erro ao buscar beneficiários:', xhr.responseText);
            }
        });


        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);


        var $cpf = $('#CPF');
        if ($('#CPF').val()) {
            var mask = $cpf.val().replace(/\D/g, '');
            mask = mask.replace(/(\d{3})(\d)/, '$1.$2');
            mask = mask.replace(/(\d{3})(\d)/, '$1.$2');
            mask = mask.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
            $cpf.val(mask);
        }
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        normalizarCpfBeneficiarios();

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "CPF": $(this).find("#CPF").val().replace(/\./g, '').replace(/-/g, ''),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "Beneficiarios": beneficiarios
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    window.location.href = urlRetorno;
                }
        });
    })

})

function normalizarCpfBeneficiarios() {
    if (!beneficiarios || beneficiarios.length === 0) return;

    beneficiarios.forEach(function (b) {
        if (b.CPF) {
            b.CPF = b.CPF.replace(/\./g, '').replace(/-/g, '');
        }
    });
}

function limparCamposBeneficiario() {
    $('#CpfBeneficiario').val('');
    $('#NomeBeneficiario').val('');
    $('#btnIncluirBeneficiario').prop('disabled', true);
}
function removerBeneficiario(index) {
    beneficiarios.splice(index, 1);
    atualizarGridBeneficiarios();
}

function validarBeneficiario() {
    const cpf = $('#CpfBeneficiario').val().trim();
    const nome = $('#NomeBeneficiario').val().trim();

    const cpfValido = cpf.length === 14;
    const nomeValido = nome.length > 0;

    $('#btnAtualizarBeneficiario').prop('disabled', !(cpfValido && nomeValido)); 
    $('#btnIncluirBeneficiario').prop('disabled', !(cpfValido && nomeValido));
}

function atualizarGridBeneficiarios() {

    var table = $('#gridBeneficiarios');
    var tbody = table.find('tbody');

    tbody.empty();

    if (!beneficiarios || beneficiarios.length === 0) {
        table.hide();
        return;
    }

    table.show();

    beneficiarios.forEach(function (b, index) {

        var cpfFormatado = formatarCpf(b.CPF);

        tbody.append(
            '<tr data-index="' + index + '">' +
            '<td class="text-center">' +
            cpfFormatado +
            '<input type="hidden" class="beneficiario-id" value="' + (b.Id || 0) + '" />' +
            '</td>' +
            '<td class="text-center">' + b.Nome + '</td>' +
            '<td class="text-center text-nowrap">' +
            '<div class="btn-group" role="group">' +
            '<button type="button" class="btn btn-primary btn-sm mr-2" ' +
            'onclick="abrirModoEdicao(' + index + ')">Alterar</button>' +
            '<button type="button" class="btn btn-danger btn-sm" ' +
            'onclick="removerBeneficiario(' + index + ')">Excluir</button>' +
            '</div>' +
            '</td>' +
            '</tr>'
        );
    });
}

function abrirModoEdicao(index) {
    indexEdicao = index;
    var beneficiarioEdicao = beneficiarios[indexEdicao];
    limparCamposBeneficiario();
    $('#CpfBeneficiario').val(formatarCpf(beneficiarioEdicao.CPF));
    $('#NomeBeneficiario').val(beneficiarioEdicao.Nome);
    $('#btnIncluirBeneficiario').hide();
    $('#grupoEdicao').show();
    $('#gridBeneficiarios').addClass('table-disabled');
}

function editarBeneficiario() {
    beneficiarios[indexEdicao].CPF = $('#CpfBeneficiario').val();
    beneficiarios[indexEdicao].Nome = $('#NomeBeneficiario').val();
    cancelarModoEdicao();
    atualizarGridBeneficiarios();
}

function cancelarModoEdicao() {
    indexEdicao = null;
    limparCamposBeneficiario();
    $('#btnIncluirBeneficiario').show();
    $('#grupoEdicao').hide();
    $('#gridBeneficiarios').removeClass('table-disabled');
}


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


function formatarCpf(cpf) {
    if (!cpf) return '';

    cpf = cpf.replace(/\D/g, '').padStart(11, '0');

    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/,
        '$1.$2.$3-$4');
}
function aplicarMascaraCpf(input) {
    let valor = input.value.replace(/\D/g, '');

    if (valor.length > 11)
        valor = valor.slice(0, 11);

    valor = valor.replace(/(\d{3})(\d)/, '$1.$2');
    valor = valor.replace(/(\d{3})(\d)/, '$1.$2');
    valor = valor.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    input.value = valor;
}