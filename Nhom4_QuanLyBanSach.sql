
-- Kiểm tra xem database có tồn tại hay không
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_BANSACH')
BEGIN
    -- Đóng tất cả các kết nối đến database (nếu có)
    ALTER DATABASE QL_BANSACH SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    -- Xóa database
    DROP DATABASE QL_BANSACH;
END
-- Tạo database mới
CREATE DATABASE QL_BANSACH;

go

USE QL_BANSACH;

-- Thông báo database đã được tạo thành công
PRINT N'Database QL_BANSACH đã tạo thành công.';

-- Bảng Sách
CREATE TABLE Sach (
    id VARCHAR(50) PRIMARY KEY,
    tieude NVARCHAR(255),
	theloai_id VARCHAR(50),
    gia DECIMAL(10, 2),
    soLuongTon INT,
    hinhAnh VARCHAR(max),
    namXuatBan INT,
	MoTa NVARCHAR(max),
    nxb_id VARCHAR(50)
);
-- Bảng account
CREATE TABLE NguoiDung (
    id VARCHAR(50) PRIMARY KEY,
    username NVARCHAR(50) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL, -- Mật khẩu (nên được mã hóa)
    role VARCHAR(10) NOT NULL -- Ví dụ: 'admin', 'staff', 'customer'
);


-- Bảng Khách hàng
CREATE TABLE Khachhang (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    diachi NVARCHAR(255),
    sodienthoai VARCHAR(10),
    email VARCHAR(255),
	id_NguoiDung VARCHAR(50) 
);
-- Bảng Đơn đặt hàng
CREATE TABLE DonHang (
    id VARCHAR(50) PRIMARY KEY,
    nguoidung_id VARCHAR(50) null,
    trangthaiDH int DEFAULT 0,
    ngayDatHang DATETIME,
    tongTien DECIMAL(10, 2),-- So luong * DonGia
);

-- Bảng Chi tiết đơn hàng
CREATE TABLE ChiTietDonHang (
    id VARCHAR(50) PRIMARY KEY,
    donhang_id VARCHAR(50),
    sach_id VARCHAR(50),
    soLuong INT,
    giaDonVi DECIMAL(10, 2)
);

-- Bảng Thể loại
CREATE TABLE TheLoai (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255)
);

-- Bảng Nhà xuất bản
CREATE TABLE NhaXuatBan (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    diachi NVARCHAR(255),
    sodienthoai VARCHAR(10),
    email VARCHAR(255)
);

-- Bảng Tác giả
CREATE TABLE TacGia (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    ngaySinh DATE,
    quocTich NVARCHAR(255)
);

-- Bảng Nhân viên
CREATE TABLE NhanVien (
    id VARCHAR(50) PRIMARY KEY,
    ten NVARCHAR(255),
    chucVu NVARCHAR(255),
    sodienthoai VARCHAR(10),
    email VARCHAR(255),
	id_NguoiDung VARCHAR(50) 
);

CREATE TABLE HoaDon (
    id VARCHAR(50) PRIMARY KEY, 
	donhang_id VARCHAR(50) UNIQUE,
    ngayLap DATETIME,          
    tongTien DECIMAL(10, 2)   , 
	phuongThucTT nvarchar(50),
	trangthaiTT nvarchar(50)
);





go
-- Bảng Tác giả - Sách (nhiều - nhiều)
CREATE TABLE TacGia_Sach (
    sach_id VARCHAR(50),
    tacgia_id VARCHAR(50),
    PRIMARY KEY (sach_id, tacgia_id)
);
-- thêm khóa ngoại cho bảng NguoiDung
ALTER TABLE Khachhang
ADD CONSTRAINT FK_Khachhang_NguoiDung
FOREIGN KEY (id_NguoiDung) REFERENCES NguoiDung(id);

ALTER TABLE NhanVien
ADD CONSTRAINT FK_NhanVien_NguoiDung
FOREIGN KEY (id_NguoiDung) REFERENCES NguoiDung(id);
-- thêm

go
ALTER TABLE HoaDon
ADD CONSTRAINT FK_HoaDon_DonHang FOREIGN KEY (donhang_id) REFERENCES DonHang(id);
go


-- Thêm khóa ngoại cho bảng Sách (thể loại và nhà xuất bản)
ALTER TABLE Sach
ADD CONSTRAINT FK_Sach_TheLoai FOREIGN KEY (theloai_id) REFERENCES TheLoai(id),
    CONSTRAINT FK_Sach_NhaXuatBan FOREIGN KEY (nxb_id) REFERENCES NhaXuatBan(id);
go
-- Thêm khóa ngoại cho bảng Đơn hàng (khách hàng và nhân viên)
ALTER TABLE DonHang
ADD CONSTRAINT FK_DonHang_NguoiDung FOREIGN KEY (nguoidung_id) REFERENCES NguoiDung(id);
go
-- Thêm khóa ngoại cho bảng Chi tiết đơn hàng (sách và đơn hàng)
ALTER TABLE ChiTietDonHang
ADD CONSTRAINT FK_ChiTietDonHang_Sach FOREIGN KEY (sach_id) REFERENCES Sach(id),
    CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (donhang_id) REFERENCES DonHang(id);
go

-- Thêm khóa ngoại cho bảng Tác giả - Sách (tác giả và sách)
ALTER TABLE TacGia_Sach
ADD CONSTRAINT FK_TacGia_Sach_Sach FOREIGN KEY (sach_id) REFERENCES Sach(id),
    CONSTRAINT FK_TacGia_Sach_TacGia FOREIGN KEY (tacgia_id) REFERENCES TacGia(id);

-- Thêm dữ liệu cho các bảng

-- Thêm dữ liệu vào bảng Nhà xuất bản
INSERT INTO NhaXuatBan (id, ten, diachi, sodienthoai, email) VALUES
('NXB001', N'Kim Đồng', N'Hà Nội', '0912345678', 'kimdong@email.com'),
('NXB002', N'Trẻ', N'TP.HCM', '0987654321', 'tre@email.com'),
('NXB003', N'Giáo dục', N'Hà Nội', '0901234567', 'giaoduc@email.com'),
('NXB004', N'Văn học', N'Hà Nội', '0976543210', 'vanhoc@email.com'),
('NXB005', N'Lao động', N'Hà Nội', '0998765432', 'laodong@email.com'),
('NXB006', N'Nhã Nam', N'Hà Nội', '0912345679', 'nhanam@email.com'),
('NXB007', N'Phụ nữ', N'TP.HCM', '0987654322', 'phunu@email.com'),
('NXB008', N'Hồng Đức', N'Hà Nội', '0901234568', 'hongduc@email.com'),
('NXB009', N'Thanh Niên', N'Hà Nội', '0976543211', 'thanhnien@email.com'),
('NXB010', N'Công an nhân dân', N'Hà Nội', '0998765433', 'congan@email.com'),
('NXB011', N'Quốc gia', N'Hà Nội', '0905123456', 'quocgia@email.com'),
('NXB012', N'Thế giới', N'TP.HCM', '0918273645', 'thegioi@email.com'),
('NXB013', N'Hội nhà văn', N'Hà Nội', '0921354678', 'hoinhavan@email.com'),
('NXB014', N'Tri thức', N'TP.HCM', '0927485963', 'trithuc@email.com'),
('NXB015', N'Đại học quốc gia', N'Hà Nội', '0932567890', 'daihoc@email.com');

-- Thêm dữ liệu vào bảng Thể loại
INSERT INTO TheLoai (id, ten) VALUES
('TL001', N'Truyện tranh'),
('TL002', N'Tiểu thuyết'),
('TL003', N'Sách giáo khoa'),
('TL004', N'Khoa học'),
('TL005', N'Lịch sử'),
('TL006', N'Tâm lý'),
('TL007', N'Kinh dị'),
('TL008', N'Phiêu lưu'),
('TL009', N'Lãng mạn'),
('TL010', N'Trinh thám'),
('TL011', N'Kiếm hiệp'),
('TL012', N'Giả tưởng'),
('TL013', N'Ngôn tình'),
('TL014', N'Du lịch'),
('TL015', N'Sức khỏe');

-- Thêm dữ liệu vào bảng Tác giả
INSERT INTO TacGia (id, ten, ngaySinh, quocTich) VALUES
('TG001', N'Tô Hoài', '1920-09-27', N'Việt Nam'),
('TG002', N'Nguyễn Nhật Ánh', '1955-05-07', N'Việt Nam'),
('TG003', N'J.K. Rowling', '1965-07-31', N'Anh'),
('TG004', N'Stephen King', '1947-09-21', N'Mỹ'),
('TG005', N'Haruki Murakami', '1949-01-12', N'Nhật Bản'),
('TG006', N'Paulo Coelho', '1947-08-24', N'Brazil'),
('TG007', N'Dan Brown', '1964-06-22', N'Mỹ'),
('TG008', N'J.R.R. Tolkien', '1892-01-03', N'Anh'),
('TG009', N'Jane Austen', '1775-12-16', N'Anh'),
('TG010', N'Agatha Christie', '1890-09-15', N'Anh'),
('TG011', N'Kim Dung', '1924-03-10', N'Trung Quốc'),
('TG012', N'George R.R. Martin', '1948-09-20', N'Mỹ'),
('TG013', N'Nicholas Sparks', '1965-12-31', N'Mỹ'),
('TG014', N'Lonely Planet', NULL, NULL),
('TG015', N'Adam Fawer', '1970-05-17', N'Mỹ');

-- Thêm dữ liệu vào bảng Sách với mô tả đầy đủ
INSERT INTO Sach (id, tieude, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id, moTa) VALUES
('S001', N'Dế Mèn Phiêu Lưu Ký', 'TL001', 50000, 100, 'https://example.com/images/book1.jpg', 1941, 'NXB001', N'Câu chuyện về một chú dế mèn đáng yêu và những cuộc phiêu lưu của nó trong thế giới tự nhiên.'),
('S002', N'Kính vạn hoa', 'TL002', 75000, 50, 'https://example.com/images/book2.jpg', 1995, 'NXB002', N'Một cuốn sách tuyệt vời với những hình ảnh đẹp và những câu chuyện thú vị.'),
('S003', N'Harry Potter và Hòn đá Phù thủy', 'TL002', 100000, 200, 'https://example.com/images/book3.jpg', 1997, 'NXB003', N'Câu chuyện về một cậu bé phù thủy tên là Harry Potter và những cuộc phiêu lưu kỳ diệu của cậu ấy tại trường Hogwarts.'),
('S004', N'It',  'TL004', 80000, 30, 'https://example.com/images/book4.jpg', 1986, 'NXB004', N'Một câu chuyện kinh dị đáng sợ và hấp dẫn về một nhóm thiếu niên đối mặt với một thực thể đáng sợ.'),
('S005', N'Rừng Na Uy', 'TL002', 90000, 70, 'https://example.com/images/book5.jpg', 1987, 'NXB005', N'Câu chuyện về cuộc sống của con người trong một khung cảnh thiên nhiên tuyệt đẹp.'),
('S006', N'Nhà giả kim',  'TL006', 60000, 150, 'https://example.com/images/book6.jpg', 1988, 'NXB006', N'Một cuốn sách triết lý về cuộc hành trình tìm kiếm hạnh phúc.');



INSERT INTO NguoiDung (id, username, password, role) VALUES
('user123', N'user123', '12345', 'customer'),
('user456', N'user456', '12345', 'customer'),
('user789', N'user789', '12345', 'customer'),

('admin123', N'admin123', '12345', 'admin'),

('staff123', N'staff123', '12345', 'staff'),
('staff456', N'staff456', '12345', 'staff'),
('staff789', N'staff789', '12345', 'staff');
-- Chèn dữ liệu vào bảng Khachhang
INSERT INTO Khachhang (id, ten, diachi, sodienthoai, email, id_NguoiDung) VALUES
('KH001', N'Nguyễn Văn A', N'123 Đường Trần Hưng Đạo, Hà Nội', '0987654321', 'nguyenvana@example.com', 'user123'),
('KH002', N'Trần Thị B', N'456 Nguyễn Du, Hồ Chí Minh', '0912345678', 'tranthib@example.com', 'user456'),
('KH003', N'Lê Văn C', N'789 Lê Thánh Tôn, Đà Nẵng', '0789456123', 'levanc@example.com', 'user789');

-- Chèn dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (id, ten, chucVu, sodienthoai, email, id_NguoiDung) VALUES
('NV001', N'Phạm Văn D', N'Quản lý', '0901234567', 'phamvand@example.com', 'staff123'),
('NV002', N'Huỳnh Thị E', N'Nhân viên kinh doanh', '0936547890', 'huynhthie@example.com', 'staff456'),
('NV003', N'Đỗ Văn F', N'Nhân viên kỹ thuật', '0775552211', 'dovanf@example.com', 'staff789'),
('NV004', N'Nguyễn Văn G', N'Admin', '0989123456', 'nguyenvang@example.com', 'admin123');
-- Thêm dữ liệu vào bảng Đơn đặt hàng
INSERT INTO DonHang (id, nguoidung_id, trangthaiDH, ngayDatHang, tongTien) VALUES
('DH001', 'user123',0, '2023-03-01', 150000),
('DH002', 'user123', 1, '2023-03-05', 75000),
('DH003', 'admin123', 0, '2023-03-10', 180000),
('DH004', null, 0, '2023-04-12', 110000);


-- Thêm dữ liệu vào bảng Chi tiết đơn hàng
INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi) VALUES
('CTDH001', 'DH001', 'S001', 2, 50000),
('CTDH002', 'DH001', 'S002', 1, 75000),
('CTDH003', 'DH002', 'S003', 3, 100000),
('CTDH004', 'DH003', 'S004', 2, 80000),
('CTDH005', 'DH003', 'S005', 1, 90000);


-- Thêm dữ liệu vào bảng Đặt trước

-- Thêm dữ liệu vào bảng Tác giả - Sách
INSERT INTO TacGia_Sach (sach_id, tacgia_id) VALUES
('S001', 'TG001'),
('S002', 'TG002'),
('S003', 'TG003'),
('S004', 'TG004'),
('S005', 'TG005'),
('S006', 'TG006');
-- TRIGGER START
-- INSTEAD OF trigger ngăn chặn hành động trước khi nó xảy ra. AFTER (hoặc FOR) trigger thực hiện kiểm tra sau khi hành động đã diễn ra. 

--1 Trigger kiểm tra khi thêm sách mới có năm xuất bản lớn hơn 1940 và giá sách lớn hơn 0
CREATE TRIGGER TR_KiemTraSachMoi
ON Sach
INSTEAD OF INSERT
AS
BEGIN
    -- Kiểm tra điều kiện cho năm xuất bản và giá sách
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE namXuatBan <= 1940 OR gia <= 0
    )
    BEGIN
        -- Nếu điều kiện không thỏa, in thông báo lỗi và không thêm sách
        RAISERROR(N'Năm xuất bản phải lớn hơn 1940 và giá sách phải lớn hơn 0. Vui lòng nhập giá trị phù hợp ^ v ^', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Thêm sách mới nếu các điều kiện được thỏa mãn
        INSERT INTO Sach(id, tieude, isbn, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id)
        SELECT id, tieude, isbn, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id
        FROM inserted;
    END
END;

INSERT INTO Sach (id, tieude, isbn, theloai_id, gia, soLuongTon, hinhAnh, namXuatBan, nxb_id)
VALUES ('S0017', N'Sách B', '978-1234567890', 'TL001', -1000, 50, 'image.jpg', 1930, 'NXB001');
--2 Trigger kiểm tra email của Khachhang được cập nhật
create TRIGGER TR_KiemTraEmailKH
ON KhachHang
FOR UPDATE
AS
BEGIN
    IF UPDATE(email)
    BEGIN
        DECLARE @newEmail VARCHAR(255), @count int;
        SELECT @newEmail = email FROM inserted;

        -- Kiểm tra định dạng email hợp lệ
        IF NOT EXISTS (SELECT 1 FROM inserted WHERE email LIKE '%_@__%.__%')
        BEGIN
            print'email khong khong dung cu phap'
            ROLLBACK TRANSACTION;
            RETURN;
        END;
		set @count=(select count(*) from KhachHang where email=@newEmail)
		if (@count>1)
		begin
		print'Email da ton tai'
		ROLLBACK TRANSACTION;
        RETURN;
		end
		 end
END


SELECT * FROM Khachhang
UPDATE Khachhang 
SET	 email='nguyenvana@email.com'
WHERE id='KH002'
--3 Trigger Ngăn Chặn Xóa Sách Đã Được Đặt Hàng 
CREATE TRIGGER TR_KiemTraXoaSach
ON Sach
INSTEAD OF DELETE
AS
BEGIN
    -- Kiểm tra xem sách có xuất hiện trong bảng ChiTietDonHang không
    IF EXISTS (
        SELECT 1
        FROM deleted d
        INNER JOIN ChiTietDonHang ctdh ON d.id = ctdh.sach_id
    )
    BEGIN
        -- Nếu sách đã từng được đặt hàng, in thông báo lỗi và ngăn thao tác xóa
        RAISERROR(N'Sách đã được đặt hàng và không thể xóa.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Nếu sách chưa được đặt hàng, cho phép xóa sách khỏi bảng Sach
        DELETE FROM Sach
        WHERE id IN (SELECT id FROM deleted);
    END
END;


SELECT * FROM ChiTietDonHang

DROP TRIGGER TR_KiemTraXoaSach ON ALL SERVER
DELETE FROM Sach WHERE id = 'S002';
--4 Trigger xóa chi tiết đơn hàng khi xóa đơn hàng 
CREATE TRIGGER TR_XoaCTDHKhiXoaDH
ON DonHang
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        DELETE FROM ChiTietDonHang
        WHERE donhang_id IN (SELECT id FROM deleted);
		DELETE FROM DonHang WHERE id IN (SELECT id FROM deleted);
    END
END;

SELECT * FROM DonHang
SELECT * FROM ChiTietDonHang
DELETE FROM DonHang WHERE DonHang.id='DH001'
--5 Trigger tự động cập nhật tổng tiền khi thay đổi số lượng sách(thêm xóa sửa) trong chi tiết đơn hang (Dùng if update)

CREATE TRIGGER TR_UpdateTongTienDonHang
ON ChiTietDonHang
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Khai báo biến để lưu giá trị tổng tiền
    DECLARE @donhang_id VARCHAR(50);

    -- Kiểm tra trường hợp INSERT hoặc UPDATE
    IF (UPDATE(soLuong))
    BEGIN
        -- Lấy mã đơn hàng từ bảng inserted (dữ liệu sau khi thay đổi)
        SELECT @donhang_id = i.donhang_id
        FROM inserted i;

        -- Cập nhật tổng tiền của đơn hàng dựa trên tổng số tiền trong chi tiết đơn hàng
        UPDATE DonHang
        SET tongTien = (
            SELECT SUM(c.soLuong * c.giaDonVi)
            FROM ChiTietDonHang c
            WHERE c.donhang_id = @donhang_id
        )
        WHERE id = @donhang_id;
    END

    -- Kiểm tra trường hợp DELETE
    IF NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Lấy mã đơn hàng từ bảng deleted (dữ liệu trước khi xóa)
        SELECT @donhang_id = d.donhang_id
        FROM deleted d;

        -- Cập nhật tổng tiền của đơn hàng dựa trên tổng số tiền trong chi tiết đơn hàng
        UPDATE DonHang
        SET tongTien = (
            SELECT SUM(c.soLuong * c.giaDonVi)
            FROM ChiTietDonHang c
            WHERE c.donhang_id = @donhang_id
        )
        WHERE id = @donhang_id;
    END
END;
GO
--test
SELECT * FROM DonHang WHERE id = 'DH001';
SELECT * FROM ChiTietDonHang WHERE ChiTietDonHang.donhang_id = 'DH001';


INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi)
VALUES ('CT001', 'DH001', 'S001', 2, 150000);

UPDATE ChiTietDonHang
SET soLuong = 3
WHERE id = 'CT001';

DELETE FROM ChiTietDonHang
WHERE id = 'CT001';

--update donhang_id sach_id
-- update hoadon trong bai 1
-- update hoa don - donhang 11
--6 Trigger kiểm tra số lượng tồn kho trước khi thêm/cập nhật Chi tiết đơn hàng (Ngăn chặn việc bán hàng vượt quá số lượng tồn kho)
-- và cập nhật số lượng tồn kho trong Sách khi thêm/xóa/sửa Chi tiết đơn hàng


CREATE TRIGGER TR_QuanLySoLuongTon
ON ChiTietDonHang
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Kiểm tra số lượng tồn cho INSERT và UPDATE
    IF EXISTS(SELECT 1 FROM inserted i JOIN Sach s ON i.sach_id = s.id WHERE i.soLuong > s.soLuongTon) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        RAISERROR(N'Số lượng đặt mua vượt quá số lượng tồn kho.', 16, 1);
        ROLLBACK TRANSACTION;
    END

    -- Xử lý trường hợp INSERT
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon - i.soLuong
        FROM Sach s
        JOIN inserted i ON s.id = i.sach_id;
    END

    -- Xử lý trường hợp UPDATE
    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon + d.soLuong - i.soLuong
        FROM Sach s
        JOIN inserted i ON s.id = i.sach_id
        JOIN deleted d ON s.id = d.sach_id;
    END

    -- Xử lý trường hợp DELETE
    IF EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon + d.soLuong
        FROM Sach s
        JOIN deleted d ON s.id = d.sach_id;
    END
END;

--TEST TRIGGER

-- Xem số lượng tồn kho hiện tại của sách 'S001':
SELECT soLuongTon FROM Sach WHERE id = 'S001';
-- Kết quả: 100

-- Thêm một chi tiết đơn hàng mới cho sách 'S001' với số lượng 50:
INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi)
VALUES ('CTDH016', 'DH001', 'S001', 50, 50000);

-- Kiểm tra lại số lượng tồn kho của sách 'S001' sau khi bán 50 cuốn
SELECT soLuongTon FROM Sach WHERE id = 'S001';
-- Kết quả: 50

--Thử thêm một chi tiết đơn hàng mới với số lượng vượt quá số lượng tồn (60):
INSERT INTO ChiTietDonHang (id, donhang_id, sach_id, soLuong, giaDonVi)
VALUES ('CTDH017', 'DH001', 'S001', 60, 50000);
-- => Hiện thông báo lỗi "Số lượng đặt mua vượt quá số lượng tồn kho."

 SELECT id,soLuongTon FROM Sach WHERE id = 'S001';
UPDATE ChiTietDonHang set soLuong= 80 WHERE ChiTietDonHang.id='CTDH016'

DELETE FROM ChiTietDonHang WHERE ChiTietDonHang.id='CTDH016'
--7 Trigger tự động tạo Hóa đơn khi Đơn hàng chuyển sang trạng thái "Đã hoàn thành":

CREATE TRIGGER TR_TaoHoaDonTuDong
ON DonHang
AFTER UPDATE
AS
BEGIN
    -- Kiểm tra xem có đơn hàng nào chuyển sang trạng thái "Đã hoàn thành" hay không
    IF UPDATE(trangthaiDH) AND EXISTS (SELECT 1 FROM inserted WHERE trangthaiDH =1)
    BEGIN
        -- Duyệt qua các đơn hàng vừa được cập nhật trạng thái
        INSERT INTO HoaDon (id,donhang_id, ngayLap, tongTien, phuongThucTT, trangthaiTT)
        SELECT NEWID(),i.id, GETDATE(), i.tongTien, N'Tiền mặt', N'Chưa thanh toán'
        FROM inserted i
        WHERE i.trangthaiDH = 1 AND NOT EXISTS (
            SELECT 1 FROM HoaDon WHERE donhang_id = i.id
        );
    END;
END;

-- TEST TRIGGER 
SELECT * FROM HOADON
-- tạo hóa đơn cho đơn hàng có mã 'DH012'
UPDATE DonHang
SET trangthaiDH = 1
WHERE id = 'DH012';
-- tự động tạo hóa đơn với đơn hàng id là 'DH012'
SELECT * FROM HoaDon WHERE donhang_id = 'DH012';

-- Xem trigger trên bảng
exec sp_helptrigger ChiTietDonHang


-- Xem nội dung trigger
exec sp_helptext TR_QuanLySoLuongTon
-- Xóa trigger
DROP TRIGGER TR_TaoHoaDonTuDong;
-- Sửa trigger
alter TRIGGER TR_KiemTraEmailKH
ON KhachHang
AFTER UPDATE
AS
BEGIN
    IF UPDATE(email)
    BEGIN
        DECLARE @newEmail VARCHAR(255);
        SELECT @newEmail = email FROM inserted;

        -- Kiểm tra định dạng email hợp lệ
        IF NOT EXISTS (SELECT 1 FROM inserted WHERE email LIKE '%_@__%.__%')
        BEGIN
            print'email khong khong dung cu phap'
            ROLLBACK TRANSACTION;
            RETURN;
        END;

		 IF EXISTS (
        SELECT 1
        FROM Khachhang AS k
        INNER JOIN inserted AS i
            ON k.email = i.email
        WHERE k.id <> i.id
    )
    BEGIN
        -- Nếu có email trùng, hiển thị thông báo lỗi và rollback
        print'Email đa ton tai. Vui long nhap email khac.'
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Nếu email không trùng, thực hiện cập nhật.
        UPDATE Khachhang
        SET ten = i.ten,
            diachi = i.diachi,
            sodienthoai = i.sodienthoai,
			email = i.email
        FROM inserted AS i
        WHERE Khachhang.id = i.id;
    END
        -- Kiểm tra email có trùng lặp không (trừ bản ghi đang cập nhật)
    END
END

-- Vô hiệu hóa trigger

DISABLE TRIGGER TR_KiemTraEmailKH ON KhachHang;
-- Kích hoạt lại trigger

ENABLE TRIGGER TR_KiemTraEmailKH ON KhachHang;





-- TRIGGER END

-- Khai báo biến cục bộ
DECLARE @bien1 INT;
DECLARE @bien2 NVARCHAR(50);

-- Gán giá trị cho biến
SET @bien1 = 10;
SET @bien2 = 'Hello SQL';

-- In ra giá trị của biến
PRINT @bien1;
PRINT @bien2;

-- Sử dụng biến toàn cục
SELECT @@VERSION AS 'Phiên bản SQL Server';
SELECT @@LANGUAGE AS 'Ngôn ngữ hiện tại';

-- Cấu trúc If...Else
DECLARE @age INT = 20;

IF @age >= 18
    PRINT 'Người lớn';
ELSE
    PRINT 'Trẻ em';

-- Cấu trúc If Exists
IF EXISTS (SELECT * FROM Khachhang WHERE ten = 'Nguyen Van A')
    PRINT 'Khách hàng tồn tại';
ELSE
    PRINT 'Khách hàng không tồn tại';


	-- Cấu trúc Case...When trong câu SELECT
SELECT tieude,
    CASE
        WHEN gia >= 500000 THEN 'Đắt'
        WHEN gia >= 200000 THEN 'Trung bình'
        ELSE 'Rẻ'
    END AS 'Phân loại giá'
FROM Sach;


-- Hàm thống kê
SELECT 
    SUM(gia) AS TongGia, 
    AVG(gia) AS GiaTrungBinh, 
    COUNT(*) AS TongSoSach, 
    MAX(gia) AS GiaCaoNhat, 
    MIN(gia) AS GiaThapNhat
FROM Sach;


-- Hàm xử lý chuỗi
SELECT 
    LEN(ten) AS 'Độ dài tên', 
    UPPER(ten) AS 'Viết hoa', 
    LOWER(ten) AS 'Viết thường', 
    LEFT(ten, 3) AS 'Lấy 3 ký tự đầu', 
    RIGHT(ten, 3) AS 'Lấy 3 ký tự cuối',
    REPLACE(ten, 'a', 'A') AS 'Thay thế a bằng A'
FROM Khachhang;


-- Hàm toán học
SELECT 
    ABS(-10) AS 'Giá trị tuyệt đối',
    POWER(2, 3) AS 'Lũy thừa 2^3',
    SQRT(16) AS 'Căn bậc hai của 16',
    ROUND(123.456, 2) AS 'Làm tròn đến 2 chữ số thập phân';


	-- Hàm xử lý ngày giờ
SELECT 
    GETDATE() AS 'Ngày giờ hiện tại',
    YEAR(GETDATE()) AS 'Năm hiện tại',
    MONTH(GETDATE()) AS 'Tháng hiện tại',
    DAY(GETDATE()) AS 'Ngày hiện tại',
    DATEADD(DAY, 7, GETDATE()) AS 'Ngày sau 7 ngày',
    DATEDIFF(DAY, '2024-01-01', GETDATE()) AS 'Số ngày đã trôi qua trong năm';


	go


SELECT COUNT(maphieunhap) AS mangay
FROM PHIEUNHAP
WHERE CAST(ngaynhap AS DATE) = CAST(GETDATE() AS DATE);

SELECT COUNT(maphieunhap) AS mangay
FROM PHIEUNHAP
WHERE CONVERT(DATE, ngaynhap) = CONVERT(DATE, GETDATE());

SELECT COUNT(maphieunhap) AS mangay
FROM PHIEUNHAP
WHERE DATEDIFF(DAY, ngaynhap, GETDATE()) = 0;



-- PROC
-- Dang Nhap
-- STORE PROC LẤY TẤT CẢ THỂ LOẠI
CREATE or alter PROCEDURE SP_Book_GetAllType
AS
BEGIN
	select *
	from TheLoai
END
GO
-- STORE PROC LẤY THỂ LOẠI theo id
CREATE or alter PROCEDURE SP_LayThongTinTheLoai @idTL VARCHAR(10)
AS
BEGIN
	select *
	from TheLoai tl
	where tl.id=@idTL
END
GO

CREATE or alter PROCEDURE  SP_LayTrangThaiDonHang  @TrangThai int,@idND VARCHAR(50)
as
begin
select * from DonHang dh  where  dh.trangthaiDH=@TrangThai AND @idND =dh.nguoidung_id
end

exec SP_LayTrangThaiDonHang 0, 'user123'
--  Lấy Chi Tiết Đơn Hàng Theo Đơn Hàng ID

CREATE OR ALTER PROCEDURE SP_LayChiTietDonHangTheoDH
	@OrderID varchar(50)

AS
BEGIN
	SELECT *
	FROM ChiTietDonHang ct
	WHERE ct.donhang_id = @OrderID
END


--  Lấy Chi Tiết Đơn Hàng Theo Chi Tiết Đơn Hàng ID

CREATE OR ALTER PROCEDURE SP_LayChiTietDonHangTheoID
	@OrderDetailID varchar(50)

AS
BEGIN
	SELECT *
	FROM ChiTietDonHang ct
	WHERE ct.id = @OrderDetailID
END
GO
-- lấy chi tiết thông tin sách

CREATE or alter PROCEDURE SP_ThongTinSachDuocDatCuaKhTheoId
    @SachId VARCHAR(50)
AS
BEGIN
    SELECT 
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
        TL.ten AS TenTheLoai,
        TG.ten AS TenTacGia,
		NXB.ten as TenNhaXuatBan

    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id

    WHERE 
        S.id = @SachId;
END;

-- lấy chi tiết thông tin sách theo thể loại

CREATE or alter PROCEDURE SP_DanhSachSachTheoTheLoai
    @TheLoaiId VARCHAR(50)
AS
BEGIN
    SELECT 
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
		s.theloai_id,
		s.nxb_id,
        TL.ten AS TenTheLoai,
        TG.ten AS TenTacGia,
		NXB.ten as TenNhaXuatBan

    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id

    WHERE 
        S.theloai_id = @TheLoaiId;
END;


-- Lấy tất cả sách

CREATE or alter PROCEDURE SP_DanhSachSach
AS
BEGIN
    SELECT 
		s.id,
        S.tieude AS TenSach,
		S.HinhAnh,
		S.Gia,
		S.SoLuongTon,
		S.NamXuatBan,
		S.MoTa,
		s.theloai_id,
		s.nxb_id,
        TL.ten AS TenTheLoai,
        TG.ten AS TenTacGia,
		NXB.ten as TenNhaXuatBan

    FROM 
        Sach S
        INNER JOIN TheLoai TL ON S.theloai_id = TL.id
        INNER JOIN TacGia_Sach TGS ON S.id = TGS.sach_id
        INNER JOIN TacGia TG ON TGS.tacgia_id = TG.id
        INNER JOIN NhaXuatBan NXB ON NXB.id = S.nxb_id

END;
-- Stored Procedure để cập nhật đơn hàng, đặt ngày đặt hàng là ngày hiện tại

CREATE OR ALTER PROCEDURE SP_UpdateDonHang
    @id VARCHAR(50),
    @nguoidung_id VARCHAR(50) = NULL,
    @trangthaiDH INT = NULL,
    @tongTien DECIMAL(10, 2) = NULL
AS
BEGIN
    -- Kiểm tra xem đơn hàng tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM DonHang WHERE id = @id)
    BEGIN
        RAISERROR('Đơn hàng không tồn tại.', 16, 1)
        RETURN 0
    END

    -- Cập nhật các trường dữ liệu
	-- Đặt ngày đặt hàng là ngày hiện tại
	DECLARE @ngayDatHang DATETIME = GETDATE();

    UPDATE DonHang
    SET
        nguoidung_id = ISNULL(@nguoidung_id, nguoidung_id),
        trangthaiDH = ISNULL(@trangthaiDH, trangthaiDH),
        ngayDatHang = @ngayDatHang,  -- Sử dụng biến @ngayDatHang
        tongTien = ISNULL(@tongTien, tongTien)
    WHERE
        id = @id;



    RETURN 1;
END;
-- Stored Procedure để cập nhật chi tiết đơn hàng
CREATE PROCEDURE SP_UpdateChiTietDonHang
    @id VARCHAR(50),
    @donhang_id VARCHAR(50) = NULL,
    @sach_id VARCHAR(50) = NULL,
    @soLuong INT = NULL,
    @giaDonVi DECIMAL(10, 2) = NULL
AS
BEGIN
    -- Kiểm tra xem chi tiết đơn hàng tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM ChiTietDonHang WHERE id = @id)
    BEGIN
        RAISERROR('Chi tiết đơn hàng không tồn tại.', 16, 1)
        RETURN
    END

    -- Cập nhật các trường dữ liệu nếu giá trị truyền vào khác NULL
    UPDATE ChiTietDonHang
    SET
        donhang_id = ISNULL(@donhang_id, donhang_id),
        sach_id = ISNULL(@sach_id, sach_id),
        soLuong = ISNULL(@soLuong, soLuong),
        giaDonVi = ISNULL(@giaDonVi, giaDonVi)
    WHERE
        id = @id;
    
    -- Kiểm tra dữ liệu sau cập nhật
    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Không có thay đổi được thực hiện.', 16, 1)
        RETURN 1;  -- Trả về mã lỗi khác 0
    END

    -- Trả về thông tin chi tiết đơn hàng cập nhật (tùy chọn)
    SELECT *
    FROM ChiTietDonHang
    WHERE id = @id;

    RETURN 0; -- Trả về 0 nếu cập nhật thành công
END;
-- store procedure xóa ctdh
CREATE PROCEDURE SP_DeleteChiTietDonHang
    @id VARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM ChiTietDonHang WHERE id = @id)
        RETURN 1;  -- Lỗi: Không tồn tại

    DELETE FROM ChiTietDonHang WHERE id = @id;
    IF @@ROWCOUNT = 0
        RETURN 2;  -- Lỗi: Không có dòng nào bị xóa

    RETURN 0; -- Thành công
END;

-- PROC CAP NHAT SACH
CREATE OR ALTER PROCEDURE SP_UpdateSach
    @id VARCHAR(50),
    @tieude NVARCHAR(255),
	@theloai_id VARCHAR(50),
    @gia DECIMAL(10, 2),
    @soLuongTon INT,
    @hinhAnh VARCHAR(max),
    @namXuatBan INT,
	@MoTa NVARCHAR(max),
    @nxb_id VARCHAR(50)
AS
BEGIN
    -- Kiểm tra xem sách có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Sach WHERE id = @id)
    BEGIN
        RAISERROR('Sách không tồn tại.', 16, 1);
        RETURN;
    END

	-- Kiểm tra dữ liệu đầu vào. Cần thêm nhiều kiểm tra tùy theo yêu cầu.
    -- Ví dụ, kiểm tra @gia phải lớn hơn 0, @soLuongTon phải >= 0, etc.
    IF @gia < 0
	BEGIN
		RAISERROR('Giá phải lớn hơn hoặc bằng 0.', 16, 1);
		RETURN;
	END

    IF @soLuongTon < 0
	BEGIN
		RAISERROR('Số lượng tồn phải lớn hơn hoặc bằng 0.', 16, 1);
		RETURN;
	END
    
    -- Bắt đầu giao dịch để đảm bảo tính nguyên vẹn dữ liệu
    BEGIN TRANSACTION
    
    BEGIN TRY
        UPDATE Sach
        SET
            tieude = @tieude,
			theloai_id = @theloai_id,
            gia = @gia,
            soLuongTon = @soLuongTon,
            hinhAnh = @hinhAnh,
            namXuatBan = @namXuatBan,
			MoTa = @MoTa,
            nxb_id = @nxb_id
        WHERE
            id = @id;

        -- Commit giao dịch nếu cập nhật thành công
        COMMIT TRANSACTION;

        SELECT 'Cập nhật sách thành công.';
    END TRY
    BEGIN CATCH
        -- Nếu có lỗi xảy ra, rollback giao dịch
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
		
        -- Xử lý lỗi, ghi log, thông báo lỗi cho người dùng
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
		
        -- Thêm thông tin lỗi chi tiết vào @ErrorMessage
        SELECT @ErrorMessage += ' (' + CAST(ERROR_NUMBER() AS VARCHAR(10)) + '): ' +  
               ISNULL(CAST(ERROR_PROCEDURE() AS VARCHAR(255)), '') + ' ' +
               ISNULL(CAST(ERROR_LINE() AS VARCHAR(10)), '')

        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
-- Tính tổng tiền của một đơn hàng dùng function

CREATE FUNCTION dbo.fn_TongTienDonHang (@donhang_id VARCHAR(50))
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @tongTien DECIMAL(10, 2)
    
    SELECT @tongTien = SUM(soLuong * giaDonVi)
    FROM ChiTietDonHang
    WHERE donhang_id = @donhang_id

    RETURN @tongTien
END

-- PROC CẬP NHẬT - TÁC GIẢ SÁCH
Select * from tacgia
-- Procedure để cập nhật thông tin tác giả của một cuốn sách
CREATE PROCEDURE SP_UpdateSachTacGia
    @sachId VARCHAR(50),
    @tacGiaIds VARCHAR(MAX)  -- Dữ liệu tác giả cần cập nhật
AS
BEGIN
    -- Kiểm tra xem sách có tồn tại không.  Quan trọng!
    IF NOT EXISTS (SELECT 1 FROM Sach WHERE id = @sachId)
    BEGIN
        RAISERROR('Sách không tồn tại.', 16, 1);
        RETURN;
    END
    
	-- Kiểm tra xem tác giả được đưa vào có tồn tại không?
    -- (Thêm kiểm tra này nếu cần thiết).
	
    -- Bắt đầu giao dịch.
    BEGIN TRANSACTION
    BEGIN TRY

        -- Xóa tất cả các mối quan hệ tác giả-sách hiện có cho cuốn sách này
        DELETE FROM TacGia_Sach WHERE sach_id = @sachId;
		
		-- Tách chuỗi các ID tác giả.
		DECLARE @tacGiaId VARCHAR(50)
		DECLARE @tacGiaIdsCursor CURSOR FOR
		SELECT value
		FROM STRING_SPLIT(@tacGiaIds, ',')
		
		OPEN @tacGiaIdsCursor

		FETCH NEXT FROM @tacGiaIdsCursor INTO @tacGiaId
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			-- Kiểm tra xem tác giả có tồn tại không
			IF EXISTS(SELECT 1 FROM TacGia WHERE id = @tacGiaId)
			BEGIN
				INSERT INTO TacGia_Sach (sach_id, tacgia_id) VALUES (@sachId, @tacGiaId);
			END
			ELSE
			BEGIN
				--Xử lý trường hợp tác giả không tồn tại. Ví dụ báo lỗi
				RAISERROR('Tác giả "%s" không tồn tại.', 16, 1, @tacGiaId);
				-- Có thể rollback giao dịch tại đây nếu muốn
				RETURN
			END	
            FETCH NEXT FROM @tacGiaIdsCursor INTO @tacGiaId
		END
		CLOSE @tacGiaIdsCursor;
		DEALLOCATE @tacGiaIdsCursor;
		-- Commit giao dịch
		COMMIT TRANSACTION
		
		SELECT 'Cập nhật tác giả thành công';

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        -- Xử lý lỗi, ghi log, thông báo lỗi cho người dùng.
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
		-- Thông tin lỗi chi tiết hơn
		SELECT @ErrorMessage += ' (' + CAST(ERROR_NUMBER() AS VARCHAR(10)) + '): ' + 
		ISNULL(CAST(ERROR_PROCEDURE() AS VARCHAR(255)), '') + ' ' + 
		ISNULL(CAST(ERROR_LINE() AS VARCHAR(10)), '');
		RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END CATCH
END;
END;
-- PROC LOGIN
CREATE or alter PROCEDURE SP_LOGIN
    @Username NVARCHAR(50),
    @Password NVARCHAR(255),
    @UserRole VARCHAR(10) OUTPUT,
    @FullName NVARCHAR(255) OUTPUT,
	@idND VARCHAR(50) OUTPUT

AS
BEGIN
    SELECT  @UserRole = role,
            @FullName = CASE
                            WHEN role = 'customer' THEN k.ten
                            WHEN role = 'staff' THEN n.ten
                            WHEN role = 'admin' THEN n.ten

                            ELSE NULL
                        END,
						   @idND = NguoiDung.id

    FROM NguoiDung
    LEFT JOIN Khachhang k ON NguoiDung.id = k.id_NguoiDung
    LEFT JOIN NhanVien n ON NguoiDung.id = n.id_NguoiDung
    WHERE username = @Username AND password = @Password;
    
    IF @UserRole IS NULL
    BEGIN
        RAISERROR('Tên người dùng hoặc mật khẩu không chính xác.', 16, 1);
        RETURN;
    END

    IF @FullName IS NULL
    BEGIN
        RAISERROR('Không tìm thấy thông tin.', 16, 1);
        RETURN;
    END

    PRINT N'Đăng nhập thành công.';
END;


-- procedure lấy danh sách nhà xuất bản

CREATE PROCEDURE SP_DanhSachNXB
AS
BEGIN
    SELECT id, ten, diachi, sodienthoai, email
    FROM NhaXuatBan;
END;

-- procedure lấy danh sách tác giá

CREATE PROCEDURE SP_DanhSachTacGia
AS
BEGIN
    SELECT * 
    FROM TacGia;
END;


exec SP_DanhSachTacGia


-- procedure

-- 


-- thêm kh
CREATE PROCEDURE ThemKH
    @id VARCHAR(50),
    @ten VARCHAR(255),
    @diachi VARCHAR(255),
    @sodienthoai VARCHAR(10),
    @email VARCHAR(255)
AS
BEGIN
    INSERT INTO Khachhang (id, ten, diachi, sodienthoai, email)
    VALUES (@id, @ten, @diachi, @sodienthoai, @email);
END;



-- cập nhật hàng tồn kho

CREATE PROCEDURE UpdateTonKho
    @sach_id VARCHAR(50),
    @soLuong INT
AS
BEGIN
    UPDATE Sach
    SET soLuongTon = soLuongTon + @soLuong
    WHERE id = @sach_id;
END;


-- lấy thông tin sách theo id

CREATE PROCEDURE HienThiSachId
    @id VARCHAR(50)
AS
BEGIN
    SELECT * FROM Sach
    WHERE id = @id;
END;


-- Lấy Thông Tin Khách Hàng Và Đơn Hàng Của Họ
CREATE PROCEDURE ThongTinKhVaDH
    @khachhang_id VARCHAR(50)
AS
BEGIN
    SELECT k.id AS KhachHangID, k.ten AS TenKhachHang, d.id AS DonHangID, d.trangthaiDH AS TrangThaiDonHang, d.tongTien AS TongTien
    FROM Khachhang k
    LEFT JOIN DonHang d ON k.id = d.khachhang_id
    WHERE k.id = @khachhang_id;
END;

-- Lay sach duoc dat nhieu nhat



-- Tìm Sách Theo Từ Khóa Trong Tiêu Đề
CREATE PROCEDURE TimSachTheoTuKhoa
    @keyword NVARCHAR(255)
AS
BEGIN
    SELECT * FROM Sach
    WHERE tieude LIKE '%' + @keyword + '%';
END;

--  Tính Tổng Số Sách Theo Thể Loại
CREATE PROCEDURE TongSachTL
    @genre_id VARCHAR(50)
AS
BEGIN
    SELECT COUNT(*) AS TongSoSach
    FROM Sach
    WHERE theloai_id = @genre_id;
END;


-- lay doanh thu theo tháng

--  Lấy Các Đơn Hàng Chưa Thanh Toán Trong Khoảng Thời Gian
-------------------------------- CURSOR




-- Lấy thông tin tài khoản

CREATE PROCEDURE Account_GetAll
AS
BEGIN
    -- Lấy thông tin tài khoản khách hàng
    SELECT 
        nd.id AS AccountID,
        nd.username AS Username,
        nd.role AS Role,
        kh.ten AS FullName,
        kh.diachi AS Address,
        kh.sodienthoai AS PhoneNumber,
        kh.email AS Email
    FROM 
        NguoiDung nd
    JOIN 
        Khachhang kh ON nd.id = kh.id_NguoiDung
    WHERE 
        nd.role = 'customer'

    UNION ALL

    -- Lấy thông tin tài khoản nhân viên
    SELECT 
        nd.id AS AccountID,
        nd.username AS Username,
        nd.role AS Role,
        nv.ten AS FullName,
        NULL AS Address, -- Nhân viên không có cột địa chỉ trong cấu trúc hiện tại
        nv.sodienthoai AS PhoneNumber,
        nv.email AS Email
    FROM 
        NguoiDung nd
    JOIN 
        NhanVien nv ON nd.id = nv.id_NguoiDung
    WHERE 
        nd.role IN ('staff', 'admin')
END;


-- PROCEDURE: Cập nhật trạng thái đơn hàng và số lượng tồn kho khi đơn hàng được thanh toán.


CREATE PROCEDURE sp_CapNhatDonHang
    @donhang_id VARCHAR(50)
AS
BEGIN
    DECLARE @sach_id VARCHAR(50), @soLuong INT;

    -- Cập nhật trạng thái đơn hàng
    UPDATE DonHang
    SET trangthaiDH = 1 -- Đã thanh toán
    WHERE id = @donhang_id;

    -- Giảm số lượng tồn kho của từng sách trong đơn hàng
    DECLARE cur CURSOR FOR
        SELECT sach_id, soLuong
        FROM ChiTietDonHang
        WHERE donhang_id = @donhang_id;

    OPEN cur;
    FETCH NEXT FROM cur INTO @sach_id, @soLuong;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        UPDATE Sach
        SET soLuongTon = soLuongTon - @soLuong
        WHERE id = @sach_id;

        FETCH NEXT FROM cur INTO @sach_id, @soLuong;
    END;
    CLOSE cur;
    DEALLOCATE cur;
END;


-- FUNCTION: Tính tổng doanh thu từ tất cả các đơn hàng đã thanh toán.
CREATE FUNCTION fn_TongDoanhThu()
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @tongDoanhThu DECIMAL(10, 2);

    SELECT @tongDoanhThu = SUM(tongTien)
    FROM DonHang
    WHERE trangthaiDH = 1; -- Đã thanh toán

    RETURN @tongDoanhThu;
END;


-- Tìm kiếm sách theo thể loại và khoảng giá.

CREATE PROCEDURE sp_TimSachTheoTheLoaiGia
    @theloai_id VARCHAR(50),
    @giaMin DECIMAL(10, 2),
    @giaMax DECIMAL(10, 2)
AS
BEGIN
    SELECT *
    FROM Sach
    WHERE theloai_id = @theloai_id
      AND gia BETWEEN @giaMin AND @giaMax;
END;


-- PROCEDURE: Lấy danh sách khách hàng theo tổng giá trị đơn hàng đã mua.

CREATE PROCEDURE sp_LayDanhSachKhachHangTheoGiaTri
    @minValue DECIMAL(10, 2),
    @maxValue DECIMAL(10, 2)
AS
BEGIN
    SELECT k.ten, k.diachi, k.sodienthoai, SUM(d.tongTien) AS TongGiaTriMua
    FROM Khachhang k
    JOIN DonHang d ON k.id = d.khachhang_id
    WHERE d.trangthaiDH = 1 -- Đã thanh toán
    GROUP BY k.ten, k.diachi, k.sodienthoai
    HAVING SUM(d.tongTien) BETWEEN @minValue AND @maxValue;
END;


-- Kiểm tra số lượng tồn kho của sách.
CREATE FUNCTION fn_KiemTraTonKho(@sach_id VARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @soLuongTon INT;

    SELECT @soLuongTon = soLuongTon
    FROM Sach
    WHERE id = @sach_id;

    RETURN @soLuongTon;
END;


-- CURSOR: Tạo danh sách top 5 sách bán chạy nhất.

CREATE PROCEDURE sp_TopSachBanChay
AS
BEGIN
    DECLARE @sach_id VARCHAR(50), @tongSoLuong INT;

    DECLARE cur CURSOR FOR
        SELECT TOP 5 sach_id, SUM(soLuong) AS tongSoLuong
        FROM ChiTietDonHang
        GROUP BY sach_id
        ORDER BY tongSoLuong DESC;

    OPEN cur;
    FETCH NEXT FROM cur INTO @sach_id, @tongSoLuong;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT 'Sách ID: ' + @sach_id + ', Số lượng đã bán: ' + CAST(@tongSoLuong AS VARCHAR);

        FETCH NEXT FROM cur INTO @sach_id, @tongSoLuong;
    END;
    CLOSE cur;
    DEALLOCATE cur;
END;
