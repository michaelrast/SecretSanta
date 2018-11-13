using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
    class Program
    {
        List<SkoMember> skoMembers;

        static void Main(string[] args)
        {
            ProcessSecretSanta pss = new ProcessSecretSanta();

            Console.WriteLine("Hit p for production or t for test.");

            var processed = false;

            while (!processed)
            {
                var key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'p':
                        pss.Process(true);
                        processed = true;
                        break;
                    case 't':
                        pss.Process(false);
                        processed = true;
                        break;
                    default:
                        Console.WriteLine("Could not recognize key.  Please hit p for production or t for test.");
                        break;
                }
            }
        }
    }
}
