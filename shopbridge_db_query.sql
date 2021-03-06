ALTER TABLE [dbo].[T_Inventory] DROP CONSTRAINT [DF__T_Invento__isDel__1367E606]
GO
ALTER TABLE [dbo].[T_Inventory] DROP CONSTRAINT [DF__T_Invento__Updat__1273C1CD]
GO
ALTER TABLE [dbo].[T_Inventory] DROP CONSTRAINT [DF__T_Invento__Creat__117F9D94]
GO
ALTER TABLE [dbo].[T_Inventory] DROP CONSTRAINT [DF__T_Invento__isAva__108B795B]
GO
/****** Object:  Table [dbo].[T_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
DROP TABLE [dbo].[T_Inventory]
GO
/****** Object:  StoredProcedure [dbo].[proc_List_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
DROP PROCEDURE [dbo].[proc_List_Inventory]
GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
DROP PROCEDURE [dbo].[proc_InsertUpdate_Inventory]
GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
DROP PROCEDURE [dbo].[proc_Delete_Inventory]
GO
/****** Object:  Database [ShopBridgeDb]    Script Date: 6/21/2021 7:11:22 PM ******/
DROP DATABASE [ShopBridgeDb]
GO
/****** Object:  Database [ShopBridgeDb]    Script Date: 6/21/2021 7:11:22 PM ******/
CREATE DATABASE [ShopBridgeDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopBridgeDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQL2012\MSSQL\DATA\ShopBridgeDb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShopBridgeDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQL2012\MSSQL\DATA\ShopBridgeDb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShopBridgeDb] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopBridgeDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopBridgeDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ShopBridgeDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopBridgeDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopBridgeDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopBridgeDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopBridgeDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET RECOVERY FULL 
GO
ALTER DATABASE [ShopBridgeDb] SET  MULTI_USER 
GO
ALTER DATABASE [ShopBridgeDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopBridgeDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopBridgeDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopBridgeDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShopBridgeDb', N'ON'
GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Gaurav Gupta>
-- Create date: <Create Date,, 06-06-2021>
-- Description:	<Description,,Create - Update Inventory>
-- =============================================
CREATE PROCEDURE [dbo].[proc_Delete_Inventory]
    @InventoryId BIGINT ,
    @ChangesByIP VARCHAR(50)
AS
    BEGIN
        
        UPDATE  dbo.T_Inventory
        SET     isDeleted = 1 ,
                UpdatedByIP = @ChangesByIP ,
                UpdateDateTime = GETDATE()
        WHERE   InventoryId = @InventoryId
            
    END


GO
/****** Object:  StoredProcedure [dbo].[proc_InsertUpdate_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Gaurav Gupta>
-- Create date: <Create Date,, 06-06-2021>
-- Description:	<Description,,Create - Update Inventory>
-- =============================================
CREATE PROCEDURE [dbo].[proc_InsertUpdate_Inventory]
    @InventoryId BIGINT ,
    @InventoryName NVARCHAR(250) ,
    @InventoryDescription NVARCHAR(500) ,
    @InventoryPrice DECIMAL(18, 2) ,
    @isAvailable BIT ,
    @ChangesByIP VARCHAR(50)
AS
    BEGIN
        IF ( @InventoryId = 0 )
            BEGIN
                INSERT  INTO dbo.T_Inventory
                        ( InventoryName ,
                          InventoryDescription ,
                          InventoryPrice ,
                          isAvailable ,
                          CreateDateTime ,
                          UpdateDateTime ,
                          isDeleted ,
                          CreatedByIP ,
                          UpdatedByIP
	                    )
                VALUES  ( @InventoryName , -- InventoryName - nvarchar(250)
                          @InventoryDescription , -- InventoryDescription - nvarchar(500)
                          @InventoryPrice , -- InventoryPrice - decimal
                          @isAvailable , -- isAvailable - bit
                          GETDATE() , -- CreateDateTime - datetime
                          NULL , -- UpdateDateTime - datetime
                          0 , -- isDeleted - bit
                          @ChangesByIP , -- CreatedByIP - varchar(50)
                          NULL  -- UpdatedByIP - varchar(50)
	                    )

                SET @InventoryId = SCOPE_IDENTITY()

                SELECT  InventoryId ,
                        InventoryName ,
                        InventoryDescription ,
                        InventoryPrice ,
                        isAvailable ,
                        CreateDateTime ,
                        UpdateDateTime ,
                        isDeleted ,
                        CreatedByIP ,
                        UpdatedByIP ,
                        'Added' AS msg
                FROM    T_Inventory
                WHERE   InventoryId = @InventoryId
                        
            END
        ELSE
            BEGIN
                UPDATE  dbo.T_Inventory
                SET     InventoryName = @InventoryName ,
                        InventoryDescription = @InventoryDescription ,
                        InventoryPrice = @InventoryPrice ,
                        isAvailable = @isAvailable ,
                        UpdatedByIP = @ChangesByIP ,
                        UpdateDateTime = GETDATE()
                WHERE   InventoryId = @InventoryId

                SELECT  InventoryId ,
                        InventoryName ,
                        InventoryDescription ,
                        InventoryPrice ,
                        isAvailable ,
                        CreateDateTime ,
                        UpdateDateTime ,
                        isDeleted ,
                        CreatedByIP ,
                        UpdatedByIP ,
                        'Updated' AS msg
                FROM    T_Inventory
                WHERE   InventoryId = @InventoryId
            END

    END


GO
/****** Object:  StoredProcedure [dbo].[proc_List_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Gaurav Gupta>
-- Create date: <Create Date,, 06-06-2021>
-- Description:	<Description,,Create - Update Inventory>
-- =============================================
CREATE PROCEDURE [dbo].[proc_List_Inventory] @InventoryId BIGINT
AS
    BEGIN
        
        SELECT  InventoryId ,
                InventoryName ,
                InventoryDescription ,
                InventoryPrice ,
                isAvailable
        FROM    T_Inventory WITH ( NOLOCK )
        WHERE   (InventoryId = @InventoryId OR @InventoryId = 0)
                AND ISNULL(isDeleted, 0) = 0
            
    END


GO
/****** Object:  Table [dbo].[T_Inventory]    Script Date: 6/21/2021 7:11:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Inventory](
	[InventoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[InventoryName] [nvarchar](250) NOT NULL,
	[InventoryDescription] [nvarchar](500) NULL,
	[InventoryPrice] [decimal](18, 2) NULL,
	[isAvailable] [bit] NULL,
	[CreateDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[isDeleted] [bit] NULL,
	[CreatedByIP] [varchar](50) NULL,
	[UpdatedByIP] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[T_Inventory] ON 

INSERT [dbo].[T_Inventory] ([InventoryId], [InventoryName], [InventoryDescription], [InventoryPrice], [isAvailable], [CreateDateTime], [UpdateDateTime], [isDeleted], [CreatedByIP], [UpdatedByIP]) VALUES (1, N'Shoe', N'Shoe - Adidas', CAST(2000.00 AS Decimal(18, 2)), 1, CAST(0x0000AD3F00000000 AS DateTime), CAST(0x0000AD3F00000000 AS DateTime), 0, NULL, NULL)
INSERT [dbo].[T_Inventory] ([InventoryId], [InventoryName], [InventoryDescription], [InventoryPrice], [isAvailable], [CreateDateTime], [UpdateDateTime], [isDeleted], [CreatedByIP], [UpdatedByIP]) VALUES (2, N'Mobile', N'Samsung1', CAST(14999.00 AS Decimal(18, 2)), 0, CAST(0x0000AD3F00000000 AS DateTime), CAST(0x0000AD4E01286E43 AS DateTime), 0, NULL, N' 103.89.58.130')
INSERT [dbo].[T_Inventory] ([InventoryId], [InventoryName], [InventoryDescription], [InventoryPrice], [isAvailable], [CreateDateTime], [UpdateDateTime], [isDeleted], [CreatedByIP], [UpdatedByIP]) VALUES (3, N'string', N'string', CAST(0.00 AS Decimal(18, 2)), 1, CAST(0x0000AD4D01039F93 AS DateTime), CAST(0x0000AD4E0126DB9B AS DateTime), 1, N'111', N' 103.89.58.130')
INSERT [dbo].[T_Inventory] ([InventoryId], [InventoryName], [InventoryDescription], [InventoryPrice], [isAvailable], [CreateDateTime], [UpdateDateTime], [isDeleted], [CreatedByIP], [UpdatedByIP]) VALUES (4, N'Inventory Shop', N'Shop Description', CAST(1500000.00 AS Decimal(18, 2)), 1, CAST(0x0000AD4E01270202 AS DateTime), NULL, 0, N' 103.89.58.130', NULL)
SET IDENTITY_INSERT [dbo].[T_Inventory] OFF
ALTER TABLE [dbo].[T_Inventory] ADD  DEFAULT ((0)) FOR [isAvailable]
GO
ALTER TABLE [dbo].[T_Inventory] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[T_Inventory] ADD  DEFAULT (getdate()) FOR [UpdateDateTime]
GO
ALTER TABLE [dbo].[T_Inventory] ADD  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER DATABASE [ShopBridgeDb] SET  READ_WRITE 
GO
