USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetUserCurrent]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





CREATE PROCEDURE [dbo].[p_GetUserCurrent]

AS
BEGIN
SET NOCOUNT ON

-- Get current user
SELECT * 
from authority_view
WHERE id = dbo.fGetAuthorityId()

SET NOCOUNT OFF
END





GO
