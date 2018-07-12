using System.Collections.Generic;
using System.Text.RegularExpressions;
using ContactManager.Models.Validation;
using ContactsManager.Models;
using System.Collections;
namespace ContactManager.Models
{
    public class ContactManagerService : IContactManagerService
    {
        private IValidationDictionary _validationDictionary;
        private IContactManagerRepository _repository;
        private IValidationDictionary validationDictionary;
        private EntityContactManagerRepository entityContactManagerRepository;

        public ContactManagerService(IValidationDictionary validationDictionary)
             : this(validationDictionary, new EntityContactManagerRepository())
        { }

        public ContactManagerService(IValidationDictionary validationDictionary, IContactManagerRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }


        public bool ValidateContact(Contact contactToValidate)
        {
            if (contactToValidate.FIRST_NAME.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is required.");
            if (contactToValidate.LAST_NAME.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            if (contactToValidate.PHONE.Length > 0 && !Regex.IsMatch(contactToValidate.PHONE, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                _validationDictionary.AddError("Phone", "Invalid phone number.");
            if (contactToValidate.EMAIL.Length > 0 && !Regex.IsMatch(contactToValidate.EMAIL, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            return _validationDictionary.IsValid;
        }

        #region IContactManagerService Members

        public bool CreateContact(Contact contactToCreate)
        {
            // Validation logic
            if (!ValidateContact(contactToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateContact(contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContact(Contact contactToEdit)
        {
            // Validation logic
            if (!ValidateContact(contactToEdit))
                return false;

            // Database logic
            try
            {
                _repository.EditContact(contactToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteContact(Contact contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Contact GetContact(int id)
        {
            return _repository.GetContact(id);
        }

        public IEnumerable<Contact> ListContacts()
        {
            return _repository.ListContacts();
        }

        IEnumerable IContactManagerService.ListContacts()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

   
}