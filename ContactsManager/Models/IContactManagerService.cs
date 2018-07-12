using ContactsManager.Models;
using System.Collections.Generic;

namespace ContactManager.Models
{
    public interface IContactManagerService
    {
        bool CreateContact(Contact contactToCreate);
        bool DeleteContact(Contact contactToDelete);
        bool EditContact(Contact contactToEdit);
        Contact GetContact(int id);
        System.Collections.IEnumerable ListContacts();
    }
}