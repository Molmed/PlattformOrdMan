USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetArticleNumberById]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetArticleNumberById]
(
	@id int
)

AS
BEGIN
SET NOCOUNT ON

select * from article_number_view 
WHERE id = @id

SET NOCOUNT OFF
END






GO
