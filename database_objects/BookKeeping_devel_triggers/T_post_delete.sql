USE [BookKeeping]
GO
CREATE TRIGGER [dbo].[T_post_delete] ON [dbo].[post]
AFTER DELETE

AS
BEGIN 
SET NOCOUNT ON

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
	 attention_flag)
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
     'D',
	 invoice_number,
	 final_prize,
	 authority_id_confirmed_order,
	 confirmed_order_date,
	 delivery_deviation,
	 purchase_order_no,
	 sales_order_no,
	 place_of_purchase_id,
	 customer_number_id,
	 attention_flag
FROM deleted
	
SET NOCOUNT OFF
END

GO
