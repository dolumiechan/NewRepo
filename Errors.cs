using System;
public static class Errors
{
    private const string Text = "нельзя воодить пустые символы";
    private const string Empty = "пусто!";
    private const string NotThisRange = "нету такого диапозона!";


    public static void DoubleText()
    {
        Console.WriteLine(Text);
    }
    public static void DoubleEmpty()
    {
        Console.WriteLine(Empty);
    }
    public static void Range()
    {
        Console.WriteLine(NotThisRange);
    }

}
