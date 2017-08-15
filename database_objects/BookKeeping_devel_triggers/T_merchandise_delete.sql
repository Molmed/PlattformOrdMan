USE [BookKeeping]
GO
CREATE TRIGGER [dbo].[T_merchandise_delete] ON [dbo].[merchandise]
AFTER DELETE

AS
BEGIN
SET NOCOUNT ON

INSERT INTO merchandise_history
	(merchandise_id,
	 identifier,
	 supplier_id,
	 enabled, 
	 comment,
	 amount,
	 appr_prize,
	 storage,
	 category,
	 invoice_category_id,
	 currency_id,
	 changed_action)
SELECT
	 merchandise_id,
	 identifier,
	 supplier_id,
	 enabled, 
	 comment,
	 amount,
	 appr_prize,
	 storage,
	 category,
	 invoice_category_id,
	 currency_id,
     'D'
FROM deleted
	
SET NOCOUNT OFF
END

GO
