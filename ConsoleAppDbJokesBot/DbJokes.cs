using Npgsql;

namespace ConsoleAppDbJokesBot;

public class DbJokes
{
    private string connectionString = "Host=194.67.105.79;Username=bestdatabasebotuser;Password=0;Database=bestdatabasebotuserdb";
    private NpgsqlConnection _connection;
    private Random _random; 
    
    public DbJokes()
    {
        _random = new Random();
        
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    private int GetMaxId()
    {
        string sqlRequest = "SELECT max(id) FROM jokes";
        var command = new NpgsqlCommand(sqlRequest, _connection);

        int maxId = int.Parse(command.ExecuteScalar().ToString());

        return maxId;
    }

    public string GetRandomJoke()
    {
        int maxId = GetMaxId();

        Object sqlResponse = null;
        Object sqlResponse2 = null;

        do
        {
            int randomId = _random.Next(1, maxId + 1);
            string sqlRequest = $"SELECT name FROM restaurant_table WHERE id={randomId}";
            NpgsqlCommand command = new NpgsqlCommand(sqlRequest, _connection);
            
            string sqlRequest2 = $"SELECT info FROM restaurant_table WHERE id={randomId}";
            NpgsqlCommand command2 = new NpgsqlCommand(sqlRequest2, _connection);
            
            sqlResponse = command.ExecuteScalar();
            sqlResponse2 = command2.ExecuteScalar();
        } while (sqlResponse == null);

        string jokeText = sqlResponse.ToString();
        jokeText += "  " + sqlResponse2.ToString();

        return jokeText;
    }
}