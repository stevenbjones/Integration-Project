
/* 2 query's, man vrouw per semester */

select COUNT(Geslacht) as 'Aantal Vrouw',CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar
FROM dbo.tblStudentGegevens  
where Geslacht = 'V' 
group  by CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])
order by YEAR([Module begindatum]),semester 


select COUNT(Geslacht) as 'Aantal Man' ,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum])
FROM dbo.tblStudentGegevens  
where Geslacht = 'M' 
group  by CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END, YEAR([Module begindatum])
order by  YEAR([Module begindatum]), semester 
/****************************Aantal Studenten / module **********************************************/

select Module ,count(Geslacht) , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])
order by  YEAR([Module begindatum]), semester 

/*************************************Geslaagde mensen/module *************************************/

select Module ,COUNT([Module attest]) as geslaagd , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
where [Module attest] = 'Geslaagd'
group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])
order by  YEAR([Module begindatum]), semester 

/******************************************Gemiddelde duur aantal semesters van de opleiding / student PER MOdule per afstudeerjaar********************************************************/

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


/**********Aantal modules dat hernomen worden per module per semester***************/
/*Klopt deze data?*/
create or alter view StudentenMetEenVorigeKlas as
select Module as module ,[Module attest] as moduleAttest, REPLACE([Klas vorig schooljaar], ' ', '') as VorigeKlas  ,Klas as Klas,[Module begindatum] as begindatum
from tblStudentGegevens
where [Klas vorig schooljaar]  != ''

select Module,CASE WHEN MONTH([begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR(begindatum), substring(VorigeKlas, 1, len(VorigeKlas) - 1) , substring([Klas], 1, len([Klas])-1)
from StudentenMetEenVorigeKlas
where substring(VorigeKlas, 1, len(VorigeKlas) - 1) =  substring([Klas], 1, len([Klas])-1)

select Module,count(Module),CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum])
from tblStudentGegevens
where [Verleende studiebewijzen 1ste zit] = ''
and [Reden stoppen] = ''
and [Klas vorig schooljaar] = ''
group by Module,  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, semester ASC

/**********Aantal afgestudeerden per semester***************/

select COUNT(Stamnummer) as [aantal afgestudeerden], CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar
from tblStudentGegevens
where Module = 'Module Toegepaste verpleegkunde (40 weken)' and [Module attest] = 'Geslaagd'
group by  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])


/*aantal reden stoppen per semester*/
select  [Reden stoppen] ,count([Reden stoppen]) as aantal,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester , YEAR([Module begindatum])
from tblStudentGegevens
where [Reden stoppen] != ''
group by [Reden stoppen],CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])
order by  YEAR([Module begindatum]) ASC, semester ASC

/*************************************aantal school leren kennen per semester************************************/


select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as aantal, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum])
from tblStudentGegevens
where Module = 'Module Initiatie verpleegkunde (20 weken)'
group by [School leren kennen], CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])
HAVING YEAR([Module begindatum]) >= (Year(GETDATE())-5)
order by  YEAR([Module begindatum]) ASC, semester ASC

/**********************************************************************************/
voorbije 5 jaar


