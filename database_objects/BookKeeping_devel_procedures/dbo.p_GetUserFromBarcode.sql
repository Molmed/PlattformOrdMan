USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetUserFromBarcode]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





CREATE PROCEDURE [dbo].[p_GetUserFromBarcode](
@chiasma_barcode varchar(255)
)

AS
BEGIN
SET NOCOUNT ON

-- Get current user
SELECT *
from authority_view
WHERE chiasma_barcode = @chiasma_barcode

SET NOCOUNT OFF
END





GO
