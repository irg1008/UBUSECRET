using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Main;
using Newtonsoft.Json;

namespace Main.Tests
{
    [TestClass()]
    public class UserSerializableTests
    {
        User u;

        [TestInitialize()]
        public void Startup()
        {
            u = new User("User", "user@user.com", "Use^P@ssword33");
        }

        [TestCleanup]
        public void CleanUp()
        {
            u = null;
        }

        [TestMethod()]
        public void User_JSONTest()
        {
            // Conseguimos el JSON
            string uJSON = u.To_JSON();

            // Deserialziamos el JSON recibido.
            User desSecret = u.From_JSON(uJSON);

            // Volvemos a conseguir el JSON del deserializado.
            string desJSON = desSecret.To_JSON();

            // El usuario original y el extraido del JSON deben ser iguales.
            Assert.IsTrue(uJSON == desJSON);
        }
    }
}