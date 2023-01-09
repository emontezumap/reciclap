insert INTO 
    usuarios
    (nombre, id_ciudad, id_grupo, clave)
values
    ('admin2', 'ec5cb176-757a-4d71-b06a-3c7ca7dcadf2', '97cfae57-45b6-4194-83c4-1311e3346e42', '1c142b2d01aa34e9a36bde480645a57fd69e14155dacfab5a3f9257b77fdc8d8');
insert into paises
    (nombre)
values
    ('Venezuela');
insert into paises
    (nombre)
values
    ('México');
select id, nombre
from paises
insert into estados
    (nombre, id_pais)
values
    ('Falcón', '');
select *
from estados;
insert into ciudades
    (nombre, id_estado)
values
    ('Punto Fijo', '7ef08be4-a2d1-435a-9713-df90e25e3c3a');
insert into ciudades
    (nombre, id_estado)
values
    ('Maracay', 'dc0b9693-bf5f-47b4-9a28-ecac4e22feb0');
insert into ciudades
    (nombre, id_estado)
values
    ('Judibana', '7ef08be4-a2d1-435a-9713-df90e25e3c3a');
insert into ciudades
    (nombre, id_estado)
values
    ('Turmero', 'dc0b9693-bf5f-47b4-9a28-ecac4e22feb0');
select *
from ciudades;
update ciudades set activo = 1;

insert into grupos
    (descripcion, es_administrador)
values
    ('Usuarios', 0);
select *
from grupos;
use prueba;
insert into profesiones
    (descripcion)
values
    ('Ing. de Sistemas');
insert into profesiones
    (descripcion)
values
    ('Chef');
insert into profesiones
    (descripcion)
values
    ('Ing. Químico');
select *
from profesiones

select nombre, email, id_grupo, clave
from usuarios;
select id, descripcion
from grupos;

update usuarios set email = 'admin2api@yandex.com' where nombre = 'admin2';

