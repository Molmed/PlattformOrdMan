USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetMerchandiseById]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetMerchandiseById](
@id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

select * from merchandise_view
WHERE id = @id

SET NOCOUNT OFF
END






GO
