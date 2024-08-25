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
    public bool SaveContact(Contact contact)
{
    if (contact == null)
    {
        throw new ArgumentNullException(nameof(contact));
    }

    using (var connection = DAppDbContext.GetConnection())
    {
        try
        {
            connection.Open();

            var command = new MySqlCommand(
                @"INSERT INTO contacts (id, Firstname, Lastname, PhoneNumber) 
                  VALUES (@id, @Firstname, @Lastname, @PhoneNumber)
                  ON DUPLICATE KEY UPDATE 
                  Firstname = @Firstname, Lastname = @Lastname, PhoneNumber = @PhoneNumber", 
                connection);

            command.Parameters.AddWithValue("@id", contact.Id == Guid.Empty ? Guid.NewGuid() : contact.Id);
            command.Parameters.AddWithValue("@Firstname", contact.Firstname);
            command.Parameters.AddWithValue("@Lastname", contact.Lastname);
            command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);

            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            throw new Exception("Error saving contact", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    return true;
}    

public bool DeleteContact(string id)
{
    using (var connection = DAppDbContext.GetConnection())
    {
        try
        {
            connection.Open();

            var command = new MySqlCommand("DELETE FROM contacts WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            int result = command.ExecuteNonQuery();

            return result > 0;
        }
        catch (MySqlException ex)
        {
            throw new Exception("Error deleting contact", ex);
        }
        finally
        {
            connection.Close();
        }
    }

public Contact GetContactById(string id)
{
    using (var connection = DAppDbContext.GetConnection())
    {
        try
        {
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM contacts WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Contact
                    {
                        Id = Guid.Parse(reader["id"].ToString()),
                        Firstname = reader["Firstname"].ToString(),
                        Lastname = reader["Lastname"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }
            }
        }
        catch (MySqlException ex)
        {
            throw new Exception("Error find contact by ID", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    return null;
}
}
