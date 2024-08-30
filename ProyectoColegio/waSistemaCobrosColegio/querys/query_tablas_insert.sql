use master;
go

drop database colegio2024;

create database colegio2024;
go

use colegio2024;
go

if object_id('categoria') is not null
drop table categoria;
create table categoria(
	id				int primary key identity(100,1),
	nombre			varchar(20),
	estado			bit default(1)
);
go

if object_id('categoria_detalle') is not null
drop table categoria_detalle;
create table categoria_detalle(
	id				int primary key identity(200,1),
	id_categoria	int,
	nombre			varchar(20),
	estado			bit default(1)
);
go

if object_id('apoderado') is not null
drop table apoderado;
create table apoderado(
	id					int primary key identity(300,1),
	tipo_documento		varchar(10),
	numero_documento	varchar(20),
	nombre				varchar(50),
	apellido			varchar(50),
	direccion			varchar(50),
	telefono			varchar(20),
	email				varchar(50),
	relacion			varchar(20),
	fecha_nacimiento	date,
	fecha_registro		date default(getdate()),
	fecha_actualizacion	date default(getdate()),
	estado				bit default(1)
);
go

if object_id('usuario') is not null
drop table usuario;
create table usuario(
	id					int primary key identity(20000,1),
	rol					varchar(20),
	tipo_documento		varchar(10),
	numero_documento	varchar(20),
	nombre				varchar(50),
	apellido			varchar(50),
	direccion			varchar(50),
	telefono			varchar(20),
	email				varchar(50),
	clave				varchar(50),
	genero				char(1),
	fecha_nacimiento	date,
	fecha_registro		date default(getdate()),
	fecha_actualizacion	date default(getdate()),
	fecha_acceso		date default(getdate()),
	estado				bit default(1)
);
go

if object_id('estudiante') is not null
drop table estudiante;
create table estudiante(
	id					int primary key identity(30000,1),
	tipo_documento		varchar(10),
	numero_documento	varchar(20),
	nombre				varchar(50),
	apellido			varchar(50),
	direccion			varchar(50),
	telefono			varchar(20),
	email				varchar(50),
	genero				char(1),
	fecha_nacimiento	date,
	fecha_registro		date default(getdate()),
	fecha_actualizacion	date default(getdate()),
	estado				bit default(1)
);
go

if object_id('matricula') is not null
drop table matricula;
create table matricula(
	id					int primary key identity(100000,1),
	id_estudiante		int,
	id_apoderado		int,
	periodo				varchar(10),
	nivel				varchar(10),
	grado				char(1),
	seccion				char(1),
	situacion			varchar(20),
	descripcion			varchar(50),
	fecha_registro		date default(getdate()),
	fecha_actualizacion	date default(getdate()),
	estado				varchar(20) default('GENERADO')
);
go

if object_id('matricula_detalle') is not null
drop table matricula_detalle;
create table matricula_detalle(
	id					int primary key identity(200000,1),
	id_matricula		int,
	concepto			varchar(50),
	monto				decimal(6,2),
	estado				varchar(20) default('PENDIENTE')
);
go

if object_id('pago') is not null
drop table pago;
create table pago(
	id					int primary key identity(300000,1),
	id_matricula		int,
	monto_total			decimal(6,2),
	metodo_pago			varchar(20),
	numero_op			varchar(10),
	url_voucher			varchar(max),
	fecha_registro		date default(getdate()),
	fecha_actualizacion	date default(getdate()),
	estado				varchar(20)	default('COMPLETADO')
);
go

if object_id('pago_detalle') is not null
drop table pago_detalle;
create table pago_detalle(
	id						int primary key identity(400000,1),
	id_pago					int,
	id_matricula_detalle	int,
	concepto				varchar(50),
	monto					decimal(6,2),
	estado					varchar(20) default('COMPLETADO')
);
go

if object_id('type_pago_detalle') is not null
drop type type_pago_detalle
go
CREATE TYPE type_pago_detalle AS TABLE(
	id_matricula_detalle	int,
	concepto				varchar(50),
	monto					decimal(6,2)
);
GO

if object_id('SP_CATEGORIA_DETALLE_LISTAR') is not null
drop proc SP_CATEGORIA_DETALLE_LISTAR
go
create proc SP_CATEGORIA_DETALLE_LISTAR
as
begin
	select * from categoria_detalle;	
end
go

if object_id('SP_ESTUDIANTE_LISTAR') is not null
drop proc SP_ESTUDIANTE_LISTAR
go
create proc SP_ESTUDIANTE_LISTAR
as
begin
	select * from estudiante;
end
go

if object_id('SP_ESTUDIANTE_LISTAR_MATRICULADOS') is not null
drop proc SP_ESTUDIANTE_LISTAR_MATRICULADOS
go
create proc SP_ESTUDIANTE_LISTAR_MATRICULADOS
as
begin
	select * from estudiante e
	inner join matricula m on m.id_estudiante = e.id;
end
go

if object_id('SP_APODERADO_LISTAR') is not null
drop proc SP_APODERADO_LISTAR
go
create proc SP_APODERADO_LISTAR
as
begin
	select * from apoderado;
end
go

if object_id('SP_MATRICULA_LISTAR') is not null
drop proc SP_MATRICULA_LISTAR
go
create proc SP_MATRICULA_LISTAR
as
begin
	select * from matricula;
end
go

if object_id('SP_USUARIO_LISTAR') is not null
drop proc SP_USUARIO_LISTAR
go
create proc SP_USUARIO_LISTAR
as
begin
	select
	id,
	isnull(rol,'') as rol,
	isnull(tipo_documento,'') as tipo_documento,
	isnull(numero_documento,'') as numero_documento,
	nombre,
	apellido,
	isnull(direccion,'') as direccion,
	isnull(telefono,'') as telefono,
	email,
	clave,
	isnull(genero,'') as genero,
	isnull(fecha_nacimiento,'') as fecha_nacimiento,
	fecha_registro,
	fecha_actualizacion,
	fecha_acceso,
	estado
	from usuario;
end
go

if object_id('SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA') is not null
drop proc SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA
go
create proc SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA(
@id_matricula int
)
as begin
	select * from matricula_detalle
	where id_matricula=@id_matricula;
end
go

if object_id('SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA_PENDIENTE') is not null
drop proc SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA_PENDIENTE
go
create proc SP_MATRICULA_DETALLE_LISTAR_ID_MATRICULA_PENDIENTE(
@id_matricula int
)
as begin
	select * from matricula_detalle
	where id_matricula=@id_matricula and estado='PENDIENTE';
end
go

if object_id('SP_MATRICULA_DETALLE_LISTAR_ID') is not null
drop proc SP_MATRICULA_DETALLE_LISTAR_ID
go
create proc SP_MATRICULA_DETALLE_LISTAR_ID(
@id int
)
as begin
	select * from matricula_detalle where id=@id;
end
go

if object_id('SP_MATRICULA_DETALLE_LISTAR') is not null
drop proc SP_MATRICULA_DETALLE_LISTAR
go
create proc SP_MATRICULA_DETALLE_LISTAR
as begin
	select * from matricula_detalle;
end
go

if object_id('SP_MATRICULA_DETALLE_LISTAR_PORDNIESTUDIANTE') is not null
drop proc SP_MATRICULA_DETALLE_LISTAR_PORDNIESTUDIANTE
go
create proc SP_MATRICULA_DETALLE_LISTAR_PORDNIESTUDIANTE(
@numero_documento varchar(20)
)
as begin
	select md.* from matricula_detalle md
	join matricula m on m.id=md.id_matricula
	join estudiante e on e.id = m.id_estudiante
	where e.numero_documento = @numero_documento and md.estado='PENDIENTE';
end
go

if object_id('SP_ESTUDIANTE_CREAR') is not null
drop proc SP_ESTUDIANTE_CREAR
go
create proc SP_ESTUDIANTE_CREAR(
@tipo_documento varchar(10),
@numero_documento varchar(20),
@nombre varchar(50),
@apellido varchar(50),
@direccion varchar(50),
@telefono varchar(20),
@email varchar(20),
@genero char(1),
@fecha_nacimiento date
)
as
begin
	insert into estudiante(tipo_documento,numero_documento,nombre,apellido,direccion,telefono,email,genero,fecha_nacimiento) values
	(@tipo_documento,@numero_documento,@nombre,@apellido,@direccion,@telefono,@email,@genero,@fecha_nacimiento);
end
go

if object_id('SP_APODERADO_CREAR') is not null
drop proc SP_APODERADO_CREAR
go
create proc SP_APODERADO_CREAR(
@tipo_documento varchar(10),
@numero_documento varchar(20),
@nombre varchar(50),
@apellido varchar(50),
@direccion varchar(50),
@telefono varchar(20),
@email varchar(20),
@relacion varchar(10),
@fecha_nacimiento date
)
as
begin
	insert into apoderado(tipo_documento,numero_documento,nombre,apellido,direccion,telefono,email,relacion,fecha_nacimiento) values
	(@tipo_documento,@numero_documento,@nombre,@apellido,@direccion,@telefono,@email,@relacion,@fecha_nacimiento);
end
go

if object_id('SP_USUARIO_CREAR') is not null
drop proc SP_USUARIO_CREAR
go
create proc SP_USUARIO_CREAR(
@rol varchar(20),
@tipo_documento varchar(10),
@numero_documento varchar(20),
@nombre varchar(50),
@apellido varchar(50),
@direccion varchar(50),
@telefono varchar(20),
@email varchar(50),
@clave varchar(50),
@genero char(1),
@fecha_nacimiento date
)
as
begin
	insert into usuario(rol,tipo_documento,numero_documento,nombre,apellido,direccion,telefono,email,clave,genero,fecha_nacimiento) values
	(@rol,@tipo_documento,@numero_documento,@nombre,@apellido,@direccion,@telefono,@email,@clave,@genero,@fecha_nacimiento);
end
go

if object_id('SP_MATRICULA_CREAR') is not null
drop proc SP_MATRICULA_CREAR
go
create proc SP_MATRICULA_CREAR(
@id_estudiante int,
@id_apoderado int,
@periodo varchar(10),
@nivel varchar(10),
@grado char(1),
@seccion char(1),
@situacion varchar(20),
@descripcion varchar(50)
)
as
begin
	insert into matricula(id_estudiante,id_apoderado,periodo,nivel,grado,seccion,situacion,descripcion) values
	(@id_estudiante,@id_apoderado,@periodo,@nivel,@grado,@seccion,@situacion,@descripcion);
end
go

if object_id('SP_USUARIO_REGISTRAR') is not null
drop proc SP_USUARIO_REGISTRAR
go
create proc SP_USUARIO_REGISTRAR(
@nombre varchar(50),
@apellido varchar(50),
@email varchar(50),
@clave varchar(50)
)
as
begin
	insert into usuario(nombre,apellido,email,clave) values
	(@nombre,@apellido,@email,@clave);
end
go

if object_id('SP_ESTUDIANTE_LEER_PORID') is not null
drop proc SP_ESTUDIANTE_LEER_PORID
go
create proc SP_ESTUDIANTE_LEER_PORID(
@id varchar(20)
)
as
begin
	select * from estudiante
	where id=@id;
end
go

exec SP_ESTUDIANTE_LEER_PORDNI '47054857';

if object_id('SP_ESTUDIANTE_LEER_PORDNI') is not null
drop proc SP_ESTUDIANTE_LEER_PORDNI
go
create proc SP_ESTUDIANTE_LEER_PORDNI(
@dni varchar(20)
)
as
begin
	select * from estudiante
	where numero_documento=@dni;
end
go

if object_id('SP_ESTUDIANTE_VALIDA_EXISTENCIA') is not null
drop proc SP_ESTUDIANTE_VALIDA_EXISTENCIA
go
create proc SP_ESTUDIANTE_VALIDA_EXISTENCIA(
@dni varchar(20)
)
as
begin
	select count(*) from estudiante
	where numero_documento=@dni;
end
go

if object_id('SP_APODERADO_LEER_PORDNI') is not null
drop proc SP_APODERADO_LEER_PORDNI
go
create proc SP_APODERADO_LEER_PORDNI(
@dni varchar(20)
)
as
begin
	select * from apoderado
	where numero_documento=@dni;
end
go

if object_id('SP_APODERADO_LEER_PORID') is not null
drop proc SP_APODERADO_LEER_PORID
go
create proc SP_APODERADO_LEER_PORID(
@id varchar(20)
)
as
begin
	select * from apoderado
	where id=@id;
end
go

if object_id('SP_MATRICULA_LEER_PORID') is not null
drop proc SP_MATRICULA_LEER_PORID
go
create proc SP_MATRICULA_LEER_PORID(
@id int
)
as
begin
	select * from matricula
	where id=@id;
end
go

if object_id('SP_MATRICULA_LEER_PORIDESTUDIANTE') is not null
drop proc SP_MATRICULA_LEER_PORIDESTUDIANTE
go
create proc SP_MATRICULA_LEER_PORIDESTUDIANTE(
@id_estudiante int
)
as
begin
	select * from matricula
	where id_estudiante=@id_estudiante;
end
go

if object_id('trigger_nueva_matricula') is not null
drop trigger trigger_nueva_matricula
go
create trigger trigger_nueva_matricula
on matricula for insert
as
begin
	declare @periodo varchar(10)
	declare @id_matricula int
	set @id_matricula = (select id from inserted)
	set @periodo = (select periodo from matricula where id=@id_matricula)
	insert into matricula_detalle(id_matricula,concepto,monto) values
	(@id_matricula,'MATRICULA '+@periodo,100),
	(@id_matricula,'PENSION MARZO '+@periodo,250),
	(@id_matricula,'PENSION ABRIL '+@periodo,250),
	(@id_matricula,'PENSION MAYO '+@periodo,250),
	(@id_matricula,'PENSION JUNIO '+@periodo,250),
	(@id_matricula,'PENSION JULIO '+@periodo,250),
	(@id_matricula,'PENSION AGOSTO '+@periodo,250),
	(@id_matricula,'PENSION SEPTIEMBRE '+@periodo,250),
	(@id_matricula,'PENSION OCTUBRE '+@periodo,250),
	(@id_matricula,'PENSION NOVIEMBRE '+@periodo,250),
	(@id_matricula,'PENSION DICIEMBRE '+@periodo,250);
end
go

if object_id('SP_MATRICULA_VALIDAEXISTENCIA') is not null
drop proc SP_MATRICULA_VALIDAEXISTENCIA
go
create proc SP_MATRICULA_VALIDAEXISTENCIA(
@id_estudiante int,
@periodo varchar(10)
)
as
begin
	select count(*) from matricula
	where id_estudiante=@id_estudiante and periodo=@periodo;
end
go

if object_id('SP_USUARIO_AUTENTICAR') is not null
drop proc SP_USUARIO_AUTENTICAR
go
create proc SP_USUARIO_AUTENTICAR(
@email varchar(50),
@clave varchar(50)
)
as
begin
	select
		id,
		isnull(rol,'') as rol,
		isnull(tipo_documento,'') as tipo_documento,
		isnull(numero_documento,'') as numero_documento,
		nombre,
		apellido,
		isnull(direccion,'') as direccion,
		isnull(telefono,'') as telefono,
		email,
		clave,
		isnull(genero,'') as genero,
		isnull(fecha_nacimiento,'') as fecha_nacimiento,
		fecha_registro,
		fecha_actualizacion,
		fecha_acceso,
		estado
	from usuario
	where email=@email and clave=@clave;
end
go

if object_id('SP_PAGO_CREAR') is not null
drop proc SP_PAGO_CREAR
go
create proc SP_PAGO_CREAR(
@id_matricula int,
@monto_total decimal(6,2),
@metodo_pago varchar(20),
@numero_op varchar(20),
@tDetalle type_pago_detalle readonly
)
as
begin
	insert into pago(id_matricula,monto_total,metodo_pago,numero_op) values
	(@id_matricula,@monto_total,@metodo_pago,@numero_op);

	update matricula_detalle set
	estado = 'COMPLETADO'
	from matricula_detalle md
	inner join @tDetalle td on td.id_matricula_detalle=md.id;

	insert into pago_detalle (id_pago,id_matricula_detalle,concepto,monto)
	select @@identity, td.id_matricula_detalle, td.concepto, td.monto from @tDetalle td;
end
go

if object_id('SP_PAGO_LISTAR') is not null
drop proc SP_PAGO_LISTAR
go
create proc SP_PAGO_LISTAR
as begin
	select * from pago
end
go

if object_id('SP_PAGO_LEER_PORID') is not null
drop proc SP_PAGO_LEER_PORID
go
create proc SP_PAGO_LEER_PORID(
@id int
)
as begin
	select * from pago where id=@id;
end
go

if object_id('SP_PAGO_DETALLE_LISTAR') is not null
drop proc SP_PAGO_DETALLE_LISTAR
go
create proc SP_PAGO_DETALLE_LISTAR
as
begin
	select * from pago_detalle;
end
go

if object_id('SP_PAGO_DETALLE_LISTAR_PORIDPAGO') is not null
drop proc SP_PAGO_DETALLE_LISTAR_PORIDPAGO
go
create proc SP_PAGO_DETALLE_LISTAR_PORIDPAGO(
@id_pago int
)
as
begin
	select * from pago_detalle where id_pago=@id_pago;
end
go

--if object_id('SP_PAGO_CREAR_TEST') is not null
--drop proc SP_PAGO_CREAR_TEST
--go
--create proc SP_PAGO_CREAR_TEST(
--@id_estudiante int,
--@id_matricula varchar(20),
--@monto_total decimal(6,2),
--@metodo_pago varchar(20),
--@numero_op varchar(20),
--@tDetalle type_pago_detalle readonly
--)
--as
--begin
--	update matricula_detalle set
--	estado = 'COMPLETADO'
--	from matricula_detalle md
--	inner join @tDetalle td on md.id = td.id;
--	--INSERT INTO pago_detalle (id, id_matricula_detalle)
--	--SELECT D.id, D.id_matricula_detalle
--	--FROM @tDetalle D;
--end
--go

insert into categoria (nombre) values
('DOCUMENTO'),
('ROL'),
('GENERO'),
('RELACION'),
('PERIODO'),
('NIVEL'),
('GRADO'),
('SECCION'),
('SITUACION'),
('MEDIO PAGO');

insert into categoria_detalle (id_categoria,nombre) values
(100,'DNI'),
(100,'CEX'),
(100,'PAS'),
(101,'ADMIN'),
(101,'CONTABILIDA'),
(101,'TESORERIA'),
(101,'FACTURACION'),
(101,'TESORERIA'),
(102,'M'),
(102,'F'),
(103,'PADRE'),
(103,'MADRE'),
(103,'ABUELO'),
(103,'TIO'),
(104,'2020'),
(104,'2021'),
(104,'2022'),
(104,'2023'),
(104,'2024'),
(105,'INICIAL'),
(105,'PRIMARIA'),
(105,'SECUNDARIA'),
(106,'1'),
(106,'2'),
(106,'3'),
(106,'4'),
(106,'5'),
(106,'6'),
(107,'A'),
(107,'B'),
(107,'C'),
(108,'REPITE'),
(108,'PROMOVIDO'),
(108,'INGRESO'),
(109,'EFECTIVO'),
(109,'DEPOSITO'),
(109,'PLIN'),
(109,'YAPE');

INSERT INTO estudiante (tipo_documento, numero_documento, nombre, apellido, direccion, telefono, email, genero, fecha_nacimiento) VALUES
('DNI', '45012192', 'Juan', 'perez', 'Av. Lima 123', '900123456', 'perez@gmail.com', 'M', '1995/05/20'),
('CEX', '46098456', 'Ana', 'gonzalez', 'Calle Sucre 456', '901654321', 'gonzalez@gmail.com', 'F', '1992/04/15'),
('PAS', '47054857', 'Luis', 'lopez', 'Jr. Tacna 789', '902345678', 'lopez@gmail.com', 'M', '2000/11/10'),
('DNI', '48012111', 'Maria', 'diaz', 'Av. Arequipa 321', '903456789', 'diaz@gmail.com', 'F', '1998/03/25'),
('CEX', '49098444', 'Carlos', 'martinez', 'Calle Callao 654', '904567890', 'martinez@gmail.com', 'M', '1993/12/30'),
('PAS', '50054555', 'Sofia', 'carrillo', 'Jr. Huancavelica 987', '905678901', 'carrillo@gmail.com', 'F', '2001/09/12'),
('DNI', '51012888', 'Pedro', 'alvarez', 'Av. Puno 234', '906789012', 'alvarez@gmail.com', 'M', '1994/06/18'),
('CEX', '52098119', 'Laura', 'sanchez', 'Calle Arequipa 567', '907890123', 'sanchez@gmail.com', 'F', '2003/02/14'),
('PAS', '53054119', 'Diego', 'romero', 'Jr. Lambayeque 876', '908901234', 'romero@gmail.com', 'M', '1991/07/22'),
('DNI', '54012328', 'Elena', 'hidalgo', 'Av. Amazonas 135', '909012345', 'hidalgo@gmail.com', 'F', '1997/10/05');

INSERT INTO apoderado (tipo_documento, numero_documento, nombre, apellido, direccion, telefono, email, relacion, fecha_nacimiento) VALUES
('DNI', '45012991', 'Fernando', 'perez', 'Av. San Martin 123', '900123456', 'perez@gmail.com', 'PADRE', '1990/06/15'),
('CEX', '46098882', 'Lucia', 'gonzalez', 'Calle El Sol 456', '901654321', 'gonzalez@gmail.com', 'MADRE', '1992/08/20'),
('PAS', '47054773', 'Ricardo', 'lopez', 'Jr. La Paz 789', '902345678', 'lopez@gmail.com', 'TIO', '2000/10/10'),
('DNI', '48012664', 'Carmen', 'diaz', 'Av. Moquegua 321', '903456789', 'diaz@gmail.com', 'MADRE', '1995/02/25'),
('CEX', '49098556', 'Alberto', 'martinez', 'Calle Chanchamayo 654', '904567890', 'martinez@gmail.com', 'PADRE', '1993/12/01'),
('PAS', '50054667', 'Luz', 'carrillo', 'Jr. Huancayo 987', '905678901', 'carrillo@gmail.com', 'ABUELO', '1999/04/12'),
('DNI', '51012778', 'Javier', 'alvarez', 'Av. La Libertad 234', '906789012', 'alvarez@gmail.com', 'PADRE', '1991/07/17'),
('CEX', '52098889', 'Rosa', 'sanchez', 'Calle Los Heroes 567', '907890123', 'sanchez@gmail.com', 'MADRE', '2003/03/30'),
('PAS', '53054990', 'Jorge', 'romero', 'Jr. Los Angeles 876', '908901234', 'romero@gmail.com', 'TIO', '1994/11/22'),
('DNI', '54012001', 'Isabel', 'hidalgo', 'Av. Tacna 135', '909012345', 'hidalgo@gmail.com', 'ABUELO', '1996/12/05');

INSERT INTO usuario (rol, tipo_documento, numero_documento, nombre, apellido, direccion, telefono, email, clave, genero, fecha_nacimiento) VALUES
('ADMINISTRADOR', 'DNI', '45012122', 'Samuel', 'perez', 'Av. San Bartolo 123', '900123456', 'perez@gmail.com', '123', 'M', '1993/05/15'),
('CONTABILIDAD', 'CEX', '46098344', 'Juana', 'gonzalez', 'Calle Lima 456', '901654321', 'gonzalez@gmail.com', '456', 'F', '1992/07/20'),
('TESORERIA', 'PAS', '47054566', 'Nicolas', 'lopez', 'Jr. Cusco 789', '902345678', 'lopez@gmail.com', '789', 'M', '1994/12/10'),
('FACTURACION', 'DNI', '48012677', 'Rosa', 'diaz', 'Av. Arequipa 321', '903456789', 'diaz@gmail.com', '321', 'F', '1995/02/25'),
('SECRETARIA', 'CEX', '49098788', 'Esteban', 'martinez', 'Calle Callao 654', '904567890', 'martinez@gmail.com', '654', 'M', '1993/06/01'),
('ADMINISTRADOR', 'PAS', '50054899', 'Angela', 'carrillo', 'Jr. Huancayo 987', '905678901', 'carrillo@gmail.com', '987', 'F', '1991/10/12'),
('CONTABILIDAD', 'DNI', '51012900', 'David', 'alvarez', 'Av. La Victoria 234', '906789012', 'alvarez@gmail.com', '147', 'M', '2000/11/17'),
('TESORERIA', 'CEX', '52098122', 'Patricia', 'sanchez', 'Calle Villanueva 567', '907890123', 'sanchez@gmail.com', '258', 'F', '1999/03/30'),
('FACTURACION', 'PAS', '53054233', 'Antonio', 'romero', 'Jr. Huancayo 876', '908901234', 'romero@gmail.com', '369', 'M', '1994/08/22'),
('SECRETARIA', 'DNI', '54012044', 'Carla', 'hidalgo', 'Av. Tacna 135', '909012345', 'hidalgo@gmail.com', '741', 'F', '1998/12/05');

select * from categoria
select * from categoria_detalle
select * from apoderado
select * from estudiante
select * from usuario
select * from matricula
select * from matricula_detalle
select * from pago
select * from pago_detalle

--realizar 10 inserciones de una tabla estudiante con los siguientes campos:
--tipo_documento de tipo varchar(3) DNI, CEX, PAS
--numero_documento de varchar(10) con números entre 40000000 hasta 80000000
--nombre varchar(50)
--apellido varchar(50)
--dirección varchar(50)
--teléfono varchar(9) con números entre 900000000 hasta 999999999
--email varchar(50) que contenga solo apellido minúscula sin tilde
--genero char(1) M o F
--fecha_nacimiento entre 1990/01/31 y 2004/12/31

--realizar 10 inserciones de una tabla apoderado con los siguientes campos:
--tipo_documento de tipo varchar(3) DNI, CEX, PAS
--numero_documento de varchar(10) con números entre 40000000 hasta 80000000
--nombre varchar(50)
--apellido varchar(50)
--dirección varchar(50)
--teléfono varchar(9) con números entre 900000000 hasta 999999999
--email varchar(50) que contenga solo apellido minúscula sin tilde
--relacion varchar(10) solo PADRE, MADRE, ABUELO, TIO
--fecha_nacimiento entre 1990/01/31 y 2004/12/31

--realizar 10 inserciones de una tabla usuarios con los siguientes campos:
--rol varchar(10) de tipo ADMIN, CONTABILIDAD, TESORERIA, FACTURACION, SECRETARIA
--tipo_documento de tipo varchar(3) DNI, CEX, PAS
--numero_documento de varchar(10) con números entre 40000000 hasta 80000000
--nombre varchar(50)
--apellido varchar(50)
--dirección varchar(50)
--teléfono varchar(9) con números entre 900000000 hasta 999999999
--email varchar(50) que contenga solo apellido minúscula sin tilde
--clave varchar(50) números entre 0 hasta 1000
--genero char(1) M o F
--fecha_nacimiento entre 1990/01/31 y 2004/12/31