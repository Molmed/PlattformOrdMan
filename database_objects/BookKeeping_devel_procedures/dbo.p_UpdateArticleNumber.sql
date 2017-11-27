USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_UpdateArticleNumber]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_UpdateArticleNumber](
@id INTEGER,
@identifier VARCHAR(255) = null,
@merchandise_id integer = null,
@active bit
)

AS
BEGIN
SET NOCOUNT ON

if @active = 1 and not isnull(@merchandise_id, 0) = 0
begin
	update article_number set
	active = 0
	where merchandise_id = @merchandise_id
end

UPDATE article_number SET
identifier = @identifier,
merchandise_id = @merchandise_id,
active = @active
WHERE article_number_id = @id

IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update article number with id: %s', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END






GO
