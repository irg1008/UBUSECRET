using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;
using Interfaces;
namespace DataAccess
{
    public class DB : IDB
    {
        private SortedList<string, User> tblUsuarios = new SortedList<string, User>();
        public DB()
        {
            // Inicilización de los elementos de la base de datos
            User uAdmin = new User("Administrador", "Administración", "admin@ubu.es", "Proyectos")
            {
                Password = "P@ssword",
                IsAdmin = true
            };
            //tblUsers.Add(uAdmin.Id, uAdmin);
        }

        User IDB.ReadUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public bool InsertUser(User usuario)
        {
            throw new NotImplementedException();
        }

        User IDB.DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        Secret IDB.ReadSecret(Guid id)
        {
            throw new NotImplementedException();
        }

        Secret IDB.DeleteSecret(Guid id)
        {
            throw new NotImplementedException();
        }

        bool IDB.insertSecret(Secret secret)
        {
            throw new NotImplementedException();
        }

        bool IDB.attachSecret(Guid userId, Guid secretId)
        {
            throw new NotImplementedException();
        }

        bool IDB.dettachSecret(Guid userId, Guid secretId)
        {
            throw new NotImplementedException();
        }

        bool IDB.verifyUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        bool IDB.makeUserAdmin(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}