USE [Image]
GO
/****** Object:  Table [dbo].[ActualLabel]    Script Date: 05/11/2021 3:07:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActualLabel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[actual_label_name] [nvarchar](250) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_ActualLabel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageInfo]    Script Date: 05/11/2021 3:07:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[image_id] [int] NULL,
	[image_link] [text] NULL,
	[predict_label] [nchar](10) NULL,
	[actual_label_id] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[deleted_at] [datetime] NULL,
 CONSTRAINT [PK_ImageInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ActualLabel] ON 

INSERT [dbo].[ActualLabel] ([id], [actual_label_name], [created_at], [updated_at], [deleted_at]) VALUES (1, N'T-shirt', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ActualLabel] OFF
GO
SET IDENTITY_INSERT [dbo].[ImageInfo] ON 

INSERT [dbo].[ImageInfo] ([id], [image_id], [image_link], [predict_label], [actual_label_id], [created_at], [updated_at], [deleted_at]) VALUES (1, 1, N'https://stackoverflow.com/questions/2169080/alter-a-mysql-column-to-be-auto-increment', N'dự đoán   ', 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ImageInfo] OFF
GO
