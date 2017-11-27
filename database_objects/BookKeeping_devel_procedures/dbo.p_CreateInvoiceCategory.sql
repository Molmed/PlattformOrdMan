USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_CreateInvoiceCategory]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_CreateInvoiceCategory](
@identifier VARCHAR(255),
@number INTEGER
)

AS
BEGIN
SET NOCOUNT ON

DECLARE @invoice_category_id INTEGER

INSERT INTO invoice_category
(identifier,
number
)
VALUES
(@identifier,
@number
)

IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to create invoice category with identifier: %s', 15, 1, @identifier)
	RETURN
END

SET @invoice_category_id = NULL
SELECT @invoice_category_id = invoice_category_id FROM invoice_category WHERE identifier = @identifier
IF @invoice_category_id IS NULL
BEGIN
	RAISERROR('invoice_category_id for invoice_category was not found', 15, 1)
	RETURN
END

SELECT 
invoice_category_id as id,
identifier,
number
FROM invoice_category WHERE invoice_category_id = @invoice_category_id

SET NOCOUNT OFF
END






GO
