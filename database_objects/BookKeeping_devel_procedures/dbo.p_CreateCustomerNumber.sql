USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_CreateCustomerNumber]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[p_CreateCustomerNumber](
	@identifier VARCHAR(255),
	@description varchar(255),
	@supplier_id int,
	@place_of_purchase varchar(30),
	@enabled bit = 1
)

AS
BEGIN
SET NOCOUNT ON

declare @place_of_purchase_id int


select @place_of_purchase_id = place_of_purchase_id
from place_of_purchase 
where code = @place_of_purchase

insert into customer_number
(identifier, description, supplier_id, place_of_purchase_id, enabled)
values
(@identifier, @description, @supplier_id, @place_of_purchase_id, @enabled)

SELECT 
	customer_number_id as id,
	identifier,
	description,
	supplier_id,
	pop.code as place_of_purchase,
	enabled
FROM customer_number cn
	inner join place_of_purchase pop on pop.place_of_purchase_id = cn.place_of_purchase_id
where customer_number_id = SCOPE_IDENTITY()

SET NOCOUNT OFF
END


GO
