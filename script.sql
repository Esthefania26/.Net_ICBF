USE [master]
GO
/****** Object:  Database [COPY_ICBF]    Script Date: 27/06/2024 7:09:31 p. m. ******/
CREATE DATABASE [COPY_ICBF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'COPY_ICBF_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\COPY_ICBF.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'COPY_ICBF_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\COPY_ICBF.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [COPY_ICBF] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [COPY_ICBF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [COPY_ICBF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [COPY_ICBF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [COPY_ICBF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [COPY_ICBF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [COPY_ICBF] SET ARITHABORT OFF 
GO
ALTER DATABASE [COPY_ICBF] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [COPY_ICBF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [COPY_ICBF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [COPY_ICBF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [COPY_ICBF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [COPY_ICBF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [COPY_ICBF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [COPY_ICBF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [COPY_ICBF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [COPY_ICBF] SET  ENABLE_BROKER 
GO
ALTER DATABASE [COPY_ICBF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [COPY_ICBF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [COPY_ICBF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [COPY_ICBF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [COPY_ICBF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [COPY_ICBF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [COPY_ICBF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [COPY_ICBF] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [COPY_ICBF] SET  MULTI_USER 
GO
ALTER DATABASE [COPY_ICBF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [COPY_ICBF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [COPY_ICBF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [COPY_ICBF] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [COPY_ICBF] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [COPY_ICBF] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [COPY_ICBF] SET QUERY_STORE = ON
GO
ALTER DATABASE [COPY_ICBF] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [COPY_ICBF]
GO
/****** Object:  Table [dbo].[Asistencias]    Script Date: 27/06/2024 7:09:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencias](
	[Id_Asistencia] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usuario] [int] NULL,
	[Fecha] [date] NULL,
	[Detalles] [text] NULL,
	[Id_Nino] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Asistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jardines]    Script Date: 27/06/2024 7:09:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jardines](
	[Id_Jardin] [int] IDENTITY(1,1) NOT NULL,
	[NombreJardin] [varchar](100) NULL,
	[Direccion] [varchar](200) NULL,
	[Estado] [varchar](20) NULL,
	[Id_Usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Jardin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ninos]    Script Date: 27/06/2024 7:09:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ninos](
	[Id_Nino] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Fecha] [date] NULL,
	[Sangre] [varchar](5) NULL,
	[Ciudad] [varchar](100) NULL,
	[Telefono] [varchar](20) NULL,
	[Direccion] [varchar](200) NULL,
	[EPS] [varchar](100) NULL,
	[Id_Usuario] [int] NULL,
	[Id_Jardin] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Nino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportesAcademicos]    Script Date: 27/06/2024 7:09:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportesAcademicos](
	[Id_Reporte] [int] IDENTITY(1,1) NOT NULL,
	[AnoEscolar] [int] NULL,
	[Nivel] [varchar](20) NULL,
	[Notas] [char](1) NULL,
	[Descripcion] [text] NULL,
	[FechaEntrega] [date] NULL,
	[Id_Nino] [int] NULL,
	[Id_Usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Reporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 27/06/2024 7:09:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_Rol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 27/06/2024 7:09:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](50) NULL,
	[Contrasena] [varchar](255) NULL,
	[Correo] [varchar](100) NULL,
	[Id_Rol] [int] NULL,
	[Nombre] [varchar](100) NULL,
	[Cedula] [varchar](20) NULL,
	[Telefono] [varchar](20) NULL,
	[Celular] [varchar](20) NULL,
	[Direccion] [varchar](200) NULL,
	[FechaNacimiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Asistencias] ON 

INSERT [dbo].[Asistencias] ([Id_Asistencia], [Id_Usuario], [Fecha], [Detalles], [Id_Nino]) VALUES (6, 15, CAST(N'2024-06-13' AS Date), N'Sano', 21)
INSERT [dbo].[Asistencias] ([Id_Asistencia], [Id_Usuario], [Fecha], [Detalles], [Id_Nino]) VALUES (7, 17, CAST(N'2024-06-01' AS Date), N'Decaído', 22)
INSERT [dbo].[Asistencias] ([Id_Asistencia], [Id_Usuario], [Fecha], [Detalles], [Id_Nino]) VALUES (8, 18, CAST(N'2024-06-04' AS Date), N'Sano', 23)
SET IDENTITY_INSERT [dbo].[Asistencias] OFF
GO
SET IDENTITY_INSERT [dbo].[Jardines] ON 

INSERT [dbo].[Jardines] ([Id_Jardin], [NombreJardin], [Direccion], [Estado], [Id_Usuario]) VALUES (21, N'Jardín de las Mariposas', N'Calle de los Jazmines, Nº 10', N'Activo', 16)
INSERT [dbo].[Jardines] ([Id_Jardin], [NombreJardin], [Direccion], [Estado], [Id_Usuario]) VALUES (22, N'Jardín Encantado', N'Avenida del Roble Blanco, Nº 23', N'Activo', 18)
INSERT [dbo].[Jardines] ([Id_Jardin], [NombreJardin], [Direccion], [Estado], [Id_Usuario]) VALUES (23, N'Jardín de los Aromas', N'Rincón de las Margaritas, Nº 6', N'Activo', 16)
INSERT [dbo].[Jardines] ([Id_Jardin], [NombreJardin], [Direccion], [Estado], [Id_Usuario]) VALUES (24, N'Jardín de la Tranquilidad', N'Avenida de los Girasoles, Nº 18', N'Activo', 18)
INSERT [dbo].[Jardines] ([Id_Jardin], [NombreJardin], [Direccion], [Estado], [Id_Usuario]) VALUES (25, N'Santo ', N'Calle 123', N'Activo', 16)
SET IDENTITY_INSERT [dbo].[Jardines] OFF
GO
SET IDENTITY_INSERT [dbo].[Ninos] ON 

INSERT [dbo].[Ninos] ([Id_Nino], [Nombre], [Fecha], [Sangre], [Ciudad], [Telefono], [Direccion], [EPS], [Id_Usuario], [Id_Jardin]) VALUES (21, N'Miguel', CAST(N'2020-06-01' AS Date), N'B+', N'Bogota', N'513541565', N'calle 57 # 80-60 Sur', N'Famisanar', 16, 21)
INSERT [dbo].[Ninos] ([Id_Nino], [Nombre], [Fecha], [Sangre], [Ciudad], [Telefono], [Direccion], [EPS], [Id_Usuario], [Id_Jardin]) VALUES (22, N'Laura', CAST(N'2019-06-13' AS Date), N'O+', N'Bogota', N'84466515', N'calle 90 # 80-60 Norte', N'Compensar', 17, 23)
INSERT [dbo].[Ninos] ([Id_Nino], [Nombre], [Fecha], [Sangre], [Ciudad], [Telefono], [Direccion], [EPS], [Id_Usuario], [Id_Jardin]) VALUES (23, N'Andres', CAST(N'2021-06-01' AS Date), N'O+', N'Bogota', N'56132231', N'calle 57 # 80-60 Sur', N'EPS SURA', 18, 22)
SET IDENTITY_INSERT [dbo].[Ninos] OFF
GO
SET IDENTITY_INSERT [dbo].[ReportesAcademicos] ON 

INSERT [dbo].[ReportesAcademicos] ([Id_Reporte], [AnoEscolar], [Nivel], [Notas], [Descripcion], [FechaEntrega], [Id_Nino], [Id_Usuario]) VALUES (4, 2024, N'Jardín', N'A', N'Rendimiento Académico Bueno', CAST(N'2024-06-30' AS Date), 21, 15)
INSERT [dbo].[ReportesAcademicos] ([Id_Reporte], [AnoEscolar], [Nivel], [Notas], [Descripcion], [FechaEntrega], [Id_Nino], [Id_Usuario]) VALUES (5, 2024, N'Pre-jardín', N'S', N'Su desempeño fue excelente sigue así ', CAST(N'2024-06-30' AS Date), 22, 16)
INSERT [dbo].[ReportesAcademicos] ([Id_Reporte], [AnoEscolar], [Nivel], [Notas], [Descripcion], [FechaEntrega], [Id_Nino], [Id_Usuario]) VALUES (6, 2024, N'Prenatal', N'A', N'No rindió como se esperaba, sigue intentando', CAST(N'2024-06-30' AS Date), 23, 17)
SET IDENTITY_INSERT [dbo].[ReportesAcademicos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id_Rol], [NombreRol]) VALUES (2, N'Acudiente')
INSERT [dbo].[Roles] ([Id_Rol], [NombreRol]) VALUES (1, N'Administrador')
INSERT [dbo].[Roles] ([Id_Rol], [NombreRol]) VALUES (3, N'Madre Comunitaria')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id_Usuario], [NombreUsuario], [Contrasena], [Correo], [Id_Rol], [Nombre], [Cedula], [Telefono], [Celular], [Direccion], [FechaNacimiento]) VALUES (15, N'Juan Sebastian Madrid', N'7894561', N'Juan@gmail.com', 2, NULL, N'321456989', N'2548782', N'322145682', N'calle 90 # 80-60 Norte', CAST(N'2001-03-20' AS Date))
INSERT [dbo].[Usuarios] ([Id_Usuario], [NombreUsuario], [Contrasena], [Correo], [Id_Rol], [Nombre], [Cedula], [Telefono], [Celular], [Direccion], [FechaNacimiento]) VALUES (16, N'M-Esthefania Narvaez', N'32165494', N'estefa@gmail.com', 3, NULL, N'98565322', N'258972', N'31125856', N'calle 100 # 89-60 Sur', CAST(N'2003-11-20' AS Date))
INSERT [dbo].[Usuarios] ([Id_Usuario], [NombreUsuario], [Contrasena], [Correo], [Id_Rol], [Nombre], [Cedula], [Telefono], [Celular], [Direccion], [FechaNacimiento]) VALUES (17, N'Karen Martinez', N'521328465', N'karen@gmail.com', 2, NULL, N'25682512', N'4865132', N'65445131', N'calle 80# 89-60 Sur', CAST(N'2002-02-20' AS Date))
INSERT [dbo].[Usuarios] ([Id_Usuario], [NombreUsuario], [Contrasena], [Correo], [Id_Rol], [Nombre], [Cedula], [Telefono], [Celular], [Direccion], [FechaNacimiento]) VALUES (18, N'Sofia Hernandez', N'6532541', N'sofia@gmail.com', 3, NULL, N'8944651', N'4451515', N'468516', N'calle 50# 80-60 Norte', CAST(N'2001-06-20' AS Date))
INSERT [dbo].[Usuarios] ([Id_Usuario], [NombreUsuario], [Contrasena], [Correo], [Id_Rol], [Nombre], [Cedula], [Telefono], [Celular], [Direccion], [FechaNacimiento]) VALUES (19, N'yudis enith', N'33434', N'enithy@gmail.com', 2, NULL, N'2363434', N'434231', N'2323', N'usaquen', CAST(N'1987-06-15' AS Date))
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__4F0B537FA2BD994A]    Script Date: 27/06/2024 7:09:32 p. m. ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[NombreRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__B4ADFE38B08BD2E5]    Script Date: 27/06/2024 7:09:32 p. m. ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asistencias]  WITH CHECK ADD FOREIGN KEY([Id_Nino])
REFERENCES [dbo].[Ninos] ([Id_Nino])
GO
ALTER TABLE [dbo].[Asistencias]  WITH CHECK ADD FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Jardines]  WITH CHECK ADD FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD FOREIGN KEY([Id_Jardin])
REFERENCES [dbo].[Jardines] ([Id_Jardin])
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[ReportesAcademicos]  WITH CHECK ADD FOREIGN KEY([Id_Nino])
REFERENCES [dbo].[Ninos] ([Id_Nino])
GO
ALTER TABLE [dbo].[ReportesAcademicos]  WITH CHECK ADD FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([Id_Rol])
REFERENCES [dbo].[Roles] ([Id_Rol])
GO
USE [master]
GO
ALTER DATABASE [COPY_ICBF] SET  READ_WRITE 
GO
