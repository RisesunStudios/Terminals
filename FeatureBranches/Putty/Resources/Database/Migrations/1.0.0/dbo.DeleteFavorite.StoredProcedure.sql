USE [{DATABASE_NAME}]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFavorite]    Script Date: 12/10/2012 22:16:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteFavorite]
	(
	@Id int
	)
AS
	delete from Favorites where Id = @Id
GO
