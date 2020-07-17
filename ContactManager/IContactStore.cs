using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public interface IContactStore
    {
        Contact Add(Contact contact);
        bool Remove(Contact contact, out Contact removed);

        IEnumerable<Contact> Contacts { get; }
    }
}
