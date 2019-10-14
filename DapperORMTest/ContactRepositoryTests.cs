using System;
using DapperORM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
namespace DapperORMTest
{
    [TestClass]
    public class ContactRepositoryTests
    {
        [TestMethod]
        public void Get_all_should_return_6_results()
        {
            //arrange
            IContactRepository repository = CreateRepository();
            //act
            var contacts = repository.GetAll();
            //assert
            contacts.Should().NotBeNull();
            contacts.Count.Should().Be(6);

        }
        static int id; 
        [TestMethod]
        public void Insert_should_assign_identity_to_new_entity()
        {
            //arrange
            IContactRepository repository = CreateRepository();
            var contact = new Contact
            {
                Company = "Integrant",
                Email = "OmarElEssiely@outlook.com",
                FirstName = "Omar",
                LastName = "ElEssiely",
                Title = "Alexandria"
            };

            //act
            repository.Add(contact);

            //assert 
            contact.ContactId.Should().NotBe(0, "because Identity should have been assigned by database.");
            Console.WriteLine("New ID : " + contact.ContactId);
            id = contact.ContactId;
        }
        [TestMethod]
        public void Find_should_retrieve_existing_entity()
        {
            // arrange
            IContactRepository repository = CreateRepository();

            // act
            //var contact = repository.Find(id);
            var contact = repository.Find(id);

            // assert
            contact.Should().NotBeNull();
            contact.ContactId.Should().Be(id);
            contact.FirstName.Should().Be("Joe");
            contact.LastName.Should().Be("Blow");
            contact.Email.Should().Be("joe.blow@gmail.com");
            contact.Company.Should().Be("Microsoft");
            contact.Title.Should().Be("Developer");
        }

        [TestMethod]
        public void Modify_should_update_existing_entity()
        {
            // arrange
            IContactRepository repository = new ContactRepository(); //CreateRepository();

            // act
            //var contact = repository.Find(id);
            var contact = repository.Find(id);
            contact.FirstName = "Bob";
            //repository.Update(contact);
            repository.Save(contact);

            // create a new repository for verification purposes
            IContactRepository repository2 = CreateRepository();
            //var modifiedContact = repository2.Find(id);
            var modifiedContact = repository2.Find(id);

            // assert
            modifiedContact.FirstName.Should().Be("Bob");
        }
        [TestMethod]
        public void Delete_should_remove_entity()
        {
            // arrange
            IContactRepository repository = CreateRepository();

            // act
            repository.Remove(id);

            // create a new repository for verification purposes
            IContactRepository repository2 = CreateRepository();
            var deletedEntity = repository2.Find(id);

            // assert
            deletedEntity.Should().BeNull();
        }

        private IContactRepository CreateRepository()
        {
            return new ContactRepository();
        }
    }
}
