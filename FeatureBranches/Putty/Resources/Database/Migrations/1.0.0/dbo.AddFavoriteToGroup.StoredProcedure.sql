USE [{DATABASE_NAME}]
GO
/****** Object:  StoredProcedure [dbo].[AddFavoriteToGroup]    Script Date: 12/10/2012 22:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddFavoriteToGroup]
	(
	@favoriteId int,
	@groupId int
	)

AS
GO
