USE [master]
GO
/****** Object:  Database [hlab_web_db]    Script Date: 03/23/2020 3:15:23 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'hlab_web_db')
BEGIN
CREATE DATABASE [hlab_web_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'hlab_web_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.DFM_SQL\MSSQL\DATA\hlab_web_db.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'hlab_web_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.DFM_SQL\MSSQL\DATA\hlab_web_db_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
END
GO
ALTER DATABASE [hlab_web_db] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [hlab_web_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [hlab_web_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [hlab_web_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [hlab_web_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [hlab_web_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [hlab_web_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [hlab_web_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [hlab_web_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [hlab_web_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [hlab_web_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [hlab_web_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [hlab_web_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [hlab_web_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [hlab_web_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [hlab_web_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [hlab_web_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [hlab_web_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [hlab_web_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [hlab_web_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [hlab_web_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [hlab_web_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [hlab_web_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [hlab_web_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [hlab_web_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [hlab_web_db] SET  MULTI_USER 
GO
ALTER DATABASE [hlab_web_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [hlab_web_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [hlab_web_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [hlab_web_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [hlab_web_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [hlab_web_db] SET QUERY_STORE = OFF
GO
USE [hlab_web_db]
GO
/****** Object:  UserDefinedFunction [dbo].[GenerateCoupon]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerateCoupon]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GenerateCoupon]()
RETURNS INT
AS
BEGIN
	RETURN (SELECT ISNULL(MAX(coupon) + 1, 0) FROM hlab_test_coupon_logs)
END' 
END
GO
/****** Object:  Table [dbo].[hlab_order_logs]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_order_logs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_order_logs](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[order_date] [datetime] NULL,
	[received_by] [nvarchar](16) NULL,
	[proc_status] [bit] NULL,
	[total_amount] [decimal](7, 2) NULL,
	[record_status] [bit] NULL,
 CONSTRAINT [PK_hlab_order_logs] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_customers]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_customers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_customers](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[company_name] [nvarchar](50) NULL,
	[first_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NULL,
	[street] [nvarchar](256) NULL,
	[city_id] [int] NULL,
	[province_id] [int] NULL,
	[postal_code] [nvarchar](12) NULL,
	[fax] [nvarchar](16) NULL,
	[is_public] [bit] NULL,
	[is_semi_public] [bit] NULL,
	[is_approve_financing] [bit] NULL,
	[env_distr] [nvarchar](128) NULL,
	[dw_officer] [nvarchar](64) NULL,
	[dw_phone] [nvarchar](20) NULL,
	[com_code] [nvarchar](50) NULL,
	[msa_horizonid] [int] NULL,
	[msa_customerid] [nvarchar](12) NULL,
	[date_registered] [datetime] NULL,
	[email] [nvarchar](256) NULL,
	[location] [nvarchar](256) NULL,
	[status] [bit] NULL,
	[coupon] [int] NULL,
	[sample_number] [nvarchar](16) NULL,
 CONSTRAINT [PK_hlab_customers] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[OrderSummaryView]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OrderSummaryView]'))
EXEC dbo.sp_executesql @statement = N'





CREATE VIEW [dbo].[OrderSummaryView] AS 
SELECT 
hol.order_id,
COALESCE(hol.customer_id,0) AS customer_id,
hc.first_name,
hc.last_name,
hol.order_date,
hol.received_by,
hol.proc_status,
hol.total_amount,
COALESCE(hol.record_status, CAST(0 AS BIT)) AS record_status
FROM [dbo].[hlab_order_logs] AS hol
LEFT JOIN [dbo].[hlab_customers] AS hc ON hc.customer_id = hol.customer_id
' 
GO
/****** Object:  Table [dbo].[hlab_order_items]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_order_items]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_order_items](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[test_pkg_id] [int] NULL,
	[order_id] [int] NULL,
	[amount] [decimal](7, 2) NULL,
	[trans_id] [int] NULL,
 CONSTRAINT [PK_hlab_order_items_new] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_pkgs]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_pkgs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_pkgs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lab_code] [nvarchar](24) NULL,
	[price] [decimal](7, 2) NULL,
	[lab_fee] [decimal](5, 2) NULL,
	[analysis] [nvarchar](256) NULL,
	[sample_container] [nvarchar](256) NULL,
	[description] [nvarchar](max) NULL,
	[pkg_class_id] [int] NULL,
 CONSTRAINT [PK_hlab_test_procedures] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_transactions]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_transactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_transactions](
	[trans_id] [int] IDENTITY(1,1) NOT NULL,
	[test_pkg_id] [int] NULL,
	[price] [decimal](7, 2) NULL,
	[collect_datetime] [datetime] NULL,
	[submtd_by] [nvarchar](64) NULL,
	[submtd_datetime] [datetime] NULL,
	[rcv_by_id] [int] NULL,
	[rcv_date] [datetime] NULL,
	[test_date] [datetime] NULL,
	[is_flood_sample] [bit] NULL,
	[notes] [nvarchar](512) NULL,
	[customer_id] [int] NULL,
	[coupon] [int] NULL,
	[date_entered] [datetime] NULL,
	[work_date] [datetime] NULL,
	[transaction_type_id] [int] NULL,
	[report_type_id] [int] NULL,
	[temp] [nvarchar](16) NULL,
	[project_id] [int] NULL,
	[idnty_name] [nvarchar](96) NULL,
	[idnty_location] [nvarchar](96) NULL,
	[sample_type_id] [int] NULL,
	[sample_legal_loc] [nvarchar](120) NULL,
	[town] [nvarchar](48) NULL,
	[latitude] [nvarchar](16) NULL,
	[longitude] [nvarchar](16) NULL,
	[utm_x] [nvarchar](16) NULL,
	[utm_y] [nvarchar](16) NULL,
	[zone] [nvarchar](16) NULL,
	[msa_worknbr] [int] NULL,
	[msa_horizonid] [int] NULL,
	[rm_id] [int] NULL,
	[is_paid] [bit] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_hlab_test_transactions] PRIMARY KEY CLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[OrderDetailsView]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetailsView]'))
EXEC dbo.sp_executesql @statement = N'




CREATE VIEW [dbo].[OrderDetailsView] AS 
SELECT 
COALESCE(hoi.id,0) AS order_item_id,
COALESCE(hol.order_id,0) AS order_id,
hol.order_date,
hol.received_by,
COALESCE(hc.customer_id,0) AS customer_id,
hc.first_name,
hc.last_name,
COALESCE(htt.trans_id,0) AS trans_id,
htt.test_date,
COALESCE(htp.id ,0) AS pkg_id,
COALESCE(htp.pkg_class_id,0) AS pkg_class_id,
htp.lab_code,
htp.analysis,
htp.[description],
COALESCE(hoi.amount,0) AS amount
FROM [dbo].[hlab_order_logs] AS hol
JOIN hlab_order_items AS hoi ON hol.order_id = hoi.order_id
LEFT JOIN hlab_test_transactions AS htt ON htt.trans_id = hoi.trans_id
LEFT JOIN hlab_test_pkgs AS htp ON htp.id = hoi.test_pkg_id
LEFT JOIN hlab_customers AS hc ON hc.customer_id = hol.customer_id
' 
GO
/****** Object:  Table [dbo].[hlab_test_payments]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_payments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_payments](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[payment_type_id] [int] NULL,
	[paid_amount] [decimal](7, 2) NULL,
	[payment_date] [datetime] NULL,
 CONSTRAINT [PK_hlab_test_payments] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_payment_types]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_payment_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_payment_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[payment] [nvarchar](24) NULL,
 CONSTRAINT [PK_hlab_test_payment_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[OrderPaymentsView]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OrderPaymentsView]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[OrderPaymentsView] AS
SELECT
htpy.payment_id,
htpy.order_id,
htpy.paid_amount,
htpy.payment_date,
htpy.payment_type_id,
htpt.payment
FROM
hlab_test_payments AS htpy
JOIN hlab_test_payment_types AS htpt ON htpt.id = htpy.payment_type_id
' 
GO
/****** Object:  Table [dbo].[hlab_test_params]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_params]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_params](
	[param_id] [int] IDENTITY(1,1) NOT NULL,
	[param_name] [nvarchar](256) NULL,
 CONSTRAINT [PK_hlab_test_params] PRIMARY KEY CLUSTERED 
(
	[param_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_results]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_results]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_results](
	[result_id] [int] IDENTITY(1,1) NOT NULL,
	[param_id] [int] NULL,
	[result] [nvarchar](32) NULL,
	[unit_id] [int] NULL,
	[trans_id] [int] NULL,
	[analysis_date] [datetime] NULL,
	[is_failed] [bit] NULL,
	[msa_worknbr] [int] NULL,
	[test_note] [nvarchar](1028) NULL,
	[test_desc] [nvarchar](1028) NULL,
 CONSTRAINT [PK_hlab_test_results] PRIMARY KEY CLUSTERED 
(
	[result_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_measurement_units]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_measurement_units]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_measurement_units](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[unit_of_measurement] [nvarchar](24) NULL,
 CONSTRAINT [PK_hlab_test_measurement_units] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[TestResultsView]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[TestResultsView]'))
EXEC dbo.sp_executesql @statement = N'



CREATE VIEW [dbo].[TestResultsView] AS
SELECT 
COALESCE(htr.result_id,0) AS result_id,
COALESCE(htr.param_id,0) AS param_id,
htp.param_name,
htr.result,
COALESCE(htr.unit_id,0) AS unit_id,
htmu.unit_of_measurement,
COALESCE(htr.trans_id,0) AS trans_id,
htr.test_note,
htr.test_desc,
htr.analysis_date,
COALESCE(htr.is_failed,CAST(0 AS BIT)) AS is_failed
FROM
[dbo].[hlab_test_results] AS htr 
LEFT JOIN [dbo].[hlab_test_params] AS htp ON htr.param_id = htp.param_id
LEFT JOIN [dbo].[hlab_test_measurement_units] AS htmu ON htr.unit_id = htmu.id
' 
GO
/****** Object:  Table [dbo].[hlab_test_pkgs_class]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_pkgs_class]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_pkgs_class](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[pkg_class] [nvarchar](256) NULL,
 CONSTRAINT [PK_hlab_test_pkgs_class] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_projects]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_projects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[project] [nvarchar](32) NULL,
 CONSTRAINT [PK_hlab_test_projects] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_sample_types]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_sample_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_sample_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sample_type] [nvarchar](12) NULL,
 CONSTRAINT [PK_hlab_test_sample_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_transaction_types]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_transaction_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_transaction_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[transaction_type] [nvarchar](24) NULL,
 CONSTRAINT [PK_hlab_test_transaction_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_receivers]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_receivers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_receivers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[receiver] [nvarchar](64) NULL,
 CONSTRAINT [PK_hlab_receivers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_invoice]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_invoice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_invoice](
	[invoice_id] [int] IDENTITY(1,1) NOT NULL,
	[invoice_date] [datetime] NULL,
	[paid_by] [nvarchar](128) NULL,
	[payment_option_id] [int] NULL,
	[payment_type_id] [int] NULL,
	[trans_id] [int] NULL,
	[paid_amount] [decimal](7, 2) NULL,
	[payment_date] [datetime] NULL,
 CONSTRAINT [PK_hlab_invoice] PRIMARY KEY CLUSTERED 
(
	[invoice_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[TestTransactionsView]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[TestTransactionsView]'))
EXEC dbo.sp_executesql @statement = N'

CREATE VIEW [dbo].[TestTransactionsView] AS
SELECT 
COALESCE(htt.trans_id,0) AS trans_id,
COALESCE(hc.customer_id,0) AS customer_id,
hc.first_name,
hc.last_name,
COALESCE(htt.test_pkg_id,0) AS test_pkg_id,
htp.lab_code,
COALESCE(rcv_by_id,0) AS rcv_by_id,
receiver,
COALESCE(transaction_type_id,0) transaction_type_id,
transaction_type,
COALESCE(project_id,0) AS project_id, 
project,
COALESCE(sample_type_id,0) AS sample_type_id,
sample_type,
collect_datetime,
submtd_by,
submtd_datetime,
rcv_date,
test_date,
COALESCE(is_flood_sample, CAST(0 AS BIT)) AS is_flood_sample,
date_entered,
work_date,
COALESCE(report_type_id,0) AS report_type_id,
temp,
idnty_name,
idnty_location,
sample_legal_loc,
town,
htt.is_paid,
hi.invoice_id,
COALESCE(hi.payment_type_id,0) AS payment_type_id,
htpt.payment,
COALESCE(htt.[status], CAST(0 AS BIT)) AS [status],
COALESCE(htt.[coupon], 0) AS [coupon],
htpcl.[class_id],
htpcl.pkg_class
FROM 
[dbo].[hlab_test_transactions] AS htt
INNER JOIN [dbo].[hlab_customers] AS hc ON htt.customer_id = hc.customer_id
LEFT JOIN [dbo].[hlab_test_pkgs] AS htp ON htt.test_pkg_id = htp.id
INNER JOIN hlab_test_pkgs_class AS htpcl ON htp.pkg_class_id = htpcl.class_id
LEFT JOIN [dbo].[hlab_receivers] AS hr ON htt.rcv_by_id = hr.id
LEFT JOIN [dbo].[hlab_test_transaction_types] AS httt ON htt.transaction_type_id = httt.id
LEFT JOIN [dbo].[hlab_test_projects] AS htprj ON htt.project_id = htprj.id
LEFT JOIN [dbo].[hlab_test_sample_types] AS htst ON htt.sample_type_id = htst.id
LEFT JOIN [dbo].[hlab_invoice] AS hi ON hi.trans_id = htt.trans_id
LEFT JOIN [dbo].[hlab_test_payment_types] AS htpt ON hi.payment_type_id = htpt.id
' 
GO
/****** Object:  Table [dbo].[hlab_test_coupon_logs]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_coupon_logs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_coupon_logs](
	[coupon_log_id] [int] IDENTITY(1,1) NOT NULL,
	[coupon_issued_date] [datetime] NULL,
	[customer_id] [int] NULL,
	[coupon] [int] NULL,
 CONSTRAINT [PK_hlab_test_coupon_logs] PRIMARY KEY CLUSTERED 
(
	[coupon_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_cities]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_cities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_cities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city] [nvarchar](124) NULL,
	[province_id] [int] NULL,
 CONSTRAINT [PK_hlab_cities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_provinces]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_provinces]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_provinces](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[province] [nvarchar](150) NULL,
 CONSTRAINT [PK_hlab_provinces] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[HorizonLabCustomerView]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[HorizonLabCustomerView]'))
EXEC dbo.sp_executesql @statement = N'

CREATE VIEW [dbo].[HorizonLabCustomerView] AS 
SELECT 
hc.customer_id,
COALESCE(hc.company_name,'''') AS company_name,
COALESCE(hc.first_name,'''') AS first_name,
COALESCE(hc.last_name,'''') AS last_name,
COALESCE(hc.street,'''') AS street,
COALESCE(hc.email,'''') AS email,
COALESCE(hc.city_id,0) AS city_id,
COALESCE(hct.city,'''') AS city,
COALESCE(hc.province_id,0) AS province_id,
COALESCE(hprv.province,'''') AS province,
COALESCE(hc.postal_code,'''') AS postal_code,
COALESCE(hc.fax,'''') AS fax,
COALESCE(hc.[status], CAST(0 AS BIT)) AS [status],
COALESCE(hc.is_public, CAST(0 AS BIT)) AS is_public,
COALESCE(hc.is_semi_public, CAST(0 AS BIT)) AS is_semi_public,
COALESCE(hc.is_approve_financing,CAST(0 AS BIT)) AS is_approve_financing,
COALESCE(hc.env_distr,'''') AS env_distr,
COALESCE(hc.dw_officer,'''') AS dw_officer,
COALESCE(hc.dw_phone,'''') AS dw_phone,
COALESCE(hc.com_code,'''') AS com_code,
COALESCE(hc.[location],'''') AS [location],
COALESCE(hc.coupon,0) AS coupon,
hc.date_registered,
htcl.coupon_issued_date,
htcl.coupon_log_id,
COALESCE(hc.sample_number,'''') AS sample_number
FROM 
hlab_customers AS hc
LEFT JOIN hlab_cities AS hct ON hc.city_id = hct.id
LEFT JOIN hlab_provinces AS hprv ON hc.province_id = hprv.id
LEFT JOIN hlab_test_coupon_logs AS htcl ON htcl.customer_id = hc.customer_id AND htcl.coupon = hc.coupon
' 
GO
/****** Object:  Table [dbo].[AllCustomerResults]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllCustomerResults]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AllCustomerResults](
	[WorkNbr] [nvarchar](50) NULL,
	[Discription] [nvarchar](250) NULL,
	[Result] [nvarchar](50) NULL,
	[Price] [nvarchar](50) NULL,
	[TestType] [nvarchar](50) NULL,
	[DiscriptionNbr] [nvarchar](50) NULL,
	[Failed] [bit] NULL,
	[QCBatch] [nvarchar](50) NULL,
	[AnalysisDate] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AllCustomerWork]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AllCustomerWork]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AllCustomerWork](
	[HorizonID] [int] NULL,
	[WorkNbr] [int] NULL,
	[WorkNbr_old] [int] NULL,
	[LastTestDate] [date] NULL,
	[FloodSample] [bit] NULL,
	[PaymentType] [int] NULL,
	[Amount] [float] NULL,
	[PaidAmount] [float] NULL,
	[Paidby] [int] NULL,
	[Project] [nvarchar](50) NULL,
	[Identification_Name] [nvarchar](50) NULL,
	[Identification_Location] [nvarchar](max) NULL,
	[SampleType] [int] NULL,
	[LegalLocation] [nvarchar](max) NULL,
	[Town] [nvarchar](50) NULL,
	[RM] [nvarchar](50) NULL,
	[CollectDate] [date] NULL,
	[CollectTime] [time](7) NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL,
	[UTM_X] [int] NULL,
	[UTM_Y] [int] NULL,
	[Zone] [nvarchar](50) NULL,
	[SubmittedBy] [nvarchar](50) NULL,
	[SubmittedDate] [date] NULL,
	[SubmittedTime] [time](7) NULL,
	[ReceivedBy] [nvarchar](50) NULL,
	[ReceivedDate] [date] NULL,
	[ReceivedTime] [time](7) NULL,
	[Temperatur] [nvarchar](50) NULL,
	[ReportType] [int] NULL,
	[WorkDate] [date] NULL,
	[ReceiveDate] [date] NULL,
	[Notes] [nvarchar](max) NULL,
	[CouponID] [int] NULL,
	[CouponID_Paid] [int] NULL,
	[Transmitted] [bit] NULL,
	[TestType] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Coupons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Coupons](
	[CouponID] [int] NOT NULL,
	[WorkNbr] [int] NULL,
	[HorizonID] [int] NULL,
	[nDateActivated] [date] NULL,
	[nDateUsed] [date] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Customer_SP]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer_SP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer_SP](
	[HorizonID] [int] NULL,
	[CustomerID] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[SampleNbr] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[MailingAddress] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Province] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[HomePhone1] [nvarchar](50) NULL,
	[HomePhone2] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Fax] [nvarchar](50) NULL,
	[SemiPublic] [bit] NULL,
	[ApprForInvoicing] [bit] NULL,
	[LastTestDateX] [date] NULL,
	[FloodSampleX] [bit] NULL,
	[PaymentTypeX] [tinyint] NULL,
	[AmountX] [decimal](5, 2) NULL,
	[PaidbyX] [tinyint] NULL,
	[ProjectX] [nvarchar](50) NULL,
	[Identification_NameX] [nvarchar](50) NULL,
	[Identification_LocationX] [nvarchar](50) NULL,
	[SampleTypeX] [tinyint] NULL,
	[LegalLocationX] [nvarchar](50) NULL,
	[TownX] [nvarchar](50) NULL,
	[RMX] [nvarchar](50) NULL,
	[CollectDateX] [date] NULL,
	[CollectTimeX] [time](7) NULL,
	[LatitudeX] [nvarchar](50) NULL,
	[LongitudeX] [nvarchar](50) NULL,
	[UTM_XX] [int] NULL,
	[UTM_YX] [int] NULL,
	[ZoneX] [nvarchar](50) NULL,
	[SubmittedByX] [nvarchar](50) NULL,
	[SubmittedDateX] [date] NULL,
	[SubmittedTimeX] [time](7) NULL,
	[ReceivedByX] [nvarchar](50) NULL,
	[ReceivedDateX] [date] NULL,
	[ReceivedTimeX] [time](7) NULL,
	[Date_EnteredX] [nvarchar](1) NULL,
	[TemperaturX] [nvarchar](50) NULL,
	[NotesX] [nvarchar](50) NULL,
	[Public] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[Environment_District] [nvarchar](1) NULL,
	[DW_Officer] [nvarchar](50) NULL,
	[DW_Phone] [nvarchar](50) NULL,
	[Com_Code] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_api_keys]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_api_keys]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_api_keys](
	[id] [uniqueidentifier] NOT NULL,
	[consumer] [nvarchar](50) NULL,
 CONSTRAINT [PK_hlab_api_keys] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_customer_phone]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_customer_phone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_customer_phone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[phone] [nvarchar](20) NULL,
 CONSTRAINT [PK_hlab_customer_phone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_rural_municipalities]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_rural_municipalities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_rural_municipalities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[province_id] [int] NULL,
	[rural_municipality] [nvarchar](150) NULL,
 CONSTRAINT [PK_hlab_rural_municpality] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_service_details]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_service_details]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_service_details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[service_detail] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[service_id] [int] NULL,
 CONSTRAINT [PK_hlab_service_details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_services]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_services]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_services](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[service_name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NULL,
	[status] [bit] NULL,
	[image_file_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_hlab_services] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_default_pkg_params]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_default_pkg_params]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_default_pkg_params](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[param_id] [int] NULL,
	[pgkg_id] [int] NOT NULL,
 CONSTRAINT [PK_hlab_test_default_pkg_params] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_descriptions]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_descriptions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_descriptions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](256) NULL,
 CONSTRAINT [PK_hlab_test_descriptions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_payment_options]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_payment_options]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_payment_options](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[payment_option] [nvarchar](12) NULL,
 CONSTRAINT [PK_hlab_test_payment_option] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_pkgs_category]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_pkgs_category]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_pkgs_category](
	[pkg_id] [int] IDENTITY(1,1) NOT NULL,
	[package_ctgry] [nvarchar](256) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_hlab_test_pkgs] PRIMARY KEY CLUSTERED 
(
	[pkg_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_report_types]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_report_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_report_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[report_type] [nvarchar](24) NULL,
 CONSTRAINT [PK_hlab_test_report_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_test_result_description]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_test_result_description]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_test_result_description](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[test_result_description] [nvarchar](300) NULL,
	[msa_worknbr] [int] NULL,
 CONSTRAINT [PK_hlab_test_result_description] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_user_access]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_user_access]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_user_access](
	[access_id] [int] IDENTITY(1,1) NOT NULL,
	[access_name] [nvarchar](16) NULL,
	[access_level] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[access_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_users]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[fname] [nvarchar](32) NULL,
	[lname] [nvarchar](32) NULL,
	[email] [nvarchar](50) NULL,
	[date_reg] [datetime] NULL,
	[username] [nvarchar](16) NULL,
	[password] [nvarchar](50) NULL,
	[access_id] [int] NOT NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_web_gallery]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_web_gallery]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_web_gallery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[image_file_name] [nvarchar](max) NOT NULL,
	[upload_date] [datetime] NULL,
	[upload_by] [smallint] NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_web_news]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_web_news]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_web_news](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[section_name] [nvarchar](32) NULL,
	[section_content] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[hlab_web_services_intro]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hlab_web_services_intro]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[hlab_web_services_intro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[content] [nvarchar](max) NULL,
 CONSTRAINT [PK_hlab_web_services_intro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ManitobaCommunities]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManitobaCommunities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ManitobaCommunities](
	[CityID] [int] NOT NULL,
	[City] [nvarchar](124) NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RuralMunicpalities]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RuralMunicpalities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RuralMunicpalities](
	[Municpality] [nvarchar](150) NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[xx_hlab_test_types]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xx_hlab_test_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xx_hlab_test_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](max) NULL,
	[rate] [decimal](7, 2) NULL,
	[sp_rate] [decimal](7, 2) NULL,
	[Type] [nvarchar](12) NULL,
 CONSTRAINT [PK_hlab_test_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[xxhlab_test_coupons]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xxhlab_test_coupons]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[xxhlab_test_coupons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[trans_id] [int] NULL,
	[date_assigned] [datetime] NULL,
	[date_used] [datetime] NULL,
	[msa_couponid] [int] NULL,
 CONSTRAINT [PK_hlab_test_coupons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hlab_api_keys_id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[hlab_api_keys] ADD  CONSTRAINT [DF_hlab_api_keys_id]  DEFAULT (newid()) FOR [id]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hlab_customers_date_registered]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[hlab_customers] ADD  CONSTRAINT [DF_hlab_customers_date_registered]  DEFAULT (getdate()) FOR [date_registered]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hlab_test_coupon_logs_coupon_issued_date]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[hlab_test_coupon_logs] ADD  CONSTRAINT [DF_hlab_test_coupon_logs_coupon_issued_date]  DEFAULT (getdate()) FOR [coupon_issued_date]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_hlab_users_date_reg]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[hlab_users] ADD  CONSTRAINT [DF_hlab_users_date_reg]  DEFAULT (getdate()) FOR [date_reg]
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDefaultPackageParameters]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetDefaultPackageParameters]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetDefaultPackageParameters] AS' 
END
GO

ALTER PROCEDURE [dbo].[sp_GetDefaultPackageParameters]
@package_id INT
AS
SELECT
CAST(ROW_NUMBER() OVER(ORDER BY htp.id) AS INT) AS row_id,
htp.id AS package_id,
htp.lab_code,
htprm.param_id,
htprm.param_name
FROM
hlab_test_pkgs AS htp
INNER JOIN hlab_test_default_pkg_params AS htdp ON htp.id = htdp.pgkg_id
INNER JOIN hlab_test_params AS htprm ON htdp.param_id = htprm.param_id
WHERE
htp.id=@package_id
GO
/****** Object:  StoredProcedure [dbo].[sp_GetHorizonLabTransactionDetails]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetHorizonLabTransactionDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetHorizonLabTransactionDetails] AS' 
END
GO


-- =============================================
-- Author:		Mark Tan
-- Create date: Nov 29, 2019
-- Description:	Stored Procedure for retrieving horizon lab transaction details
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetHorizonLabTransactionDetails] 
	@trans_id INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	htt.[trans_id],
	htt.[test_pkg_id],
	htp.lab_code,
	htp.[price],
	htt.[is_paid],
	htt.[collect_datetime],
	htt.[submtd_by],
	htt.[submtd_datetime],
	htt.[rcv_by_id],
	hrec.receiver,
	htt.[rcv_date],
	htt.[test_date],
	htt.[is_flood_sample],
	htt.[notes],
	htt.[customer_id],
	htc.first_name,
	htc.last_name,
	htc.street,
	htc.city_id,
	hlc.city,
	htc.province_id,
	hlp.province,
	htc.postal_code,
	htc.is_public,
	htc.is_approve_financing,
	htc.is_semi_public,
	htt.[date_entered],
	htt.[work_date],
	htt.[transaction_type_id],
	httr.transaction_type,
	htt.[report_type_id],
	htrt.report_type,
	htt.[temp],
	htt.[project_id],
	htpr.project,
	htt.[idnty_name],
	htt.[idnty_location],
	htt.[sample_type_id],
	htst.sample_type,
	htt.[sample_legal_loc],
	htt.[town],
	htt.[rm_id],
	hrm.rural_municipality,
	htt.[latitude],
	htt.[longitude],
	htt.[utm_x],
	htt.[utm_y],
	htt.[zone],
	htpc.[class_id],
	htpc.pkg_class,
	hi.[invoice_id],
	htc.coupon AS customer_coupon,
	htcl.coupon_issued_date,
	htcl.coupon_log_id,
	htt.coupon
FROM hlab_test_transactions AS htt
	LEFT JOIN hlab_test_pkgs AS htp ON htt.test_pkg_id = htp.id
	INNER JOIN hlab_customers AS htc ON htt.customer_id = htc.customer_id
	LEFT JOIN hlab_test_transaction_types AS httr ON htt.transaction_type_id = httr.id
	LEFT JOIN hlab_test_report_types AS htrt ON htt.report_type_id = htrt.id
	LEFT JOIN hlab_test_projects AS htpr ON htt.project_id = htpr.id
	LEFT JOIN hlab_test_sample_types AS htst ON htt.sample_type_id = htst.id
	INNER JOIN hlab_test_pkgs_class AS htpc ON htp.pkg_class_id = htpc.class_id
	LEFT JOIN hlab_receivers AS hrec ON htt.rcv_by_id = hrec.id
	LEFT JOIN hlab_rural_municipalities AS hrm ON htt.rm_id = hrm.id
	LEFT JOIN hlab_cities AS hlc ON hlc.id = htc.city_id
	LEFT JOIN hlab_provinces AS hlp ON hlp.id = hlc.province_id
	LEFT JOIN [dbo].[hlab_invoice] AS hi ON hi.trans_id = htt.trans_id
	LEFT JOIN hlab_test_coupon_logs AS htcl ON htcl.customer_id = htc.customer_id AND htcl.coupon = htc.coupon
WHERE htt.trans_id = @trans_id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetHorizonLabTransactionInvoices]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetHorizonLabTransactionInvoices]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetHorizonLabTransactionInvoices] AS' 
END
GO

ALTER PROCEDURE [dbo].[sp_GetHorizonLabTransactionInvoices] 
	@trans_id INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
hi.invoice_id,
hi.invoice_date,
hi.paid_by,
hi.payment_option_id,
htpo.payment_option,
hi.payment_type_id,
hpt.payment,
hi.trans_id,
hi.paid_amount,
hi.payment_date
FROM
[dbo].[hlab_invoice] AS hi
LEFT JOIN [dbo].[hlab_test_payment_options] AS htpo ON htpo.id = hi.payment_option_id
LEFT JOIN [dbo].[hlab_test_payment_types] AS hpt ON hpt.id = hi.payment_type_id
WHERE hi.trans_id = @trans_id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTestResults]    Script Date: 03/23/2020 3:15:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetTestResults]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetTestResults] AS' 
END
GO

ALTER PROCEDURE [dbo].[sp_GetTestResults] 
	@trans_id INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
COALESCE(htr.result_id,0) AS result_id,
COALESCE(htr.param_id,0) AS param_id,
htp.param_name,
htr.result,
COALESCE(htr.unit_id,0) AS unit_id,
htmu.unit_of_measurement,
COALESCE(htr.trans_id,0) AS trans_id,
htr.test_note,
htr.test_desc,
htr.analysis_date,
COALESCE(htr.is_failed,CAST(0 AS BIT)) AS is_failed
FROM
[dbo].[hlab_test_results] AS htr 
LEFT JOIN [dbo].[hlab_test_params] AS htp ON htr.param_id = htp.param_id
LEFT JOIN [dbo].[hlab_test_measurement_units] AS htmu ON htr.unit_id = htmu.id
WHERE
htr.trans_id = @trans_id
END
GO
USE [master]
GO
ALTER DATABASE [hlab_web_db] SET  READ_WRITE 
GO
