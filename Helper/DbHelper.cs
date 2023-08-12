using System.Data.SqlClient;
using autolog.Data;

namespace autolog.Helper;

public class DbHelper
{
    IConfiguration _configuration;
    public DbHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private static string your_password = "";
    private string connectionString = $"";

    public List<LogType> GetLogTypes()
    {
        List<LogType> logTypes = new List<LogType>();

        string configConnectionString = _configuration.GetConnectionString("azure_conntection");
        if (configConnectionString != null) connectionString = configConnectionString;


        var conn = new SqlConnection(connectionString);
        using (SqlConnection connection = conn)
        {
            connection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM LogType", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string className = reader.GetString(2);

                LogType logType = new LogType
                {
                    Id = id,
                    ClassName = className,
                    Title = title
                };

                logTypes.Add(logType);
            }
            reader.Close();

            return logTypes;
        }
    }

    public void CreateLogManagerClassFromDb()
    {
        ClassGenerator classGenerator = new ClassGenerator();

        foreach (LogType logType in GetLogTypes())
        {
            classGenerator.GenerateClass(logType.ClassName);
        }
    }


}