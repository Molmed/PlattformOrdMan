USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetPostsByCustomerNumberId]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[p_GetPostsByCustomerNumberId](
@customer_number_id INTEGER)

AS
BEGIN
SET NOCOUNT ON

SELECT * from post_view
WHERE customer_number_id = @customer_number_id

SET NOCOUNT OFF
END



GO
