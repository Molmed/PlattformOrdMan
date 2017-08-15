USE [BookKeeping]
GO
/****** Object:  View [dbo].[merchandise_view]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[merchandise_view] AS
SELECT 
	m.merchandise_id AS id,
	m.identifier,
	m.supplier_id,
	m.enabled,
	m.comment,
	m.amount,
	m.appr_prize,
	m.storage,
	m.category,
	m.invoice_category_id,
	m.currency_id,
	an.article_number_id,
	an.identifier as article_number_identifier,
	an.merchandise_id as article_number_merchandise_id,
	an.active as article_number_active,
	s.identifier as supplier_identifier
FROM merchandise m 
LEFT OUTER JOIN article_number an ON (an.merchandise_id = m.merchandise_id AND an.active = 1)
LEFT OUTER JOIN supplier s ON (m.supplier_id = s.supplier_id)






GO
