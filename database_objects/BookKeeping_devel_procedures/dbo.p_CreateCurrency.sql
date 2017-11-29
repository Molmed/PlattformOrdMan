USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_CreateCurrency]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_CreateCurrency](
@identifier VARCHAR(255) = NULL,
@currency_code VARCHAR(3) = NULL,
@symbol nvarchar(20) = NULL
)

AS
BEGIN
SET NOCOUNT ON

DECLARE @currency_id INT

INSERT INTO currency
(identifier,
currency_code,
symbol)
VALUES
(@identifier,
@currency_code,
@symbol
)

SET @currency_id = SCOPE_IDENTITY()

SELECT 
	currency_id,
	symbol,
	identifier,
	currency_code
FROM currency
WHERE currency_id = @currency_id

SET NOCOUNT OFF
END






GO
