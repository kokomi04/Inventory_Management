Create Table product
(
	id int IDENTITY PRIMARY KEY,
	nameProduct NVARCHAR(100)NOT NULL,
	note NVARCHAR(100)NOT NULL default N'Không có',
)

Create Table whereStatus
(
	id int IDENTITY PRIMARY KEY,
	idProduct int NOT NULL ,
	status int NOT NULL DEFAULT 0, --trong kho

	FOREIGN KEY (idProduct) REFERENCES dbo.product(id)
)

Create Table inout
(
	id int IDENTITY PRIMARY KEY,
	idWhereStatus int NOT NULL ,
	nameUser NVARCHAR(100) DEFAULT N'Khong co',
	dateEdited DATE DEFAULT GETDATE(),
	bo int NOT NULL DEFAULT 0,
	tray int NOT NULL DEFAULT 0,
	mica int NOT NULL DEFAULT 0,

	FOREIGN KEY (idWhereStatus) REFERENCES dbo.whereStatus(id)
)
/*
Create Table unit
(
	id int IDENTITY PRIMARY KEY,
	idProduct int NOT NULL ,
	bo int NOT NULL DEFAULT 0,
	tray int NOT NULL DEFAULT 0,
	mica int NOT NULL DEFAULT 0,
	FOREIGN KEY (idProduct) REFERENCES dbo.product(id)
)
*/

Insert into dbo.product
(nameProduct)
Values (N'U10C100')
Insert into dbo.product
(nameProduct)
Values (N'UBN2304')
Insert into dbo.product
(nameProduct)
Values (N'U10C153')

Insert into dbo.whereStatus
(idProduct, status)
Values (1, 0)
Insert into dbo.whereStatus
(idProduct, status)
Values (1, 1)
Insert into dbo.whereStatus
(idProduct, status)
Values (2, 0)
Insert into dbo.whereStatus
(idProduct, status)
Values (2, 1)
Insert into dbo.whereStatus
(idProduct, status)
Values (3, 0)
Insert into dbo.whereStatus
(idProduct, status)
Values (3, 1)

Insert into dbo.inout
(idWhereStatus, nameUser, dateEdited, bo)
Values (1, N'Duy', GETDATE(), 3)
Insert into dbo.inout
(idWhereStatus, nameUser, dateEdited, bo)
Values (2, N'Son', GETDATE(), 6)
Insert into dbo.inout
(idWhereStatus, nameUser, dateEdited, bo)
Values (3, N'Son', GETDATE(), 5)
Insert into dbo.inout
(idWhereStatus, nameUser, dateEdited, bo)
Values (4, N'Duy', GETDATE(), 9)
Insert into dbo.inout
(idWhereStatus, nameUser, dateEdited, bo)
Values (5, N'Thanh', GETDATE(), 11)
Insert into dbo.inout
(idWhereStatus, nameUser, dateEdited, bo)
Values (6, N'Quan', GETDATE(), 15)


Select * From dbo.product
Select * From dbo.whereStatus
Select * From dbo.inout

Update dbo.inout set mica = 0 where idWhereStatus = 3

------------------------------
Create Alter PROC USP_Load
As
Begin

select temp1.id, temp1.nameProduct as N'Tên hàng', temp2.bo as N'Ngoài chuyền', temp1.bo as N'Trong kho', temp1.mica as N'(Lẻ Mica)', temp1.tray as N'(Lẻ Tray)', temp1.note as N'Ghi chú' from 

(select p.id, p.nameProduct, i.nameUser, i.bo, i.mica, i.tray, w.status, i.dateEdited, p.note
From dbo.product as p, dbo.inout as i, dbo.whereStatus as w 
Where p.id = w.idProduct and w.id = i.idWhereStatus and w.status = 0) as temp1,

(select p.nameProduct, i.nameUser, i.bo, w.status, i.dateEdited, p.note
From dbo.product as p, dbo.inout as i, dbo.whereStatus as w 
Where p.id = w.idProduct and w.id = i.idWhereStatus and w.status = 1) as temp2 

where temp1.nameProduct = temp2.nameProduct 

End
Go

------------------------------

Create Alter PROC USP_Nhap
@id int, @count int, @type int, @nameUser nvarchar(100), @dateEdit date, @note nvarchar(100)
As Begin
	Declare @nowcount int
	Declare @nowcount1 int
	Declare @nowmica int
	Declare @nowmica1 int
	Declare @nowtray int
	Declare @nowtray1 int
	Declare @idwhereStatus int
	Declare @idwhereStatus1 int

	Select @idwhereStatus = id From dbo.whereStatus Where idProduct = @id and status = 0
	Select @idwhereStatus1 = id From dbo.whereStatus Where idProduct = @id and status = 1
	Select @nowcount = bo From dbo.inout Where id = @idwhereStatus
	Select @nowcount1 = bo From dbo.inout Where id = @idwhereStatus1
	Select @nowmica = mica From dbo.inout Where id = @idwhereStatus
	Select @nowtray = tray From dbo.inout Where id = @idwhereStatus

	If(@nowcount1 >= @count and @type = 1 and @count > 0)
	Begin
		Update dbo.inout Set bo = @nowcount + @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus
		Update dbo.inout Set bo = @nowcount1 - @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus1
		Update dbo.product Set note = @note Where id = @id
	End

	If(@type = 2 and @count > 0)
	Begin
		Update dbo.inout Set mica = @nowmica + @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus
		Update dbo.product Set note = @note Where id = @id
	End

	If(@type = 3 and @count > 0)
	Begin
		Update dbo.inout Set tray = @nowtray + @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus
		Update dbo.product Set note = @note Where id = @id
	End	
End
Go

Create Alter PROC USP_Xuat
@id int, @count int, @type int, @nameUser nvarchar(100), @dateEdit date, @note nvarchar(100)
As Begin
	Declare @nowcount int
	Declare @nowcount1 int
	Declare @nowmica int
	Declare @nowmica1 int
	Declare @nowtray int
	Declare @nowtray1 int
	Declare @idwhereStatus int
	Declare @idwhereStatus1 int

	Select @idwhereStatus = id From dbo.whereStatus Where idProduct = @id and status = 0
	Select @idwhereStatus1 = id From dbo.whereStatus Where idProduct = @id and status = 1
	Select @nowcount = bo From dbo.inout Where id = @idwhereStatus
	Select @nowcount1 = bo From dbo.inout Where id = @idwhereStatus1
	Select @nowmica = mica From dbo.inout Where id = @idwhereStatus
	Select @nowtray = tray From dbo.inout Where id = @idwhereStatus

	If(@nowcount >= @count and @type = 1 and @count > 0)
	Begin
		Update dbo.inout Set bo = @nowcount - @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus
		Update dbo.inout Set bo = @nowcount1 + @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus1
		Update dbo.product Set note = @note Where id = @id
	End

	If(@nowmica != 0 and @nowmica >= @count and @type = 2 and @count > 0)
	Begin
		Update dbo.inout Set mica = @nowmica - @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus
		Update dbo.product Set note = @note Where id = @id
	End

	If(@nowtray != 0 and @nowtray >= @count and @type = 3 and @count > 0)
	Begin
		Update dbo.inout Set tray = @nowtray - @count, nameUser = @nameUser,dateEdited = @dateEdit 
			where idWhereStatus = @idwhereStatus
		Update dbo.product Set note = @note Where id = @id
	End	
End
Go

Create Alter Proc AddProduct
@nameProduct Nvarchar(100) 
As Begin

	Insert Into dbo.product
		(nameProduct )
	Values(@nameProduct)

End
Go

Create Trigger UTG_AddWhere
On dbo.product 
For Insert
As
Begin
	Declare @idmax int

	SELECT @idmax = id FROM inserted

	Insert Into dbo.whereStatus
		(idProduct, status)
	Values(@idmax, 0)
	Insert Into dbo.whereStatus
		(idProduct, status)
	Values(@idmax, 1)

End
Go

Create Alter Proc AddInout
@bo1 int, @bo2 int
As Begin
	Declare @idmaxWhere int

	SELECT @idmaxWhere = Max(id) FROM dbo.whereStatus
	Declare @idmaxWhere1 int = @idmaxWhere - 1

	Insert Into dbo.inout
		(idWhereStatus, bo)
	Values(@idmaxWhere1, @bo1)
	Insert Into dbo.inout
		(idWhereStatus, bo)
	Values(@idmaxWhere, @bo2)
End
Go

Create Alter PROC USP_DelInout
@id int
As Begin
	Delete dbo.inout Where idWhereStatus in (select id From dbo.whereStatus Where idProduct = @id)  
End
Go

Create Trigger DelWhere
On dbo.inout
For Delete
As Begin
	Delete dbo.whereStatus Where id in (Select idWhereStatus From deleted)
End
Go

Create Trigger DelPro
On dbo.whereStatus
For Delete
As Begin
	Delete dbo.product Where id in (Select idProduct From deleted)
End
Go

Create Alter PROC USP_Edit
@id int, @bo1 int, @bo2 int
As Begin
	--Declare @nowcount int
	--Declare @nowcount1 int
	Declare @idwhereStatus int
	Declare @idwhereStatus1 int

	Select @idwhereStatus = id From dbo.whereStatus Where idProduct = @id and status = 0
	Select @idwhereStatus1 = id From dbo.whereStatus Where idProduct = @id and status = 1
	--Select @nowcount = bo From dbo.inout Where id = @idwhereStatus
	--Select @nowcount1 = bo From dbo.inout Where id = @idwhereStatus1

	If(@bo1 >= 0 and @bo2 >= 0)
	Begin
		Update dbo.inout Set bo = @bo1
			where idWhereStatus = @idwhereStatus
		Update dbo.inout Set bo = @bo2
			where idWhereStatus = @idwhereStatus1		
	End
End
Go

use Inventory

select * From product
select * from whereStatus
select * from inout

delete product where id = 4
delete inout where id = 8