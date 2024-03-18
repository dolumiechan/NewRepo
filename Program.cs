using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
class Program
{
    static void Main(string[] args)
    {
        ICommandBasket[] CommandBaskets = new ICommandBasket[]
        {
        new AddProduct(),
        new RemoveProduct(),
        new PrintInfo(),
        new PrintInfoCategoryProduct(),
        new GetProductRangePrice()
        };
        while (true)
        {
            ShowMenu(CommandBaskets);
            if (InputHelper.Input("выберите что вы будете делать: ", 1, CommandBaskets.Length, out int inputvalue))
            {
                CommandBaskets[inputvalue - 1].Run();
            }
        }
    }
    static void ShowMenu(ICommandBasket[] CommandBaskets)
    {
        for (int i = 0; i < CommandBaskets.Length; i++)
        {
            ICommandBasket item = CommandBaskets[i];
            Console.WriteLine($"{i + 1}--{item}");
        }
    }
}
public class LocaleManager
{
    public static readonly LocaleManager Instance;
    static LocaleManager()
    {
        Instance = new LocaleManager();
    }
    private LocaleManager() { }
    Dictionary<string, string> currentDictionary = new Dictionary<string, string>();
    string GetValueFromDict(string Key)
    {
        currentDictionary.TryGetValue(Key, out string value);
        return value;
    }
    void SetLocale(ValueOfLocale input)
    {
        if (input == ValueOfLocale.Ru)
        {
            currentDictionary = Locales.russianDictionary;
        }
        else if (input == ValueOfLocale.En)
        {
            currentDictionary = Locales.englishDictionary;
        }
    }
    public class Choice : ICommandBasket
    {
        public void Run()
        {
            InputHelper.Input("Выберите язык: 1 - английский, 2 - русский", 1, 1, out int choice);
            if (choice == 1)
            {
                Instance.SetLocale(ValueOfLocale.En);
            }
            else if (choice == 2)
            {
                Instance.SetLocale(ValueOfLocale.Ru);
            } 
        }
    }
    public static class Locales
    {
        public static Dictionary<string, string> russianDictionary = new Dictionary<string, string>() {
    {"KeyAddProduct", "Добавить продукт"},
        {"KeyRemoveProduct", "Убрать продукт" },
        {"KeyPrintInfo", "Показать информацию"},
        {"KeyPrintInfoCategoryProduct", "Показать информацию по категориям продуктов"},
        {"KeyGetProduct", "Получение продукта"},
        {"KeyGetProductRangePrice","Получение диапазона цен продуктов"}
    };
        public static Dictionary<string, string> englishDictionary = new Dictionary<string, string>() {
        {"KeyAddProduct", "Adding product"},
        {"KeyRemoveProduct", "Removing product"},
        {"KeyPrintInfo", "Print info"},
        {"PrintInfoCategoryProduct", "Print info by category"},
        {"GetProduct", "Get product"},
        {"KeyGetProductRangePrice", "Get range of product's price"}
    };
    }
    public enum ValueOfLocale
    {
        Ru,
        En
    }
}