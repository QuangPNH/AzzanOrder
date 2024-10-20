USE [master]
GO
/****** Object:  Database [OrderingAssistSystem]    Script Date: 21/10/2024 11:06:56 SA ******/
CREATE DATABASE [OrderingAssistSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrderingAssistSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OrderingAssistSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrderingAssistSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\OrderingAssistSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [OrderingAssistSystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrderingAssistSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrderingAssistSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrderingAssistSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrderingAssistSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrderingAssistSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrderingAssistSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [OrderingAssistSystem] SET  MULTI_USER 
GO
ALTER DATABASE [OrderingAssistSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrderingAssistSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrderingAssistSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrderingAssistSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OrderingAssistSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrderingAssistSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OrderingAssistSystem', N'ON'
GO
ALTER DATABASE [OrderingAssistSystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [OrderingAssistSystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OrderingAssistSystem]
GO
/****** Object:  Table [dbo].[About]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[About](
	[AboutId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[OwnerId] [int] NULL,
 CONSTRAINT [PK_About] PRIMARY KEY CLUSTERED 
(
	[AboutId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](max) NULL,
	[BankNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[Phone] [varchar](10) NULL,
	[Gmail] [nvarchar](max) NULL,
	[BirthDate] [datetime] NULL,
	[RoleId] [int] NULL,
	[HomeAddress] [nvarchar](max) NULL,
	[WorkAddress] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[ManagerId] [int] NULL,
	[OwerId] [int] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Feedbackid] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[MemberId] [int] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Feedbackid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemCategory]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCategory](
	[ItemCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ItemCategoryName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Discount] [float] NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_ItemCategory] PRIMARY KEY CLUSTERED 
(
	[ItemCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[MemberName] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[Phone] [varchar](10) NOT NULL,
	[Gmail] [nvarchar](max) NULL,
	[BirthDate] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[Point] [int] NULL,
	[Image] [nvarchar](max) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuCategory]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuCategory](
	[MenuItemId] [int] NOT NULL,
	[ItemCategoryId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsForCombo] [bit] NULL,
 CONSTRAINT [PK_MenuCategory] PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC,
	[ItemCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[MenuItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[Description] [nvarchar](max) NULL,
	[Discount] [float] NULL,
	[Image] [nvarchar](max) NULL,
	[IsAvailable] [bit] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[MemberId] [int] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NULL,
	[OrderDetailId] [int] NULL,
	[TableId] [int] NULL,
	[Cost] [float] NULL,
	[Tax] [float] NULL,
	[MemberId] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NULL,
	[MenuItemId] [int] NULL,
	[OrderId] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Owner]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owner](
	[OwnerId] [int] IDENTITY(1,1) NOT NULL,
	[OwnerName] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[Phone] [varchar](10) NULL,
	[Gmail] [nvarchar](max) NULL,
	[BirthDate] [datetime] NULL,
	[BankId] [int] NULL,
	[Image] [nvarchar](max) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[PromotionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table](
	[TableId] [int] IDENTITY(1,1) NOT NULL,
	[QR] [nvarchar](max) NULL,
	[Status] [bit] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[VoucherDetailId] [int] NOT NULL,
	[ItemCategoryId] [int] NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[VoucherDetailId] ASC,
	[ItemCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoucherDetail]    Script Date: 21/10/2024 11:06:56 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherDetail](
	[VoucherDetailId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Discount] [float] NULL,
 CONSTRAINT [PK_VoucherDetail] PRIMARY KEY CLUSTERED 
(
	[VoucherDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[About] ON 

INSERT [dbo].[About] ([AboutId], [Title], [Content], [OwnerId]) VALUES (1, N'About Us', N'AZZAN was born to fulfill the mission of restoring the original coffee culture that has been forgotten and realizing the dream of Vietnamese Chocolate.

We are doing the opposite of what is happening with Cacao and coffee in Vietnam. From choosing seedlings to taking care of the trees, harvesting, processing raw materials, roasting and packaging finished products.

We want to be involved in every step of the process to be able to control and create the best products. Our dream is to produce products from pure, quality, safe coffee and Cacao that are recognized by the community and honored in every place and country we visit.', 1)
INSERT [dbo].[About] ([AboutId], [Title], [Content], [OwnerId]) VALUES (2, N'Vision', N'Is an organization that guides the culture of enjoying and providing high quality agricultural products - recognized by the community.', 1)
INSERT [dbo].[About] ([AboutId], [Title], [Content], [OwnerId]) VALUES (3, N'Mission', N'Bringing high quality standards to Vietnamese agricultural products (coffee and cocoa)', 1)
INSERT [dbo].[About] ([AboutId], [Title], [Content], [OwnerId]) VALUES (4, N'Core Values', N'Thinking and acting based on the interests of the people.

Creative research on the art of appreciation and the science of production.

Living creatively and romantically in a scientific environment and ethical standards.', 1)
SET IDENTITY_INSERT [dbo].[About] OFF
GO
SET IDENTITY_INSERT [dbo].[Bank] ON 

INSERT [dbo].[Bank] ([BankId], [BankName], [BankNumber]) VALUES (1, N'NH So Timo', N'9021687094268')
SET IDENTITY_INSERT [dbo].[Bank] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Gender], [Phone], [Gmail], [BirthDate], [RoleId], [HomeAddress], [WorkAddress], [Image], [ManagerId], [OwerId], [IsDelete]) VALUES (1, N'Trịnh Hoàng Dung', 1, N'0967375045', N'dungthhe170357@fpt.edu.vn', CAST(N'2003-09-06T00:00:00.000' AS DateTime), 1, N'Hà Nội', N'Azzan', N'anh-1.png', NULL, 1, 0)
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Gender], [Phone], [Gmail], [BirthDate], [RoleId], [HomeAddress], [WorkAddress], [Image], [ManagerId], [OwerId], [IsDelete]) VALUES (2, N'Hoàng Minh Tuấn', 0, N'0388536414', N'vipxpert3721@gmail.com', CAST(N'2003-03-01T00:00:00.000' AS DateTime), 2, N'Hà Nội', N'Azzan', N'anh-2.png', 1, 1, 0)
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Gender], [Phone], [Gmail], [BirthDate], [RoleId], [HomeAddress], [WorkAddress], [Image], [ManagerId], [OwerId], [IsDelete]) VALUES (3, N'Ðào Nguyễn Quang Thành', 1, N'0395746221', N'daothanh1121@gmail.com', CAST(N'2003-11-21T00:00:00.000' AS DateTime), 3, N'Hà Nội', N'Azzan', N'anh-3.png', 1, 1, 0)
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Gender], [Phone], [Gmail], [BirthDate], [RoleId], [HomeAddress], [WorkAddress], [Image], [ManagerId], [OwerId], [IsDelete]) VALUES (5, N'Nguyễn Văn An', 1, N'0123412341', N'emailao@gmail.com', CAST(N'2003-11-06T00:00:00.000' AS DateTime), 1, N'Hải Phòng', N'The Coffee H', N'anh-4.png', NULL, 1, 0)
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Gender], [Phone], [Gmail], [BirthDate], [RoleId], [HomeAddress], [WorkAddress], [Image], [ManagerId], [OwerId], [IsDelete]) VALUES (6, N'Phạm Ngọc Tuấn ', 1, N'0987654321', N'emailao2@gmail.com', CAST(N'2004-01-01T00:00:00.000' AS DateTime), 2, N'Hải Phòng', N'The Coffee H ', N'anh-5.png', 5, 1, 0)
INSERT [dbo].[Employee] ([EmployeeId], [EmployeeName], [Gender], [Phone], [Gmail], [BirthDate], [RoleId], [HomeAddress], [WorkAddress], [Image], [ManagerId], [OwerId], [IsDelete]) VALUES (7, N'Đào Ngọc Huyền ', 0, N'0808080808', N'emailao3@gmail.com', CAST(N'2002-01-09T00:00:00.000' AS DateTime), 3, N'Hải Phòng', N'The Coffee H', N'anh-6.png', 5, 1, 0)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([Feedbackid], [Content], [MemberId]) VALUES (1, N'Great coffee and fast service!', 1)
INSERT [dbo].[Feedback] ([Feedbackid], [Content], [MemberId]) VALUES (2, N'Loved the atmosphere!', 2)
INSERT [dbo].[Feedback] ([Feedbackid], [Content], [MemberId]) VALUES (3, N'Great coffee and fast service!', 1)
INSERT [dbo].[Feedback] ([Feedbackid], [Content], [MemberId]) VALUES (5, N'Great coffee and fast service!', 1)
INSERT [dbo].[Feedback] ([Feedbackid], [Content], [MemberId]) VALUES (6, N'Great coffee and fast service!', 1)
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemCategory] ON 

INSERT [dbo].[ItemCategory] ([ItemCategoryId], [ItemCategoryName], [Description], [Discount], [Image]) VALUES (1, N'CAFÉ', N'COFFEE', 0, N'https://www.flaticon.com/free-icon/coffee-cup_8230211')
INSERT [dbo].[ItemCategory] ([ItemCategoryId], [ItemCategoryName], [Description], [Discount], [Image]) VALUES (2, N'TRÀ', N'TEA', 0, N'https://www.freepik.com/icon/hot-tea_4300605')
INSERT [dbo].[ItemCategory] ([ItemCategoryId], [ItemCategoryName], [Description], [Discount], [Image]) VALUES (3, N'S?A CHUA', N'YOGURT', 0, N'https://www.google.com/imgres?q=s%E1%BB%AFa%20chua%20yogurt%20icon&imgurl=https%3A%2F%2Fpng.pngtree.com%2Fpng-clipart%2F20210312%2Fourmid%2Fpngtree-yogurt-clip-art-plain-yogurt-png-image_3033785.jpg&imgrefurl=https%3A%2F%2Fvi.pngtree.com%2Ffree-png-vectors%2Fyogurt&docid=Ap701QDLEZfmFM&tbnid=cPdzadW5b2u4TM&vet=12ahUKEwjb_rbak9uIAxVnqFYBHR3-E24QM3oECDcQAA..i&w=360&h=360&hcb=2&ved=2ahUKEwjb_rbak9uIAxVnqFYBHR3-E24QM3oECDcQAA')
INSERT [dbo].[ItemCategory] ([ItemCategoryId], [ItemCategoryName], [Description], [Discount], [Image]) VALUES (4, N'SINH T? & NU?C ÉP', N'SMOOTHIE&FRUIT JUICE', 0, N'https://www.pngegg.com/vi/png-bydmc')
INSERT [dbo].[ItemCategory] ([ItemCategoryId], [ItemCategoryName], [Description], [Discount], [Image]) VALUES (5, N'ÐÁ XAY', N'ICE BLENDED', 0, N'https://vi.pngtree.com/free-png-vectors/%C4%91%C3%A1-xay')
INSERT [dbo].[ItemCategory] ([ItemCategoryId], [ItemCategoryName], [Description], [Discount], [Image]) VALUES (6, N'L?P PH?', N'TOPPING', 0, N'https://www.flaticon.com/free-icon/topping_3157439')
SET IDENTITY_INSERT [dbo].[ItemCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberId], [MemberName], [Gender], [Phone], [Gmail], [BirthDate], [Address], [Point], [Image], [IsDelete]) VALUES (1, N'Nguyễn Tuấn Cường', 1, N'0934422800', N'cuongnthe171665@fpt.edu.vn', CAST(N'2003-08-13T00:00:00.000' AS DateTime), N'Hà Nội', 1000, N'anh-5.png', 0)
INSERT [dbo].[Member] ([MemberId], [MemberName], [Gender], [Phone], [Gmail], [BirthDate], [Address], [Point], [Image], [IsDelete]) VALUES (2, N'Nguyễn Quốc Tuấn', 1, N'0523452907', N'tuannqhe160900@fpt.edu.vn', CAST(N'2000-01-25T00:00:00.000' AS DateTime), N'Hà Nội', 1500, N'anh-6.png', 0)
INSERT [dbo].[Member] ([MemberId], [MemberName], [Gender], [Phone], [Gmail], [BirthDate], [Address], [Point], [Image], [IsDelete]) VALUES (3, N'Sầm Trung Hòa An ', 1, N'0387919688', N'ansthhe176321@fpt.edu.vn', CAST(N'2003-04-23T00:00:00.000' AS DateTime), N'Thái Nguyên', 1000, N'anh-7.png', 0)
INSERT [dbo].[Member] ([MemberId], [MemberName], [Gender], [Phone], [Gmail], [BirthDate], [Address], [Point], [Image], [IsDelete]) VALUES (4, N'Bùi Quang Minh', 1, N'0399955598', N'buiquangminh.business@gmail.com', CAST(N'2003-07-31T00:00:00.000' AS DateTime), N'Quảng Ninh', 2000, N'anh-8.png', 0)
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (1, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (2, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (3, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (4, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (5, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (6, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (7, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (8, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (9, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (10, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (11, 1, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (12, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (13, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (14, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (15, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (16, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (17, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (18, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (19, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (20, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (21, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (22, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (23, 2, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (24, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (25, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (26, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (27, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (28, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (29, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (30, 3, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (31, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (32, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (33, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (34, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (35, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (36, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (37, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (38, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (39, 4, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (40, 5, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (41, 5, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (42, 5, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (43, 5, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (44, 5, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (45, 5, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (46, 6, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (47, 6, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (48, 6, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (49, 6, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (50, 6, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (51, 6, NULL, NULL, 0)
INSERT [dbo].[MenuCategory] ([MenuItemId], [ItemCategoryId], [StartDate], [EndDate], [IsForCombo]) VALUES (52, 6, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[MenuItem] ON 

INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (1, N'Café Ðen/ Café Nâu', 22, N'Black Coffee/ Milk Coffee', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (2, N'B?c S?u', 25, N'White coffee', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (3, N'B?c S?u C?t D?a', 28, N'White coffee coconut', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (4, N'Café Woody', 35, N'Woody Coffee', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (5, N'Café kem tr?ng', 35, N'Egg Custard coffee', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (6, N'Cacao Nóng/Ðá', 30, N'Hot Cacao/Ice', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (7, N'Café Espresso', 25, NULL, 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (8, N'Americano', 30, NULL, 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (9, N'Capucchino', 35, NULL, 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (10, N'Latte', 35, NULL, 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (11, N'Caramel Machitato', 40, NULL, 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (12, N'H?ng Trà S?a', 30, N'Black Milk Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (13, N'Trà S?a H?t D?', 35, N'Chestnut Milk Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (14, N'Trà S?a Chocolate', 35, N'Chocolate Milk Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (15, N'Trà S?a Hokkaido', 35, N'Hokkaido Milk Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (16, N'Trà S?a B?c Hà', 35, N'Mint Milk Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (17, N'Trà Th?ch Ðào', 25, N'Jelly Peach Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (18, N'Trà Hoa Qu?', 35, N'Fruits Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (19, N'Trà Matcha Ð?u Ð?', 35, N'Matcha Red Beans Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (20, N'Trà Dua H?u B?c Hà', 35, N'Watermelon Mint Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (21, N'Trà Ðào', 25, N'Peach Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (22, N'Trà Ðào Cam S?', 35, N'Orange Lemon Grass Peach Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (23, N'Trà Lipton', 20, N'Lipton Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (24, N'S?a Chua Ðánh Ðá', 20, N'Ice Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (25, N'S?a Chua Coffee', 25, N'Coffee Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (26, N'S?a Chua Th?ch D?a', 25, N'Jelly Coconut Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (27, N'S?a Chua Vi?t Qu?t', 25, N'Blueberry Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (28, N'S?a Chua Th?ch Ðào', 25, N'Jelly Peach Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (29, N'S?a Chua Ð?u Ð?', 30, N'Red Beans Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (30, N'S?a Chua Trân Châu', 30, N'Jelly Yogurt', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (31, N'Sinh T? Chu?i', 20, N'Banana', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (32, N'Sinh T? Chu?i Xoài', 25, N'Banana With Mango', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (33, N'Sinh T? Xoài', 25, N'Mango', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (34, N'Sinh T? Bo', 35, N'Avocado', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (35, N'Sinh T? Bo Chu?i', 35, N'Avocado With Banana', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (36, N'Nu?c Chanh Tuoi', 20, N'Lemon Juice', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (37, N'Nu?c Chanh Leo', 25, N'Passion Juice', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (38, N'Nu?c Dua H?u', 25, N'Watermelon Juice', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (39, N'Nu?c Cam', 35, N'Orange Juice', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (40, N'Trà Xanh Matcha', 35, N'Matcha Green Tea', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (41, N'Vi?t Qu?t', 30, N'Blueberry', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (42, N'Chocolate B?c Hà', 35, N'Mint Chocolate', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (43, N'Chanh Tuy?t', 30, N'Snow Lemon', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (44, N'Chanh Leo Tuy?t', 35, N'Snow Passion Fruit', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (45, N'Ð?u Xanh C?t D?a', 35, N'Coconut Green Beans', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (46, N'Th?ch D?a', 5, N'Coconut Jelly', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (47, N'Th?ch Ðào', 5, N'Peach Jelly', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (48, N'Trân Châu', 7, N'Bubble', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (49, N'Kem Tuoi', 7, N'Whipping Cream', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (50, N'Ð?u Ð?', 10, N'Red Bean', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (51, N'Hu?ng Duong', 20, N'Sunflower Seeds', 0, NULL, 1, 1)
INSERT [dbo].[MenuItem] ([MenuItemId], [ItemName], [Price], [Description], [Discount], [Image], [IsAvailable], [EmployeeId]) VALUES (52, N'H?t Bí', 20, N'Pumpkin Seeds', 0, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[MenuItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([NotificationId], [Title], [Content], [MemberId], [EmployeeId]) VALUES (1, N'Discount offer', N'Get 10% off on all coffee orders', 1, 2)
INSERT [dbo].[Notification] ([NotificationId], [Title], [Content], [MemberId], [EmployeeId]) VALUES (2, N'Happy Hour', N'50% off on all drinks from 3-5 PM', 2, 2)
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [OrderDate], [OrderDetailId], [TableId], [Cost], [Tax], [MemberId], [Status]) VALUES (1, CAST(N'2024-09-20T00:00:00.000' AS DateTime), 1, 1, 10.5, 1.05, 1, 1)
INSERT [dbo].[Order] ([OrderId], [OrderDate], [OrderDetailId], [TableId], [Cost], [Tax], [MemberId], [Status]) VALUES (2, CAST(N'2024-09-21T00:00:00.000' AS DateTime), 2, 2, 8, 0.8, 2, 0)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([OrderDetailId], [Quantity], [MenuItemId], [OrderId]) VALUES (1, 2, 1, 1)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [Quantity], [MenuItemId], [OrderId]) VALUES (2, 1, 2, 2)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Owner] ON 

INSERT [dbo].[Owner] ([OwnerId], [OwnerName], [Gender], [Phone], [Gmail], [BirthDate], [BankId], [Image], [IsDelete]) VALUES (1, N'Nguyễn Quang Minh', 1, N'0866187621', N'nguyenminh7902@gmail.com', CAST(N'2002-09-07T00:00:00.000' AS DateTime), 1, N'anh-4.png', 0)
INSERT [dbo].[Owner] ([OwnerId], [OwnerName], [Gender], [Phone], [Gmail], [BirthDate], [BankId], [Image], [IsDelete]) VALUES (4, N'Nguyễn Văn An', 1, N'0912345678', N' annv22@fe.edu.vn

', CAST(N'1988-11-10T00:00:00.000' AS DateTime), 1, N'anh-5.png', 0)
INSERT [dbo].[Owner] ([OwnerId], [OwnerName], [Gender], [Phone], [Gmail], [BirthDate], [BankId], [Image], [IsDelete]) VALUES (5, N'Nguyễn Hải Dương', 1, N'0988787778', N'emailao4@gmail.com', CAST(N'1994-10-10T00:00:00.000' AS DateTime), NULL, N'anh-6', 0)
SET IDENTITY_INSERT [dbo].[Owner] OFF
GO
SET IDENTITY_INSERT [dbo].[Promotion] ON 

INSERT [dbo].[Promotion] ([PromotionId], [Title], [Description], [Image], [EmployeeId]) VALUES (1, N'Sale 20% cho các mặt hàng kem', N'Tưng bừng chào hè nóng lực, còn gì tuyệt hơn một cốc kem mát lạnh. Sự kiện kéo dài từ 1/7 đến hết 1/9 đừng bỏ lỡ cơ hội này.', N'anh-sale-he.png', 1)
INSERT [dbo].[Promotion] ([PromotionId], [Title], [Description], [Image], [EmployeeId]) VALUES (2, N'Sale 20% cho các loại đồ uống nóng', N'Mùa đông lạnh già thì sao chứ, hãy để Azzan làm ấp trái tim bạn. Từ ngày 1/11 - 30/12 bạn sẽ nhận được ưu đãi khi mua các đồ uống nóng từ cửa hàng. Nhanh chân ngay nào.', N'anh-sale-dong.png', 1)
SET IDENTITY_INSERT [dbo].[Promotion] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Magager')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'Bartender')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Table] ON 

INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (1, N'QR_000', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (2, N'QR_001', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (3, N'QR_002', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (4, N'QR_003', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (5, N'QR_004', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (6, N'QR_005', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (7, N'QR_006', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (8, N'QR_007', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (9, N'QR_008', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (10, N'QR_009', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (11, N'QR_010', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (12, N'QR_011', 1, 1)
INSERT [dbo].[Table] ([TableId], [QR], [Status], [EmployeeId]) VALUES (13, N'QR_012', 1, 1)
SET IDENTITY_INSERT [dbo].[Table] OFF
GO
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (1, 1, 0)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (1, 2, 0)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (1, 3, 0)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (1, 4, 0)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (1, 5, 0)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (2, 1, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (2, 2, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (2, 3, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (2, 4, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (2, 5, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (3, 1, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (3, 2, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (3, 3, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (3, 4, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (3, 5, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (4, 1, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (4, 2, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (4, 3, 1)
INSERT [dbo].[Voucher] ([VoucherDetailId], [ItemCategoryId], [IsActive]) VALUES (4, 4, 0)
GO
SET IDENTITY_INSERT [dbo].[VoucherDetail] ON 

INSERT [dbo].[VoucherDetail] ([VoucherDetailId], [Title], [StartDate], [EndDate], [Discount]) VALUES (1, N'Summer "Cool"', CAST(N'2024-06-01T02:19:30.393' AS DateTime), CAST(N'2024-09-01T02:19:30.393' AS DateTime), 10)
INSERT [dbo].[VoucherDetail] ([VoucherDetailId], [Title], [StartDate], [EndDate], [Discount]) VALUES (2, N'Black Friday', CAST(N'2024-11-24T00:00:00.000' AS DateTime), CAST(N'2024-11-30T00:00:00.000' AS DateTime), 25)
INSERT [dbo].[VoucherDetail] ([VoucherDetailId], [Title], [StartDate], [EndDate], [Discount]) VALUES (3, N'Christmas Special', CAST(N'2024-12-20T00:00:00.000' AS DateTime), CAST(N'2024-12-31T00:00:00.000' AS DateTime), 20)
INSERT [dbo].[VoucherDetail] ([VoucherDetailId], [Title], [StartDate], [EndDate], [Discount]) VALUES (4, N'New Year Discount', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-10T00:00:00.000' AS DateTime), 10)
SET IDENTITY_INSERT [dbo].[VoucherDetail] OFF
GO
ALTER TABLE [dbo].[About]  WITH CHECK ADD  CONSTRAINT [FK_About_Owner] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Owner] ([OwnerId])
GO
ALTER TABLE [dbo].[About] CHECK CONSTRAINT [FK_About_Owner]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Owner] FOREIGN KEY([OwerId])
REFERENCES [dbo].[Owner] ([OwnerId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Owner]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Role]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Member]
GO
ALTER TABLE [dbo].[MenuCategory]  WITH CHECK ADD  CONSTRAINT [FK_MenuCategory_ItemCategory] FOREIGN KEY([ItemCategoryId])
REFERENCES [dbo].[ItemCategory] ([ItemCategoryId])
GO
ALTER TABLE [dbo].[MenuCategory] CHECK CONSTRAINT [FK_MenuCategory_ItemCategory]
GO
ALTER TABLE [dbo].[MenuCategory]  WITH CHECK ADD  CONSTRAINT [FK_MenuCategory_MenuItem] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[MenuItem] ([MenuItemId])
GO
ALTER TABLE [dbo].[MenuCategory] CHECK CONSTRAINT [FK_MenuCategory_MenuItem]
GO
ALTER TABLE [dbo].[MenuItem]  WITH CHECK ADD  CONSTRAINT [FK_MenuItem_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[MenuItem] CHECK CONSTRAINT [FK_MenuItem_Employee]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Employee]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Member]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Member]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Table] FOREIGN KEY([TableId])
REFERENCES [dbo].[Table] ([TableId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Table]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_MenuItem] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[MenuItem] ([MenuItemId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_MenuItem]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[Owner]  WITH CHECK ADD  CONSTRAINT [FK_Owner_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([BankId])
GO
ALTER TABLE [dbo].[Owner] CHECK CONSTRAINT [FK_Owner_Bank]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [FK_Promotion_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [FK_Promotion_Employee]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_ItemCategory] FOREIGN KEY([ItemCategoryId])
REFERENCES [dbo].[ItemCategory] ([ItemCategoryId])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_ItemCategory]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_VoucherDetail] FOREIGN KEY([VoucherDetailId])
REFERENCES [dbo].[VoucherDetail] ([VoucherDetailId])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_VoucherDetail]
GO
USE [master]
GO
ALTER DATABASE [OrderingAssistSystem] SET  READ_WRITE 
GO
