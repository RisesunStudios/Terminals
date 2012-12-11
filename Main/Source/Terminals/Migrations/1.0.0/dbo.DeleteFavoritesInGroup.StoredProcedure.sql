USE [{DATABASE_NAME}]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFavoritesInGroup]    Script Date: 12/10/2012 22:16:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DeleteFavoritesInGroup]
	(
	@FavoriteId int,
  @GroupId int
	)
AS
	delete from FavoritesInGroup 
  where
  FavoriteId = @FavoriteId and GroupId = @GroupId
GO
