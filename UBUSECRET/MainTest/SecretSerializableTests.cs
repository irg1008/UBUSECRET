using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Main;
using Newtonsoft.Json;

namespace Main.Tests
{
    [TestClass()]
    public class SecretSerializableTests
    {
        readonly User owner = new User("Owner", "owner@user.com", "P@ssword");
        Secret s;

        [TestInitialize()]
        public void Startup()
        {
            s = new Secret("A", "Secret a", owner);
        }

        [TestCleanup]
        public void CleanUp()
        {
            s = null;
        }

        [TestMethod()]
        public void Secret_JSONTest()
        {
            // Conseguimos el JSON
            string sJSON = s.To_JSON();

            // Deserialziamos el JSON recibido.
            Secret desSecret = s.From_JSON(sJSON);

            // Volvemos a conseguir el JSON del deserializado.
            string desJSON = desSecret.To_JSON();

            // El secreto original y el extraido del JSON deben ser iguales.
            Assert.IsTrue(sJSON == desJSON);
        }
    }
}