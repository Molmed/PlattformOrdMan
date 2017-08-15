USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_RegretOrderPost]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_RegretOrderPost](
@id INTEGER)

AS
BEGIN
SET NOCOUNT ON

UPDATE post 
SET 
	authority_id_orderer = NULL,
	order_date = NULL,
	arrival_date = NULL,
	arrival_sign = NULL,
	invoice_status = 'Incoming',
	authority_id_invoicer = NULL,
	invoice_date = NULL
WHERE post_id = @id
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END






GO
