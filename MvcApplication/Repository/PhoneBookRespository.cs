using Models;
using Repository;
using MySql.Data.MySqlClient;

namespace Repository


{
    public class PhoneBookRespository : IPhoneBookRepository
    {
        

        public PhoneBookRespository()
        {

        }
        public List<Contact> GetContacts()
        {
            var contacts = new List<Contact>();

            using (var connection = DAppDbContext.GetConnection())
            {
                try
                {
                    connection.Open();

                    var command = new MySqlCommand("SELECT * FROM contacts", connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact contact = new Contact
                            {
                                Id = Guid.Parse(reader["id"].ToString()),
                                Firstname = reader["Firstname"].ToString(),
                                Lastname = reader["Lastname"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            };

                            contacts.Add(contact);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Error fetching contacts", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return contacts;
        }

   public bool SaveContact(List<Contact> model)
        {

            try
            {
                var stringModel = JsonConvert.SerializeObject(model);
                System.IO.File.WriteAllText(filePath, stringModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

    }
}