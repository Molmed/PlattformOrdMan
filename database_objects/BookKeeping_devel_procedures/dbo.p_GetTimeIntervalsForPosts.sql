USE [BookKeeping]
GO
/****** Object:  StoredProcedure [dbo].[p_GetTimeIntervalsForPosts]    Script Date: 7/27/2017 1:42:18 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[p_GetTimeIntervalsForPosts]

AS
BEGIN
SET NOCOUNT ON

SELECT
description,
months
FROM time_intervals_for_posts
ORDER BY months ASC

SET NOCOUNT OFF
END






GO
