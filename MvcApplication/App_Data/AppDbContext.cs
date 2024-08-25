using System.Data;
using MySql.Data.MySqlClient;

public class DAppDbContext
{
    private static readonly string connectionString = "Server=localhost;restdb;Pwd=123456;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}