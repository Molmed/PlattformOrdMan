USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetCustomerNumbersAll]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[p_GetCustomerNumbersAll]

AS
BEGIN
SET NOCOUNT ON

SELECT 
	cn.customer_number_id as id,
	cn.identifier,
	cn.description,
	cn.supplier_id,
	pop.code as place_of_purchase,
	cn.enabled
from customer_number cn
	inner join place_of_purchase pop on cn.place_of_purchase_id = pop.place_of_purchase_id

SET NOCOUNT OFF
END 


GO
