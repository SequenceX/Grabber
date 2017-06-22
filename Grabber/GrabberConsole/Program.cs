using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabberConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Grabber Grab1 = new Grabber("110a");
            Console.WriteLine(Grab1.IdentificateName(Grab1.ToGrab));

            Console.ReadKey();


        }
    }
}
