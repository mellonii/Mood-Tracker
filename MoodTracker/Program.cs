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
        
        var moodRepository = new MoodRepository();
        var moodService = new MoodService(moodRepository);
        
        Console.WriteLine("Mood Tracker");

        while (true)
        {
            var command = Console.ReadLine() + "";
            string[] text;
            switch (command)
            {
                case "1":
                    // добавить запись о настроении
                    text = Console.ReadLine().Split(' ');
                    var mood1 = Models.MoodParser.StringToEnum(text[0]);
                    var comment1 = text[1];
                    var date1 = DateTimeOffset.Parse(text[2]);
                    moodService.AddEntry(mood1, comment1, date1);
                    break;
                case "2": // список всех записей
                    Console.WriteLine(moodService.AllEntries());
                    break;
                case "3":
                    // редактировать запись, - если оставить
                    text = Console.ReadLine().Split(' ');
                    var id3 = Int16.Parse(text[0]);
                    var mood3 = Models.MoodParser.StringToEnum(text[1]);
                    var comment3 = text[2];
                    var date3 = DateTimeOffset.Parse(text[3]);
                    moodService.EditEntry(id3, mood3, comment3, date3);
                    break;
                case "4":
                    // удалить запись
                    var id4 = Int16.Parse(Console.ReadLine());
                    moodService.DeleteEntry(id4);
                    break;
                default:
                    break;
            }
        }
    }
}