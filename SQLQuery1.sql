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
and [Module attest] = 'Geslaagd'

create or alter view AantalModulesPerAfgestudeerdeStudent as
select count(tblStudentGegevens.Module) as aantalModules, GeslaagdeStudenten.Stamnummer
from tblStudentGegevens INNER JOIN GeslaagdeStudenten ON tblStudentGegevens.Stamnummer = GeslaagdeStudenten.Stamnummer
group by   GeslaagdeStudenten.Stamnummer

select ROUND(AVG(CAST(AantalModulesPerAfgestudeerdeStudent.aantalModules as float)),2), YEAR([Module begindatum])
from tblStudentGegevens INNER JOIN AantalModulesPerAfgestudeerdeStudent ON tblStudentGegevens.Stamnummer = AantalModulesPerAfgestudeerdeStudent.Stamnummer
GRoup by YEAR([Module begindatum])


/**********Aantal afgestudeerden per semester***************/

select COUNT(Stamnummer) as [aantal afgestudeerden], MONTH([Module begindatum]) as maand, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
where Module = 'Module Toegepaste verpleegkunde (40 weken)' and [Module attest] = 'Geslaagd'
group by  MONTH([Module begindatum]), YEAR([Module begindatum])


/*aantal reden stoppen per semester*/
select coalesce(nullif([Reden stoppen] ,''), 'onbekend') as [Reden stoppen], count([Reden stoppen]) as aantal, MONTH([Module begindatum]) as maand, YEAR([Module begindatum])
from tblStudentGegevens
group by [Reden stoppen], MONTH([Module begindatum]), YEAR([Module begindatum])

/*************************************aantal school leren kennen per semester************************************/



select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as aantal, MONTH([Module begindatum]) as maand, YEAR([Module begindatum])
from tblStudentGegevens
group by [School leren kennen], MONTH([Module begindatum]), YEAR([Module begindatum])