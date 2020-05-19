select * from dbo.tblStudentGegevens


SELECT mannen = Round(((select COUNT(Geslacht) * 100 FROM dbo.tblStudentGegevens  where Geslacht = 'M' )),1) / COUNT(Geslacht), vrouwen = (select COUNT(Geslacht) * 100 FROM dbo.tblStudentGegevens where Geslacht = 'V' )  / COUNT(Geslacht)
from tblStudentGegevens



select mannen = (select COUNT(Geslacht) * 100 FROM dbo.tblStudentGegevens  where Geslacht = 'M' ) / 
COUNT(Geslacht), vrouwen = (select COUNT(Geslacht) * 100 FROM dbo.tblStudentGegevens  where Geslacht = 'V' )  / COUNT(Geslacht)
from dbo.tblStudentGegevens  where MONTH([Module begindatum]) =02 Group By YEAR([Module begindatum])

select mannen =  (select COUNT(Geslacht) * 100 FROM dbo.tblStudentGegevens  where Geslacht = 'M' ) / COUNT(Geslacht)
from dbo.tblStudentGegevens 
group by YEAR([Module begindatum])





/* 2 query's, man vrouw per semester */

select COUNT(Geslacht) , MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar
FROM dbo.tblStudentGegevens  
where Geslacht = 'V' 
group  by MONTH([Module begindatum]) , YEAR([Module begindatum])


select COUNT(Geslacht) , MONTH([Module begindatum]), YEAR([Module begindatum])
FROM dbo.tblStudentGegevens  
where Geslacht = 'M' 
group  by MONTH([Module begindatum]) , YEAR([Module begindatum])

/****************************Aantal Studenten / module **********************************************/

select Module ,count(Geslacht) , MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
group by module , MONTH([Module begindatum]) , YEAR([Module begindatum])

/*************************************Geslaagde mensen/module *************************************/

select Module ,COUNT([Module attest]) as geslaagd ,count(Geslacht), MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
where [Module attest] = 'Geslaagd'
group by module , MONTH([Module begindatum]) , YEAR([Module begindatum])

/******************************************Gemiddelde duur aantal semesters van de opleiding / student PER MOdule per afstudeerjaar********************************************************/
select COUNT(Module), Stamnummer
from tblStudentGegevens
where Module = 'Module Toegepaste verpleegkunde (40 weken)'
and [Module attest] = 'Geslaagd'
group by Stamnummer
order by Stamnummer DESC


create or alter view GeslaagdeStudenten as
select Stamnummer, Module
from tblStudentGegevens
where Module = 'Module Toegepaste verpleegkunde (40 weken)'
and [Module attest] = 'Geslaagd'sq

create or alter view AantalModulesPerAfgestudeerdeStudent as
select count(tblStudentGegevens.Module) as aantalModules, GeslaagdeStudenten.Stamnummer
from tblStudentGegevens INNER JOIN GeslaagdeStudenten ON tblStudentGegevens.Stamnummer = GeslaagdeStudenten.Stamnummer
group by   GeslaagdeStudenten.Stamnummer

select ROUND(AVG(CAST(AantalModulesPerAfgestudeerdeStudent.aantalModules as float)),2), YEAR([Module begindatum])
from tblStudentGegevens INNER JOIN AantalModulesPerAfgestudeerdeStudent ON tblStudentGegevens.Stamnummer = AantalModulesPerAfgestudeerdeStudent.Stamnummer
GRoup by YEAR([Module begindatum])

/*********Gemiddelde duur per student (diegene die afgestudeerd zijn) per student***************/

select count(tblStudentGegevens.Module) as aantalModules, GeslaagdeStudenten.Stamnummer
from tblStudentGegevens INNER JOIN GeslaagdeStudenten ON tblStudentGegevens.Stamnummer = GeslaagdeStudenten.Stamnummer
group by   GeslaagdeStudenten.Stamnummer
substring([Klas vorig schooljaar], 0, len([Klas vorig schooljaar]) - 1) =  substring([Klas], 0, len([Klas])-1)

/**********Aantal modules dat hernomen worden per module per semester***************/

select Module, count(Module) as aantalModules,MONTH([Module begindatum]), YEAR([Module begindatum])
from tblStudentGegevens
where [Klas vorig schooljaar]  != ''
and substring([Klas vorig schooljaar], 0, len([Klas vorig schooljaar]) - 1) =  substring([Klas], 0, len([Klas])-1)
and [Module attest] != 'Geslaagd'
group by Module, MONTH([Module begindatum]), YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, MONTH([Module begindatum]) ASC


create or alter view StudentenMetEenVorigeKlas as
select Module as module ,[Module attest] as moduleAttest, REPLACE([Klas vorig schooljaar], ' ', '') as VorigeKlas  ,Klas as Klas,[Module begindatum] as begindatum
from tblStudentGegevens
where [Klas vorig schooljaar]  != ''

select Module,MONTH(begindatum), YEAR(begindatum), substring(VorigeKlas, 1, len(VorigeKlas) - 1) , substring([Klas], 1, len([Klas])-1)
from StudentenMetEenVorigeKlas
where substring(VorigeKlas, 1, len(VorigeKlas) - 1) =  substring([Klas], 1, len([Klas])-1)

select Module,count(Module), MONTH([Module begindatum]), YEAR([Module begindatum])
from tblStudentGegevens
where [Verleende studiebewijzen 1ste zit] = ''
and Module != 'Module Toegepaste verpleegkunde (40 weken)'
and [Reden stoppen] = ''
and [Klas vorig schooljaar] = ''
group by Module,  MONTH([Module begindatum]), YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, MONTH([Module begindatum]) ASC

group by Module, MONTH(begindatum), YEAR(begindatum)
order by  YEAR(Module begindatum) ASC, MONTH([Module begindatum]) ASC



/*****/

select *
from tblStudentGegevens
where YEAR([Module begindatum]) = '2017'
and [Reden stoppen] = ''
and [Verleende studiebewijzen 1ste zit] = ''
and Module = 'Module Toegepaste verpleegkunde (40 weken)'


/**********Aantal afgestudeerden per semester***************/

select COUNT(Stamnummer) as [aantal afgestudeerden], MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
where Module = 'Module Toegepaste verpleegkunde (40 weken)' and [Module attest] = 'Geslaagd'
group by  MONTH([Module begindatum]), YEAR([Module begindatum])


/*aantal reden stoppen per semester*/
select  [Reden stoppen] ,count([Reden stoppen]) as aantal, MONTH([Module begindatum]) as maand, YEAR([Module begindatum])
from tblStudentGegevens
where [Reden stoppen] != ''
group by [Reden stoppen], MONTH([Module begindatum]), YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, MONTH([Module begindatum]) ASC

/*************************************aantal school leren kennen per semester************************************/



select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as aantal, MONTH([Module begindatum]) as maand, YEAR([Module begindatum])
from tblStudentGegevens
where [Klas vorig schooljaar] not LIKE Klas + '*'
and Module = 'Module Initiatie verpleegkunde (20 weken)'
group by [School leren kennen], MONTH([Module begindatum]), YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, MONTH([Module begindatum]) ASC



select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as aantal, MONTH([Module begindatum]) as maand, YEAR([Module begindatum])
from tblStudentGegevens
where Module = 'Module Initiatie verpleegkunde (20 weken)'
group by [School leren kennen], MONTH([Module begindatum]), YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, MONTH([Module begindatum]) ASC

/**********************************************************************************/
voorbije 5 jaar