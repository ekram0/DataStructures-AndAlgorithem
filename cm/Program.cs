using ContactManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm
{
    class Program
    {
        static void Main()
        {
            ContactStore store = new ContactStore();
            Repl repl = new Repl(Console.In, Console.Out, store);

            repl.Run();
        }
    }
}
