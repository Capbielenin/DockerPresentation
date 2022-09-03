namespace KebabMenuApplication.Repositories.DbQueries;

public static class DatabaseQueries
{
    public const string GetActiveMenu = 
    $@"
        SELECT  
        m.[MenuId] AS MenuId 
        ,m.[CreationDate] AS CreationDate
        FROM [dbo].[Menu] m
        JOIN [dbo].[ActiveMenu] a
        ON m.MenuId = a.MenuId
        WHERE a.IsNewest = 1;";
    
    public const string SetActiveMenu = 
    @"
        UPDATE ActiveMenu SET IsNewest = 0 WHERE IsNewest = 1;
        UPDATE ActiveMenu SET IsNewest = 1 WHERE MenuId = @id;
    ";
    public const string GetAllMenus = 
    @"
        SELECT  
            [MenuId] AS MenuId
            ,[CreationDate] AS CreationDate
        FROM [dbo].[Menu]
    ";
    public const string GetMenusById = 
        @"
        SELECT  
        [MenuId] AS MenuId
        ,[CreationDate] AS CreationDate
        FROM [dbo].[Menu]
        WHERE MenuId = @id;
    ";

    public const string GetAllMenuItems =
        @"
        SELECT [MenuItemId]
        ,[MenuId]
        ,[Name]
        ,[Price]
        FROM [MenuDB].[dbo].[MenuItem];
    ";
    
    public const string GetMenuItemsByMenuId =
        @"
        SELECT [MenuItemId]
        ,[MenuId]
        ,[Name]
        ,[Price]
        FROM [MenuDB].[dbo].[MenuItem]
        WHERE MenuId = @id;
    ";
    public const string CreateMenu = 
        @"
        INSERT INTO [dbo].[Menu]
           ([MenuId]
           ,[CreationDate])
            VALUES
           (@MenuId
           ,@CreationDate)
    ";
    public const string CreateItemsByMenuId = 
        @"
        INSERT INTO [dbo].[MenuItem]
           ([MenuItemId]
           ,[MenuId]
           ,[Name]
           ,[Price])
     VALUES
           (@MenuItemId
           ,@MenuId
           ,@Name
           ,@Price); 
    ";
    
    public const string DeleteMenu = 
    @"
         DELETE FROM dbo.Menu 
         WHERE MenuId = @id
    ";

    public const string DeleteItemsByMenuId = 
    @"
        DELETE FROM dbo.MenuItem 
        WHERE MenuId = @id
    ";

}