using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//verweis Microsoft.VisualBasic.dll
using Microsoft.VisualBasic.CompilerServices;


namespace GrabberConsole
{
    class Grabber
    {

        //Konstruktor
        public Grabber(string WhatToGrab)
        {
            
            
           
            ToGrab = WhatToGrab;
         
        }

        //Getter/Setter
        
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
                try
                {
                    if (IdentificateName(toGrab) == "No Valid Genname")
                    {
                        throw new Exception(string.Format("No Valid Genname"));
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }
        }



        //Methoden

        //todo grab funktion für Gene
        //todo grab funktion für Strands

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




    }
}
