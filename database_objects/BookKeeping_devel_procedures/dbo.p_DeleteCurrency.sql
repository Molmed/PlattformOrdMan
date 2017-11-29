USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_DeleteCurrency]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_DeleteCurrency](
@currency_id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

DELETE FROM currency WHERE currency_id = @currency_id

SET NOCOUNT OFF
END 






GO
