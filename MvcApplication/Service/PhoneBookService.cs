using Models;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
public class PhoneBookService
    {
        private readonly IPhoneBookRepository _repo;

        //dependency injection
        public PhoneBookService()
        {
             _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public bool DeleteContact(string Id)
        {
            if(Id == null){
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _repo.DeleteContact(id);
        }

        public Contact GetContactById(string id)
        {

            if(Id == null){
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            
            return _repo.GetContactById(id);

            //if (contact == null)
            //{
            //    return null; handel in controller
            //}
            //return contact;
        }

        public List<Contact> GetContacts()
        {
            return _repo.GetContacts();
        }

        public bool SaveContact(Contact model)
        {
            var existingContact = _repo.GetContactByPhoneNumber(model.PhoneNumber);
            if (existingContact != null){
                return false; // message in controller
            }else{
                model.Id = Guid.NewGuid();
                return _repo.SaveContact(model);
            }
            
    }
}
