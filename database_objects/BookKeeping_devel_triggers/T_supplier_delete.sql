USE [BookKeeping]
GO
CREATE TRIGGER [dbo].[T_supplier_delete] ON [dbo].[supplier]
AFTER DELETE

AS
BEGIN 
SET NOCOUNT ON

INSERT INTO supplier_history
	(supplier_id,
	 identifier,
	 enabled, 
	 comment,
	 tel_nr,
	 customer_nr_inst,
	 customer_nr_clin,
	 contract_terminate,
	 short_name,
	 changed_action)
SELECT
	 supplier_id,
	 identifier,
	 enabled, 
	 comment,
	 tel_nr,
	 customer_nr_inst,
	 customer_nr_clin,
	 contract_terminate,
	 short_name,
     'D'
FROM deleted
	
SET NOCOUNT OFF
END

GO
