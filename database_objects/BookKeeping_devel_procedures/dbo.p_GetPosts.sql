USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetPosts]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[p_GetPosts](
@booker_from_date DATETIME = NULL,
@time_restriction_to_completed_posts BIT
)

AS
BEGIN
SET NOCOUNT ON

select * from post_view 
WHERE book_date > ISNULL(@booker_from_date, 0) OR 
(@time_restriction_to_completed_posts = 1 AND invoice_status = 'Incoming')

SET NOCOUNT OFF
END



GO
