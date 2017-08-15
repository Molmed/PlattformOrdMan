USE [BookKeeping]
GO
/****** Object:  View [dbo].[authority_view]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[authority_view] AS
SELECT 
	a.authority_id AS id,
	a.identifier,
	a.name,
	a.account_status,
	a.user_type,
	pop.code as place_of_purchase,
	a.chiasma_barcode,
	a.comment
FROM authority a
INNER JOIN place_of_purchase pop on a.place_of_purchase_id = pop.place_of_purchase_id





GO
