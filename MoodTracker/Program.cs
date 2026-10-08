using System.Text;
using MoodTracker.Services;
// using MoodTracker.Exceptions;
using MoodTracker.Repository;

namespace MoodTracker;

class Program
{
    /*
    private static void HandleErrors(Action func)
    {
        try
        {
            func();
        }
        catch (BusinesException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    */
    
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var repository = new MoodRepository();
        var service = new MoodService();
        Console.WriteLine("Mood Tracker");
        
    }
}