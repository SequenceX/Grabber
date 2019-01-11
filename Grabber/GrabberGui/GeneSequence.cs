using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrimerDesignerGUI
{
    class GeneSequence
    {
// Diese Klasse benötigt Klasse Primer zur berechnung der TM.
        //Konstruktor
        public GeneSequence(string DNASequence,string SequenceName)
        {
            this.sequence = DNASequence.ToUpper();
            this.seqName = SequenceName;
            this.länge = sequence.Length;
            if (!this.CheckForDnaChar())
            {
                throw new Exception(string.Format("None DNA Char found in {0}! No Primers created",seqName));
            }          
        }
        //Getter / Setter
        private string sequence;
        public string Sequence
        {
            get
            { return sequence; }
            set
            {
                sequence = value.ToUpper();
                länge = sequence.Length;
                if (!this.CheckForDnaChar())
                {
                    throw new Exception(string.Format("None DNA Char found in {0}! No Primers created", seqName));
                    
                }
            }
        }
        private int länge;
        public int Länge
        {
            get
            { return länge; }
        }
        private string seqName;
        public string SeqName
        {
            get
            { return seqName; }
        }
        //Methoden
        public bool CheckForDnaChar()
        {
            //Return true if onyl DNA Chars are used
            int gcount = sequence.ToUpper().Count(f => f == 'G');
            int ccount = sequence.ToUpper().Count(f => f == 'C');
            int acount = sequence.ToUpper().Count(f => f == 'A');
            int tcount = sequence.ToUpper().Count(f => f == 'T');
            bool check;
            if (gcount + ccount + acount + tcount == länge)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }
        public string[] GetGenestrandSeqPrimer(int distanceBetweenPrimer = 90, int minSeqLänge = 120, int maxSeqLänge = 1000, int searchStartArea = 20)
        {
            string[] strReturn = new string[2] { "", "" };
            double targetTM = 58.00D;
            //Check der Min&Max Länge für Genestrand
            if (länge < minSeqLänge || länge > maxSeqLänge)
            {
                throw new Exception(string.Format("Sequenzlänge für {0} liegt auserhalb des zulässingen Bereichs." + minSeqLänge + " - "+ maxSeqLänge ,seqName));
            }
            if (distanceBetweenPrimer >(minSeqLänge + searchStartArea + 80))
            {
                throw new Exception(string.Format("Der Abstand der Primer für {0} ist zu groß gewählt so das keine Primer mehr ersellt werden können.",seqName));
            }
            if ((länge/2) < ((distanceBetweenPrimer/2) + searchStartArea + 40))
            {
                throw new Exception(string.Format("Der Abstand der Primer oder die Search Area für {0} ist zu groß gewählt so das keine Primer mehr ersellt werden können.", seqName));
            }
            int midOfSeq=Länge/2;
            //Search for F Primer
            string firstSixFBases;
            string fPrimerStartPair;
            int fPrimerStartPos;
            int firstFPrimerstartPos;
            firstFPrimerstartPos = midOfSeq - (distanceBetweenPrimer / 2);
            fPrimerStartPos = firstFPrimerstartPos;
            fPrimerStartPair = sequence.Substring(fPrimerStartPos,2);
            //Loop for search Primer
            int fUnicCounter = 0;
            bool fUnic = false;
            while (fUnic==false)
            {
                //Search for 2x high Temp Start
                bool fHighTempStart = false;
                bool fMidTempStart = false;
                for (int i = 0; i < searchStartArea; i++)
                {
                    if (fPrimerStartPair == "GG" || fPrimerStartPair == "GC" || fPrimerStartPair == "CG" || fPrimerStartPair == "CC")
                    {  
                        //Unic Check
                        firstSixFBases = sequence.Substring(fPrimerStartPos - 4, 2 + 4);
                        var matchesHighF = Regex.Matches(sequence, firstSixFBases);
                        if (matchesHighF.Count == 1)
                        {
                            fHighTempStart = true;
                            fUnic = true;
                            break;
                        }
                        else
                        {
                            fPrimerStartPos -= 1;
                            fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                        }
                    }
                    else
                    {
                        fPrimerStartPos -= 1;
                        fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                    }
                }
                //Search for 1x high Temp Start
                if (fHighTempStart==false)
                {
                    fPrimerStartPos = firstFPrimerstartPos;
                    fPrimerStartPair = sequence.Substring(fPrimerStartPos, 1);
                    for (int i = 0; i < searchStartArea; i++)
                    {
                        if (fPrimerStartPair == "G" || fPrimerStartPair == "C" )
                        {
                            firstSixFBases = sequence.Substring(fPrimerStartPos - 4, 2 + 4);
                            var matchesMidF = Regex.Matches(sequence, firstSixFBases);
                            if (matchesMidF.Count == 1)
                            {
                                fMidTempStart = true;
                                fUnic = true;
                                break;
                            }
                            else
                            {
                                fPrimerStartPos -= 1;
                                fPrimerStartPair = sequence.Substring(fPrimerStartPos, 1);
                            }
                        }
                        else
                        {
                            fPrimerStartPos -= 1;
                            fPrimerStartPair = sequence.Substring(fPrimerStartPos, 1);
                        }
                     }
                 }
                //Add Second Base on MidTemp Start
                if (fMidTempStart == true)
                {
                    fPrimerStartPos -= 1;
                    fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                }
                //Low Temp Star
                if (fHighTempStart == false && fMidTempStart == false)
                {
                    fPrimerStartPos = firstFPrimerstartPos;
                    fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                }
                //Unic Check
                firstSixFBases = sequence.Substring(fPrimerStartPos - 4, 2 + 4);
                var matchesF = Regex.Matches(sequence, firstSixFBases);
                if (matchesF.Count==1)
                {
                    fUnic = true;
                }
                firstFPrimerstartPos--;
                fUnicCounter++;
                if (fUnicCounter==searchStartArea)
                {
                    throw new Exception(string.Format("Für {0} konnte kein Unic F-Primer innerhalb der suchkreterien gefunden werden.",seqName));
                }
            }
            //Create f Primers
            Primer fPrimer = new Primer(sequence.Substring(fPrimerStartPos - 4, 2 + 4), 50, 50);
            Primer nextFPrimer = new Primer(sequence.Substring(fPrimerStartPos - 4-1, 2 + 4+1), 50, 50);
            int nextPrimerCounter = 0;
            //Primer verlägern bis Abweichung zu TM minimal
            while (Math.Abs(targetTM - fPrimer.GetNearestNeighbourTemp()) > Math.Abs(targetTM - nextFPrimer.GetNearestNeighbourTemp()))
            {  
                fPrimer.Sequence = sequence.Substring(fPrimerStartPos - 4 - nextPrimerCounter, 2 + 4 + nextPrimerCounter);
                nextFPrimer.Sequence = sequence.Substring(fPrimerStartPos - 4 - nextPrimerCounter - 1, 2 + 4 + nextPrimerCounter + 1);
                nextPrimerCounter++;
            }
            //Return F Primer
            strReturn[0] = fPrimer.Sequence;
                //REV PRIMER 
                string firstSixRBases;
                string rPrimerStartPair;
                int rPrimerStartPos;
                int firstRPrimerstartPos;
                firstRPrimerstartPos = fPrimerStartPos + distanceBetweenPrimer + 2;
                rPrimerStartPos = firstRPrimerstartPos;
                rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
            //Loop for search Primer
            int rUnicCounter = 0;
            bool rUnic = false;
            while (rUnic == false)
            {
                bool rHighTempStart = false;
                bool rMidTempStart = false;
                //Search for 2x high Temp Start
                for (int i = 0; i < searchStartArea; i++)
                {
                    if (rPrimerStartPair == "GG" || rPrimerStartPair == "GC" || rPrimerStartPair == "CG" || rPrimerStartPair == "CC")
                    {
                        firstSixRBases = sequence.Substring(rPrimerStartPos, 2 + 4);
                        var matchesHighR = Regex.Matches(sequence, firstSixRBases);
                        if (matchesHighR.Count == 1)
                        {
                            rHighTempStart = true;
                            rUnic = true;
                            break;
                        }
                        else
                        {
                            rPrimerStartPos += 1;
                            rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
                        }
                        rHighTempStart = true;
                        break; 
                    }
                    else
                    {
                        rPrimerStartPos += 1;
                        rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);            
                    }
                }
                //Search for 1x high Temp Start
                if (rHighTempStart == false)
                {
                    rPrimerStartPos = firstRPrimerstartPos;
                    rPrimerStartPair = sequence.Substring(rPrimerStartPos, 1);
                    for (int i = 0; i < searchStartArea; i++)
                    {
                        if (rPrimerStartPair == "G" || rPrimerStartPair == "C")
                        {
                            firstSixRBases = sequence.Substring(rPrimerStartPos, 2 + 4);
                            var matchesMidR = Regex.Matches(sequence, firstSixRBases);
                            if (matchesMidR.Count == 1)
                            {
                                rMidTempStart = true;
                                rUnic = true;
                                break;
                            }
                            else
                            {
                                rPrimerStartPos += 1;
                                rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
                            }
                        }
                        else
                        {
                            rPrimerStartPos += 1;
                            rPrimerStartPair = sequence.Substring(rPrimerStartPos, 1);
                        }
                    }
                }
                //Add Second Base on MidTemp Start
                if (rMidTempStart == true)
                {
                    rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
                }
                //Low Temp Star
                if (rHighTempStart == false && rMidTempStart == false)
                {
                    rPrimerStartPos = firstRPrimerstartPos;
                    rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
                }
                //Unic Check
                firstSixRBases = sequence.Substring(rPrimerStartPos, 2 + 4);
                var matchesR = Regex.Matches(sequence, firstSixRBases);
                if (matchesR.Count == 1)
                {
                    rUnic = true;
                }
                firstRPrimerstartPos--;
                rUnicCounter++;
                if (rUnicCounter == searchStartArea)
                {
                    throw new Exception(string.Format("Für {0} konnte kein Unic R-Primer innerhalb der suchkreterien gefunden werden.", seqName));
                }
            }
            //Create r Primers
            Primer rPrimer = new Primer(sequence.Substring(rPrimerStartPos, 2 + 4), 50, 50);
            Primer nextRPrimer = new Primer(sequence.Substring(rPrimerStartPos, 2 + 4 + 1), 50, 50);
            int nextRPrimerCounter = 0;
            //Primer verlägern bis Abweichung zu TM minimal
            while (Math.Abs(targetTM - rPrimer.GetNearestNeighbourTemp()) > Math.Abs(targetTM - nextRPrimer.GetNearestNeighbourTemp()))
            {
                rPrimer.Sequence = sequence.Substring(rPrimerStartPos , 2 + 4 + nextRPrimerCounter);
                nextRPrimer.Sequence = sequence.Substring(rPrimerStartPos , 2 + 4 + nextRPrimerCounter + 1);
                nextRPrimerCounter++;
            }
            //Return R Primer
            rPrimer.Sequence = rPrimer.GetReverseComplement();
            strReturn[1] = rPrimer.Sequence;
            return strReturn;
        }
        public void CreateSeqFile(string NameOfSeq, string SaveToPath)
        {
            string path = SaveToPath + @"\" + NameOfSeq + ".txt"; //@"c:\temp\MyTest.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(sequence);
                    //sw.WriteLine("...");
                    //sw.WriteLine("...");
                }
            }
            string newPath = SaveToPath + @"\" + NameOfSeq + ".seq";
            File.Delete(newPath); // Delete the existing file if exists
            File.Move(path, newPath); // Rename the oldFileName into newFileName
        }
        static public string ReadInSeqFile(string Path)
        {
            string documentsFolderPath;
            documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string checkPath = Path.ToLower();
            if (checkPath.IndexOf(".seq") == -1)
            {
                throw new Exception(string.Format("Die Datei {0} ist keine .SEQ Datei.", checkPath));
            }
            else
            {
                File.Copy(Path, documentsFolderPath + @"\PrimerCreatingReadIn.txt", true);
                //string ReadIN = File.ReadAllText(documentsFolderPath + @"\PrimerCreatingReadIn.txt");
                string ReadIN="";
                using (StreamReader sr = new StreamReader(Path))
                {
                    while (sr.Peek() >= 0)
                    {
                        ReadIN=sr.ReadLine();
                    }
                }
                File.Delete(documentsFolderPath + @"\PrimerCreatingReadIn.txt");
                //if (ReadIN.IndexOf("^^") != -1)//SUCHMUSTER AN ECHTES ANPASSEN  //NOT USED, Only Read in Last Line fixed Problem
                //{
                //    ReadIN = ReadIN.Substring(ReadIN.IndexOf("^^") + 3);//VERSCHIEBUNG AN SUCHMUSTER ANPASSEN
                //}
                return ReadIN;
            }
        }
    }
 }

