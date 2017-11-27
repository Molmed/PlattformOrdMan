USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_UpdateCurrency]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_UpdateCurrency](
@currency_id INTEGER,
@identifier VARCHAR(255) = NULL,
@currency_code VARCHAR(3) = NULL,
@symbol NVARCHAR(20) = NULL
)

AS
BEGIN
SET NOCOUNT ON

UPDATE currency SET
identifier = @identifier,
currency_code = @currency_code,
symbol = @symbol
WHERE currency_id = @currency_id

SET NOCOUNT OFF
END






GO
