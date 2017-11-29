USE [BookKeeping]
GO
CREATE TRIGGER [dbo].[T_article_number_delete] ON [dbo].[article_number]
AFTER DELETE

AS
BEGIN 
SET NOCOUNT ON

INSERT INTO article_number_history
	(article_number_id,
	 identifier,
	 merchandise_id,
	 active, 
	 changed_action)
SELECT
	 article_number_id,
	 identifier,
	 merchandise_id,
	 active,      
     'D'
FROM deleted
	
SET NOCOUNT OFF
END

GO
