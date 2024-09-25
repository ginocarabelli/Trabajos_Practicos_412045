CREATE DATABASE Billing
GO
USE Billing
GO
CREATE TABLE Articles(
article_id int primary key,
article_name varchar(50),
unit_price decimal(10,2)
)

CREATE TABLE PaymentForms(
payment_form_id int identity primary key,
payment_form varchar(50)
)

CREATE TABLE Bills(
invoice_id int primary key,
invoice_date datetime,
payment_form_id int,
client varchar(75),
CONSTRAINT fk_payment_form_id FOREIGN KEY (payment_form_id) REFERENCES PaymentForms(payment_form_id)
)

CREATE TABLE InvoiceDetails(
invoice_details_id int primary key,
article_id int,
cantidad int,
invoice_id int,
CONSTRAINT fk_invoice_id FOREIGN KEY (invoice_id) REFERENCES Bills(invoice_id),
CONSTRAINT fk_article_id  FOREIGN KEY (article_id) REFERENCES Articles(article_id)
)
INSERT INTO Articles VALUES(1, 'Zapatilla', 209000.00) 
INSERT INTO Articles VALUES(2, 'Remera', 80000.00)


INSERT INTO PaymentForms VALUES('Efectivo')
INSERT INTO PaymentForms VALUES('Transferencia')
INSERT INTO PaymentForms VALUES('Credito')
INSERT INTO PaymentForms VALUES('Debito')

SET DATEFORMAT DMY
GO
INSERT INTO Bills VALUES(1, '29/08/2024', 2, 'Gino Carabelli')
INSERT INTO InvoiceDetails VALUES(1, 1, 1, 1)
INSERT INTO InvoiceDetails VALUES(2, 2, 1, 1)
GO
CREATE PROCEDURE SP_GetAll
AS
BEGIN
	SELECT b.invoice_id AS INVOICE_ID, invoice_date AS INVOICE_DATE,
	pf.payment_form_id AS PAYMENT_FORM_ID, pf.payment_form AS PAYMENT_FORM_NAME,
	id.invoice_details_id AS INVOICE_DETAILS_ID, id.cantidad AS QUANTITY,
	a.article_id AS ARTICLE_ID, a.article_name AS ARTICLE_NAME, a.unit_price AS UNIT_PRICE,
	client as CLIENT
	FROM Bills b
	JOIN PaymentForms pf ON pf.payment_form_id = b.payment_form_id
	JOIN InvoiceDetails id ON id.invoice_id = b.invoice_id
	JOIN Articles a ON a.article_id = id.article_id
END
GO
CREATE PROCEDURE SP_GetInvoiceDetailsById
	@ID int
AS
BEGIN
	SELECT invoice_details_id AS INVOICE_DETAILS_ID,
	invoice_id AS INVOICE_ID,
	a.article_id AS ARTICLE_ID , a.article_name AS ARTICLE_NAME, a.unit_price AS UNIT_PRICE,
	cantidad AS QUANTITY
	FROM InvoiceDetails id
	JOIN Articles a ON a.article_id = id.article_id
	WHERE invoice_details_id = @ID
END
GO
CREATE PROCEDURE SP_GetPaymentFormById
	@ID int
AS
BEGIN
	SELECT *
	FROM PaymentForms
	WHERE payment_form_id = @ID
END
GO
CREATE PROCEDURE SP_GetArticleById
	@ID int
AS
BEGIN
	SELECT a.article_id AS ARTICLE_ID, a.article_name AS ARTICLE_NAME, a.unit_price AS UNIT_PRICE
	FROM Articles a
	WHERE article_id = @ID
END
GO
CREATE PROCEDURE SP_GetInvoiceById
	@ID int
AS
BEGIN
	SELECT invoice_id AS INVOICE_ID, invoice_date AS INVOICE_DATE,
	pf.payment_form_id AS PAYMENT_FORM_ID, pf.payment_form AS PAYMENT_FORM_NAME,
	client as CLIENT
	FROM Bills b
	JOIN PaymentForms pf ON pf.payment_form_id = b.payment_form_id
	WHERE invoice_id = @ID
END

-- CRUD SP
CREATE PROCEDURE SP_SaveInvoice
	@ID int,
	@INVOICE_DATE datetime,
	@PAYMENT_FORM_ID int,
	@CLIENT varchar(75)
AS
BEGIN
	INSERT INTO Bills VALUES(@ID, @INVOICE_DATE, @PAYMENT_FORM_ID, @CLIENT)
END
GO
CREATE PROCEDURE SP_UpdateInvoice
	@ID int,
	@INVOICE_DATE datetime,
	@PAYMENT_FORM_ID int,
	@CLIENT varchar(75)
AS
BEGIN
	UPDATE Bills
	SET invoice_date = @INVOICE_DATE,
	payment_form_id = @PAYMENT_FORM_ID,
	client = @CLIENT
	WHERE invoice_id = @ID
END
GO
CREATE PROCEDURE SP_DeleteInvoice
	@ID int
AS
BEGIN
	DELETE FROM Bills
	WHERE invoice_id = @ID
END
GO
CREATE PROCEDURE SP_SaveInvoiceDetail
	@ID int,
	@ARTICLE_ID int,
	@QUANTITY int,
	@INVOICE_ID int
AS
BEGIN
	INSERT INTO InvoiceDetails VALUES(@ID, @ARTICLE_ID, @QUANTITY, @INVOICE_ID)
END
GO
CREATE PROCEDURE SP_UpdateInvoiceDetail
	@ID int,
	@ARTICLE_ID int,
	@QUANTITY int,
	@INVOICE_ID int
AS
BEGIN
	UPDATE InvoiceDetails 
	SET article_id = @ARTICLE_ID,
	cantidad = @QUANTITY,
	invoice_id = @INVOICE_ID
	WHERE invoice_details_id = @ID
END
GO
CREATE PROCEDURE SP_DeleteInvoiceDetail
	@ID int
AS
BEGIN
	DELETE FROM InvoiceDetails WHERE invoice_details_id = @ID
END
GO
CREATE PROCEDURE SP_GetAllArticles
AS
BEGIN
	SELECT * FROM Articles
END
GO
CREATE PROCEDURE SP_GetArticleById
	@ID INT
AS
BEGIN
	SELECT * FROM Articles WHERE article_id = @ID
END
GO
CREATE PROCEDURE SP_SaveArticle
	@ID int,
	@NAME varchar(50),
	@UNIT_PRICE decimal(10,2)
AS
BEGIN
	INSERT INTO Articles values (@ID, @NAME, @UNIT_PRICE)
END
GO
CREATE PROCEDURE SP_UpdateArticle
	@ID int,
	@NAME varchar(50),
	@UNIT_PRICE decimal(10,2)
AS
BEGIN
	UPDATE Articles
	SET article_name = @NAME,
	unit_price = @UNIT_PRICE
	WHERE article_id = @ID
END
GO
CREATE PROCEDURE SP_DeleteArticle
	@ID int
AS
BEGIN
	DELETE FROM Articles
	WHERE article_id = @ID
END