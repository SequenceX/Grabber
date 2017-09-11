using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//verweis Microsoft.VisualBasic.dll
using Microsoft.VisualBasic.CompilerServices;


namespace GrabberGui
{
    class Grabber
    {

        //Konstruktor
        public Grabber(string WhatToGrab, string TargetPath)
        {
            OutputPath = TargetPath;
            ToGrab = WhatToGrab;
            GeneType = "";//Genetyp wird über Setter aktualisiert
        }

        //Getter/Setter
        private string geneType;
        public string GeneType
        {
            get
            {
                return geneType;
            }
            private set
            {
                geneType = IdentificateName(toGrab);
            }
        }

        private string outputPath;
        public string OutputPath
        {
            get
            {
                return outputPath;
            }
            private set
            {
                    if (Directory.Exists(value))
                    {
                        outputPath = value;
                    }
                    else
                    {
                        throw new Exception(string.Format("No Valid Output Path"));
                    }
            }
        }

        private string toGrab;
        public string ToGrab
        {
            get
            {
                return toGrab;
            }
            set
            {
                toGrab = value;
                    if (IdentificateName(toGrab) == "No Valid Genname")
                    {
                        throw new Exception(string.Format("No Valid Genname"));
                    }
                    else
                    {
                        GeneType = "";//Genetyp wird über Setter aktualisiert
                    }
            }
        }



        //Methoden
        public void AddPräfix()
        {
            string CopyTo = OutputPath;
            string path = "";
            string geneType = IdentificateName(ToGrab);
            if (geneType == "Standard Gen ohne präfix" || geneType == "Standard Gen old Ecom ohne präfix")
            {   //ergänzt den Präfix je nach dem welche Order gefunden wird, bzw ob Ordner gefunden wird
                path = @"K:\GSM\LaufendeGSYs\";
                string[] dirX = Directory.GetDirectories(path, "a" + ToGrab + "_*");
                if (dirX.Length == 0)
                {
                    dirX = Directory.GetDirectories(path, "k" + ToGrab + "_*");
                    if (dirX.Length == 0)
                    {
                        //Kein Ordner gefunden, Old Gene
                    }
                    else
                    {
                        ToGrab = "k" + ToGrab; //  K Ordner gefunden
                    }
                }
                else
                {
                    ToGrab = "a" + ToGrab;//  A Ordner gefunden
                }
            }
        }

        public void GrabIt()
        {
            string CopyTo = OutputPath;
            string path = "";
            string geneType = IdentificateName(ToGrab);
            if (geneType== "Standard Gen ohne präfix" || geneType == "Standard Gen old Ecom ohne präfix")
            {   //ergänzt den Präfix je nach dem welche Order gefunden wird, bzw ob Ordner gefunden wird
                path = @"K:\GSM\LaufendeGSYs\";
                string[] dirX = Directory.GetDirectories(path, "a" + ToGrab + "_*");
                if (dirX.Length == 0)
                {
                     dirX = Directory.GetDirectories(path, "k" + ToGrab + "_*");
                    if (dirX.Length == 0)
                    {
                        //Kein Ordner gefunden, Old Gene
                    }
                    else
                    {
                        ToGrab = "k" + ToGrab; //  K Ordner gefunden
                    }
                }
                else
                {
                    ToGrab = "a" + ToGrab;//  A Ordner gefunden
                    }    
            }            
            // GrundPfade aussuchen
            if (geneType == "GeneStrand")
            {
                path = @"K:\GSM\LaufendeGeneStrands\";
            }
            else
            {
                path = @"K:\GSM\LaufendeGSYs\";
            }
            string[] dirs = Directory.GetDirectories(path, ToGrab + "_*");
            // exakten Dateipfad ermitteln

                if (dirs.Length == 0)
                {
                    throw new Exception(string.Format("Gene {0} not found", ToGrab));
                }


            dirs = Directory.GetFiles(dirs[0] + @"\", ToGrab + "-*");

                if (dirs.Length == 0)
                {
                    throw new Exception(string.Format("Gene {0} not found.", ToGrab));
                }
            
            // Copy to Target
            File.Copy(dirs[0], CopyTo + @"\" + Path.GetFileName(dirs[0]), true);
        }

        public string IdentificateName(string Name)
        {
            string[,] pattern = new string[9,2];
            pattern[0,0] = "###[A-Z]";    //GeneStrand
            pattern[1, 0] = "[A-Z]##[A-Z]";    //StdGen ohne präfix
            pattern[2, 0] = "[A-Z][A-Z]##[A-Z]";   //StdGen
            pattern[3, 0] = "[A-Z][A-Z]##[A-Z][A-Z]";  //Subfragment
            pattern[4, 0] = "[A-Z][A-Z]##[A-Z][A-Z][A-Z]"; //doppeltes subfragment
            pattern[5, 0] = "[A-Z][A-Z]##";    //StdGen old Ecom ohne präfix
            pattern[6, 0] = "[A-Z][A-Z][A-Z]##";   //StdGen old Ecom
            pattern[7, 0] = "[A-Z][A-Z][A-Z]##[A-Z]";  //Subfragment old Ecom
            pattern[8, 0] = "[A-Z][A-Z][A-Z]##[A-Z][A-Z]"; //doppeltes subfragment old Ecom
            //Präfixlose Teile fehlen da überschneidungen mit anderen Mustern enstehen würden
            pattern[0, 1] = "GeneStrand";
            pattern[1, 1] = "Standard Gen ohne präfix";
            pattern[2, 1] = "Standard Gen";
            pattern[3, 1] = "Subfragment";
            pattern[4, 1] = "doppeltes subfragment";
            pattern[5, 1] = "Standard Gen old Ecom ohne präfix"; 
            pattern[6, 1] = "Standard Gen old Ecom"; 
            pattern[7, 1] = "Subfragment old Ecom";
            pattern[8, 1] = "doppeltes subfragment old Ecom"; 
            for (int i = 0; i <= 8; i++)
            {
                if (LikeOperator.LikeString(Name, pattern[i,0], Microsoft.VisualBasic.CompareMethod.Text))//CompareMethod.Binary für Case sensitive
                {
                    return pattern[i, 1];
                }         
            }
            return "No Valid Genname";
        }

        public void NextName()
        {
            string nameToChange;
            string changedName="";
            if (GeneType== "Standard Gen ohne präfix" || GeneType == "Standard Gen old Ecom ohne präfix"|| GeneType == "GeneStrand")
            {
                nameToChange = ToGrab.ToUpper();
            }
            else
            {
                nameToChange =  ToGrab.Substring(1, ToGrab.Length - 1).ToUpper();// old  ToGrab.Substring(0, 1) + ToGrab.Substring(1, ToGrab.Length - 1).ToUpper()
            }
            string[] strAllChars = new string[nameToChange.Length];
            char[] chrAllChars = new char[nameToChange.Length];
            for (int i = 0; i < nameToChange.Length; i++)
            {
                strAllChars[i] = nameToChange.Substring(nameToChange.Length - 1 - i, 1);
                
            }
            for (int i = 0; i < strAllChars.Length; i++)
            {
                chrAllChars[i] = Convert.ToChar(strAllChars[i]);
            }
            for (int i = 0; i < nameToChange.Length; i++)
            {
                if (chrAllChars[i] == 'Z')
                {
                    chrAllChars[i] = 'A';
                }
                else if (chrAllChars[i] == '9')
                {
                    chrAllChars[i] = '0';
                }
                else
                {
                    chrAllChars[i]++;
                    break;
                }
            }
           for (int i = chrAllChars.Length-1; i >=0 ; i--)
            {
                changedName = changedName + chrAllChars[i];
            }
            ToGrab = changedName;
        }

        public void PreviousName()
        {
            string nameToChange;
            string changedName = "";
            if (GeneType == "Standard Gen ohne präfix" || GeneType == "Standard Gen old Ecom ohne präfix" || GeneType == "GeneStrand")
            {
                nameToChange = ToGrab.ToUpper();
            }
            else
            {
                nameToChange =  ToGrab.Substring(1, ToGrab.Length - 1).ToUpper(); // old  ToGrab.Substring(0, 1) + ToGrab.Substring(1, ToGrab.Length - 1).ToUpper();
            }
            string[] strAllChars = new string[nameToChange.Length];
            char[] chrAllChars = new char[nameToChange.Length];
            for (int i = 0; i < nameToChange.Length; i++)
            {
                strAllChars[i] = nameToChange.Substring(nameToChange.Length - 1 - i, 1);

            }
            for (int i = 0; i < strAllChars.Length; i++)
            {
                chrAllChars[i] = Convert.ToChar(strAllChars[i]);
            }
            for (int i = 0; i < nameToChange.Length; i++)
            {
                if (chrAllChars[i] == 'A')
                {
                    chrAllChars[i] = 'Z';
                }
                else if (chrAllChars[i] == '0')
                {
                    chrAllChars[i] = '9';
                }
                else
                {
                    chrAllChars[i]--;
                    break;
                }
            }
            for (int i = chrAllChars.Length - 1; i >= 0; i--)
            {
                changedName = changedName + chrAllChars[i];
            }
            ToGrab = changedName;
        }










    }
}
