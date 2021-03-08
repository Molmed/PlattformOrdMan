USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_UpdatePostSalesOrderNumber]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[p_UpdatePostSalesOrderNumber](
@id INTEGER,
@purchase_sales_order_no varchar(255)
)

AS
BEGIN
SET NOCOUNT ON

UPDATE post 
SET 
	purchase_sales_order_no = @purchase_sales_order_no
WHERE post_id = @id
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END


GO
