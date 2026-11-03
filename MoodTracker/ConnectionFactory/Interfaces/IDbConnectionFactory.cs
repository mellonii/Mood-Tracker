using System.Data;

namespace MoodTracker.ConnectionFactory.Interfaces;

public interface IDbConnectionFactory 
{
    IDbConnection Create();
}