USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_ConfirmPostArrival]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_ConfirmPostArrival](
@id INTEGER,
@arrival_sign INTEGER)

AS
BEGIN
SET NOCOUNT ON

UPDATE post 
SET 
	arrival_sign = @arrival_sign,
	arrival_date = GETDATE()
WHERE post_id = @id
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END






GO
