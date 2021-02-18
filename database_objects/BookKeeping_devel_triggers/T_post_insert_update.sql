USE [BookKeeping_devel_ee]
GO
/****** Object:  Trigger [dbo].[T_post_insert_update]    Script Date: 7/27/2017 1:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Logs insert and update operations to the aliquot table.

alter TRIGGER [dbo].[T_post_insert_update] ON [dbo].[post]
AFTER INSERT, UPDATE

AS
BEGIN
SET NOCOUNT ON

DECLARE @action CHAR(1)

IF EXISTS(SELECT * FROM deleted) SET @action = 'U' ELSE SET @action = 'I'

INSERT INTO post_history
	(post_id,
	 article_number_id,
	 authority_id_booker,
	 book_date,
	 merchandise_id,
	 supplier_id,
	 appr_prize,
	 order_date,
	 authority_id_orderer,
	 predicted_arrival,
	 invoice_inst,
	 invoice_clin,
	 arrival_date,
	 arrival_sign,
	 amount,
	 invoice_status,
	 authority_id_invoicer,
	 invoice_date,
	 invoice_absent,
	 currency_id,
	 changed_action,
	 invoice_number,
	 final_prize,
	 authority_id_confirmed_order,
	 confirmed_order_date,
	 delivery_deviation,
	 purchase_order_no,
	 sales_order_no,
	 place_of_purchase_id,
	 customer_number_id,
	 attention_flag,
	 account,
	 periodization,
	 periodization_answered,
	 has_periodization,
	 account_answered,
	 has_account)
SELECT
	 post_id,
	 article_number_id,
	 authority_id_booker,
	 book_date,
	 merchandise_id,
	 supplier_id,
	 appr_prize,
	 order_date,
	 authority_id_orderer,
	 predicted_arrival,
	 invoice_inst,
	 invoice_clin,
	 arrival_date,
	 arrival_sign,
	 amount,
	 invoice_status,
	 authority_id_invoicer,
	 invoice_date,
	 invoice_absent,
	 currency_id,
     @action,
	 invoice_number,
	 final_prize,
	 authority_id_confirmed_order,
	 confirmed_order_date,
	 delivery_deviation,
	 purchase_order_no,
	 sales_order_no,
	 place_of_purchase_id,
	 customer_number_id,
	 attention_flag,
	 account,
	 periodization,
	 periodization_answered,
	 has_periodization,
	 account_answered,
	 has_account
FROM inserted
	
SET NOCOUNT OFF
END

GO
