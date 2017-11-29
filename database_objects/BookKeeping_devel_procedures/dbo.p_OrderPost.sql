USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_OrderPost]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_OrderPost](
@id INTEGER,
@authority_id_orderer INTEGER)

AS
BEGIN
SET NOCOUNT ON

UPDATE post 
SET 
	authority_id_orderer = @authority_id_orderer,
	order_date = GETDATE()
WHERE post_id = @id
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END






GO
