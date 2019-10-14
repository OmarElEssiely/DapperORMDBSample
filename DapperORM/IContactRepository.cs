using System;
using System.Collections.Generic;
using System.Text;

namespace DapperORM
{
    public interface IContactRepository
    {
        Contact Find(int id);
        List<Contact> GetAll();
        Contact Add(Contact contact);
        Contact Update(Contact contact);
        void Remove(int id);
        void Save(Contact contact);
    }
}
