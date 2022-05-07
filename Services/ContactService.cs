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
    }
}
