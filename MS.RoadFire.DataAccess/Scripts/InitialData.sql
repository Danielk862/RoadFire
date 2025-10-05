USE [RoadFire];


-------------------------------
-- 1️⃣ ROLES
-------------------------------
INSERT INTO [dbo].[Roles] (Name, Description, IsActive, RegistrationDate)
VALUES 
('Administrador', 'Acceso total al sistema', 1, GETDATE()),
('Ventas', 'Encargado de gestionar ventas y clientes', 1, GETDATE()),
('Compras', 'Encargado de compras y proveedores', 1, GETDATE()),
('Inventario', 'Encargado de controlar el stock', 1, GETDATE());


-------------------------------
-- 2️⃣ EMPLEADOS
-------------------------------
INSERT INTO [dbo].[Employees] 
(FirtsName, SecondName, Surname, SecondSurname, BornDate, Address, Phone, Mobile, Email, IsActive, RegistrationDate)
VALUES 
('Carlos', 'Andrés', 'Gómez', 'Pérez', '1990-05-10', 'Calle 10 #5-20', '6015551001', '3115551001', 'carlos.gomez@roadfire.com', 1, GETDATE()),
('Laura', 'Marcela', 'Rodríguez', 'López', '1992-08-15', 'Cra 8 #12-45', '6015551002', '3125551002', 'laura.rodriguez@roadfire.com', 1, GETDATE()),
('Javier', 'Eduardo', 'Martínez', 'Ruiz', '1988-03-22', 'Av 30 #7-80', '6015551003', '3135551003', 'javier.martinez@roadfire.com', 1, GETDATE()),
('Sofía', 'Carolina', 'Díaz', 'Hernández', '1995-12-01', 'Calle 45 #18-22', '6015551004', '3145551004', 'sofia.diaz@roadfire.com', 1, GETDATE());


-------------------------------
-- 3️⃣ USUARIOS
-------------------------------
INSERT INTO [dbo].[Users] 
(Username, Password, CreatedAt, State, EmployeeId, RoleId)
VALUES
('admin', 'admin123', GETDATE(), 1, 1, 1),
('vendedor1', 'ventas123', GETDATE(), 1, 2, 2),
('compras1', 'compras123', GETDATE(), 1, 3, 3),
('inventario1', 'inv123', GETDATE(), 1, 4, 4);


-------------------------------
-- 4️⃣ CATEGORÍAS (solo motocicletas)
-------------------------------
INSERT INTO [dbo].[Categories] (Name, Description, IsActive, RegistrationDate)
VALUES
('Aceites y lubricantes', 'Aceites de motor y líquidos para motocicletas', 1, GETDATE()),
('Frenos', 'Pastillas, discos y fluidos de freno', 1, GETDATE()),
('Llantas', 'Llantas y rines para motocicletas', 1, GETDATE()),
('Accesorios', 'Casco, guantes, luces y otros accesorios', 1, GETDATE()),
('Repuestos de motor', 'Pistones, bujías, filtros y más', 1, GETDATE());


-------------------------------
-- 5️⃣ PROVEEDORES
-------------------------------
INSERT INTO [dbo].[Suppliers] 
(Name, Address, Phone, Nit, ContactPerson, Website, Description, RegistrationDate)
VALUES
('MotoParts S.A.S.', 'Zona Industrial #23-11', '6015552001', '900123456', 'Andrés López', 'www.motoparts.com', 'Proveedor de repuestos y aceites', GETDATE()),
('SpeedPro Ltda.', 'Av 68 #45-67', '6015552002', '901654321', 'Paola Hernández', 'www.speedpro.com', 'Proveedor de llantas y frenos', GETDATE());


-------------------------------
-- 6️⃣ PRODUCTOS
-------------------------------
INSERT INTO [dbo].[Products] 
(Sku, Description, Price, CategoryId, SupplierId, IsActive, RegistrationDate)
VALUES
('OIL-10W40', 'Aceite sintético 10W-40 Motul 1L', 45000, 1, 1, 1, GETDATE()),
('FR-PAD-HONDA', 'Pastillas de freno Honda CB110', 32000, 2, 2, 1, GETDATE()),
('TIRE-MICHELIN-17', 'Llanta Michelin Pilot Street 17"', 210000, 3, 2, 1, GETDATE()),
('HL-LED-UNIV', 'Luz LED frontal universal para moto', 65000, 4, 1, 1, GETDATE()),
('SPK-NGK-CR7E', 'Bujía NGK CR7E para motocicleta', 18000, 5, 1, 1, GETDATE());


-------------------------------
-- 7️⃣ CLIENTES
-------------------------------
INSERT INTO [dbo].[Customers]
(TypeIdentification, Identification, FirstName, SecondName, Surname, SecondSurname, Address, Phone, Email, IsActive, RegistrationDate)
VALUES
('CC', '1012456789', 'Luis', 'Fernando', 'Ríos', 'Pérez', 'Calle 80 #25-15', '6015553001', 'luis.rios@gmail.com', 1, GETDATE()),
('CC', '1023456789', 'María', 'Alejandra', 'Quintero', 'García', 'Cra 10 #12-22', '6015553002', 'maria.quintero@gmail.com', 1, GETDATE());


-------------------------------
-- 8️⃣ STOCK (productos actuales)
-------------------------------
INSERT INTO [dbo].[Stock] (ProductId, Quantity, ValueUnit, RegistrationDate)
VALUES
(1, 50, 45000, GETDATE()),
(2, 80, 32000, GETDATE()),
(3, 20, 210000, GETDATE()),
(4, 35, 65000, GETDATE()),
(5, 100, 18000, GETDATE());

