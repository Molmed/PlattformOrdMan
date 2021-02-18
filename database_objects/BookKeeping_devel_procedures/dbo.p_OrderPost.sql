USE [BookKeeping_devel_ee]
GO
/****** Object:  StoredProcedure [dbo].[p_OrderPost]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






alter PROCEDURE [dbo].[p_OrderPost](
@id INTEGER,
@authority_id_orderer INTEGER,
@account varchar(255),
@has_account bit,
@account_answered bit,
@periodization varchar(255),
@has_periodization bit,
@periodization_answered bit)

AS
BEGIN
SET NOCOUNT ON

UPDATE post 
SET 
	authority_id_orderer = @authority_id_orderer,
	order_date = GETDATE(),
	account = @account,
	has_account = @has_account,
	account_answered = @account_answered,
	periodization = @periodization,
	has_periodization = @has_periodization,
	periodization_answered = @periodization_answered

WHERE post_id = @id
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END






GO
