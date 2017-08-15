USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetUsers]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





CREATE PROCEDURE [dbo].[p_GetUsers]

AS
BEGIN
SET NOCOUNT ON

-- Get users
SELECT * 
from authority_view
ORDER BY name ASC

SET NOCOUNT OFF
END





GO
