namespace PowerBoss.Domain.Utilities;

public static class StringUtilities
{
    private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

    public static string Random(int length)
    {
        Random random = new();

        return new string(Enumerable.Repeat(Chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}