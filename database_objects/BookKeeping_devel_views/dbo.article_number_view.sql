USE [BookKeeping]
GO
/****** Object:  View [dbo].[article_number_view]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[article_number_view] AS
SELECT 
	article_number_id as id,
	identifier,
	merchandise_id,
	active
FROM article_number






GO
