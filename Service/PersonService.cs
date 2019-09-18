using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using IMS_Einleitung.Model;

namespace IMS_Einleitung.Service
{
    class PersonService
    {
            
        public IEnumerable<Person> GetAllPersons()
        {
            var query = @"SELECT P.ID, VORNAME, NACHNAME, a.ID AS ADRESS_ID, STREET, HOUSENUMBER, ZIP, CITY
                      FROM PERSON AS P INNER JOIN ADDRESS AS A ON P.ID_ADDRESS = A.ID";

            using (var connection = ConnectionFactory.GetConnection())
            {
                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                var persons = MapPersonFromReader(reader);
                connection.Close();
                return persons;

            }
        }

        public IEnumerable<Person> GetPersonsByName(string name)
        {
            var query = @"SELECT P.ID, VORNAME, NACHNAME, a.ID AS ADRESS_ID, STREET, HOUSENUMBER, ZIP, CITY
                      FROM PERSON AS P INNER JOIN ADDRESS AS A ON P.ID_ADDRESS = A.ID
                      WHERE NACHNAME = '" + name + "';";

            using (var connection = ConnectionFactory.GetConnection())
            {

                var command = new SqlCommand(query, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                var persons = MapPersonFromReader(reader);
                connection.Close();
                return persons;
            }

        }


        public void AddNewPerson(Person person)
        {
            // Adresse hinzufügen falls vorahden
            var address = person.Address;
            if (address?.Id == 0)
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    var query = @"INSERT INTO ADDRESS (STREET, HOUSENUMBER, ZIP, CITY)
                                    VALUES (@street, @streetNr, @zip, @city);
                                    SELECT SCOPE_IDENTITY();";

                    var insertCommand = new SqlCommand(query, connection);
                    insertCommand.Parameters.AddWithValue("@street", address.Street);
                    insertCommand.Parameters.AddWithValue("@streetNr", address.HouseNumber);
                    insertCommand.Parameters.AddWithValue("@zip", address.Zip);
                    insertCommand.Parameters.AddWithValue("@city", address.City);


                    connection.Open();
                    var id = Convert.ToInt32(insertCommand.ExecuteScalar());
                    address.Id = (int) id;
                    connection.Close();
                }

            }

            using (var connection = ConnectionFactory.GetConnection())
            {
                var query = @"INSERT INTO PERSON(VORNAME, NACHNAME, ID_ADDRESS)
                            VALUES(@firstname, @lastname, @addressId)
                            SELECT SCOPE_IDENTITY();";

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@firstname", person.FirstName);
                command.Parameters.AddWithValue("@lastname", person.LastName);
                command.Parameters.AddWithValue("@addressId", person.Address?.Id);
                connection.Open();
                person.Id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }

        private IEnumerable<Person> MapPersonFromReader(SqlDataReader reader)
        {
            var persons = new List<Person>();
            while (reader.Read())
            {
                var id = (int)reader[0];
                var firstName = (string)reader[1];
                var lastName = (string)reader[2];
                var addressId = (int)reader[3];
                var street = (string)reader[4];
                var houseNr = (string)reader[5];
                var zip = (string)reader[6];
                var city = (string)reader[7];
                var person = new Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Id = id,
                    Address = new Address
                    {
                        Id = addressId,
                        Street = street,
                        HouseNumber = houseNr,
                        Zip = zip,
                        City = city
                    }
                };
                persons.Add(person);
            }
            return persons;
        }
    }
}
