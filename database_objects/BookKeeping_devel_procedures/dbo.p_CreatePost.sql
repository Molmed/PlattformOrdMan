USE [BookKeeping_devel_ee]
GO
/****** Object:  StoredProcedure [dbo].[p_CreatePost]    Script Date: 7/27/2017 1:42:17 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



alter PROCEDURE [dbo].[p_CreatePost](
@article_number_id int = null,
@authority_id_booker INTEGER,
@comment VARCHAR(1024) = NULL,
@merchandise_id INTEGER,
@supplier_id INTEGER = NULL,
@amount VARCHAR(255) = NULL,
@appr_prize MONEY = NULL,
@currency_id INTEGER,
@invoice_inst BIT = NULL,
@invoice_clin BIT = NULL,
@invoice_absent BIT = NULL,
@invoice_number varchar(255) = null,
@final_prize money = null,
@delivery_deviation varchar(1024) = null,
@purchase_sales_order_no varchar(255) = null,
@place_of_purchase varchar(30),
@periodization varchar(255) = null,
@has_periodization BIT = null,
@periodization_answered bit = null,
@account varchar(255) = null,
@account_answered bit = null,
@has_account bit = null
)

AS
BEGIN
SET NOCOUNT ON

DECLARE @post_id INTEGER
declare @place_of_purchase_id int

select @place_of_purchase_id = place_of_purchase_id 
from place_of_purchase 
where code = @place_of_purchase

INSERT INTO post
	(
		article_number_id,
		comment,
		authority_id_booker,
		book_date,
		merchandise_id,
		supplier_id,
		appr_prize,
		amount,
		currency_id,
		invoice_inst,
		invoice_clin,
		invoice_absent,
		invoice_number,
		final_prize,
		delivery_deviation,
		purchase_sales_order_no,
		place_of_purchase_id,
		periodization,
		has_periodization,
		periodization_answered,
		account,
		account_answered,
		has_account
	)
VALUES
	(
		@article_number_id,
		@comment,
		@authority_id_booker,
		GETDATE(),
		@merchandise_id,
		@supplier_id,
		@appr_prize,
		@amount,
		@currency_id,
		@invoice_inst,
		@invoice_clin,
		@invoice_absent,
		@invoice_number,
		@final_prize,
		@delivery_deviation,
		@purchase_sales_order_no,
		@place_of_purchase_id,
		@periodization,
		@has_periodization,
		@periodization_answered,
		@account,
		@account_answered,
		@has_account
	)

IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to create order post', 15, 1)
	RETURN
END

select * from post_view
WHERE id = SCOPE_IDENTITY()

SET NOCOUNT OFF
END



GO
