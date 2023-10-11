USE [master]
GO
/****** Object:  Database [BMS100]    Script Date: 8/22/2023 7:19:27 AM ******/
CREATE DATABASE [BMS100]
GO
ALTER DATABASE [BMS100] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BMS100] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BMS100] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BMS100] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BMS100] SET ARITHABORT OFF 
GO
ALTER DATABASE [BMS100] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BMS100] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BMS100] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BMS100] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BMS100] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BMS100] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BMS100] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BMS100] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BMS100] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BMS100] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BMS100] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BMS100] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BMS100] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BMS100] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BMS100] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BMS100] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BMS100] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BMS100] SET RECOVERY FULL 
GO
ALTER DATABASE [BMS100] SET  MULTI_USER 
GO
ALTER DATABASE [BMS100] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BMS100] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BMS100] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BMS100] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BMS100] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BMS100] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BMS100', N'ON'
GO
ALTER DATABASE [BMS100] SET QUERY_STORE = ON
GO
ALTER DATABASE [BMS100] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BMS100]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActorID] [int] NULL,
	[Timestamp] [datetime] NULL,
	[Action] [nvarchar](30) NULL,
	[Activity Summary] [nvarchar](100) NULL,
	[ItemsId] [nvarchar](30) NULL,
	[TableName] [nvarchar](50) NULL,
	[Activity Details] [ntext] NULL,
 CONSTRAINT [PK_Activity Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[ProductId] [int] NOT NULL,
	[Pages] [int] NULL,
	[Language] [nvarchar](50) NULL,
	[Publisher] [nvarchar](100) NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book_Author]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Author](
	[BookId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Book_Author] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[CustomerId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_CartDetail] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Phone] [nchar](10) NULL,
	[DOB] [date] NULL,
	[IsMale] [bit] NULL,
	[IsWholesale] [bit] NOT NULL,
	[Image] [nvarchar](150) NULL,
	[Status] [bit] NULL,
	[AccountId] [nvarchar](450) NULL,
	[DefaultShippingInfoId] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exchange]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exchange](
	[ExchangeId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[BaseProductId] [int] NULL,
	[QuantityBaseProduct] [int] NULL,
 CONSTRAINT [PK__Exchange__72E6008B8FB6C369] PRIMARY KEY CLUSTERED 
(
	[ExchangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [nvarchar](50) NULL,
	[ProductId] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[CustomerId] [int] NULL,
	[CustomerName] [nvarchar](100) NULL,
	[ShipAddress] [nvarchar](300) NULL,
	[Phone] [nchar](10) NULL,
	[Note] [nvarchar](max) NULL,
	[AmountPaid] [int] NULL,
	[VAT] [float] NULL,
	[StaffId] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [int] NULL,
	[Discount] [float] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderPaymentHistory]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPaymentHistory](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentAmount] [decimal](15, 0) NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[OrderId] [int] NULL,
	[StaffId] [int] NULL,
 CONSTRAINT [PK_OrderPaymentHistory] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Barcode] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[UnitInStock] [int] NULL,
	[AvailableUnit] [int] NULL,
	[PurchasePrice] [decimal](7, 0) NULL,
	[RetailPrice] [int] NULL,
	[RetailDiscount] [float] NULL,
	[WholesalePrice] [int] NULL,
	[WholesaleDiscount] [float] NULL,
	[Size] [nvarchar](50) NULL,
	[Weight] [nvarchar](50) NULL,
	[Description] [ntext] NULL,
	[Status] [bit] NULL,
	[IsBook] [bit] NULL,
	[SubCategoryId] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[PurchaseOrderId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[StaffId] [int] NULL,
	[SupplierId] [int] NULL,
	[Status] [nvarchar](30) NULL,
	[Description] [nvarchar](max) NULL,
	[AmountPaid] [decimal](15, 0) NULL,
	[VAT] [float] NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderDetail]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderDetail](
	[PurchaseOrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](7, 0) NULL,
	[Discount] [float] NULL,
 CONSTRAINT [PK_PurchaseOrderDetail_1] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchasePaymentHistory]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchasePaymentHistory](
	[Payment_ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentAmount] [decimal](15, 0) NULL,
	[Purchase_ID] [int] NULL,
	[StaffId] [int] NULL,
 CONSTRAINT [PK_PurchasePaymentHistory] PRIMARY KEY CLUSTERED 
(
	[Payment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[ReviewId] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [float] NULL,
	[Comment] [text] NULL,
	[AccountId] [int] NULL,
	[ProductId] [int] NULL,
	[Status] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[CreatorId] [int] NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShippingInfo]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingInfo](
	[ShipInfoId] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nchar](10) NOT NULL,
	[ShipAddress] [nvarchar](200) NOT NULL,
	[CustomerId] [int] NULL,
	[Status] [bit] NULL,
	[CustomerName] [nvarchar](50) NULL,
	[ProvinceCode] [int] NULL,
	[Province] [nvarchar](50) NULL,
	[DistrictCode] [int] NULL,
	[District] [nvarchar](50) NULL,
	[WardCode] [int] NULL,
	[Ward] [nvarchar](50) NULL,
 CONSTRAINT [PK_ShippingInfo] PRIMARY KEY CLUSTERED 
(
	[ShipInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](70) NOT NULL,
	[DOB] [date] NOT NULL,
	[IdCard] [nchar](12) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [nchar](10) NOT NULL,
	[Image] [nvarchar](150) NULL,
	[IsMale] [bit] NOT NULL,
	[AccountId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stationery]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stationery](
	[ProductId] [int] NOT NULL,
	[Material] [nvarchar](50) NULL,
	[Origin] [nvarchar](50) NULL,
	[Brand] [nvarchar](100) NULL,
 CONSTRAINT [PK_Other_1] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryName] [nvarchar](50) NOT NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 8/22/2023 7:19:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](150) NULL,
	[SupplierName] [nvarchar](300) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Status] [bit] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Email] [nvarchar](100) NULL,
	[ProvinceCode] [int] NULL,
	[Province] [nvarchar](50) NULL,
	[DistrictCode] [int] NULL,
	[District] [nvarchar](50) NULL,
	[WardCode] [int] NULL,
	[Ward] [nvarchar](50) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230818072052_initdb', N'7.0.7')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6c49ba70-aedd-4329-9a6e-9a72213ec70c', N'Khách hàng', N'KHÁCH HÀNG', N'17a7d0bf-e6a5-466b-a752-43471583065b')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b2adc40c-504d-4fc2-8761-74c3085d5663', N'Nhân viên', N'NHÂN VIÊN', N'815cb206-4d21-4922-b52d-69f04f6ca2fd')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bc9f3126-4f42-4dac-b5b9-0bcefa3b5abe', N'Chủ cửa hàng', N'CHỦ CỬA HÀNG', N'bdbf1c21-aff1-4843-965f-3ef49281f63c')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2f56df91-ebbc-44b3-b152-d949e464c731', N'b2adc40c-504d-4fc2-8761-74c3085d5663')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'94ef9ea8-b762-4396-a0d2-61de8eebcee2', N'b2adc40c-504d-4fc2-8761-74c3085d5663')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4538f62e-3ac3-4fa4-bdc2-a9d201b69aa8', N'bc9f3126-4f42-4dac-b5b9-0bcefa3b5abe')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Discriminator], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2f56df91-ebbc-44b3-b152-d949e464c731', N'ApplicationUser', N'staff1@gmail.com', N'STAFF1@GMAIL.COM', N'staff1@gmail.com', N'STAFF1@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEHDthWj+REk80fr0hfbCnWnautmKadvfOeCJpTrmc31g8RcHVWuWSQ1f5BTQaVP4pQ==', N'UK2C4SIV3NZMPBR7BNGP6BV2UKL3SI6V', N'b30f4fdb-9057-444c-b072-0ac6f66d5f01', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Discriminator], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4538f62e-3ac3-4fa4-bdc2-a9d201b69aa8', N'ApplicationUser', N'owner@gmail.com', N'OWNER@GMAIL.COM', N'owner@gmail.com', N'OWNER@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGgBL5+ePYwMlp2SobPlxaXEl+Wi3L8jAAJytLw7zCjsgtn81xUX9pHCBM0bQdPUtA==', N'VOEZD3HPYLD24ASMN4WCQDZNWFBU54DL', N'563670d1-5dea-4c85-9f0c-b558df72116f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Discriminator], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'94ef9ea8-b762-4396-a0d2-61de8eebcee2', N'ApplicationUser', N'staff2@gmail.com', N'STAFF2@GMAIL.COM', N'staff2@gmail.com', N'STAFF2@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGpie+qJyLjRFsNrDuK57ofXqWj5n5xJhJTMMM9q7L+FE41IW9Q6Nq6BOBA8/x3gnQ==', N'MJKZSBJ3XZ6HGKYTB6BUBKUFRV6ICZYF', N'0f44f5ca-be50-4605-8eda-ab2ff87b32b6', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Description], [Status]) VALUES (1, N'Daisuke Aizawa, Touzai', N'', 1)
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Description], [Status]) VALUES (2, N'Paulo Coelho', N'', 1)
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Description], [Status]) VALUES (3, N'David Baldacci', NULL, 1)
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Description], [Status]) VALUES (4, N'Liu Yong', N'Liu Yong', 1)
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Description], [Status]) VALUES (5, N'Adrian Kulp', N'', 1)
SET IDENTITY_INSERT [dbo].[Author] OFF
GO
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (1, 892, N'Tiếng Việt', NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (2, 364, N'Tiếng Việt', NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (3, 244, N'Tiếng Việt', NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (4, 227, N'Tiếng Việt', NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (5, 280, N'Tiếng Việt', NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (6, 96, N'Tiếng Việt', NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (7, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (8, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (9, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (11, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (12, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (13, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (14, NULL, NULL, NULL)
INSERT [dbo].[Book] ([ProductId], [Pages], [Language], [Publisher]) VALUES (15, NULL, NULL, NULL)
GO
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (1, 1)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (9, 1)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (11, 4)
INSERT [dbo].[Book_Author] ([BookId], [AuthorId]) VALUES (12, 5)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Sách')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Văn phòng phẩm')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (27, N'image_27.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (28, N'image_28.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (29, N'image_29.jpg', 2, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (30, N'image_30.jpg', 2, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (31, N'image_31.jpg', 3, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (32, N'image_32.jpg', 3, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (33, N'image_33.jpg', 3, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (34, N'image_34.jpg', 4, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (35, N'image_35.jpg', 4, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (36, N'image_36.jpg', 5, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (37, N'image_37.jpg', 5, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (38, N'image_38.jpg', 5, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (39, N'image_39.jpg', 5, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (40, N'image_40.jpg', 6, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (41, N'image_41.jpg', 6, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (42, N'image_42.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (43, N'image_43.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (44, N'image_44.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (45, N'image_45.jpg', 7, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (46, N'image_46.jpg', 7, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (47, N'image_47.jpg', 7, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (48, N'image_48.jpg', 7, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (49, N'image_49.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (50, N'image_50.jpg', 1, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (51, N'image_51.jpg', 2, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (52, N'image_52.jpg', 2, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (53, N'image_53.jpg', 2, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (54, N'image_54.jpg', 2, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (55, N'image_55.jpg', 3, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (56, N'image_56.jpg', 3, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (57, N'image_57.jpg', 5, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (58, N'image_58.jpg', 5, 0)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (59, N'image_59.jpg', 4, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (60, N'image_60.jpg', 4, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (61, N'image_61.jpg', 5, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (62, N'image_62.jpg', 5, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (63, N'image_63.jpg', 6, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (64, N'image_64.jpg', 7, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (65, N'image_65.jpg', 7, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (66, N'image_66.jpg', 1, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (67, N'image_67.jpg', 1, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (68, N'image_68.jpg', 1, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (69, N'image_69.jpg', 1, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (70, N'image_70.jpg', 10, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (71, N'image_71.jpg', 10, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (72, N'image_72.jpg', 10, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (73, N'image_73.jpg', 10, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (74, N'image_74.jpg', 10, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (79, N'image_79.jpg', 9, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (83, N'image_83.jpg', 11, 1)
INSERT [dbo].[Image] ([ImageId], [ImageName], [ProductId], [Status]) VALUES (84, N'image_84.jpg', 12, 1)
SET IDENTITY_INSERT [dbo].[Image] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (1, N'Sử Ký Tư Mã Thiên (Tái Bản 2023)', N'PVN1', N'Quyển', 54, 53, CAST(200000 AS Decimal(7, 0)), 198000, 5, 130000, 10, N'24 x 16 x 4.4 cm', N'1100g', N'"Sử Ký Tư Mã Thiên

Sử ký là bộ thông sử đầu tiên của Trung Quốc cổ đại. Bộ sử ký lưu giữ, chỉnh lí lại các tư liệu lịch sử vô cùng phong phú trong hơn ba ngàn năm từ thời Ngũ đế vốn có trước sử cho tới giữa thời Tây Hán.

- Ấn bản này với hình thức mới mẻ, toàn diện, góc nhìn mới, đa tầng, giúp bạn đọc hiểu rõ hơn nguyên tác. Cuốn sách còn có phần phụ như giải thích, dịch nghĩa bằng câu từ tinh tế giúp bạn đọc thưởng thức trọn vẹn nội dung tác phẩm."', 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (2, N'Những Cuộc Phiêu Lưu Của Tom Sawyer (Tái Bản 2023)', N'PVN2', N'Quyển', 51, 49, CAST(19982 AS Decimal(7, 0)), 67000, 5, 40000, 10, N'20.5 x 14.5 x 1.8 cm', N'450g', N'"
Những Cuộc Phiêu Lưu Của Tom Sawyer

Những cuộc phiêu lưu của Tom Sawyer (The Adven tures of Tom Sawyer, 1876) được coi là hồi ký của Mark Twain và tác giả mô tả Tom Sawyer, anh bạn Huck Finn và tên gian ác Injun Joe cũng như làng St. Petersburg nhờ các kỷ niệm sống tại Hanni bal khi trước.

Những cuộc phiêu lưu của Tom Sawyer là một trong những tác phẩm văn học đầu tiên của Mỹ, sử dụng những phương ngữ đặc trưng của vùng miền, tác giả đã tạo nên nền văn hóa riêng của thị trấn nhỏ bé này. Mark Twain đã vẽ ra những cuộc phiêu lưu của cậu bé Tom Sawyer dũng cảm tinh nghịch từ những ký ức tuổi thơ của mình ở một thị trấn ven sông Missouri vào những năm 1840, đó chính là điều khiến cuốn sách trở nên thú vị nhất. Những cuộc phiêu lưu của Tom Sawyer là một câu chuyện hài hước và xúc động về một thế giới tưởng tượng và những nỗi sợ của một cậu bé; còn là tiếng nói châm biếm gay gắt về văn hóa và xã hội lúc bấy giờ. Đây là một trong những tác phẩm của Mark Twain được nhiều độc giả yêu thích và được ca ngợi như một tác phẩm văn học cổ điển của Mỹ."', 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (3, N'Cây Cam Ngọt Của Tôi', N'PVN3', N'Quyển', 51, 51, CAST(29874 AS Decimal(7, 0)), 81000, 5, 30000, 10, N'20 x 14.5 cm', N'280', N'"“Vị chua chát của cái nghèo hòa trộn với vị ngọt ngào khi khám phá ra những điều khiến cuộc đời này đáng sống... một tác phẩm kinh điển của Brazil.” - Booklist

“Một cách nhìn cuộc sống gần như hoàn chỉnh từ con mắt trẻ thơ… có sức mạnh sưởi ấm và làm tan nát cõi lòng, dù người đọc ở lứa tuổi nào.” - The National

"', 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (4, N'Nhà Giả Kim (Tái Bản 2020)', N'PVN4', N'Quyển', 45, 45, CAST(20000 AS Decimal(7, 0)), 60000, 5, 40000, 10, N'20.5 x 13 cm', N'220g', N'"Tất cả những trải nghiệm trong chuyến phiêu du theo đuổi vận mệnh của mình đã giúp Santiago thấu hiểu được ý nghĩa sâu xa nhất của hạnh phúc, hòa hợp với vũ trụ và con người. 

Tiểu thuyết Nhà giả kim của Paulo Coelho như một câu chuyện cổ tích giản dị, nhân ái, giàu chất thơ, thấm đẫm những minh triết huyền bí của phương Đông. Trong lần xuất bản đầu tiên tại Brazil vào năm 1988, sách chỉ bán được 900 bản. Nhưng, với số phận đặc biệt của cuốn sách dành cho toàn nhân loại, vượt ra ngoài biên giới quốc gia, Nhà giả kim đã làm rung động hàng triệu tâm hồn, trở thành một trong những cuốn sách bán chạy nhất mọi thời đại, và có thể làm thay đổi cuộc đời người đọc.

“Nhưng nhà luyện kim đan không quan tâm mấy đến những điều ấy. Ông đã từng thấy nhiều người đến rồi đi, trong khi ốc đảo và sa mạc vẫn là ốc đảo và sa mạc. Ông đã thấy vua chúa và kẻ ăn xin đi qua biển cát này, cái biển cát thường xuyên thay hình đổi dạng vì gió thổi nhưng vẫn mãi mãi là biển cát mà ông đã biết từ thuở nhỏ. Tuy vậy, tự đáy lòng mình, ông không thể không cảm thấy vui trước hạnh phúc của mỗi người lữ khách, sau bao ngày chỉ có cát vàng với trời xanh nay được thấy chà là xanh tươi hiện a trước mắt. ‘Có thể Thượng đế tạo ra sa mạc chỉ để cho con người biết quý trọng cây chà là,’ ông nghĩ.”

- Trích Nhà giả kim

"', 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (5, N'Từ Điển Tiếng “Em” - Tái Bản 2021', N'PVN5', N'Quyển', 46, 45, CAST(20009 AS Decimal(7, 0)), 69000, 5, 40000, 10, N'12 x 10 cm', N'300g', N'"TỪ ĐIỂN TIẾNG “EM” – Định nghĩa về thế giới mới!

Bạn sẽ bất ngờ, khi cầm cuốn “từ điển” xinh xinh này trên tay.

Và sẽ còn ngạc nhiên hơn nữa, khi bắt đầu đọc từng trang sách…

Dĩ nhiên là vì “Từ điển tiếng “Em” không phải là một cuốn từ điển thông thường rồi!

Nói đến “từ điển”, xưa nay chúng ta đều nghĩ về một bộ sách đồ sộ, giải thích ý nghĩa, cách dùng, dịch, cách phát âm, và thường kèm theo các ví dụ về cách sử dụng từ đó.

Tuy nhiên, với cuốn sách “Từ điển tiếng “em”, các bạn sẽ hết sức bất ngờ với những định nghĩa mới, bắt trend, sáng tạo, thông minh và vô cùng hài hước.

"', 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (6, N'Tô Bình Yên Vẽ Hạnh Phúc (Tái Bản 2022)', N'PVN6', N'Quyển', 54, 53, CAST(20 AS Decimal(7, 0)), 88000, 5, 50000, 10, N'24 x 19 x 0.4 cm', N'150g', N'"Sau thành công của cuốn sách đầu tay “Phải lòng với cô đơn” chàng họa sĩ nổi tiếng và tài năng Kulzsc đã trở lại với một cuốn sách vô cùng đặc biệt mang tên: ""Tô bình yên - vẽ hạnh phúc” – sắc nét phong cách cá nhân với một chút “thơ thẩn, rất hiền”.

"', 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (7, N'How Business Works - Hiểu Hết Về Kinh Doanh', N'PVN7', N'Quyển', 3, 3, CAST(265335 AS Decimal(7, 0)), 380000, 5, 380000, 10, NULL, NULL, NULL, 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (8, N'Chiến Binh Cầu Vồng (Tái Bản 2020)', N'123', N'Quyển ', 3, 3, CAST(52075 AS Decimal(7, 0)), 0, 0, 0, 0, NULL, NULL, NULL, 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (9, N'Chúa Tể Bóng Tối - Tập 1 - Bản Giới Hạn - Tặng Kèm Character Card + Mini Clearfile', N'PVN9', N'Quyển ', 1, 0, CAST(100000 AS Decimal(7, 0)), 201600, 10, 201600, 15, NULL, NULL, NULL, 1, 1, 1)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (10, N'Túi 5 Bút Bi 0.5 mm Thiên Long TL-105 - Mực Đen', N'PVN10', N'Chiếc', 0, 0, CAST(15000 AS Decimal(7, 0)), 20000, 5, 20000, 5, NULL, NULL, NULL, 1, 0, 9)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (11, N'Kĩ Năng Vàng Cho Học Sinh Trung Học - Học Kĩ Năng Để Thành Công (Tái Bản 2023)', N'PVN11', N'Quyển ', 0, 0, CAST(36000 AS Decimal(7, 0)), 0, 0, 0, 0, NULL, NULL, NULL, 1, 1, 3)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (12, N'Lần Đầu Làm Bố', N'PVN12', N'Quyển', 0, 0, CAST(84150 AS Decimal(7, 0)), 0, 0, 0, 0, NULL, NULL, NULL, 1, 1, 4)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (13, N'Mái Ấm Gia Đình - Tập 7', N'PVN13', N'Quyển', 0, 0, CAST(33250 AS Decimal(7, 0)), 0, 0, 0, 0, NULL, NULL, NULL, 1, 1, 3)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (14, N'Spy X Family - Tập 9 - Tặng Kèm Standee PVC', N'PVN14', N'Quyển', 0, 0, CAST(23750 AS Decimal(7, 0)), 0, 0, 0, 0, NULL, NULL, NULL, 1, 1, 14)
INSERT [dbo].[Product] ([ProductId], [Name], [Barcode], [Unit], [UnitInStock], [AvailableUnit], [PurchasePrice], [RetailPrice], [RetailDiscount], [WholesalePrice], [WholesaleDiscount], [Size], [Weight], [Description], [Status], [IsBook], [SubCategoryId]) VALUES (15, N'Người Bà Tài Giỏi Vùng Saga', N'PVN15', N'Quyển', 1, 1, CAST(83328 AS Decimal(7, 0)), 0, 0, 0, 0, NULL, NULL, NULL, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([StaffId], [FullName], [DOB], [IdCard], [Address], [Phone], [Image], [IsMale], [AccountId]) VALUES (1, N'OwerFullName', CAST(N'2001-01-01' AS Date), N'123456789101', N'123 Example Street, City, Country', N'0123456789', N'\images\Staff\330352f8-a5c6-4d11-8a6d-3e2f0ca09ff0.png', 1, N'4538f62e-3ac3-4fa4-bdc2-a9d201b69aa8')
INSERT [dbo].[Staff] ([StaffId], [FullName], [DOB], [IdCard], [Address], [Phone], [Image], [IsMale], [AccountId]) VALUES (2, N'Staff1FullName', CAST(N'2002-02-02' AS Date), N'123456789101', N'123 Example Street, City, Country', N'0123456789', N'\images\Staff\330352f8-a5c6-4d11-8a6d-3e2f0ca09ff0.png', 1, N'2f56df91-ebbc-44b3-b152-d949e464c731')
INSERT [dbo].[Staff] ([StaffId], [FullName], [DOB], [IdCard], [Address], [Phone], [Image], [IsMale], [AccountId]) VALUES (3, N'Staff2FullName', CAST(N'2003-03-03' AS Date), N'123456789101', N'123 Example Street, City, Country', N'0123456789', N'\images\Staff\3798ced9-1c32-42aa-839d-5826d14e20ef.png', 1, N'94ef9ea8-b762-4396-a0d2-61de8eebcee2')
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET IDENTITY_INSERT [dbo].[SubCategory] ON 

INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (1, N'Văn học', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (2, N'Kinh Tế', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (3, N'Tâm lý - Kỹ năng sống', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (4, N'Sách nuôi dạy con', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (5, N'Sách thiếu nhi', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (6, N'Tiểu sử - Hồi ký', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (7, N'Sách giáo khoa', 1)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (9, N'Bút - Viết', 2)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (10, N'Dụng cụ học sinh', 2)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (12, N'Dụng cụ vẽ', 2)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (13, N'Sản phẩm về giấy', 2)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (14, N'Sản phẩm khác', 2)
INSERT [dbo].[SubCategory] ([SubCategoryId], [SubCategoryName], [CategoryId]) VALUES (15, N'Sản phẩm điện tử', 2)
SET IDENTITY_INSERT [dbo].[SubCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplier] ON 

INSERT [dbo].[Supplier] ([SupplierId], [Address], [SupplierName], [Phone], [Status], [Description], [Email], [ProvinceCode], [Province], [DistrictCode], [District], [WardCode], [Ward]) VALUES (1, N'Số nhà ...', N'Nhà cung cấp A', N'0985457175', 1, NULL, NULL, 1, N'Thành phố Hà Nội', 1, N'Quận Ba Đình', 1, N'Phường Phúc Xá')
INSERT [dbo].[Supplier] ([SupplierId], [Address], [SupplierName], [Phone], [Status], [Description], [Email], [ProvinceCode], [Province], [DistrictCode], [District], [WardCode], [Ward]) VALUES (2, NULL, N'Nhà cung cấp A', N'0985457175', 1, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [Address], [SupplierName], [Phone], [Status], [Description], [Email], [ProvinceCode], [Province], [DistrictCode], [District], [WardCode], [Ward]) VALUES (3, NULL, N'Nhà cung cấp C', N'0985457175', 1, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Supplier] ([SupplierId], [Address], [SupplierName], [Phone], [Status], [Description], [Email], [ProvinceCode], [Province], [DistrictCode], [District], [WardCode], [Ward]) VALUES (4, N'so nha 20', N'Tên nhà cung cấp 8', N'0123456798', 1, NULL, N'Storekeeper1@gmail.com', 1, N'Thành phố Hà Nội', 1, N'Quận Ba Đình', 1, N'Phường Phúc Xá')
SET IDENTITY_INSERT [dbo].[Supplier] OFF
GO
/****** Object:  Index [IX_ActivityLog_ActorID]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_ActivityLog_ActorID] ON [dbo].[ActivityLog]
(
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Book_Author_AuthorId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Book_Author_AuthorId] ON [dbo].[Book_Author]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartDetail_ProductId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_CartDetail_ProductId] ON [dbo].[CartDetail]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer] ON [dbo].[Customer]
(
	[AccountId] ASC
)
WHERE ([AccountId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Customer_DefaultShippingInfoId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Customer_DefaultShippingInfoId] ON [dbo].[Customer]
(
	[DefaultShippingInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Exchange_BaseProductId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Exchange_BaseProductId] ON [dbo].[Exchange]
(
	[BaseProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UC_Product_BaseProduct]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UC_Product_BaseProduct] ON [dbo].[Exchange]
(
	[ProductId] ASC,
	[BaseProductId] ASC
)
WHERE ([ProductId] IS NOT NULL AND [BaseProductId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Image_ProductId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Image_ProductId] ON [dbo].[Image]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Order_CustomerId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Order_CustomerId] ON [dbo].[Order]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Order_StaffId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Order_StaffId] ON [dbo].[Order]
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_ProductId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_ProductId] ON [dbo].[OrderDetail]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderPaymentHistory_OrderId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderPaymentHistory_OrderId] ON [dbo].[OrderPaymentHistory]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderPaymentHistory_StaffId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderPaymentHistory_StaffId] ON [dbo].[OrderPaymentHistory]
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_SubCategoryId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_SubCategoryId] ON [dbo].[Product]
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Product]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK_Product] ON [dbo].[Product]
(
	[Barcode] ASC
)
WHERE ([Barcode] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrder_StaffId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrder_StaffId] ON [dbo].[PurchaseOrder]
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrder_SupplierId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrder_SupplierId] ON [dbo].[PurchaseOrder]
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderDetail_ProductId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderDetail_ProductId] ON [dbo].[PurchaseOrderDetail]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchasePaymentHistory_Purchase_ID]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_PurchasePaymentHistory_Purchase_ID] ON [dbo].[PurchasePaymentHistory]
(
	[Purchase_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchasePaymentHistory_StaffId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_PurchasePaymentHistory_StaffId] ON [dbo].[PurchasePaymentHistory]
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Review_ProductId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_Review_ProductId] ON [dbo].[Review]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShippingInfo_CustomerId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_ShippingInfo_CustomerId] ON [dbo].[ShippingInfo]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Staff] ON [dbo].[Staff]
(
	[AccountId] ASC
)
WHERE ([AccountId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SubCategory_CategoryId]    Script Date: 8/22/2023 7:19:28 AM ******/
CREATE NONCLUSTERED INDEX [IX_SubCategory_CategoryId] ON [dbo].[SubCategory]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_ActivityLog_Staff] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Staff] ([StaffId])
GO
ALTER TABLE [dbo].[ActivityLog] CHECK CONSTRAINT [FK_ActivityLog_Staff]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Product]
GO
ALTER TABLE [dbo].[Book_Author]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[Book_Author] CHECK CONSTRAINT [FK_Book_Author_Author]
GO
ALTER TABLE [dbo].[Book_Author]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([ProductId])
GO
ALTER TABLE [dbo].[Book_Author] CHECK CONSTRAINT [FK_Book_Author_Book]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Customer]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Product]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_AspNetUsers_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_AspNetUsers_AccountId]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_ShippingInfo] FOREIGN KEY([DefaultShippingInfoId])
REFERENCES [dbo].[ShippingInfo] ([ShipInfoId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_ShippingInfo]
GO
ALTER TABLE [dbo].[Exchange]  WITH CHECK ADD  CONSTRAINT [FK_Exchange_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Exchange] CHECK CONSTRAINT [FK_Exchange_Product]
GO
ALTER TABLE [dbo].[Exchange]  WITH CHECK ADD  CONSTRAINT [FK_Exchange_Product1] FOREIGN KEY([BaseProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Exchange] CHECK CONSTRAINT [FK_Exchange_Product1]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Product]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([StaffId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Staff]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[OrderPaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_OrderPaymentHistory_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderPaymentHistory] CHECK CONSTRAINT [FK_OrderPaymentHistory_Order]
GO
ALTER TABLE [dbo].[OrderPaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_OrderPaymentHistory_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([StaffId])
GO
ALTER TABLE [dbo].[OrderPaymentHistory] CHECK CONSTRAINT [FK_OrderPaymentHistory_Staff]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_SubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[SubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_SubCategory]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([StaffId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Staff]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Supplier]
GO
ALTER TABLE [dbo].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[PurchaseOrderDetail] CHECK CONSTRAINT [FK_PurchaseOrderDetail_Product]
GO
ALTER TABLE [dbo].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId])
GO
ALTER TABLE [dbo].[PurchaseOrderDetail] CHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder]
GO
ALTER TABLE [dbo].[PurchasePaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_PurchasePaymentHistory_PurchaseOrder] FOREIGN KEY([Purchase_ID])
REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId])
GO
ALTER TABLE [dbo].[PurchasePaymentHistory] CHECK CONSTRAINT [FK_PurchasePaymentHistory_PurchaseOrder]
GO
ALTER TABLE [dbo].[PurchasePaymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_PurchasePaymentHistory_Staff] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staff] ([StaffId])
GO
ALTER TABLE [dbo].[PurchasePaymentHistory] CHECK CONSTRAINT [FK_PurchasePaymentHistory_Staff]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Product]
GO
ALTER TABLE [dbo].[ShippingInfo]  WITH CHECK ADD  CONSTRAINT [FK_ShippingInfo_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ShippingInfo] CHECK CONSTRAINT [FK_ShippingInfo_Customer]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_AspNetUsers_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_AspNetUsers_AccountId]
GO
ALTER TABLE [dbo].[Stationery]  WITH CHECK ADD  CONSTRAINT [FK_Stationery_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Stationery] CHECK CONSTRAINT [FK_Stationery_Product]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
USE [master]
GO
ALTER DATABASE [BMS100] SET  READ_WRITE 
GO
