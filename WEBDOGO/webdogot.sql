USE [SQLWEB2]
GO
/****** Object:  Table [dbo].[ADMIN]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMIN](
	[MAADMIN] [int] IDENTITY(1,1) NOT NULL,
	[TENQUANLY] [nvarchar](50) NULL,
	[TENDANGNHAP] [nvarchar](50) NULL,
	[MATKHAU] [nvarchar](max) NULL,
	[BIKHOA] [bit] NOT NULL,
	[SOLANDANGNHAPSAI] [int] NOT NULL,
	[THOIGIANDANGNHAPSAICUOICUNG] [datetime] NULL,
	[SessionToken] [nvarchar](200) NULL,
 CONSTRAINT [PK__ADMIN__7762F1E31E99EAB1] PRIMARY KEY CLUSTERED 
(
	[MAADMIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETHOADON]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETHOADON](
	[MACHITIETHOADON] [int] IDENTITY(1,1) NOT NULL,
	[MAHOADON] [int] NULL,
	[MASANPHAM] [int] NULL,
	[TONGTIEN] [float] NULL,
	[SOLUONG] [int] NULL,
	[MATIENDOSANXUAT] [int] NULL,
 CONSTRAINT [PK__CHITIETH__5E6E1E82C766DCE9] PRIMARY KEY CLUSTERED 
(
	[MACHITIETHOADON] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DOANHTHU]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOANHTHU](
	[MADOANHTHU] [int] IDENTITY(1,1) NOT NULL,
	[SOLUONG] [int] NULL,
	[GIADABAN] [float] NULL,
	[NGAYTAO] [datetime] NULL,
	[MACHITIETHOADON] [int] NULL,
	[MASANPHAMTHEOYEUCAU] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MADOANHTHU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIOHANG]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIOHANG](
	[MAGIOHANG] [int] IDENTITY(1,1) NOT NULL,
	[MAKHACHHANG] [int] NULL,
	[MASANPHAM] [int] NULL,
	[SOLUONG] [int] NULL,
	[TONGTIEN] [float] NULL,
	[NGAYTHEM] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MAGIOHANG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MAHOADON] [int] IDENTITY(1,1) NOT NULL,
	[MAKHACHHANG] [int] NULL,
	[TONGTIEN] [float] NULL,
	[TRANGTHAITHANHTOAN] [nvarchar](50) NULL,
	[PHUONGTHUCTHANHTOAN] [nvarchar](50) NULL,
	[MANHANVIENTAICHINH] [int] NULL,
	[NGAYLAP] [datetime] NULL,
	[SOHOADON] [nvarchar](5) NULL,
	[TRANGTHAIHUYDON] [bit] NULL,
 CONSTRAINT [PK__HOADON__A4999DF5BF02FFC4] PRIMARY KEY CLUSTERED 
(
	[MAHOADON] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MAKHACHHANG] [int] IDENTITY(1,1) NOT NULL,
	[TENDANGNHAP] [nvarchar](50) NULL,
	[MATKHAU] [nvarchar](max) NULL,
	[HOVATEN] [nvarchar](50) NULL,
	[TUOI] [int] NULL,
	[SDT] [bigint] NULL,
	[EMAIL] [nvarchar](50) NULL,
	[CAPDO] [nvarchar](10) NULL,
	[TINH] [nvarchar](max) NULL,
	[DIACHICHITIET] [nvarchar](max) NULL,
	[BIKHOA] [bit] NOT NULL,
	[SOLANDANGNHAPSAI] [int] NULL,
	[THOIGIANDANGNHAPSAICUOICUNG] [datetime] NULL,
	[SessionToken] [nvarchar](200) NULL,
 CONSTRAINT [PK__KHACHHAN__30035C2F9B43D7E4] PRIMARY KEY CLUSTERED 
(
	[MAKHACHHANG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LICHSUDANGNHAP]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LICHSUDANGNHAP](
	[IDLICHSUDANGNHAP] [int] IDENTITY(1,1) NOT NULL,
	[IDTAIKHOAN] [int] NULL,
	[THOIGIAN] [datetime] NULL,
	[DIA_CHI_IP] [nvarchar](50) NULL,
	[TRINH_DUYET] [nvarchar](200) NULL,
	[THIET_BI] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDLICHSUDANGNHAP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LIENHE]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LIENHE](
	[MALIENHE] [int] IDENTITY(1,1) NOT NULL,
	[MAKHACHHANG] [int] NULL,
	[HOVATEN] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[SDT] [bigint] NULL,
	[LOINHAN] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MALIENHE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIENSANXUAT]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIENSANXUAT](
	[MANHANVIENSANXUAT] [int] IDENTITY(1,1) NOT NULL,
	[TENNHANVIENSANXUAT] [nvarchar](50) NOT NULL,
	[TUOI] [int] NOT NULL,
	[DIACHI] [nvarchar](max) NOT NULL,
	[SDT] [bigint] NOT NULL,
	[SOCCCD] [bigint] NOT NULL,
	[TENDANGNHAP] [nvarchar](50) NULL,
	[MATKHAU] [nvarchar](max) NULL,
	[NGAYVAOLAM] [datetime] NULL,
	[TRANGTHAI] [nvarchar](50) NULL,
	[NGAYNGHILAM] [datetime] NULL,
	[VAITRO] [nvarchar](max) NULL,
	[BIKHOA] [bit] NOT NULL,
	[SOLANDANGNHAPSAI] [int] NULL,
	[THOIGIANDANGNHAPSAICUOICUNG] [datetime] NULL,
	[SessionToken] [nvarchar](200) NULL,
 CONSTRAINT [PK__NHANVIEN__3902A89538FB9899] PRIMARY KEY CLUSTERED 
(
	[MANHANVIENSANXUAT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIENTAICHINH]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIENTAICHINH](
	[MANHANVIENTAICHINH] [int] IDENTITY(1,1) NOT NULL,
	[TENNHANVIENTAICHINH] [nvarchar](50) NOT NULL,
	[TUOI] [int] NOT NULL,
	[DIACHI] [nvarchar](max) NOT NULL,
	[SDT] [bigint] NOT NULL,
	[SOCCCD] [bigint] NOT NULL,
	[TENDANGNHAP] [nvarchar](50) NULL,
	[MATKHAU] [nvarchar](max) NULL,
	[NGAYVAOLAM] [datetime] NULL,
	[TRANGTHAI] [nvarchar](50) NOT NULL,
	[NGAYNGHILAM] [datetime] NULL,
	[BIKHOA] [bit] NOT NULL,
	[SOLANDANGNHAPSAI] [int] NULL,
	[THOIGIANDANGNHAPSAICUOICUNG] [datetime] NULL,
	[SessionToken] [nvarchar](200) NULL,
 CONSTRAINT [PK__NHANVIEN__2769E569545AFED2] PRIMARY KEY CLUSTERED 
(
	[MANHANVIENTAICHINH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIENVANCHUYEN]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIENVANCHUYEN](
	[MANHANVIENVANCHUYEN] [int] IDENTITY(1,1) NOT NULL,
	[TENNHANVIENVANCHUYEN] [nvarchar](50) NOT NULL,
	[TUOI] [int] NOT NULL,
	[DIACHI] [nvarchar](max) NOT NULL,
	[SDT] [bigint] NOT NULL,
	[SOCCCD] [bigint] NOT NULL,
	[TENDANGNHAP] [nvarchar](50) NULL,
	[MATKHAU] [nvarchar](max) NULL,
	[NGAYVAOLAM] [datetime] NULL,
	[TRANGTHAI] [nvarchar](50) NULL,
	[NGAYNGHILAM] [datetime] NULL,
	[VAITRO] [nvarchar](max) NULL,
	[BIKHOA] [bit] NOT NULL,
	[SOLANDANGNHAPSAI] [int] NULL,
	[THOIGIANDANGNHAPSAICUOICUNG] [datetime] NULL,
	[SessionToken] [nvarchar](200) NULL,
 CONSTRAINT [PK__NHANVIEN__AAB2B19A5D02B962] PRIMARY KEY CLUSTERED 
(
	[MANHANVIENVANCHUYEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[MASANPHAM] [int] IDENTITY(1,1) NOT NULL,
	[TENSANPHAM] [nvarchar](50) NULL,
	[GIASANPHAM] [nvarchar](50) NULL,
	[LOAISANPHAM] [nvarchar](50) NULL,
	[CHIEUDAI] [float] NULL,
	[CHIEURONG] [float] NULL,
	[CHIEUCAO] [float] NULL,
	[CHATLIEU] [nvarchar](50) NULL,
	[MOTASANPHAM] [nvarchar](max) NULL,
	[SOLUONG] [int] NULL,
	[ANHSANPHAM] [varchar](max) NULL,
	[DABAN] [int] NULL,
 CONSTRAINT [PK__SANPHAM__9534C892D7BA6708] PRIMARY KEY CLUSTERED 
(
	[MASANPHAM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAMTHEOYEUCAU]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAMTHEOYEUCAU](
	[MASANPHAMTHEOYEUCAU] [int] IDENTITY(1,1) NOT NULL,
	[MAKHACHHANG] [int] NULL,
	[LOAISANPHAM] [nvarchar](max) NULL,
	[CHIEUDAI] [float] NULL,
	[CHIEURONG] [float] NULL,
	[CHIEUCAO] [float] NULL,
	[VATLIEU] [nvarchar](50) NULL,
	[MAUSAC] [nvarchar](50) NULL,
	[SOLUONG] [int] NULL,
	[TONGSOTIENSANXUAT] [float] NULL,
	[MOTASANPHAM] [nvarchar](100) NULL,
	[TRANGTHAIDUYET] [nvarchar](50) NULL,
	[MAHOADON] [int] NULL,
	[ANHMINHHOA] [nvarchar](max) NULL,
	[KHACHHANGCHAPNHAN] [nvarchar](50) NULL,
	[TONGTIENSANPHAM] [float] NULL,
 CONSTRAINT [PK__SANPHAMT__80ACAE24DC5F30BC] PRIMARY KEY CLUSTERED 
(
	[MASANPHAMTHEOYEUCAU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIENDOSANXUAT]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIENDOSANXUAT](
	[MATIENDOSANXUAT] [int] IDENTITY(1,1) NOT NULL,
	[NGAYSANXUAT] [datetime] NULL,
	[NGAYDUKIENHOANTHANH] [datetime] NULL,
	[NGAYHOANTHANHTHUCTE] [datetime] NULL,
	[GIAIDOANSANXUAT] [nvarchar](50) NULL,
	[MANHANVIENSANXUAT] [int] NULL,
	[MOTA] [nvarchar](max) NULL,
	[SOLUONG] [int] NULL,
	[MASANPHAMTHEOYEUCAU] [int] NULL,
	[ANHMINHHOA] [nvarchar](max) NULL,
	[NGHIEMTHU] [nvarchar](max) NULL,
 CONSTRAINT [PK__TIENDOSA__4794CD36A2E608F5] PRIMARY KEY CLUSTERED 
(
	[MATIENDOSANXUAT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VANCHUYEN]    Script Date: 5/3/2025 1:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VANCHUYEN](
	[MAVANCHUYEN] [int] IDENTITY(1,1) NOT NULL,
	[NGAYBATDAUGUI] [datetime] NOT NULL,
	[NGAYDUKIENDUOCGIAO] [datetime] NULL,
	[PHUONGTHUCVANCHUYEN] [nvarchar](max) NULL,
	[CHIPHIVANCHUYEN] [float] NULL,
	[MOTA] [nvarchar](max) NULL,
	[TRANGTHAIVANCHUYEN] [nvarchar](max) NULL,
	[MANHANVIENVANCHUYEN] [int] NULL,
	[MACHITIETHOADON] [int] NULL,
	[MASANPHAMTHEOYEUCAU] [int] NULL,
	[NGAYGIAOTHUCTE] [datetime] NULL,
 CONSTRAINT [PK__VANCHUYE__4AF50D46F1CADA92] PRIMARY KEY CLUSTERED 
(
	[MAVANCHUYEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ADMIN] ON 

INSERT [dbo].[ADMIN] ([MAADMIN], [TENQUANLY], [TENDANGNHAP], [MATKHAU], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (2, N'Vinh', N'admin', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', 0, 3, CAST(N'2025-05-02T18:00:59.993' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ADMIN] OFF
GO
SET IDENTITY_INSERT [dbo].[CHITIETHOADON] ON 

INSERT [dbo].[CHITIETHOADON] ([MACHITIETHOADON], [MAHOADON], [MASANPHAM], [TONGTIEN], [SOLUONG], [MATIENDOSANXUAT]) VALUES (92, 1148, 101, 10000, 1, NULL)
INSERT [dbo].[CHITIETHOADON] ([MACHITIETHOADON], [MAHOADON], [MASANPHAM], [TONGTIEN], [SOLUONG], [MATIENDOSANXUAT]) VALUES (93, 1150, 101, 10000, 1, NULL)
INSERT [dbo].[CHITIETHOADON] ([MACHITIETHOADON], [MAHOADON], [MASANPHAM], [TONGTIEN], [SOLUONG], [MATIENDOSANXUAT]) VALUES (94, 1151, 101, 10000, 1, NULL)
SET IDENTITY_INSERT [dbo].[CHITIETHOADON] OFF
GO
SET IDENTITY_INSERT [dbo].[DOANHTHU] ON 

INSERT [dbo].[DOANHTHU] ([MADOANHTHU], [SOLUONG], [GIADABAN], [NGAYTAO], [MACHITIETHOADON], [MASANPHAMTHEOYEUCAU]) VALUES (55, 1, 10000, CAST(N'2024-12-03T15:36:00.070' AS DateTime), 92, NULL)
INSERT [dbo].[DOANHTHU] ([MADOANHTHU], [SOLUONG], [GIADABAN], [NGAYTAO], [MACHITIETHOADON], [MASANPHAMTHEOYEUCAU]) VALUES (56, 1, 10000, CAST(N'2024-12-05T07:27:22.387' AS DateTime), NULL, 461)
SET IDENTITY_INSERT [dbo].[DOANHTHU] OFF
GO
SET IDENTITY_INSERT [dbo].[GIOHANG] ON 

INSERT [dbo].[GIOHANG] ([MAGIOHANG], [MAKHACHHANG], [MASANPHAM], [SOLUONG], [TONGTIEN], [NGAYTHEM]) VALUES (34, 52, 101, 1, 10000, CAST(N'2024-11-15T23:00:52.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[GIOHANG] OFF
GO
SET IDENTITY_INSERT [dbo].[HOADON] ON 

INSERT [dbo].[HOADON] ([MAHOADON], [MAKHACHHANG], [TONGTIEN], [TRANGTHAITHANHTOAN], [PHUONGTHUCTHANHTOAN], [MANHANVIENTAICHINH], [NGAYLAP], [SOHOADON], [TRANGTHAIHUYDON]) VALUES (1146, 54, 10000, N'Chưa thanh toán', N'Thanh toán online', 1, CAST(N'2024-11-29T22:28:39.420' AS DateTime), N'7TBJ4', NULL)
INSERT [dbo].[HOADON] ([MAHOADON], [MAKHACHHANG], [TONGTIEN], [TRANGTHAITHANHTOAN], [PHUONGTHUCTHANHTOAN], [MANHANVIENTAICHINH], [NGAYLAP], [SOHOADON], [TRANGTHAIHUYDON]) VALUES (1148, 54, 10000, N'đã thanh toán', N'Thanh toán online', NULL, CAST(N'2024-12-03T15:34:52.780' AS DateTime), N'14LI5', NULL)
INSERT [dbo].[HOADON] ([MAHOADON], [MAKHACHHANG], [TONGTIEN], [TRANGTHAITHANHTOAN], [PHUONGTHUCTHANHTOAN], [MANHANVIENTAICHINH], [NGAYLAP], [SOHOADON], [TRANGTHAIHUYDON]) VALUES (1149, 54, 10000, N'Chưa thanh toán', N'Thanh toán online', 1, CAST(N'2024-12-05T10:02:21.867' AS DateTime), N'DWLT9', NULL)
INSERT [dbo].[HOADON] ([MAHOADON], [MAKHACHHANG], [TONGTIEN], [TRANGTHAITHANHTOAN], [PHUONGTHUCTHANHTOAN], [MANHANVIENTAICHINH], [NGAYLAP], [SOHOADON], [TRANGTHAIHUYDON]) VALUES (1150, 54, 10000, N'Chưa thanh toán', N'Thanh toán khi nhận hàng', NULL, CAST(N'2024-12-05T10:06:50.340' AS DateTime), N'UFHFQ', NULL)
INSERT [dbo].[HOADON] ([MAHOADON], [MAKHACHHANG], [TONGTIEN], [TRANGTHAITHANHTOAN], [PHUONGTHUCTHANHTOAN], [MANHANVIENTAICHINH], [NGAYLAP], [SOHOADON], [TRANGTHAIHUYDON]) VALUES (1151, 54, 10000, N'Chưa thanh toán', N'Thanh toán online', NULL, CAST(N'2024-12-10T09:16:03.177' AS DateTime), N'UXSI1', NULL)
SET IDENTITY_INSERT [dbo].[HOADON] OFF
GO
SET IDENTITY_INSERT [dbo].[KHACHHANG] ON 

INSERT [dbo].[KHACHHANG] ([MAKHACHHANG], [TENDANGNHAP], [MATKHAU], [HOVATEN], [TUOI], [SDT], [EMAIL], [CAPDO], [TINH], [DIACHICHITIET], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (52, N'vinhtatoo0912', N'123', N'Nguyễn Thế Vinh', 23, 967287418, NULL, N'Thường', N'Phú Yên', N'Thôn phú nhiêu, hòa mỹ đông, tây hòa, phú yên', 0, 0, NULL, NULL)
INSERT [dbo].[KHACHHANG] ([MAKHACHHANG], [TENDANGNHAP], [MATKHAU], [HOVATEN], [TUOI], [SDT], [EMAIL], [CAPDO], [TINH], [DIACHICHITIET], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (54, N'vinh', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', N'thế vinh', 23, 967287418, N'vinhtatoo0913@gmail.com', NULL, N'Phú Yên', N'Thôn phú nhiêu, hòa mỹ đông, tây hòa, phú yên', 0, 0, NULL, NULL)
INSERT [dbo].[KHACHHANG] ([MAKHACHHANG], [TENDANGNHAP], [MATKHAU], [HOVATEN], [TUOI], [SDT], [EMAIL], [CAPDO], [TINH], [DIACHICHITIET], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (55, N'quyenvq', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', N'Nguyen Van Quyen', 23, 967287412, N'nguyenvanquyen11304@gmail.com', NULL, N'Cao Bằng', N'Thôn phú nhiêu, hòa mỹ đông, tây hòa, phú yên', 0, 0, NULL, NULL)
INSERT [dbo].[KHACHHANG] ([MAKHACHHANG], [TENDANGNHAP], [MATKHAU], [HOVATEN], [TUOI], [SDT], [EMAIL], [CAPDO], [TINH], [DIACHICHITIET], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (56, N'vinhtatoo0917', N'fbe64ca8017e1c22572d064991f69dc3a82bd31e71887ea571d0124c5924e1915bcc96c1b60b9dd32835eb58a540aa3dc54468ebd4649420b1ee5c582019ed1d', NULL, NULL, NULL, N'vinhtatoo0917@gmail.com', NULL, NULL, NULL, 0, 0, CAST(N'2025-05-03T00:37:01.770' AS DateTime), N'85384207-7b3e-4dd2-bc14-cfd60251bf0b')
INSERT [dbo].[KHACHHANG] ([MAKHACHHANG], [TENDANGNHAP], [MATKHAU], [HOVATEN], [TUOI], [SDT], [EMAIL], [CAPDO], [TINH], [DIACHICHITIET], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (59, N'thevinhtdmu2004@gmail.com', N'', N'Vinh The', NULL, NULL, N'thevinhtdmu2004@gmail.com', NULL, NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[KHACHHANG] ([MAKHACHHANG], [TENDANGNHAP], [MATKHAU], [HOVATEN], [TUOI], [SDT], [EMAIL], [CAPDO], [TINH], [DIACHICHITIET], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (60, N'Vinhtatoo06', N'fbe64ca8017e1c22572d064991f69dc3a82bd31e71887ea571d0124c5924e1915bcc96c1b60b9dd32835eb58a540aa3dc54468ebd4649420b1ee5c582019ed1d', NULL, NULL, NULL, N'vinhtatoo096@gmail.com', NULL, NULL, NULL, 0, 0, NULL, N'71c8af39-dd51-45a4-8267-934211aa599a')
SET IDENTITY_INSERT [dbo].[KHACHHANG] OFF
GO
SET IDENTITY_INSERT [dbo].[LICHSUDANGNHAP] ON 

INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (1, 60, CAST(N'2025-05-03T12:11:45.923' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (2, 60, CAST(N'2025-05-03T12:13:47.243' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (3, 60, CAST(N'2025-05-03T12:17:56.887' AS DateTime), N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (4, 60, CAST(N'2025-05-03T12:33:38.760' AS DateTime), N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (5, 60, CAST(N'2025-05-03T12:38:04.780' AS DateTime), N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (6, 60, CAST(N'2025-05-03T12:41:42.373' AS DateTime), N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
INSERT [dbo].[LICHSUDANGNHAP] ([IDLICHSUDANGNHAP], [IDTAIKHOAN], [THOIGIAN], [DIA_CHI_IP], [TRINH_DUYET], [THIET_BI]) VALUES (7, 60, CAST(N'2025-05-03T12:54:38.290' AS DateTime), N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36', N'Máy tính')
SET IDENTITY_INSERT [dbo].[LICHSUDANGNHAP] OFF
GO
SET IDENTITY_INSERT [dbo].[LIENHE] ON 

INSERT [dbo].[LIENHE] ([MALIENHE], [MAKHACHHANG], [HOVATEN], [EMAIL], [SDT], [LOINHAN]) VALUES (1, NULL, N'Nguyen Van B', N'vinhtatoo096@gmail.com', 1234, N'1234')
SET IDENTITY_INSERT [dbo].[LIENHE] OFF
GO
SET IDENTITY_INSERT [dbo].[NHANVIENSANXUAT] ON 

INSERT [dbo].[NHANVIENSANXUAT] ([MANHANVIENSANXUAT], [TENNHANVIENSANXUAT], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [VAITRO], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (9, N'Ngô Van I', 33, N'Gia Lai', 9901234567, 901234567890, N'nvsx2', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', NULL, N'Ðang làm vi?c', NULL, N'Nhân viên', 0, NULL, NULL, NULL)
INSERT [dbo].[NHANVIENSANXUAT] ([MANHANVIENSANXUAT], [TENNHANVIENSANXUAT], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [VAITRO], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (10, N'Nguyễn Thế Vinh', 29, N'B?c Ninh', 9012345678, 112233445566, N'nvsx1', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', NULL, N'Đang làm', NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[NHANVIENSANXUAT] ([MANHANVIENSANXUAT], [TENNHANVIENSANXUAT], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [VAITRO], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (11, N'Vinh', 20, N'bình dương', 912, 542, N'vinh09', N'6bb558f2a3f586d106fe800f8ad67b263daf8f41cc2facb04431e871143b87f3c78d3c7c85d81163af333c88a3e7112f0b1bc5e8743762efcac7dc8db38af750', CAST(N'2024-11-27T23:27:39.997' AS DateTime), N'Đang làm', NULL, N'Nhân viên', 0, NULL, NULL, NULL)
INSERT [dbo].[NHANVIENSANXUAT] ([MANHANVIENSANXUAT], [TENNHANVIENSANXUAT], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [VAITRO], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (12, N'Vinh', 20, N'123', 123, 123, N'123', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', CAST(N'2024-11-28T00:16:44.007' AS DateTime), N'Đang làm', NULL, N'Nhân viên', 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NHANVIENSANXUAT] OFF
GO
SET IDENTITY_INSERT [dbo].[NHANVIENTAICHINH] ON 

INSERT [dbo].[NHANVIENTAICHINH] ([MANHANVIENTAICHINH], [TENNHANVIENTAICHINH], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (9, N'Nguyễn Thế Vinh', 18, N'Phú Yên', 97771823, 9727138371, N'nvtc1', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', CAST(N'2024-12-05T00:00:00.000' AS DateTime), N'Đang làm', NULL, 0, 1, CAST(N'2025-05-02T18:05:01.717' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[NHANVIENTAICHINH] OFF
GO
SET IDENTITY_INSERT [dbo].[NHANVIENVANCHUYEN] ON 

INSERT [dbo].[NHANVIENVANCHUYEN] ([MANHANVIENVANCHUYEN], [TENNHANVIENVANCHUYEN], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [VAITRO], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (1, N'Nguy?n Van A', 35, N'Hà N?i', 9123456789, 123456789012, N'nvvc2', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', CAST(N'2024-11-27T00:00:00.000' AS DateTime), N'Đang làm', NULL, N'Trưởng VC', 0, NULL, NULL, NULL)
INSERT [dbo].[NHANVIENVANCHUYEN] ([MANHANVIENVANCHUYEN], [TENNHANVIENVANCHUYEN], [TUOI], [DIACHI], [SDT], [SOCCCD], [TENDANGNHAP], [MATKHAU], [NGAYVAOLAM], [TRANGTHAI], [NGAYNGHILAM], [VAITRO], [BIKHOA], [SOLANDANGNHAPSAI], [THOIGIANDANGNHAPSAICUOICUNG], [SessionToken]) VALUES (10, N'Tr?nh Th? K', 34, N'B?c Ninh', 9012345678, 112233445566, N'nvvc1', N'3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2', CAST(N'2024-12-06T00:00:00.000' AS DateTime), N'Đang làm', NULL, N'Nhân viên', 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NHANVIENVANCHUYEN] OFF
GO
SET IDENTITY_INSERT [dbo].[SANPHAM] ON 

INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (101, N'Tượng Trần Quốc Tuấn', N'10,000', N'Tượng gỗ', 100, 20, 70, N'Gỗ Cẩm Lai', N'Chất liệu: Gỗ Hương ĐỏSố lượng hoa lá: 16 cành/1 bìnhHình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 7, N'../IMG/sanpham/faa27881c09a21c4788b_109880_anh1-600x800.jpg', 22)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (102, N'BÌNH HOA SEN GỖ HƯƠNG ĐỎ', N'6,000,000', N'Bình - Lục bình', 0, 0, 80, N'gỗ hương đỏ đăklak', N'Chất liệu: Gỗ Hương Đỏ

Số lượng hoa lá: 16 cành/1 bình

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 28, N'../IMG/sanpham/b5acdfb85686b7d8ee97_109896_anh1-600x800.jpg', 3)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (103, N'Kệ TiVi Dài 200cm', N'22,000,000', N'Nội Thất', 200, 100, 50, N'Gỗ Samanea', N'Hiện trạng vẫn còn để nguyên hình dạng tự nhiên của cây, vân cực kỳ đẹp và hiếm.
Chất liệu: Gỗ tự nhiên liền tâm, nguyên khối, không cong vênh nứt gãy
Xuất xứ: Cambodia
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán. Bảo hành nứt nẻ cong vênh trọn đời. Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 19, N'../IMG/sanpham/f4030b17b1cc5e9207dd_109781_anh1_109894_anh1-600x338.jpg', 4)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (104, N'Tượng Khổng Minh', N'10,500,000', N'Tượng gỗ', 20, 28, 80, N'Gỗ Cẩm Lai', N'Chất liệu: Gỗ cẩm lai, tự nhiên, nguyên khối.
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 15, N'../IMG/sanpham/5e33010c891e6840310f_109832_anh1-600x800.jpg', 1)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (105, N'Bàn Ăn Gỗ Tự Nhiên Nguyên Tấm', N'55,000,000', N'Nội Thất', 221, 98, 8, N'Gỗ Samanea Vân Cẩm', N'Kích thước: dài 221cm, rộng 98cm, dày 8cm, có thể làm mặt bàn ăn, mặt bàn trà, bàn làm việc tuỳ ý thích và nhu cầu của bạn. 

Chất liệu: Gỗ tự nhiên liên tấm, nguyên khối,  không cong vênh nứt gãy

Xuất xứ: Campuchia

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán. Bảo hành nứt nẻ cong vênh đổi mới

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.

Đồ Gỗ Tây Nguyên ® Chuyên sản xuất, bán buôn, bán lẻ các sản phẩm đồ gỗ mỹ nghệ phong thủy, đồ thờ và các sản phẩm độc đáo khác từ các loại gỗ quý hiếm ( Gỗ Sưa, Thủy Tùng, Hoàng Đàn, Trắc, Cẩm, Hương, Gõ Đỏ, Gụ…) Nhận gia công chế biến theo yêu cầu.', 8, N'../IMG/sanpham/18318d46215dc003994c_109884_anh1-600x800.jpg', 2)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (106, N'TƯỢNG DI LẶC CƯỠI CÁ CHÉP', N'14,000,000', N'Tượng gỗ', 37, 40, 96, N'Gỗ hương đỏ', N'Chất liệu: Gỗ hương đỏ đăklak, tự nhiên, nguyên khối.
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 12, N'../IMG/sanpham/5bfb114f1e5dff03a64c_109887_anh1-600x800.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (107, N'Cóc Thần Tài', N'4,800,000', N'Tượng gỗ', 57, 57, 47, N'Gỗ Knia, nguyên khối', N'Kích thước: Dày 57cm, rộng 57cm, cao 47cm

Chất liệu: Gỗ Knia, nguyên khối, hàng trần mộc và gỗ cực đẹp.

Xuất xứ: Đồ Gỗ Tây Nguyên

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 100, N'../IMG/sanpham/140609101673-300x300.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (108, N'QUẢ CẦU ĐÁ MẮT MÈO XANH DƯƠNG', N'1,500,000', N'Bình - Lục bình', 100, 100, 100, N'Đá Tự Nhiên', N'Tượng trưng cho công việc thuận lợi, còn tăng cường sinh khí, kích hoạt may mắn, năng lượng dương, chống tà khí từ bên ngoài.', 25, N'../IMG/sanpham/6453b46931bed0e089af_109900_anh1-600x800.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (109, N'Đôi Hổ Gỗ Hương', N'900,000', N'Nội Thất', 15, 20, 8, N'gỗ hương đỏ đăklak', N'Kích thước:Cao 15cm, rộng 20cm, sâu 8cm

Xuất xứ: Đồ Gỗ Tây Nguyên

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 8, N'../IMG/sanpham/c586e8bdd3a332fd6bb2_109855_anh1-600x450.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (110, N'Hộp Đựng Giấy Ăn Gỗ Ba Ngăn', N'200,000', N'Gia dụng', 10, 26, 13, N'gỗ hương đỏ đăklak', N'Kích thước:  Cao 10cm,rộng 26cm, sâu 13cm
Chất liệu: Gỗ hương đỏ đăklak, tự nhiên, nguyên khối.
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.', 18, N'../IMG/sanpham/3b93d532b02a5174083b_109877_anh1-600x450.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (111, N'Tượng Chim Công', N'3,800,000', N'Tượng gỗ', 30, 30, 40, N' gỗ hương đỏ đăklak', N'Chất liệu: Gỗ hương đỏ đăklak
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bánChất liệu: Gỗ hương đỏ đăklak
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán', 5, N'../IMG/sanpham/e16e43af7aac9bf2c2bd_109808_anhkhac1-600x800.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (112, N'Đèn Thờ Gỗ Hương', N'4,000,000', N'Phong thủy', 50, 15, 50, N'gỗ hương đỏ đăklak', N'Kích thước: Cao 50cm , rộng 15cm.

Chất liệu: Gỗ Hương Đỏ Đaklak, gỗ cực đẹp. đường nét đục tinh xảo, có hồn

Xuất xứ: Đồ Gỗ Tây Nguyên

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 30, N'../IMG/sanpham/f8c08edd11def080a9cf_109809_anh1-600x800.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (113, N'Tranh Bát Mã', N'6,500,000', N'Phong thủy', 126, 66, 66, N'gỗ hương đỏ đăklak', N'Kích thước: dài 126cm, rộng 66cm
Chất liệu: Gỗ hương đỏ, tự nhiên, nguyên khối.
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.', 20, N'../IMG/sanpham/308cb4ef0ef4efaab6e5_109881_anh1-600x450.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (114, N'ĐỒNG HỒ MỎ NEO
', N'7,000,000', N'Gia dụng', 81, 70, 70, N'Gỗ Trắc Đỏ', N'Kích thước: Cao 81cm, rộng 70cm
Chất liệu: Gỗ Trắc Đỏ
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.

Đồ Gỗ Tây Nguyên ® Chuyên sản xuất, bán buôn, bán lẻ các sản phẩm đồ gỗ mỹ nghệ phong thủy, đồ thờ và các sản phẩm độc đáo khác từ các loại gỗ quý hiếm ( Gỗ Sưa, Thủy Tùng, Hoàng Đàn, Trắc, Cẩm, Hương, Gõ Đỏ, Gụ…) Nhận gia công chế biến theo yêu cầu)', 25, N'../IMG/sanpham/d2a90e81878c66d23f9d_109810_anh1-600x588.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (115, N'LÃO VỌNG NGỒI GỐC TÙNG CÂU CÁ', N'6,800,000', N'Tượng gỗ', 60, 65, 35, N'Gỗ Hương Đỏ Đaklak', N'Kích thước: Cao 60cm, Rộng 65cm, sâu 35cm

Chất liệu: Gỗ Hương Đỏ Đaklak, liền khối, chất gỗ nục nạc, vân gỗ tự nhiên rất đẹp. Đường nét đục tinh xảo, rất có hồn.

Xuất xứ: Đồ Gỗ Tây Nguyên

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 8, N'../IMG/sanpham/145318600272-600x929.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (116, N'LỤC BÌNH GỖ MUỒNG KIM PHƯỢNG', N'50,000,000', N'Bình - Lục bình', 40, 40, 157, N'Gỗ Muồng Kim Phượng
', N'Chuyên sản xuất, bán buôn, bán lẻ các sản phẩm đồ gỗ mỹ nghệ phong thủy, lục bình phong thủy, tượng gỗ phong thủy, thiềm thừ, tỳ hưu, phúc lộc thọ (tam đa), đồ thờ và các sản phẩm độc đáo khác từ các loại gỗ quý hiếm ( Gỗ Sưa, Thủy Tùng, Hoàng Đàn, Trắc, Cẩm, Hương, Gõ Đỏ, Gụ…) Nhận gia công chế biến theo yêu cầu.', 10, N'../IMG/sanpham/muongv_109796_anh1.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (117, N'Phản Gỗ Liền Tấm 3m2x1m8', N'30,000,000', N'Nội Thất', 320, 180, 20, N'Gỗ Samanea Vân Cẩm', N'Kích thước:  Dài 3m2, rộng 1m8, dày 20cm, hiện trạng vẫn còn để nguyên hình dạng tự nhiên của cây, vân cực kỳ đẹp và hiếm.
Chất liệu: Gỗ tự nhiên liền tấm, nguyên khối, không cong vênh nứt gãy
Xuất xứ: Campuchia
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán. Bảo hành nứt nẻ cong vênh đổi mới
Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 20, N'../IMG/sanpham/20170713_182051_109750_anh1-600x450.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (118, N'QUẠT PHONG THỦY TRẠM TỨ LINH', N'3,900,000', N'Phong thủy', 81, 45, 45, N'Gỗ Gõ Đỏ', N'Kích thước:  Dài 81cm, rộng 45cm, dày 7cm

Chất liệu: Gỗ Gõ Đỏ, liền khối, chất gỗ nục nạc, vân gỗ tự nhiên rất đẹp. Đường nét đục tinh xảo, rất có hồn.

Xuất xứ: Đồ Gỗ Tây Nguyên

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 30, N'../IMG/sanpham/144738745486-600x349.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (119, N'Tượng Phật Di Lặc Đứng Núi Tiền', N'7,000,000', N'Tượng gỗ', 26, 30, 60, N'gỗ hương đỏ đăklak', N'Kích thước: Cao 60cm, rộng 30cm, sâu 26cm
Chất liệu: Gỗ hương đỏ đăklak, tự nhiên, nguyên khối.
Xuất xứ: Đồ Gỗ Tây Nguyên
Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…
Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 15, N'../IMG/sanpham/f7863c88849565cb3c84_109846_anh1-600x800.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (120, N'Tượng Quan Công Gỗ Hương 48cm', N'3,600,000', N'Tượng gỗ', 20, 20, 48, N'gỗ hương đỏ đăklak', N'Kích thước: cao 48cm, rộng 20cm

Chất liệu: Gỗ Hương  nguyên khối, vân cực đẹp, không tỳ vết, đường nét đục tinh xảo, có hồn.

Xuất xứ: Đồ Gỗ Tây Nguyên

Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…

Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.

Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình

ảnh và mô tả.', 10, N'../IMG/sanpham/9dc78446f05711094846_109891_anh1-600x800.jpg', NULL)
INSERT [dbo].[SANPHAM] ([MASANPHAM], [TENSANPHAM], [GIASANPHAM], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [CHATLIEU], [MOTASANPHAM], [SOLUONG], [ANHSANPHAM], [DABAN]) VALUES (121, N'Tượng Chó Gỗ Hương', N'600,000', N'Tượng gỗ', 9, 20, 18, N'gỗ hương đỏ đăklak', N'Hình thức thanh toán: tiền mặt, chuyển khoán, thanh toán tại nhà,…  Bảo hành trọn đời gỗ. Cam kết hỗ trợ nhập lại với giá đã bán.  Hàng có sẵn, hỗ trợ giao hàng tận nhà trên toàn quốc. Cam kết sản phẩm đúng như hình ảnh và mô tả.', 20, N'../IMG/sanpham/093a5d187b069a58c317_109856_anh1-600x450.jpg', NULL)
SET IDENTITY_INSERT [dbo].[SANPHAM] OFF
GO
SET IDENTITY_INSERT [dbo].[SANPHAMTHEOYEUCAU] ON 

INSERT [dbo].[SANPHAMTHEOYEUCAU] ([MASANPHAMTHEOYEUCAU], [MAKHACHHANG], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [VATLIEU], [MAUSAC], [SOLUONG], [TONGSOTIENSANXUAT], [MOTASANPHAM], [TRANGTHAIDUYET], [MAHOADON], [ANHMINHHOA], [KHACHHANGCHAPNHAN], [TONGTIENSANPHAM]) VALUES (461, 54, N'Sản phẩm đặc biệt', 1, 1, 1, N'Gỗ Samanea', N'Màu nâu', 1, 10000, N'1', N'Duyệt', 1146, N'../IMG/sanpham/muongv_109796_anh1.jpg', N'Chấp nhận', NULL)
INSERT [dbo].[SANPHAMTHEOYEUCAU] ([MASANPHAMTHEOYEUCAU], [MAKHACHHANG], [LOAISANPHAM], [CHIEUDAI], [CHIEURONG], [CHIEUCAO], [VATLIEU], [MAUSAC], [SOLUONG], [TONGSOTIENSANXUAT], [MOTASANPHAM], [TRANGTHAIDUYET], [MAHOADON], [ANHMINHHOA], [KHACHHANGCHAPNHAN], [TONGTIENSANPHAM]) VALUES (462, 54, N'Sản phẩm đặc biệt', 1, 1, 1, N'gỗ hương đỏ đăklak', N'Màu nâu', 1, 10000, N'1', N'Duyệt', 1149, N'../IMG/sanpham/3b93d532b02a5174083b_109877_anh1-600x450.jpg', N'Chấp nhận', 20000)
SET IDENTITY_INSERT [dbo].[SANPHAMTHEOYEUCAU] OFF
GO
SET IDENTITY_INSERT [dbo].[TIENDOSANXUAT] ON 

INSERT [dbo].[TIENDOSANXUAT] ([MATIENDOSANXUAT], [NGAYSANXUAT], [NGAYDUKIENHOANTHANH], [NGAYHOANTHANHTHUCTE], [GIAIDOANSANXUAT], [MANHANVIENSANXUAT], [MOTA], [SOLUONG], [MASANPHAMTHEOYEUCAU], [ANHMINHHOA], [NGHIEMTHU]) VALUES (39, CAST(N'2024-12-05T07:27:22.000' AS DateTime), NULL, NULL, N'Lắp ráp', 10, N'Đơn hàng đã được thanh toán hãy làm việc năng nỗ nào !!!', 1, 461, N'../IMG/sanpham/3b93d532b02a5174083b_109877_anh1-600x450.jpg', N'Đang chờ')
SET IDENTITY_INSERT [dbo].[TIENDOSANXUAT] OFF
GO
SET IDENTITY_INSERT [dbo].[VANCHUYEN] ON 

INSERT [dbo].[VANCHUYEN] ([MAVANCHUYEN], [NGAYBATDAUGUI], [NGAYDUKIENDUOCGIAO], [PHUONGTHUCVANCHUYEN], [CHIPHIVANCHUYEN], [MOTA], [TRANGTHAIVANCHUYEN], [MANHANVIENVANCHUYEN], [MACHITIETHOADON], [MASANPHAMTHEOYEUCAU], [NGAYGIAOTHUCTE]) VALUES (69, CAST(N'2024-12-04T15:36:00.080' AS DateTime), CAST(N'2024-12-08T15:36:00.080' AS DateTime), N'Vận chuyển nhanh', 0, N'Đơn hàng đã được thanh toán, hãy làm việc năng nỗ nào!', N'Hoàn thành', 1, 92, NULL, CAST(N'2024-12-05T10:11:24.507' AS DateTime))
INSERT [dbo].[VANCHUYEN] ([MAVANCHUYEN], [NGAYBATDAUGUI], [NGAYDUKIENDUOCGIAO], [PHUONGTHUCVANCHUYEN], [CHIPHIVANCHUYEN], [MOTA], [TRANGTHAIVANCHUYEN], [MANHANVIENVANCHUYEN], [MACHITIETHOADON], [MASANPHAMTHEOYEUCAU], [NGAYGIAOTHUCTE]) VALUES (70, CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2024-12-19T00:00:00.000' AS DateTime), N'Vận chuyển nhanh', 0, N'Đơn hàng đã được tạo hãy vận chuyển đơn hàng đến khách hàng sớm nhất có thể', N'Đơn hàng đã tạo', 10, 93, NULL, NULL)
SET IDENTITY_INSERT [dbo].[VANCHUYEN] OFF
GO
ALTER TABLE [dbo].[ADMIN] ADD  DEFAULT ((0)) FOR [BIKHOA]
GO
ALTER TABLE [dbo].[ADMIN] ADD  DEFAULT ((0)) FOR [SOLANDANGNHAPSAI]
GO
ALTER TABLE [dbo].[KHACHHANG] ADD  DEFAULT ((0)) FOR [BIKHOA]
GO
ALTER TABLE [dbo].[KHACHHANG] ADD  DEFAULT ((0)) FOR [SOLANDANGNHAPSAI]
GO
ALTER TABLE [dbo].[NHANVIENSANXUAT] ADD  DEFAULT ((0)) FOR [BIKHOA]
GO
ALTER TABLE [dbo].[NHANVIENSANXUAT] ADD  DEFAULT ((0)) FOR [SOLANDANGNHAPSAI]
GO
ALTER TABLE [dbo].[NHANVIENTAICHINH] ADD  DEFAULT ((0)) FOR [BIKHOA]
GO
ALTER TABLE [dbo].[NHANVIENTAICHINH] ADD  DEFAULT ((0)) FOR [SOLANDANGNHAPSAI]
GO
ALTER TABLE [dbo].[NHANVIENVANCHUYEN] ADD  DEFAULT ((0)) FOR [BIKHOA]
GO
ALTER TABLE [dbo].[NHANVIENVANCHUYEN] ADD  DEFAULT ((0)) FOR [SOLANDANGNHAPSAI]
GO
ALTER TABLE [dbo].[CHITIETHOADON]  WITH CHECK ADD  CONSTRAINT [FK__CHITIETHO__MAHOA__339FAB6E] FOREIGN KEY([MAHOADON])
REFERENCES [dbo].[HOADON] ([MAHOADON])
GO
ALTER TABLE [dbo].[CHITIETHOADON] CHECK CONSTRAINT [FK__CHITIETHO__MAHOA__339FAB6E]
GO
ALTER TABLE [dbo].[CHITIETHOADON]  WITH CHECK ADD  CONSTRAINT [FK__CHITIETHO__MASAN__22AA2996] FOREIGN KEY([MASANPHAM])
REFERENCES [dbo].[SANPHAM] ([MASANPHAM])
GO
ALTER TABLE [dbo].[CHITIETHOADON] CHECK CONSTRAINT [FK__CHITIETHO__MASAN__22AA2996]
GO
ALTER TABLE [dbo].[DOANHTHU]  WITH CHECK ADD FOREIGN KEY([MACHITIETHOADON])
REFERENCES [dbo].[CHITIETHOADON] ([MACHITIETHOADON])
GO
ALTER TABLE [dbo].[DOANHTHU]  WITH CHECK ADD FOREIGN KEY([MASANPHAMTHEOYEUCAU])
REFERENCES [dbo].[SANPHAMTHEOYEUCAU] ([MASANPHAMTHEOYEUCAU])
GO
ALTER TABLE [dbo].[GIOHANG]  WITH CHECK ADD  CONSTRAINT [FK__GIOHANG__MAKHACH__3E1D39E1] FOREIGN KEY([MAKHACHHANG])
REFERENCES [dbo].[KHACHHANG] ([MAKHACHHANG])
GO
ALTER TABLE [dbo].[GIOHANG] CHECK CONSTRAINT [FK__GIOHANG__MAKHACH__3E1D39E1]
GO
ALTER TABLE [dbo].[GIOHANG]  WITH CHECK ADD FOREIGN KEY([MASANPHAM])
REFERENCES [dbo].[SANPHAM] ([MASANPHAM])
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK__HOADON__MAKHACHH__2739D489] FOREIGN KEY([MAKHACHHANG])
REFERENCES [dbo].[KHACHHANG] ([MAKHACHHANG])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK__HOADON__MAKHACHH__2739D489]
GO
ALTER TABLE [dbo].[LICHSUDANGNHAP]  WITH CHECK ADD FOREIGN KEY([IDTAIKHOAN])
REFERENCES [dbo].[KHACHHANG] ([MAKHACHHANG])
GO
ALTER TABLE [dbo].[LIENHE]  WITH CHECK ADD  CONSTRAINT [FK__LIENHE__MAKHACHH__2BFE89A6] FOREIGN KEY([MAKHACHHANG])
REFERENCES [dbo].[KHACHHANG] ([MAKHACHHANG])
GO
ALTER TABLE [dbo].[LIENHE] CHECK CONSTRAINT [FK__LIENHE__MAKHACHH__2BFE89A6]
GO
ALTER TABLE [dbo].[SANPHAMTHEOYEUCAU]  WITH CHECK ADD  CONSTRAINT [FK__SANPHAMTH__MAHOA__3587F3E0] FOREIGN KEY([MAHOADON])
REFERENCES [dbo].[HOADON] ([MAHOADON])
GO
ALTER TABLE [dbo].[SANPHAMTHEOYEUCAU] CHECK CONSTRAINT [FK__SANPHAMTH__MAHOA__3587F3E0]
GO
ALTER TABLE [dbo].[SANPHAMTHEOYEUCAU]  WITH CHECK ADD  CONSTRAINT [FK__SANPHAMTH__MAKHA__55009F39] FOREIGN KEY([MAKHACHHANG])
REFERENCES [dbo].[KHACHHANG] ([MAKHACHHANG])
GO
ALTER TABLE [dbo].[SANPHAMTHEOYEUCAU] CHECK CONSTRAINT [FK__SANPHAMTH__MAKHA__55009F39]
GO
ALTER TABLE [dbo].[TIENDOSANXUAT]  WITH CHECK ADD  CONSTRAINT [FK__TIENDOSAN__MANHA__531856C7] FOREIGN KEY([MANHANVIENSANXUAT])
REFERENCES [dbo].[NHANVIENSANXUAT] ([MANHANVIENSANXUAT])
GO
ALTER TABLE [dbo].[TIENDOSANXUAT] CHECK CONSTRAINT [FK__TIENDOSAN__MANHA__531856C7]
GO
ALTER TABLE [dbo].[TIENDOSANXUAT]  WITH CHECK ADD  CONSTRAINT [FK__TIENDOSAN__MASAN__5224328E] FOREIGN KEY([MASANPHAMTHEOYEUCAU])
REFERENCES [dbo].[SANPHAMTHEOYEUCAU] ([MASANPHAMTHEOYEUCAU])
GO
ALTER TABLE [dbo].[TIENDOSANXUAT] CHECK CONSTRAINT [FK__TIENDOSAN__MASAN__5224328E]
GO
ALTER TABLE [dbo].[VANCHUYEN]  WITH CHECK ADD FOREIGN KEY([MACHITIETHOADON])
REFERENCES [dbo].[CHITIETHOADON] ([MACHITIETHOADON])
GO
ALTER TABLE [dbo].[VANCHUYEN]  WITH CHECK ADD  CONSTRAINT [FK__VANCHUYEN__MANHA__540C7B00] FOREIGN KEY([MANHANVIENVANCHUYEN])
REFERENCES [dbo].[NHANVIENVANCHUYEN] ([MANHANVIENVANCHUYEN])
GO
ALTER TABLE [dbo].[VANCHUYEN] CHECK CONSTRAINT [FK__VANCHUYEN__MANHA__540C7B00]
GO
ALTER TABLE [dbo].[VANCHUYEN]  WITH CHECK ADD FOREIGN KEY([MASANPHAMTHEOYEUCAU])
REFERENCES [dbo].[SANPHAMTHEOYEUCAU] ([MASANPHAMTHEOYEUCAU])
GO
