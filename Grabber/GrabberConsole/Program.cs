using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace GrabberConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Grabber Grab1 = new Grabber("avj98y");
            Console.WriteLine("GeneName: " + Grab1.ToGrab);
            Console.WriteLine("Genetype: "+Grab1.GeneType);
            Console.WriteLine("--------");


            Grab1.NextName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.NextName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.NextName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.NextName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.NextName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);

            Console.WriteLine("--------");
            Grab1.PreviousName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.PreviousName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.PreviousName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.PreviousName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);
            Grab1.PreviousName();
            Console.WriteLine("nextGeneName: " + Grab1.ToGrab);








            //Grab1.ToGrab = "aa80A";
            //Grab1.NextName();
            //Console.WriteLine(Grab1.GeneType);
            //string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //Grab1.GrabIt(desktopFolderPath);




            Console.ReadKey();
        }
    }
}
