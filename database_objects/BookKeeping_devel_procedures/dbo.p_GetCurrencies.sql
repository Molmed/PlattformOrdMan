USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetCurrencies]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetCurrencies]

AS
BEGIN
SET NOCOUNT ON

SELECT
	currency_id,
	symbol,
	identifier,
	currency_code
FROM currency

SET NOCOUNT OFF
END






GO
