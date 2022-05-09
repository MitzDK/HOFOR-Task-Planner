using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOFORTaskPlanner.MockData;
using HOFORTaskPlanner.Models;

namespace HOFORTaskPlanner.Services
{
    public class ContactService
    {
        private readonly List<Contact> _contacts;
        public DbGenericService<Contact> DbService { get; set; }

        public ContactService(DbGenericService<Contact> dbService)
        {
            DbService = dbService;
            _contacts = MockContacts.GetMockContacts();
            //foreach (var contact in _contacts)
            //{
            //    DbService.AddObjectAsync(contact);
            //}
            //_contacts = DbService.GetObjectsAsync().Result.ToList();
        }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }
        public async Task AddContactAsync(Contact contact)
        {
            _contacts.Add(contact);
            await DbService.AddObjectAsync(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await DbService.UpdateObjectAsync(contact);
        }

        public List<Contact> GetContacts()
        {
           return _contacts;
        }

        public Contact GetContactById(int id)
        {
            foreach (var contact in _contacts)
            {
                if (id == contact.ContactId)
                {
                    return contact;
                }
            }

            return null;
        }

        public Contact GetContactByEmail(string email)
        {
            foreach (var contact in _contacts)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (contact.Email.ToLower().Equals(email.ToLower())) return contact;
                }
            }

            return null;
        }
    }
}
