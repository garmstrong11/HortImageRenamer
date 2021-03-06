IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'HortProd')
DROP DATABASE [HortProd]

CREATE DATABASE [HortProd]
GO

/****** Object:  Table [dbo].[tblPlantFieldUsage]    Script Date: 11/26/2015 9:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPlantFieldUsage](
	[PlantID] [int] NOT NULL,
	[CustomFieldID] [int] NOT NULL,
	[FieldValue] [nvarchar](1000) NULL,
	[ProductID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblPlantLibrary]    Script Date: 11/26/2015 9:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPlantLibrary](
	[PlantLibraryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PlantCodeFieldID] [int] NULL,
	[BotanicalNameFieldID] [int] NULL,
	[CommonNameFieldID] [int] NULL,
	[PhotoFieldID] [int] NULL,
	[PhotoXFieldID] [int] NULL,
	[PhotoYFieldID] [int] NULL,
	[PhotoPercentFieldID] [int] NULL,
	[StringingFieldID] [int] NULL,
	[InsetFieldID] [int] NULL,
	[InsetXFieldID] [int] NULL,
	[InsetYFieldID] [int] NULL,
	[InsetPercentFieldID] [int] NULL,
	[PermissionFieldID] [int] NULL,
	[Inset2FieldID] [int] NULL,
	[Inset2XFieldID] [int] NULL,
	[Inset2YFieldID] [int] NULL,
	[Inset2PercentFieldID] [int] NULL,
	[Inset3FieldID] [int] NULL,
	[Inset3XFieldID] [int] NULL,
	[Inset3YFieldID] [int] NULL,
	[Inset3PercentFieldID] [int] NULL,
	[Inset4FieldID] [int] NULL,
	[Inset4XFieldID] [int] NULL,
	[Inset4YFieldID] [int] NULL,
	[Inset4PercentFieldID] [int] NULL,
	[PCCounter] [int] NULL,
	[Inventory] [bit] NOT NULL,
	[InventoryUsesPlantCode] [bit] NOT NULL,
	[CSRID] [nvarchar](10) NULL,
	[SalesmanID] [nvarchar](15) NULL,
	[Comments] [bit] NULL,
	[StringLineItem] [nvarchar](50) NULL,
	[Digital] [bit] NULL,
	[CustStockOrderingFieldID] [int] NULL,
 CONSTRAINT [PK_tblPlantLibrary] PRIMARY KEY CLUSTERED 
(
	[PlantLibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblPlantPhotos]    Script Date: 11/26/2015 9:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPlantPhotos](
	[PhotoID] [varchar](50) NOT NULL,
	[Path] [varchar](255) NULL,
	[PixelHeight] [int] NULL,
	[PixelWidth] [int] NULL,
	[PixelsPerInch] [int] NULL,
	[RequiresPermission] [bit] NULL,
	[UpdatedAt] [datetime] NULL,
	[Ignore] [bit] NOT NULL,
 CONSTRAINT [PK_tblPlantPhotos] PRIMARY KEY CLUSTERED 
(
	[PhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblPlantLibrary] ADD  CONSTRAINT [DF_tblPlantLibrary_Inventory]  DEFAULT (0) FOR [Inventory]
GO
ALTER TABLE [dbo].[tblPlantLibrary] ADD  CONSTRAINT [DF_tblPlantLibrary_InventoryUsesPlantCode]  DEFAULT (0) FOR [InventoryUsesPlantCode]
GO
ALTER TABLE [dbo].[tblPlantLibrary] ADD  CONSTRAINT [DF_tblPlantLibrary_Digital]  DEFAULT (0) FOR [Digital]
GO
ALTER TABLE [dbo].[tblPlantPhotos] ADD  CONSTRAINT [DF_tblPlantPhotos_RequiresPermission]  DEFAULT (0) FOR [RequiresPermission]
GO
ALTER TABLE [dbo].[tblPlantPhotos] ADD  CONSTRAINT [DF_tblPlantPhotos_Ignore]  DEFAULT (0) FOR [Ignore]
GO
-- ALTER TABLE [dbo].[tblPlantLibrary]  WITH CHECK ADD  CONSTRAINT [FK_tblPlantLibrary_tblCustomField] FOREIGN KEY([PlantCodeFieldID])
-- REFERENCES [dbo].[tblCustomField] ([CustomFieldID])
-- GO
-- ALTER TABLE [dbo].[tblPlantLibrary] CHECK CONSTRAINT [FK_tblPlantLibrary_tblCustomField]
-- GO
