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

            //_contacts = MockContacts.GetMockContacts();
            //InitializeDB();

            _contacts = DbService.GetObjectsAsync().Result.ToList();
        }
        //Bruges til at initialisere database med contact objekter
        public async Task InitializeDB()
        {
            foreach (var contact in _contacts)
            {
                await DbService.AddObjectAsync(contact);
            }
        }
        //Tilføj kontaktobjekt til liste.
        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }
        //Tilføj Contact-objekt til liste og til database.
        public async Task AddContactAsync(Contact contact)
        {
            _contacts.Add(contact);
            await DbService.AddObjectAsync(contact);
        }
        //Opdatér Contact-objekt i database.
        public async Task UpdateContactAsync(Contact contact)
        {
            await DbService.UpdateObjectAsync(contact);
        }
        //Returnerer en liste af Contact-objekter, som er instanseret i kontruktøren med data fra enter MockData.MockContacts eller fra databasen. Bruges i alle razorpages, der skal bruge en liste af Contact-objekter.
        public List<Contact> GetContacts()
        {
           return _contacts;
        }
        //Bruges til at returnere et Contact med et bestemt Id.  
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
        //Finder det Contact-objekt med en givent e-mail adresse
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
        //Bruges til pagination i GetContacts-razorpage. Metoden tager en liste af contact-objekter og imod både en int, der repræsenterer den nuværende side af pagination og hvor mange objekter hver side skal bestå af som argumenter
        public List<Contact> GetPaginatedResultList(IEnumerable<Contact> contactList, int currentPage, int pageSize)
        {
            return contactList.OrderBy(contact => contact.ContactId).Skip((currentPage-1) * pageSize).Take(pageSize)
                .ToList();
        }
    }
}
