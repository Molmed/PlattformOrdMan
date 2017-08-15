USE [BookKeeping]
GO
/****** Object:  UserDefinedFunction [dbo].[fGetAuthorityIdFromSysUser]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fGetAuthorityIdFromSysUser]() RETURNS INTEGER
AS
BEGIN
	DECLARE @authority_id INTEGER 
	SET @authority_id = -1
	DECLARE @sys_user VARCHAR(255)
	
	SET @sys_user = SYSTEM_USER

	-- PICKING OUT THE ID OF THE CURRENT USER
	SELECT @authority_id = authority_id FROM authority WHERE
	identifier = @sys_user

	-- IF THE ID IS NOT FOUND, TRY WITH THE DOMAIN NAME IN FRONT OF THE USER NAME.
	IF @authority_id = -1
	BEGIN
		SELECT @authority_id = authority_id FROM authority WHERE
		identifier = 'USER\' + @sys_user
	END
	
	IF @authority_id = -1
	BEGIN
		RETURN -100
	END
		
	
	RETURN @authority_id
END







GO
