using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using IMS_Einleitung.Model;
using IMS_Einleitung.Service;

namespace IMS_Einleitung
{
    class Program
    {
        static void Main(string[] args)
        {
            ListAllPersons();

            CreatePerson();

            ListAllPersons();

        }

        private static void ListAllPersons()
        {
            var service = new PersonService();
            var persons = service.GetAllPersons();

            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }

        private static void CreatePerson()
        {
            Console.WriteLine("Name");
            var name = Console.ReadLine();
            Console.WriteLine("Vorname:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Strasse:");
            var street = Console.ReadLine();
            Console.WriteLine("Hausnummer:");
            var houseNumber = Console.ReadLine();
            Console.WriteLine("Zip:");
            var zip = Console.ReadLine();
            Console.WriteLine("Ort:");
            var city = Console.ReadLine();

            var person = new Person()
            {
                FirstName = firstName,
                LastName = name,
                Address = new Address()
                {
                    Street = street,
                    HouseNumber = houseNumber,
                    Zip = zip,
                    City = city
                }
            };

            var service = new PersonService();
            service.AddNewPerson(person);
        }
    }
}
