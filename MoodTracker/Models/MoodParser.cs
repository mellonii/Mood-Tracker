namespace MoodTracker.Models;

internal static class MoodParser
{
    public static Mood StringToEnum(string text)
    {
        return Enum.TryParse<Mood>(text, ignoreCase: true, out var mood) ? mood : Mood.Neutral;
    }
}