USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_UpdateMerchandise]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_UpdateMerchandise](
@id INTEGER,
@identifier VARCHAR(255),
@comment VARCHAR(1024),
@supplier_id INTEGER = NULL,
@amount VARCHAR(255),
@appr_prize MONEY,
@storage VARCHAR(255),
@enabled BIT,
@invoice_category_id INTEGER = NULL,
@currency_id INTEGER
)

AS
BEGIN
SET NOCOUNT ON

UPDATE merchandise SET
identifier = @identifier,
supplier_id = @supplier_id,
enabled = @enabled,
comment = @comment,
amount = @amount,
appr_prize = @appr_prize,
storage = @storage,
invoice_category_id = @invoice_category_id,
currency_id = @currency_id
WHERE merchandise_id = @id

IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to create merchandise with identifier: %s', 15, 1, @identifier)
	RETURN
END

SET NOCOUNT OFF
END






GO
