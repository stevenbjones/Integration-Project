﻿
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Section = Spire.Doc.Section;
 

namespace ProjectSAI
{

    public static class ExportWordDocument
    {
        public static void Create()
        {
            try
            {
                //Laad de template in
                Document testdoc = new Document();
                testdoc.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Template.docx");
                //Maak table
                Section section = testdoc.Sections[0];
                Table table = section.Tables[0] as Table;
                //Table table = new Table(testdoc);
                table.ApplyStyle(DefaultTableStyle.MediumShading1Accent1);

                BookmarksNavigator navigator = new BookmarksNavigator(testdoc);

                TextBodyPart part = new TextBodyPart(testdoc);

                /*Aantal studenten /module/sesmter */

                //Deze tabel klopt
                //Aantal studenten / module /semster1
                AddTableInBookmark("select Module, count(Geslacht) as value, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where MONTH([Module begindatum]) < 7  group by module ,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE())-5) order by  YEAR([Module begindatum]), semester", table, navigator, "TotaalAantalStudentenSem1", part);
                //Aantal studenten / module / semester2
                AddTableInBookmark("select Module, count(Geslacht) as value, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where MONTH([Module begindatum]) > 7  group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE())-5) order by  YEAR([Module begindatum]), semester", table, navigator, "TotaalAantalStudentenSem2", part);


                /*Geslaagde mensen / module per semester */
                //tabellen kloppen
                //Semester 1           
                AddTableInBookmark("select Module, COUNT([Module attest]) as value , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where[Module attest] = 'Geslaagd' and MONTH([Module begindatum]) < 7 group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]), semester", table, navigator, "MensenGeslaagdSem1", part);

                //Geslaagde mensen / module per semester 
                //semester 2            
                AddTableInBookmark("select Module, COUNT([Module attest]) as value , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where[Module attest] = 'Geslaagd' and MONTH([Module begindatum]) > 7 group by module , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]), semester", table, navigator, "MensenGeslaagdSem2", part);



                //Aantal afgestudeerden /semester 

                //semester 1
                AddTableInBookmarkWithoutGroups("select COUNT(Stamnummer) as value, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Module = 'Module Toegepaste verpleegkunde (40 weken)' and[Module attest] = 'Geslaagd' and MONTH([Module begindatum]) < 7 group by  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])", table, navigator, "AantalAfgestudeerdeStudentenSem1", part);

                //semester 2
                AddTableInBookmarkWithoutGroups("select COUNT(Stamnummer) as value, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Module = 'Module Toegepaste verpleegkunde (40 weken)' and[Module attest] = 'Geslaagd' and MONTH([Module begindatum]) > 7 group by  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum])", table, navigator, "AantalAfgestudeerdeStudentenSem2", part);


                //RedenStoppen  
                //Klopt
                //semester 1
                AddTableInBookmark("select[Reden stoppen] ,count([Reden stoppen]) as value,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester , YEAR([Module begindatum]) as jaar from tblStudentGegevens where [Reden stoppen] != '' and MONTH([Module begindatum]) < 7 group by [Reden stoppen],CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]) ASC, semester ASC ", table, navigator, "RedenStoppenSem1", part);

                //semester 2
                AddTableInBookmark("select[Reden stoppen] ,count([Reden stoppen]) as value,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester , YEAR([Module begindatum]) as jaar from tblStudentGegevens where [Reden stoppen] != '' and MONTH([Module begindatum]) > 7 group by [Reden stoppen],CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) order by  YEAR([Module begindatum]) ASC, semester ASC ", table, navigator, "RedenStoppenSem2", part);


                //school leren kennen    
                //Klopt
                //semester 1
                AddTableInBookmark("select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as value, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Module = 'Module Initiatie verpleegkunde (20 weken)' and MONTH([Module begindatum]) < 7 group by [School leren kennen], CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) order by  YEAR([Module begindatum]) ASC, semester ASC", table, navigator, "SchoolLerenKennenSem1", part);

                //semester 2
                AddTableInBookmark("select coalesce(nullif([School leren kennen],''), 'onbekend') as [school leren kennen], count([School leren kennen]) as value, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Module = 'Module Initiatie verpleegkunde (20 weken)' and MONTH([Module begindatum]) > 7 group by [School leren kennen], CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) order by  YEAR([Module begindatum]) ASC, semester ASC", table, navigator, "SchoolLerenKennenSem2", part);


                ////Aantal modules dat hernomen worden per module per semester*
                ////semester 1

                //// AddTableInBookmark("select Module, count(Module) as value ,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where [Verleende studiebewijzen 1ste zit] = '' and[Reden stoppen] = '' and[Klas vorig schooljaar] = '' and MONTH([Module begindatum]) < 7 group by Module,  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) order by  YEAR([Module begindatum]) ASC, semester ASC", table, navigator, "ModuleHernemenSem1", part);

                ////semester 2
                //if (ConnectDatabase.ExecuteQuery("create or alter view StudentenMetEenVorigeKlas as select Module as module,[Module attest] as moduleAttest, REPLACE([Klas vorig schooljaar], ' ', '') as VorigeKlas  ,Klas as Klas,[Module begindatum] as begindatum from tblStudentGegevens where[Klas vorig schooljaar]  != ''"))
                //{
                //    if (ConnectDatabase.ExecuteQuery())
                //        AddTableInBookmark("select Module, count(Module) as value ,CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where [Verleende studiebewijzen 1ste zit] = '' and[Reden stoppen] = '' and[Klas vorig schooljaar] = '' and MONTH([Module begindatum]) > 7 group by Module,  CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) order by  YEAR([Module begindatum]) ASC, semester ASC", table, navigator, "ModuleHernemenSem2", part);
                //}





                /******************/

                //Gemiddelde duur aantal semesters van de opleiding / student PER MOdule per afstudeerjaar (andere functie nodig)
                if (ConnectDatabase.ExecuteQuery("create or alter view GeslaagdeStudenten as select Stamnummer, Module from tblStudentGegevens where Module = 'Module Toegepaste verpleegkunde (40 weken)' and[Module attest] = 'Geslaagd'"))
                {
                    if (ConnectDatabase.ExecuteQuery("create or alter view AantalModulesPerAfgestudeerdeStudent as select count(tblStudentGegevens.Module) as aantalModules, GeslaagdeStudenten.Stamnummer from tblStudentGegevens INNER JOIN GeslaagdeStudenten ON tblStudentGegevens.Stamnummer = GeslaagdeStudenten.Stamnummer group by   GeslaagdeStudenten.Stamnummer"))
                    {
                        AddTableInBookmarkWithoutGroups("select ROUND(AVG(CAST(AantalModulesPerAfgestudeerdeStudent.aantalModules as float)),2)  as value, YEAR([Module begindatum]) as jaar from tblStudentGegevens INNER JOIN AantalModulesPerAfgestudeerdeStudent ON tblStudentGegevens.Stamnummer = AantalModulesPerAfgestudeerdeStudent.Stamnummer Group by YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5)", table, navigator, "GemiddeldeDuur", part);
                    }

                }


                //Percentage meisjes jongens
                if (ConnectDatabase.ExecuteQuery("create or alter view TotaalAantalStudentenPerSemester as select Count(Stamnummer) as 'value' , CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens group  by CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) "))
                {
                    if (ConnectDatabase.ExecuteQuery("create or alter view TotaalAantalVrouwenPerSemester as select Count(Stamnummer) as 'value', CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Geslacht = 'V' group  by CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5) "))
                    {
                        if (ConnectDatabase.ExecuteQuery("create or alter view TotaalAantalMannenPerSemester as select Count(Stamnummer) as 'value' , Geslacht, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END AS semester, YEAR([Module begindatum]) as jaar from tblStudentGegevens where Geslacht = 'M' group  by Geslacht, CASE WHEN MONTH([Module begindatum]) < 7 THEN 1 ELSE 2 END , YEAR([Module begindatum]) HAVING YEAR([Module begindatum]) >= (Year(GETDATE()) - 5)"))
                        {
                            //Mannen
                            AddTableInBookmarkWithoutGroups("select CAST(ROUND(CAST(TotaalAantalMannenPerSemester.value as float) / CAST(TotaalAantalStudentenPerSemester.value as float) * 100,2) AS nvarchar(50)) +'%' as value , TotaalAantalStudentenPerSemester.semester, TotaalAantalStudentenPerSemester.jaar from TotaalAantalStudentenPerSemester join TotaalAantalMannenPerSemester on(TotaalAantalMannenPerSemester.jaar = TotaalAantalStudentenPerSemester.jaar and TotaalAantalMannenPerSemester.semester = TotaalAantalStudentenPerSemester.semester) where TotaalAantalStudentenPerSemester.jaar >= (Year(GETDATE()) - 5) and TotaalAantalMannenPerSemester.semester = 1 ", table, navigator, "VerhoudingManSem1", part);
                            AddTableInBookmarkWithoutGroups("select CAST(ROUND(CAST(TotaalAantalMannenPerSemester.value as float) / CAST(TotaalAantalStudentenPerSemester.value as float) * 100,2) AS nvarchar(50)) +'%' as value , TotaalAantalStudentenPerSemester.semester, TotaalAantalStudentenPerSemester.jaar from TotaalAantalStudentenPerSemester join TotaalAantalMannenPerSemester on(TotaalAantalMannenPerSemester.jaar = TotaalAantalStudentenPerSemester.jaar and TotaalAantalMannenPerSemester.semester = TotaalAantalStudentenPerSemester.semester) where TotaalAantalStudentenPerSemester.jaar >= (Year(GETDATE()) - 5) and TotaalAantalMannenPerSemester.semester = 2 ", table, navigator, "VerhoudingManSem2", part);

                            //Vrouwen
                            AddTableInBookmarkWithoutGroups(" select CAST(ROUND(CAST(TotaalAantalVrouwenPerSemester.value as float) / CAST(TotaalAantalStudentenPerSemester.value as float) * 100,2) AS nvarchar(50)) +'%' as value , TotaalAantalStudentenPerSemester.semester, TotaalAantalStudentenPerSemester.jaar from TotaalAantalStudentenPerSemester join TotaalAantalVrouwenPerSemester on(TotaalAantalVrouwenPerSemester.jaar = TotaalAantalStudentenPerSemester.jaar and TotaalAantalVrouwenPerSemester.semester = TotaalAantalStudentenPerSemester.semester) where TotaalAantalStudentenPerSemester.jaar >= (Year(GETDATE()) - 5) and TotaalAantalVrouwenPerSemester.semester = 1", table, navigator, "VerhoudingVrouwSem1", part);
                            AddTableInBookmarkWithoutGroups(" select CAST(ROUND(CAST(TotaalAantalVrouwenPerSemester.value as float) / CAST(TotaalAantalStudentenPerSemester.value as float) * 100,2) AS nvarchar(50)) +'%' as value , TotaalAantalStudentenPerSemester.semester, TotaalAantalStudentenPerSemester.jaar from TotaalAantalStudentenPerSemester join TotaalAantalVrouwenPerSemester on(TotaalAantalVrouwenPerSemester.jaar = TotaalAantalStudentenPerSemester.jaar and TotaalAantalVrouwenPerSemester.semester = TotaalAantalStudentenPerSemester.semester) where TotaalAantalStudentenPerSemester.jaar >= (Year(GETDATE()) - 5) and TotaalAantalVrouwenPerSemester.semester = 2", table, navigator, "VerhoudingVrouwSem2", part);

                        }
                    }
                }


               
                testdoc.SaveToFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\output.docx", FileFormat.Docx2013);
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\output.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een onverwachte fout opgetreden bij het exporten van de data: " + ex, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void AddTableInBookmark(string sql, Table table, BookmarksNavigator navigator, string bookmark, TextBodyPart txtbodyPart)
        {       
            //Voer sql query uit en steek deze in datatable
            DataTable result = ConnectDatabase.getTable(sql);

            //Vul de table met data van de datatable
            List<string> years = new List<string>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (!years.Contains(result.Rows[i]["jaar"].ToString()))
                {
                    years.Add(result.Rows[i]["jaar"].ToString());
                }
            }
            years.Sort();

            List<string> groups = new List<string>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (!groups.Contains(result.Rows[i][0].ToString()))
                {
                    groups.Add(result.Rows[i][0].ToString());
                }
            }
            //groups.Sort(); sorteer modules
            table.ResetCells(groups.Count+1, years.Count + 1);
            table.Rows[0].Cells[0].AddParagraph().AppendText(bookmark);
            for (int i = 0; i < years.Count; i++)
            {
                table.Rows[0].Cells[i +1].AddParagraph().AppendText(years[i]);
            }
            for (int i = 1; i <= groups.Count; i++)
            {
                table.Rows[i].Cells[0].AddParagraph().AppendText(groups[i-1]);
            
            }

            for (int i = 1; i <= groups.Count; i++)
            {
                List<TableInput> tableInputByGroup = GetTableInputsByGroup(result, groups[i - 1], years);
                for (int j = 1; j <= tableInputByGroup.Count; j++)
                {
                    table.Rows[i].Cells[j].AddParagraph().AppendText(tableInputByGroup[j-1].Value.ToString());
                }
            }
            table.AutoFit(AutoFitBehaviorType.AutoFitToContents);

            navigator.MoveToBookmark(bookmark);

            txtbodyPart.BodyItems.Add(table);
            navigator.ReplaceBookmarkContent(txtbodyPart);

        }
      
        public static void AddTableInBookmarkWithoutGroups(string sql, Table table, BookmarksNavigator navigator, string bookmark, TextBodyPart txtbodyPart)
        {
            //Voer sql query uit en steek deze in datatable
            DataTable result = ConnectDatabase.getTable(sql);

            //Vul de table met data van de datatable
            List<string> years = new List<string>();
            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (!years.Contains(result.Rows[i]["jaar"].ToString()))
                {
                    years.Add(result.Rows[i]["jaar"].ToString());
                }
            }
            years.Sort();

            
          
            table.ResetCells(2, years.Count + 1);
            table.Rows[0].Cells[0].AddParagraph().AppendText(bookmark);
            for (int i = 0; i < years.Count; i++)
            {
                table.Rows[0].Cells[i + 1].AddParagraph().AppendText(years[i]);
            }
           
                 for (int i = 1; i <= years.Count; i++)
                {
                    table.Rows[1].Cells[i].AddParagraph().AppendText(result.Rows[i-1]["value"].ToString());
                }
            
            table.AutoFit(AutoFitBehaviorType.AutoFitToContents);

            navigator.MoveToBookmark(bookmark);

            txtbodyPart.BodyItems.Add(table);
            navigator.ReplaceBookmarkContent(txtbodyPart);

        }
        public static List<TableInput> GetTableInputsByGroup(DataTable datatable, string group, List<string> years)
        {
            int yearCheck = 0;
            List<TableInput> result = new List<TableInput>();

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                var temp = datatable.Rows[i][0].ToString();
                var temp2 = datatable.Rows[i]["jaar"].ToString();
                if (datatable.Rows[i][0].ToString() == group)
                {

                    if (datatable.Rows[i]["jaar"].ToString() == years[yearCheck])
                    {

                        result.Add(new TableInput()
                        {
                            Group = group,
                            Year = Convert.ToInt32(datatable.Rows[i]["jaar"].ToString()),
                            Value = Convert.ToInt32(datatable.Rows[i]["value"].ToString())
                        });
                        yearCheck++;
                    }
                    else
                    {
                        result.Add(new TableInput()
                        {
                            Group = group,
                            Year = Convert.ToInt32(years[yearCheck]),
                            Value = 0
                        });
                        i--;
                        yearCheck++;
                    }
                }
            }
            if (result.Count < years.Count)
            {
                for (int i = result.Count; i < years.Count; i++)
                {
                    result.Add(new TableInput()
                    {
                        Group = group,
                        Year = Convert.ToInt32(years[i]),
                        Value = 0
                    });
                }
               
            }


            return result;
        }

        
    }
}
