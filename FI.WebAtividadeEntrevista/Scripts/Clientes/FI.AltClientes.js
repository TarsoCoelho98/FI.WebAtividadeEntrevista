$(document).ready(function () {
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