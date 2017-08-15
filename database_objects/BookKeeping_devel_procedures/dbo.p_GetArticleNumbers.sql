USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetArticleNumbers]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetArticleNumbers]

AS
BEGIN
SET NOCOUNT ON

SELECT * FROM article_number_view

SET NOCOUNT OFF
END






GO
