USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_SignPostInvoice]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[p_SignPostInvoice](
@id INTEGER,
@authority_id_invoicer INTEGER,
@invoice_status VARCHAR(20),
@invoice_absent BIT
)

AS
BEGIN
SET NOCOUNT ON

UPDATE post 
SET 
	authority_id_invoicer = @authority_id_invoicer,
	invoice_date = GETDATE(),
	invoice_status = @invoice_status,
	invoice_absent = @invoice_absent
WHERE post_id = @id
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END



GO
