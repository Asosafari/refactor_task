using System.Data;
using MySql.Data.MySqlClient;

public class DAppDbContext
{
    private static readonly string connectionString = "Server=localhost;restdb;Pwd=123456;";

    public static MySqlConnection GetConnection()
    {
        try
        {
            return new MySqlConnection(connectionString);
        }
        catch (MySqlException ex)
        {
            throw new Exception("Error connecting to database", ex);
        }
    }
}