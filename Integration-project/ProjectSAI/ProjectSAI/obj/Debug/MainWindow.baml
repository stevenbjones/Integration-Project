   M S B A M L   `   `   `  ����E  AProjectSAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectSAI
ProjectSAI  5   	       ProjectSAI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ProjectSAI     5   	     � Title$(  "Rappport Generatie SAI en Partners��5      $��907.5��66   $��1210.5��6E    � 
ResizeMode$ CanMinimize=�6T    � WindowStartupLocation$ CenterScreen=�6m   .��5	      � $��	0,0,4,-19q�6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��
dtgStudent��5   A      
IsReadOnly .    AutoGenerateColumns .  $
��Left=�6S   $	��788��6n   $��	10,10,0,0q�6{   $	��Top=�6�   $
��1174��6�   $   HorizontalScrollBarVisibility$ Visible=�6�   "   VerticalScrollBarVisibility$ Visible=�6�   ��.     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Stamnummer��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 69   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6E   �� *
Stamnummer+ �� NotifyOnTargetUpdated .  �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  5      $ Geslacht��6%   $		 100 67   
 6C   �� *
Geslacht+ . $ TwoWay=�$ PropertyChanged=�  5      $ Geboortedatum��6%   $		 100 6=   
 6I   �� *Geboortedatum+ . $ TwoWay=� �� StringFormat$ {0:dd/MM/yyyy}��5        5      $ Nationaliteit��6%   $		 100 6<   
 6H   �� *Nationaliteit+ . $ TwoWay=�$ PropertyChanged=�  5      $ 	Thuistaal��6%   $		 100 68   
 6D   �� *	Thuistaal+ . $ TwoWay=�$ PropertyChanged=�  5      $ Proeven verpleegkunde��6%   $		 100 6D   
 6P   �� *ProevenVerpleegkunde+ . $ TwoWay=�$ PropertyChanged=�  5      $ Hoogst behaald diploma��6%   $		 100 6E   
 6Q   �� *HoogstBehaaldDiploma+ . $ TwoWay=�$ PropertyChanged=�  5      $ Herkomst studenten��6%   $		 100 6A   
 6M   �� *HerkomstStudent+ . $ TwoWay=�$ PropertyChanged=�  5!      $ Project SO-CVO��6%   $		 100 6=   
 6I   �� *ProjectSO_CVO+ . $ TwoWay=�$ PropertyChanged=�  5#      $2 ,Faciliteiten leermoeilijkheden/anderstaligen��6%   $		 100 6[   
 6g   �� *-+FaciliteitenLeermoeilijkheden_Anderstaligen+ . $ TwoWay=�$ PropertyChanged=�  5%      $ Diploma SO na CVO��6%   $		 100 6@   
 6L   �� *DiplomaSOnaCVO+ . $ TwoWay=�$ PropertyChanged=�  5'      $ Reden stoppen��6%   $		 100 6<   
 6H   �� *RedenStoppen+ . $ TwoWay=�$ PropertyChanged=�  5)      $ Diploma SO na HBO��6%   $		 100 6@   
 6L   �� *DiplomaSOnaHBO+ . $ TwoWay=�$ PropertyChanged=�  5+      $
 VDAB��6%   $		 100 63   
 6?   �� *VDAB+ . $ TwoWay=�$ PropertyChanged=�  5-      $ School leren kennen��6%   $		 100 6B   
 6N   �� *SchoolLerenKennen+ . $ TwoWay=�$ PropertyChanged=�  5/      $ Module��6%   $		 100 65   
 6A   �� *Module+ . $ TwoWay=�$ PropertyChanged=�  51      $ Module attest��6%   $		 100 6<   
 6H   �� *ModuleAttest+ . $ TwoWay=�$ PropertyChanged=�  53      $ Module begindatum��6%   $		 100 6@   
 6L   �� *ModuleBegindatum+ . $ TwoWay=�$ {0:dd/MM/yyyy}��  55      $ Module einddatum��6%   $		 100 6?   
 6K   �� *ModuleEinddatum+ . $ TwoWay=�$ {0:dd/MM/yyyy}��  57      $ Einddatum inschrijving��6%   $		 100 6E   
 6Q   �� *EinddatumInschrijving+ . $ TwoWay=�$ {0:dd/MM/yyyy}��  59      $ Afdelingscode��6%   $		 100 6<   
 6H   �� *Afdelingscode+ . $ TwoWay=�$ PropertyChanged=�  5;      $
 Klas��6%   $		 100 63   
 6?   �� *Klas+ . $ TwoWay=�$ PropertyChanged=�  5=      $' !Instellingnummer vorig schooljaar��6%   $		 100 6P   
 6\   �� *InstellingnummerVorigJaar+ . $ TwoWay=�$ PropertyChanged=�  5?      $ Attest vorig schooljaar��6%   $		 100 6F   
 6R   �� *AttestVorigSchooljaar+ . $ TwoWay=�$ PropertyChanged=�  5A      $' !Verleende studiebewijzen 1ste zit��6%   $		 100 6P   
 6\   �� * VerleendeStudiebewijzen1steZit+ . $ TwoWay=�$ PropertyChanged=�  5C      $8 2Verleende studiebewijzen 1ste zit vorig schooljaar��6%   $		 100 6a   
 6m   �� */-VerleendeStudiebewijzen1steZitVorigSchooljaar+ . $ TwoWay=�$ PropertyChanged=�  5E      $ Klas vorig schooljaar��6%   $		 100 6D   
 6P   �� *KlasVorigSchooljaar+ . $ TwoWay=�$ PropertyChanged=�  5G      $* $Instellingnummer vorige inschrijving��6%   $		 100 6S   
 6_   �� *$"InstellingnummerVorigeInschrijving+ . $ TwoWay=�$ PropertyChanged=�  5I      $  Attest vorige inschrijving��6%   $		 100 6I   
 6U   �� *AttestVorigeInschrijving+ . $ TwoWay=�$ PropertyChanged=�5N      �� 5P   
   -   5        $��chkEditCells��5P      $��Cells bewerken��6'   $
��Left=�6@   $��1053,806,0,0q�6[   $	��Top=�6q   6�   �� 5Q   
   -   5        $��	btnSubmit��5Q      {�.  $��Submit veranderingen��64   $
��Left=�6S   $��1053,826,0,0q�6n   $	��Top=�6�   $	��131��6�   $��33��6�   6�   �� 5S   
   -   5        $��btnUploadData��5S      $��Data Uploaden��6&   $
��Left=�6>   $��
10,803,0,0q�6Y   $	��Top=�6m   $	��167��6�   $��58��6�   $��16(�6�   6�   �� 5T   
   -   5        $��btnGenerateRapport��5T      $��Rapport Genereren��6+   $
��Left=�6G   $��182,806,0,0q�6b   $	��Top=�6w   $	��166��6�   $��58��6�   $��16(�6�   6�   �� 5V      -   5         ��Name$ chkEnableDatagrid��5V      $��
Data tonen��60   $
��Left=�6E   $��601,843,0,0q�6`   $	��Top=�6u   $��18��6�   $��98��6�   �� 5W   
   -   5        $��	lblOutput��5W      $�� ��6!   $
��Left=�6,   $��699,807,0,0q�6G   $	��Top=�6\   $��52��6t   $	��338��6�   5X      5Y      