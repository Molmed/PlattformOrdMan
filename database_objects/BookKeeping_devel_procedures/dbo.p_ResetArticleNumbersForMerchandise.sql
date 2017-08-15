USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_ResetArticleNumbersForMerchandise]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_ResetArticleNumbersForMerchandise](
@merchandise_id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

update article_number set
active = 0
where merchandise_id = @merchandise_id

IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update article number for merchandise with id: %s', 15, 1, @merchandise_id)
	RETURN
END

SET NOCOUNT OFF
END






GO
