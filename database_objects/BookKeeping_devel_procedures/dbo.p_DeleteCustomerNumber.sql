USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_DeleteCustomerNumber]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[p_DeleteCustomerNumber](
@id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

DELETE FROM customer_number WHERE customer_number_id = @id

SET NOCOUNT OFF
END 


GO
