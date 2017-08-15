USE [BookKeeping]

CREATE TRIGGER [dbo].[T_article_number_insert_update] ON [dbo].[article_number]
AFTER INSERT, UPDATE

AS
BEGIN
SET NOCOUNT ON

DECLARE @action CHAR(1)

IF EXISTS(SELECT * FROM deleted) SET @action = 'U' ELSE SET @action = 'I'

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
	 @action
FROM inserted
	
SET NOCOUNT OFF
END

GO
