USE [BookKeeping_devel_ee]
GO
/****** Object:  StoredProcedure [dbo].[p_UpdatePost]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



alter PROCEDURE [dbo].[p_UpdatePost](
@id INTEGER,
@comment VARCHAR(1024) = NULL,
@appr_prize MONEY = NULL,
@amount VARCHAR(255) = NULL,
@invoice_clin BIT,
@invoice_inst BIT,
@predicted_arrival VARCHAR(255) = NULL,
@invoice_status VARCHAR(20),
@invoice_absent BIT,
@currency_id INTEGER,
@authority_id_booker INTEGER,
@book_date DATETIME,
@authority_id_orderer INTEGER = NULL,
@order_date DATETIME = NULL,
@arrival_sign INTEGER = NULL,
@arrival_date DATETIME = NULL,
@authority_id_invoicer INTEGER = NULL,
@invoice_date DATETIME = NULL,
@article_number_id INTEGER = null,
@supplier_id integer = null,
@invoice_number varchar(255) = null,
@final_prize money = null,
@authority_id_confirmed_order varchar(255) = null,
@confirmed_order_date datetime = null,
@delivery_deviation varchar(1024) = null,
@purchase_order_no varchar(255) = null,
@sales_order_no varchar(255) = null,
@place_of_purchase varchar(30),
@customer_number_id int = null,
@attention_flag bit = 0,
@account varchar(255) = null,
@periodization varchar(255) = null)

AS
BEGIN
SET NOCOUNT ON


DECLARE @appr_arrival_dt DATETIME

SET @appr_arrival_dt = CONVERT(DATETIME, @predicted_arrival)


UPDATE post 
SET 
	comment = @comment,
	appr_prize = @appr_prize,
	amount = @amount,
	invoice_clin = @invoice_clin,
	invoice_inst = @invoice_inst,
	predicted_arrival = @appr_arrival_dt,
	invoice_status = @invoice_status,
	invoice_absent = @invoice_absent,
	currency_id = @currency_id,
	authority_id_booker = @authority_id_booker,
	book_date = @book_date,
	authority_id_orderer = @authority_id_orderer,
	order_date = @order_date,
	arrival_sign = @arrival_sign,
	arrival_date = @arrival_date,
	authority_id_invoicer = @authority_id_invoicer,
	invoice_date = @invoice_date,
	article_number_id = @article_number_id,
	supplier_id = @supplier_id,
	invoice_number = @invoice_number,
	final_prize = @final_prize,
	authority_id_confirmed_order = @authority_id_confirmed_order,
	confirmed_order_date = @confirmed_order_date,
	delivery_deviation = @delivery_deviation,
	purchase_order_no = @purchase_order_no,
	sales_order_no = @sales_order_no,
	place_of_purchase_id = pop.place_of_purchase_id,
	customer_number_id = @customer_number_id,
	attention_flag = @attention_flag,
	account = @account,
	periodization = @periodization
FROM place_of_purchase pop 
WHERE post_id = @id AND	pop.code = @place_of_purchase
	
IF @@ERROR <> 0
BEGIN
	RAISERROR('Failed to update post with id:', 15, 1, @id)
	RETURN
END

SET NOCOUNT OFF
END



GO
