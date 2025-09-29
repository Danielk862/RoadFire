DROP DATABASE IF EXISTS RoadFireDB;
CREATE DATABASE RoadFireDB CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE RoadFireDB;

-- =====================================
-- BLOQUE 1 – Empleados, Usuarios y Roles
-- =====================================

-- Tabla Empleado
CREATE TABLE Empleado (
    idEmpleado INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(50),
    celular VARCHAR(50),
    email VARCHAR(100)
) ENGINE=InnoDB;

-- Tabla Rol
CREATE TABLE Rol (
    idRol INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(255)
) ENGINE=InnoDB;

-- Tabla Usuario (cada usuario tiene un único rol)
CREATE TABLE Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    nombreUsuario VARCHAR(100) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    estado TINYINT DEFAULT 1,
    idEmpleado INT,
    idRol INT,
    email VARCHAR(150),
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (idEmpleado) REFERENCES Empleado(idEmpleado) ON DELETE SET NULL,
    FOREIGN KEY (idRol) REFERENCES Rol(idRol) ON DELETE SET NULL
) ENGINE=InnoDB;

-- =====================================
-- BLOQUE 2 – Productos, Categorías y Bodegas
-- =====================================

-- Tabla Categoria
CREATE TABLE Categoria (
    idCategoria INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE,
    descripcion VARCHAR(255)
) ENGINE=InnoDB;

-- Tabla Producto
CREATE TABLE Producto (
    idProducto INT AUTO_INCREMENT PRIMARY KEY,
    sku VARCHAR(50) UNIQUE,
    nombre VARCHAR(150) NOT NULL,
    precio DECIMAL(10,2) DEFAULT 0.00,
    estado ENUM('Activo','Inactivo') DEFAULT 'Activo',
    idCategoria INT,
    FOREIGN KEY (idCategoria) REFERENCES Categoria(idCategoria) ON DELETE SET NULL
) ENGINE=InnoDB;

-- Tabla Bodega
CREATE TABLE Bodega (
    idBodega INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    tipo VARCHAR(50)
) ENGINE=InnoDB;

-- Tabla Existencia
CREATE TABLE Existencia (
    idExistencia INT AUTO_INCREMENT PRIMARY KEY,
    idProducto INT NOT NULL,
    idBodega INT NOT NULL,
    cantidad INT DEFAULT 0,
    valor DECIMAL(12,2) DEFAULT 0.00,
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto) ON DELETE CASCADE,
    FOREIGN KEY (idBodega) REFERENCES Bodega(idBodega) ON DELETE CASCADE,
    UNIQUE (idProducto, idBodega)
) ENGINE=InnoDB;

-- =====================================
-- BLOQUE 3 – Proveedores, Clientes y Pedidos
-- =====================================
-- =====================================
-- CUANDO SE CREE EL PROVEEDOR, SE DEBE AGREGAR CAMPO EN PRODUCTOS DONDE SE ASIGNE EL PROVEEDOR.
-- =====================================
-- Tabla Proveedor
CREATE TABLE Proveedor (
    idProveedor INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(50),
    email VARCHAR(100)
) ENGINE=InnoDB;

-- Tabla Cliente
CREATE TABLE Cliente (
    idCliente INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(50),
    email VARCHAR(100)
) ENGINE=InnoDB;

-- Pedido de Compra
CREATE TABLE Pedido_Compra (
    idPedido INT AUTO_INCREMENT PRIMARY KEY,
    numero VARCHAR(50),
    idProveedor INT,
    fecha DATE,
    total DECIMAL(12,2),
    estado ENUM('Pendiente','Recibido','Cancelado') DEFAULT 'Pendiente',
    FOREIGN KEY (idProveedor) REFERENCES Proveedor(idProveedor) ON DELETE SET NULL
) ENGINE=InnoDB;

-- Detalle de Pedido de Compra
CREATE TABLE PedidoDetalle_Compra (
    idDetalle INT AUTO_INCREMENT PRIMARY KEY,
    idPedido INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (idPedido) REFERENCES Pedido_Compra(idPedido) ON DELETE CASCADE,
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- Pedido de Venta
CREATE TABLE Pedido_Venta (
    idVenta INT AUTO_INCREMENT PRIMARY KEY,
    numero VARCHAR(50),
    idCliente INT,
    idUsuario INT,
    fechaCreacion DATE,
    fechaEntrega DATE,
    total DECIMAL(12,2),
    estado ENUM('Pendiente','Entregado','Cancelado') DEFAULT 'Pendiente',
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente) ON DELETE SET NULL,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE SET NULL
) ENGINE=InnoDB;

-- Detalle de Pedido de Venta
CREATE TABLE PedidoDetalle_Venta (
    idDetalle INT AUTO_INCREMENT PRIMARY KEY,
    idVenta INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (idVenta) REFERENCES Pedido_Venta(idVenta) ON DELETE CASCADE,
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- =====================================
-- BLOQUE 4 – Movimientos, Notificaciones y Predicciones
-- =====================================

-- Movimientos
CREATE TABLE Movimientos (
    idMovimiento INT AUTO_INCREMENT PRIMARY KEY,
    tipo ENUM('ENTRADA','SALIDA','AJUSTE') NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    descripcion VARCHAR(255),
    idUsuario INT,
    idBodega INT,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE SET NULL,
    FOREIGN KEY (idBodega) REFERENCES Bodega(idBodega) ON DELETE SET NULL
) ENGINE=InnoDB;

-- Detalle de movimientos
CREATE TABLE DetalleMovimientos (
    idDetalle INT AUTO_INCREMENT PRIMARY KEY,
    idMovimiento INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    valorUnitario DECIMAL(10,2) DEFAULT 0.00,
    subtotal DECIMAL(12,2) GENERATED ALWAYS AS (cantidad * valorUnitario) STORED,
    FOREIGN KEY (idMovimiento) REFERENCES Movimientos(idMovimiento) ON DELETE CASCADE,
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- Notificaciones
CREATE TABLE Notificacion (
    idNotificacion INT AUTO_INCREMENT PRIMARY KEY,
    tipo VARCHAR(50),
    mensaje TEXT,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    estado ENUM('PENDIENTE','LEIDA','IGNORADA') DEFAULT 'PENDIENTE',
    idUsuario INT,
    idProducto INT,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE SET NULL,
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto) ON DELETE SET NULL
) ENGINE=InnoDB;

-- Predicciones
CREATE TABLE Prediccion (
    idPrediccion INT AUTO_INCREMENT PRIMARY KEY,
    idProducto INT NOT NULL,
    fechaAgotamiento DATE,
    sugerido INT,
    confianza DECIMAL(5,2),
    ultimaActualizacion DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Índices
CREATE INDEX idx_producto_sku ON Producto(sku);
CREATE INDEX idx_existencia_producto ON Existencia(idProducto);
CREATE INDEX idx_movimientos_tipo ON Movimientos(tipo);
