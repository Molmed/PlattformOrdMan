USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetInvoiceCategories]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetInvoiceCategories]

AS
BEGIN
SET NOCOUNT ON

SELECT 
invoice_category_id as id,
identifier,
number
FROM invoice_category
ORDER BY identifier ASC

SET NOCOUNT OFF
END






GO
