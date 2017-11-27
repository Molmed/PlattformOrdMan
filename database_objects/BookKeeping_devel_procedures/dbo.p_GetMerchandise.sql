USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetMerchandise]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetMerchandise]

AS
BEGIN
SET NOCOUNT ON

select * from merchandise_view 
ORDER BY ISNULL(supplier_identifier, '000') ASC, identifier ASC

SET NOCOUNT OFF
END






GO
