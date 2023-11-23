Create database Shop2
go
use Shop2
go

Create table PhanQuyen
(
   Idphanquyen int identity not null primary key,
   tenphanquyen nvarchar(200), 
   vaitro nvarchar(200)
)
go
CREATE TABLE Account
(
AccountID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Email VARCHAR(100) NOT NULL unique,
MatKhau VARCHAR(30) NOT NULL,
Phone nvarchar(20)  NULL,
FullName NVARCHAR(100)  NULL,
DiaChi nvarchar(300) NULL,
CreateDate DATE NULL,
IdVaiTro int,
constraint fk_phanquyen foreign key(IdVaiTro) references PhanQuyen(Idphanquyen)
)
GO
CREATE TABLE Category
(
MaDM INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
TenDM NVARCHAR(200)  NULL,
AnhDM NVARCHAR(MAX),
MoTaDM NVARCHAR(MAX)
)
GO

CREATE TABLE Magiamgia
(
Idgiamgia INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Tenvoucher nvarchar(200),
Giamgia int
)


CREATE TABLE SanPham
(
MaSP INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
MaDM INT,
TenSP NVARCHAR(200)  NULL,
AnhSP NVARCHAR(MAX)  NULL,
GiaSP decimal(18,2)  NULL,
GiaSale decimal(18,2) ,
SoLuong int,
SalePercent int,
CreateDate DATE,
NgaySua DATE,
MotaShort NVARCHAR(MAX)  NULL,
MotaDai NVARCHAR(MAX)  NULL,
SoluongReview int,
Trongluong int ,
nguyenlieu nvarchar(200)
FOREIGN KEY (MaDM) REFERENCES Category(MaDM)
)
Go
CREATE TABLE HinhAnhSanPham (
    IdHinhAnh INT NOT NULL IDENTITY(1,1),
    MaSP INT,
    DuongDan NVARCHAR(MAX),
    PRIMARY KEY (IdHinhAnh, MaSP),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
GO
CREATE TABLE FormReview
(
IdReview INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
IdSanpham INT  NULL,
tenkhachhang nvarchar(200) ,
Thongtin NVARCHAR(MAX),
FOREIGN KEY (IdSanpham) REFERENCES SanPham(MaSP)
)
GO

CREATE TABLE DonHang
(
MaDH INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
MaKH INT  NULL,
 TenKH nvarchar(200),
DiachiKH nvarchar(max),
EmailKH nvarchar(200),
SDTKH int ,
noteKH nvarchar(max),
NgayOrder DATE  NULL,
NgayThanhToan DATE  NULL,
TrangThai nvarchar(100),
TienVAT DECIMAL(18, 2) NULL, -- Assuming TienVAT is a decimal with 2 decimal places
    TienGiam DECIMAL(18, 2) NULL, -- Assuming TienGiam is a decimal with 2 decimal places
    TongTien DECIMAL(18, 2) NULL,

FOREIGN KEY (MaKH) REFERENCES Account(AccountID)
)


CREATE TABLE ChiTietDonHang
(
idchitiet INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
madonhang INT  NULL,
MaSP INT  NULL,
soluong int,
dongia decimal(18,2)
FOREIGN KEY (madonhang) REFERENCES DonHang(MaDH),
FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
)

GO
CREATE TABLE GopYKien(
	[ID] int NOT NULL PRIMARY KEY,
	HoTen nvarchar(200) ,
	Email nvarchar(200) ,
	Sdt int ,
	ChuDe nvarchar(200) ,
	DongGopY nvarchar(max)	
 )
 go
  CREATE TABLE [dbo].[ContentCategory](
    [ID] int identity NOT NULL PRIMARY KEY,
	TenDanhMuc nvarchar(200) NULL ,
	MotaDanhMuc nvarchar(max) NULL
 )
 go
CREATE TABLE Blog(
	[ID] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	 IdContent int null,
	TenBlog [nvarchar](max) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[Detail] [ntext] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nchar](250) NULL,
	[Status] [bit]  NULL,	
	[ViewCount] [int] NULL,	
	FOREIGN KEY (IdContent) REFERENCES [ContentCategory]([ID])	
)


-- Insert data into the PhanQuyen table
-- Insert data into the PhanQuyen (Roles) table
INSERT INTO PhanQuyen (tenphanquyen, vaitro)
VALUES
  (N'Admin', N'Administrator'),
  (N'User', N'General User');

-- Insert data into the Account (User Accounts) table with associated roles
INSERT INTO Account (Email, MatKhau, Phone, FullName, CreateDate, IdVaiTro)
VALUES
  (N'admin@example.com', N'admin', N'0365683018', N'Admin User', GETDATE(), 1), -- 1 corresponds to the Admin role
  (N'user1@example.com', N'user', N'1234567890', N'User One', GETDATE(), 2), -- 3 corresponds to the User role
  (N'user2@example.com', N'user', N'9876543210', N'User Two', GETDATE(), 2); -- 3 corresponds to the User role

INSERT INTO Category (TenDM, MoTaDM)
VALUES
  (N'Danh mục 1', N'Mô tả danh mục 1'),
  (N'Danh mục 2', N'Mô tả danh mục 2'),
  (N'Danh mục 3', N'Mô tả danh mục 3'),
  (N'Danh mục 4', N'Mô tả danh mục 4');
SELECT TenSP, AnhSP, GiaSale, SoLuong FROM SanPham 
 -- Thêm dữ liệu vào bảng "SanPham" với cột "SalePercent"
INSERT INTO SanPham (MaDM, TenSP, AnhSP, GiaSP, GiaSale, SoLuong, CreateDate, NgaySua, MotaShort, MotaDai, SoluongReview, Trongluong, nguyenlieu, SalePercent)
VALUES
  (1, N'Sản phẩm 1', 'img 1', 100.00, 80.00, 50, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 1', N'Mô tả dài sản phẩm 1', 10, 500, N'Nguyên liệu sản phẩm 1', 20),
  (1, N'Sản phẩm 2', 'img 2', 120.00, 100.00, 45, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 2', N'Mô tả dài sản phẩm 2', 5, 600, N'Nguyên liệu sản phẩm 2', 20),
  (2, N'Sản phẩm 3', 'img 3', 200.00, 160.00, 60, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 3', N'Mô tả dài sản phẩm 3', 15, 750, N'Nguyên liệu sản phẩm 3', 20),
  (2, N'Sản phẩm 4', 'img 4', 80.00, 64.00, 30, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 4', N'Mô tả dài sản phẩm 4', 8, 400, N'Nguyên liệu sản phẩm 4', 20),
  (3, N'Sản phẩm 5', 'img 5', 150.00, 120.00, 55, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 5', N'Mô tả dài sản phẩm 5', 12, 550, N'Nguyên liệu sản phẩm 5', 20),
  (3, N'Sản phẩm 6', 'img 6', 90.00, 72.00, 40, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 6', N'Mô tả dài sản phẩm 6', 6, 350, N'Nguyên liệu sản phẩm 6', 20),
  (4, N'Sản phẩm 7', 'img 7', 110.00, 88.00, 70, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 7', N'Mô tả dài sản phẩm 7', 14, 450, N'Nguyên liệu sản phẩm 7', 20),
  (4, N'Sản phẩm 8', 'img 8', 70.00, 56.00, 20, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 8', N'Mô tả dài sản phẩm 8', 4, 300, N'Nguyên liệu sản phẩm 8', 20),
  (1, N'Sản phẩm 9', 'img 9', 130.00, 104.00, 35, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 9', N'Mô tả dài sản phẩm 9', 9, 600, N'Nguyên liệu sản phẩm 9', 20),
  (2, N'Sản phẩm 10', 'img 10', 180.00, 144.00, 65, '2023-10-31', '2023-10-31', N'Mô tả ngắn sản phẩm 10', N'Mô tả dài sản phẩm 10', 7, 700, N'Nguyên liệu sản phẩm 10', 20);

  -- Thêm 3 mã giảm giá vào bảng "Magiamgia"
INSERT INTO Magiamgia (Tenvoucher, Giamgia)
VALUES
  (N'Giảm giá 10%', 10),  -- Mã giảm giá MGG001 giảm 10%
  (N'Giảm giá 15%', 15),  -- Mã giảm giá MGG002 giảm 15%
  (N'Giảm giá 20%', 20);  -- Mã giảm giá MGG003 giảm 20%

  -- Thêm 2 danh mục bài viết vào bảng "ContentCategory"
INSERT INTO ContentCategory (TenDanhMuc, MotaDanhMuc)
VALUES
  (N'Danh mục 1', N'Mô tả danh mục 1'),
  (N'Danh mục 2', N'Mô tả danh mục 2');
INSERT INTO Blog (IdContent, TenBlog, [Description], [Image], [Detail], CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, MetaKeywords, MetaDescriptions, [Status], ViewCount)
VALUES
  (1, N'Title 1', N'Description 1', N'Image 1', N'Content 1', GETDATE(), N'User 1', GETDATE(), N'User 1', N'Keyword 1', N'Description 1', 1, 100),
  (2, N'Title 2', N'Description 2', N'Image 2', N'Content 2', GETDATE(), N'User 2', GETDATE(), N'User 2', N'Keyword 2', N'Description 2', 1, 150),
  (1, N'Title 3', N'Description 3', N'Image 3', N'Content 3', GETDATE(), N'User 1', GETDATE(), N'User 1', N'Keyword 3', N'Description 3', 1, 120),
  (2, N'Title 4', N'Description 4', N'Image 4', N'Content 4', GETDATE(), N'User 3', GETDATE(), N'User 3', N'Keyword 4', N'Description 4', 1, 200),
  (2, N'Title 5', N'Description 5', N'Image 5', N'Content 5', GETDATE(), N'User 2', GETDATE(), N'User 2', N'Keyword 5', N'Description 5', 1, 180);
-- Insert 5 reviews for Product 1
INSERT INTO FormReview (IdSanpham, Thongtin)
VALUES
    (1, 'Review 1 for Product 1'),
    (1, 'Review 2 for Product 1'),
    (1,  'Review 3 for Product 1'),
    (1,'Review 4 for Product 1'),
    (1, 'Review 5 for Product 1'),
	 (2, 'Review 1 for Product 2'),
    (2,'Review 2 for Product 2'),
    (2, 'Review 3 for Product 2'),
    (2, 'Review 4 for Product 2'),
    (2,  'Review 5 for Product 2');
	

	
--PHẦN STORE CHO ADMIN
 --Đăng nhập kiểm tra tài khoản
GO
create PROCEDURE sp_CheckLogin
    @TaiKhoan VARCHAR(100),
    @MatKhau NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @IsAuthenticated BIT;
    IF EXISTS (
        SELECT 1
        FROM Account
        WHERE Email = @TaiKhoan
        AND MatKhau = @MatKhau
    )
    BEGIN
        SELECT 1; -- Đăng nhập thành công
    END
    ELSE
    BEGIN
        SELECT 0; -- Đăng nhập không thành công
    END
END

--exec sp_CheckLogin  'admin@example.com', 'admin'
--exec sp_CheckLogin  'user1@example.com', 'user'


--Hiển thị toàn bộ tài khoản hoặc tìm kiếm theo tên
GO
Create proc sp_GetAccount 
as
begin
  select AccountID,FullName, Email,Phone,CreateDate from  Account where IdVaiTro = 2
end
go
Create proc sp_GetAccountSearch 
@search nvarchar(100)
as
begin
  select AccountID,FullName, Email,Phone,CreateDate from  Account where FullName like N'%' + @search +'%'  and  IdVaiTro = 2
end
GO
Create proc sp_GetAccountDetail 
@AccountID int
as
begin
  select AccountID,Email,MatKhau,Phone,FullName, tenphanquyen from  Account  as a inner join PhanQuyen as b on a.IdVaiTro = b.Idphanquyen
  where IdVaiTro = 2 and AccountID = @AccountID
end
go

--Thêm 1 tài khoản từ admin
GO
CREATE PROCEDURE AddUserAccount
    @Email VARCHAR(100),
    @MatKhau VARCHAR(30),
    @Phone NVARCHAR(20),
    @FullName NVARCHAR(100),
    @IdVaiTro INT
AS
BEGIN  
        INSERT INTO Account (Email, MatKhau, Phone, FullName, CreateDate, IdVaiTro)
        VALUES (@Email, @MatKhau, @Phone, @FullName, GETDATE(), @IdVaiTro);         
END;
--Sửa tài khoản
GO
Create PROCEDURE UpdateUserAccount
    @AccountID INT,
    @MatKhau VARCHAR(30),
    @Phone NVARCHAR(20),
    @FullName NVARCHAR(100)
AS
BEGIN   
        UPDATE Account
        SET MatKhau = @MatKhau,
            Phone = @Phone,
            FullName = @FullName
        WHERE AccountID = @AccountID;
END;
--EXEC UpdateUserAccount
--    @AccountID = 1,
--    @Email = N'updated@example.com',
--    @MatKhau = N'updatedpassword',
--    @Phone = N'9876543210',
--    @FullName = N'Updated User',
--    @IdVaiTro = 2; 

--Xóa 1 tài khoản
Go
CREATE PROCEDURE sp_DeleteAccount
    @AccountID INT
AS
BEGIN
    DELETE FROM Account
    WHERE AccountID = @AccountID
END
--select * from Account

-- Hiển thị toàn bộ sản phẩm cho Trang Index
Go
Create PROCEDURE sp_GetProduct
AS
BEGIN
    SELECT
        S.MaSP,
        S.AnhSP,      
		S.TenSP,
        D.TenDM,
		S.GiaSP,
		S.GiaSale,      
		S.SoLuong,
		S.SalePercent          
    FROM SanPham S
    INNER JOIN Category D ON S.MaDM = D.MaDM
	Order By MaSP	
END
GO
--Exec sp_GetProduct
Go
create PROCEDURE sp_GetProductSearch
@search nvarchar(100)
AS
BEGIN
    SELECT
        S.MaSP,
        S.AnhSP,      
		S.TenSP,
        D.TenDM,
		S.GiaSP,
		S.GiaSale,      
		S.SoLuong,
		S.SalePercent          
    FROM SanPham S
    INNER JOIN Category D ON S.MaDM = D.MaDM
	Where TenSP like N'%' + @search +'%'
	Order By MaSP	
END
GO

--Hiển thị thông tin chi tiết 1 sản phẩm
Go
create PROCEDURE sp_GetProductItem
@masp int
AS
BEGIN
    SELECT
		S.MaSP,
		S.TenSP,
        S.AnhSP,     	
        D.TenDM,
		S.GiaSP,
		S.GiaSale,      
		S.SoLuong,
		S.SalePercent  ,
		S.CreateDate,
		S.MotaShort,
		S.MotaDai,
		S.SoluongReview,
	    S.Trongluong,
		 S.nguyenlieu       
    FROM SanPham S
    INNER JOIN Category D ON S.MaDM = D.MaDM
	where MaSP = @masp
END
GO
--Thêm 1 sản phẩm 
GO
create PROCEDURE AddProduct
    @MaDM INT,
    @TenSP NVARCHAR(200),
    @GiaSP DECIMAL(18, 2),
    @GiaSale DECIMAL(18, 2),
    @SoLuong INT,
    @SalePercent INT,   
    @MotaShort NVARCHAR(MAX),
    @MotaDai NVARCHAR(MAX),
    @Trongluong INT,
    @nguyenlieu NVARCHAR(200)
AS
BEGIN
    INSERT INTO SanPham (MaDM, TenSP, GiaSP, GiaSale, SoLuong, SalePercent,CreateDate, NgaySua, MotaShort, MotaDai, SoluongReview, Trongluong, nguyenlieu)
    VALUES (@MaDM, @TenSP, @GiaSP, @GiaSale, @SoLuong, @SalePercent,GETDATE(), GETDATE(), @MotaShort, @MotaDai, 0, @Trongluong, @nguyenlieu);
END;
Go
select * from SanPham
--Sửa 1 sản phẩm
GO
create PROCEDURE UpdateProduct
	@masp int ,
	@MaDM INT,
    @TenSP NVARCHAR(200),
    @GiaSP DECIMAL(18, 2),
    @GiaSale DECIMAL(18, 2),
    @SoLuong INT,
    @SalePercent INT,   
    @MotaShort NVARCHAR(MAX),
    @MotaDai NVARCHAR(MAX),
	@soluongreview int,
    @Trongluong INT,
    @nguyenlieu NVARCHAR(200)
AS
BEGIN
    update SanPham 
	set MaDM = @MaDM , TenSP = @TenSP , GiaSP = @GiaSP , GiaSale = @GiaSale , SoLuong = @SoLuong , SalePercent = @SalePercent , MotaShort = @MotaShort,
	MotaDai = @MotaDai , SoluongReview = @soluongreview , Trongluong = @Trongluong , nguyenlieu = @nguyenlieu , NgaySua=  GETDATE() 
	where MaSP = @masp 
END;
Go

--Xóa 1 sản phẩm
CREATE PROCEDURE DeleteProduct
    @MaSP INT
AS
BEGIN            
     DELETE FROM SanPham WHERE MaSP = @MaSP;   
END

select* from SanPham




--Thêm danh mục bài viết
GO
CREATE PROCEDURE AddContentCategory
    @TenDanhMuc NVARCHAR(200),
    @MotaDanhMuc NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ContentCategory (TenDanhMuc, MotaDanhMuc)
    VALUES (@TenDanhMuc, @MotaDanhMuc);
END;
--Cập nhật danh mục bài viết
Go
CREATE PROCEDURE UpdateContentCategory
    @ID INT,
    @TenDanhMuc NVARCHAR(200),
    @MotaDanhMuc NVARCHAR(MAX)
AS
BEGIN
        UPDATE ContentCategory
        SET TenDanhMuc = @TenDanhMuc,
            MotaDanhMuc = @MotaDanhMuc
        WHERE ID = @ID;
END;
--Xóa 1 danh mục bài viết
GO
CREATE PROCEDURE DeleteContentCategory
    @ID INT
AS
BEGIN  
      DELETE FROM ContentCategory WHERE ID = @ID;
END;

--Hiển thị toàn bộ bài viết
Go
CREATE PROCEDURE ShowAllBlog
AS
BEGIN 
        SELECT ID,Image,CreatedDate,TenBlog,CreatedBy, Description
		FROM Blog  
END;
Go

--Thêm 1 bài viết
CREATE PROCEDURE AddBlogPost
    @IdContent INT,
    @TenBlog NVARCHAR(MAX),
    @Description NVARCHAR(500),
    @Image NVARCHAR(250),
    @Detail NTEXT,
    @CreatedBy VARCHAR(50),
    @ModifiedBy VARCHAR(50),
    @MetaKeywords NVARCHAR(250),
    @MetaDescriptions NCHAR(250),
    @ViewCount INt
AS
BEGIN
 
    INSERT INTO Blog (IdContent, TenBlog, Description, Image, Detail, CreatedBy, ModifiedBy, MetaKeywords, MetaDescriptions, Status, ViewCount, CreatedDate, ModifiedDate)
    VALUES (@IdContent, @TenBlog, @Description, @Image, @Detail, @CreatedBy, @ModifiedBy, @MetaKeywords, @MetaDescriptions, 1, @ViewCount, GETDATE(), GETDATE());
END;

-- Cập nhật 1 bài viết
Go
CREATE PROCEDURE UpdateBlogPost
    @ID INT,
	@IdContent int,
    @TenBlog NVARCHAR(MAX),
    @Description NVARCHAR(500),
    @Image NVARCHAR(250),
    @Detail NTEXT,
    @ModifiedBy VARCHAR(50),
    @MetaKeywords NVARCHAR(250),
    @MetaDescriptions NCHAR(250),
    @Status BIT,
	@View int
AS
BEGIN
        UPDATE Blog
        SET
			IdContent = @IdContent,
			TenBlog = @TenBlog,
            Description = @Description,
            Image = @Image,
            Detail = @Detail,
			ModifiedDate = GETDATE(),
            ModifiedBy = @ModifiedBy,			
            MetaKeywords = @MetaKeywords,
            MetaDescriptions = @MetaDescriptions,
            Status = @Status,
            ViewCount = @View
        WHERE ID = @ID;  
END;

-- Xóa 1 bài viết
Go
CREATE PROCEDURE DeleteBlogPost
    @ID INT
AS
BEGIN
        DELETE FROM Blog WHERE ID = @ID;
END;

-- Thêm dữ liệu cho đóng góp ý kiến
GO
CREATE PROCEDURE AddFeedback
    @HoTen NVARCHAR(200),
    @Email NVARCHAR(200),
    @Sdt INT,
    @ChuDe NVARCHAR(200),
    @DongGopY NVARCHAR(MAX)
AS
BEGIN  
    INSERT INTO GopYKien (HoTen, Email, Sdt, ChuDe, DongGopY)
    VALUES (@HoTen, @Email, @Sdt, @ChuDe, @DongGopY);
END;

-- Hiển thị thông tin Review cho 1 sản phẩm
Go
CREATE PROCEDURE GetProductReviews
    @IdSanpham INT
AS
BEGIN
    SELECT *
    FROM FormReview
    WHERE IdSanpham = @IdSanpham;
END;



--Phần USER 
GO
alter PROCEDURE sp_GetProductItemUser
    @masp INT
AS
BEGIN
    SELECT
        S.MaSP,
        S.TenSP,
        S.AnhSP,     
        D.TenDM,
        S.GiaSP,
        S.GiaSale,      
        S.SoLuong,
        S.SalePercent,
        S.CreateDate,
        S.MotaShort,
        S.MotaDai,
        S.SoluongReview,
        S.Trongluong,
        S.nguyenlieu
       
    FROM SanPham S
    INNER JOIN Category D ON S.MaDM = D.MaDM
    LEFT JOIN FormReview R ON S.MaSP = R.IdSanpham
    WHERE S.MaSP =  @masp
END
GO
exec sp_GetProductItemUser 1
update FormReview set tenkhachhang = N'Thanh Sơn 5' where IdReview = 5
select IdReview,tenkhachhang,Thongtin from FormReview where IdSanpham = 1