using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerDesignerGUI
{
    class Primer
    {
        //Konstruktor
        public Primer(string Primersequenz, float Primerconcentration, float Saltconcentration)
        {
            this.sequence = Primersequenz.ToUpper();
            this.concentration = Primerconcentration;
            this.saltConcentration = Saltconcentration;
            this.sequenzLänge = sequence.Length;
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
                sequenzLänge = sequence.Length;
            }
        }
        private int sequenzLänge;
        public int SequenzLänge
        {
            get
            { return sequenzLänge; }
        }
        private float concentration;
        public float Concentration
        {
            get
            { return concentration; }
        }
        private float saltConcentration;
        public float SaltConcentration
        {
            get
            { return saltConcentration; }
        }
        //Methoden
        public double GetNearestNeighbourTemp()
        {
            int i = 0;
            double Tm = 0;
            double deltaH = 0;
            double deltaS = 0;
            double RlnK = 0;
            string NachbarBasen = null;
            // Calc DeltaH / DeltaS 
            for (i = 0; i < this.Sequence.Length - 1; i++)
            {
                NachbarBasen = this.Sequence.Substring(i, 2);
                if (string.Compare(NachbarBasen, "AA", true) == 0 | string.Compare(NachbarBasen, "TT", true) == 0)
                {
                    deltaH += -8.0;
                    deltaS += -21.9;
                }
                else if (string.Compare(NachbarBasen, "GT", true) == 0 | string.Compare(NachbarBasen, "AC", true) == 0)
                {
                    deltaH += -9.4;
                    deltaS += -25.5;
                }
                else if (string.Compare(NachbarBasen, "CT", true) == 0 | string.Compare(NachbarBasen, "AG", true) == 0)
                {
                    deltaH += -6.6;
                    deltaS += -16.4;
                }
                else if (string.Compare(NachbarBasen, "TG", true) == 0 | string.Compare(NachbarBasen, "CA", true) == 0)
                {
                    deltaH += -8.2;
                    deltaS += -21.0;
                }
                else if (string.Compare(NachbarBasen, "CC", true) == 0 | string.Compare(NachbarBasen, "GG", true) == 0)
                {
                    deltaH += -10.9;
                    deltaS += -28.4;
                }
                else if (string.Compare(NachbarBasen, "TC", true) == 0 | string.Compare(NachbarBasen, "GA", true) == 0)
                {
                    deltaH += -8.8;
                    deltaS += -23.5;
                }
                else if (string.Compare(NachbarBasen, "AT", true) == 0)
                {
                    deltaH += -5.6;
                    deltaS += -15.2;
                }
                else if (string.Compare(NachbarBasen, "CG", true) == 0)
                {
                    deltaH += -11.8;
                    deltaS += -29.0;
                }
                else if (string.Compare(NachbarBasen, "GC", true) == 0)
                {
                    deltaH += -10.5;
                    deltaS += -26.4;
                }
                else if (string.Compare(NachbarBasen, "TA", true) == 0)
                {
                    deltaH += -6.6;
                    deltaS += -18.4;
                }
            }
            //Tm calc
            RlnK = 1.987 * Math.Log(1 / (Concentration * Math.Pow(10, (-9))));
            RlnK = Math.Round(1000 * RlnK) / 1000;
            deltaH = Math.Abs(deltaH);
            deltaS = Math.Abs(deltaS);
            Tm = 1000D * ((deltaH - 3.4) / (deltaS + RlnK));
            Tm = Tm - 272.9;// to celcius
            Tm = Tm + 7.21D * Math.Log(SaltConcentration / 1000D);// salt adjust
            return Tm;
        }
        public string GetReverseComplement()
        {
            string strBase="";
            string revComp=""; 
             for (int i = 0; i < sequence.Length; i++)
             {
                 strBase = sequence.Substring((sequence.Length - 1 - i), 1);
                switch (strBase)
                {
                    case "A":
                        strBase = "T";
                        break;
                    case "T":
                        strBase = "A";
                        break;
                    case "C":
                        strBase = "G";
                        break;
                    case "G":
                        strBase = "C";
                        break;
                }
                revComp += strBase;    
             }
            return revComp;
        }


        public void CreateSeqFile(string NameOfPrimer, string SaveToPath)
        {
            string path = SaveToPath + @"\"+ NameOfPrimer+ ".txt"; //@"c:\temp\MyTest.txt";
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
            string newPath = SaveToPath + @"\" + NameOfPrimer + ".seq";
            File.Delete(newPath); // Delete the existing file if exists
            File.Move(path, newPath); // Rename the oldFileName into newFileName
        }

    }
}
