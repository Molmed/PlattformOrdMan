USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetSupplierById]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[p_GetSupplierById](
@id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

SELECT 
	supplier_id AS id,
	identifier,
	short_name,
	enabled,
	comment,
	tel_nr,
	contract_terminate
FROM supplier WHERE supplier_id = @id

SET NOCOUNT OFF
END


GO
