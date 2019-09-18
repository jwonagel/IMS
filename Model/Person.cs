using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace IMS_Einleitung.Model
{
    internal class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }
        public int Id { get; set; }


        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} {Address.ToString()}";
        }
    }
}
