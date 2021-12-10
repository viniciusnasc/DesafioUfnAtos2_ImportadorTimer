use AdoNet

create table Remedios(
Id int not null identity primary key,
Nome varchar(30) not null,
Horario Time not null
)

drop table Remedios
select * from remedios

select * from remedios where Horario = '08:00:00' 

select * from remedios where Horario = '08:00:00' and Nome = 'ALBENDAZOL'

select * from Pessoa where Horario = '{remedio.Horario}' and Nome = '{remedio.Nome}'

delete from Remedios where Horario = '17:30' and Nome = 'RISperidona'
