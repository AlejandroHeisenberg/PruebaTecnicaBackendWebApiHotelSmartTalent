insert into Hoteles values (NEWID(),'Hotel las Margaritas', 'cra 22a # 8d 08 sur','Madrid','+573116764558','hotelMargarita@correo.com',1,'2024-02-10 16:40:00','2024-02-10 20:40:00')

select * from hoteles
select * from habitaciones
select * from Reservas

insert into Habitaciones values (NEWID(),'4060BC89-77A0-4CFD-86D6-0ACA7215999B',50000,19,'sencilla', 'Piso 1, Habitación 101',0,'2024-02-14 17:16:21.747','2024-02-14 17:16:21.747',1)
insert into Habitaciones values (NEWID(),'4060BC89-77A0-4CFD-86D6-0ACA7215999B',850000,19,'Suite', 'Piso 5, Habitación 501, Vista a la ciudad',0,'2024-02-14 17:20:19.647','2024-02-14 17:20:19.647',1)



insert into Reservas values (NEWID(), '3181544C-CB2B-44F8-96ED-2524307D8344','2024-02-19 10:00:00.747','2024-02-22 17:00:00.747',2,'Alejandro Reyes','3126764658','alejo@correo.com',1,'2024-02-19 10:00:00.747','2024-02-19 10:00:00.747')
select ho.*, ha.IdHabitacion,ha.TipoHabitacion,ha.Ubicacion from habitaciones ha
inner join hoteles ho
on ho.IdHotel = Ha.IdHotel
where ho.IdHotel = '4060BC89-77A0-4CFD-86D6-0ACA7215999B'


UPDATE Reservas
SET Estado = 'RESERVADO'
WHERE IdReserva = 'D12355D8-C079-49F2-B6AD-43CFF2A36D91';



--consultar disponibilidad de habitaciones
select Habitaciones.IdHabitacion, Habitaciones.IdHotel,Habitaciones.Disponible, Habitaciones.TipoHabitacion, Hoteles.Nombre from Habitaciones
inner join Hoteles
on hoteles.IdHotel = Habitaciones.IdHotel  where habitaciones.Idhotel = '4060BC89-77A0-4CFD-86D6-0ACA7215999B' 

select * from Habitaciones where IdHabitacion in ('6CDC5CF1-B32E-42DA-8844-B996CF98D409','85E56EF9-4DCC-461B-99E2-CBD03C0CAB64','55F86518-7D1F-4E54-B586-D612B497B080');

--agregamos columna a la tabla
ALTER TABLE Habitaciones
ADD Disponible BIT NOT NULL DEFAULT 1;

delete from reservas where IdReserva = '318003FB-CF42-459C-A1AA-BE07317FDFC5'