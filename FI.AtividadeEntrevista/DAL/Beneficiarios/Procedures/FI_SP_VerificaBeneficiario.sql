CREATE PROC FI_SP_VerificaBeneficiario
    @IdBeneficiario BIGINT,
    @IdCliente      BIGINT,
    @CPF            VARCHAR(11)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 1
    FROM BENEFICIARIOS
    WHERE CPF = @CPF
      AND IDCLIENTE = @IdCliente
      AND (@IdBeneficiario = 0 OR ID <> @IdBeneficiario);
END
GO
