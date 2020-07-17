using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal interface IContactReader
    {
        IEnumerable<Contact> Read(Stream stream);
    }
}
