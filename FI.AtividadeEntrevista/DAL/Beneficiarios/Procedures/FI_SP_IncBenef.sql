CREATE PROC [dbo].[FI_SP_IncBenef]
    @CPF        VARCHAR(11),
    @NOME       VARCHAR(50),
    @IDCLIENTE  BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BENEFICIARIOS
    (
        CPF,
        NOME,
        IDCLIENTE
    )
    VALUES
    (
        @CPF,
        @NOME,
        @IDCLIENTE
    );

    SELECT SCOPE_IDENTITY();
END
GO
