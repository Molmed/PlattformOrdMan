USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetInvoiceCategoryById]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetInvoiceCategoryById](
@id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

SELECT
	invoice_category_id AS id,
	identifier,
	number
FROM invoice_category
WHERE invoice_category_id = @id

SET NOCOUNT OFF
END






GO
