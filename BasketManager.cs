using System;
using System.Collections.Generic;

class BasketManager
{
    public static readonly BasketManager basketManager;
    private List<Product> products = new List<Product>();
    private BasketManager()
    { }
    static BasketManager()
    {
        basketManager = new BasketManager();
    }
    public void Add(Product product)
    {
        products.Add(product);
    }
    public void Remuve(int index)
    {
        products.RemoveAt(index);
    }
    public void PrintInfo()
    {
        for (int i = 0; i < products.Count; i++)
        {
            Product item = products[i];
            Console.WriteLine($"\n{i + 1}--{item.NameProdurt}");
        }
    }
    public void PrintInfoCategoryProduct()
    {
        for (int i = 0; i < products.Count; i++)
        {
            Product item = products[i];
            if (item.Category != 0)
            {
                Console.WriteLine($"{i + 1}--{item.Category}");
            }
        }
    }
    public int GetCountList()
    {
        return products.Count;
    }
    public void PrintProductInfo(int index)
    {
        var nameproduct = BasketManager.basketManager.products[index - 1].NameProdurt;
        var priceproduct = BasketManager.basketManager.products[index - 1].Price;
        var category = BasketManager.basketManager.products[index - 1].Category;
        Console.WriteLine($"имя продукта: {nameproduct}\nцена продукта: {priceproduct}\nкатегория продукта: {category}");
    }
    public List<Product> GetListProduct()
    {
        return products;
    }
    public void GetProductRangePrice()
    {
        Console.WriteLine("введите минимальный диапозон продукта: \n");
        if (!int.TryParse(Console.ReadLine(), out int min))
        { Errors.Range(); }

        Console.WriteLine("введите максимальный диапозон продукта: \n");
        if (!int.TryParse(Console.ReadLine(), out int max))
        { Errors.Range(); }

        foreach (var item in products)
        {
            if (min <= item.Price && max >= item.Price)
            {
                Console.WriteLine(item.NameProdurt);
            }
        }
    }
}

class AddProduct : ICommandBasket, IInfoEnumCategoryProduct
{
    public void Run()
    {
        Console.WriteLine("\nвведите название продукта: ");
        string nameproduct = Console.ReadLine();
        Console.WriteLine("введите цену продука: ");

        if (!int.TryParse(Console.ReadLine(), out int value))
        {
            Errors.DoubleText();
            return;
        } // проверка на на адекватный ввод
        Console.WriteLine("выберите катергорию продукта: \n");
        ShowEnumCategoryProduct(); //выводит категории продуктов 

        Array colection = Enum.GetValues(typeof(CategoryProduct));
        if (InputHelper.Input("\nкакую котегорию хотите выбрать? ", 1, colection.Length, out int value2))
        {
            CategoryProduct categoryProduct = (CategoryProduct)value2;
            if (!string.IsNullOrEmpty(nameproduct))
            {
                Product Product = new Product(nameproduct, value, categoryProduct);
                BasketManager.basketManager.Add(Product);
                Console.WriteLine("\nпродукт добавлен\n");
            }
            else
            {
                Errors.DoubleText();
            }
        }
    }
    public void ShowEnumCategoryProduct()
    {
        var Colection = Enum.GetValues(typeof(CategoryProduct));
        System.Collections.IList list = Colection;
        for (int i = 0; i < list.Count; i++)
        {
            object item = list[i];
            Console.WriteLine($"{i + 1}--{item}");
        }
    }
}
class RemoveProduct : ICommandBasket
{
    public void Run()
    {
        BasketManager.basketManager.PrintInfo();    //
        if (InputHelper.Input("\nкакой продукт вы хотите удалить: ", 1, BasketManager.basketManager.GetCountList(), out int intvalue))
        {
            BasketManager.basketManager.Remuve(intvalue - 1);
            Console.WriteLine("\nпродукт удален\n");
        }
        else
        {
            Console.WriteLine("\nне удалось удалить продукт\n");
        }
    }
}
class PrintInfo : ICommandBasket
{
    public void Run()
    {
        if (BasketManager.basketManager.GetCountList() != 0)
        {
            BasketManager.basketManager.PrintInfo();
            if (BasketManager.basketManager.GetCountList() > 0)
            {
                if (InputHelper.Input("\nо каком продукте вы хотите узнать информацию:\n ", 1, BasketManager.basketManager.GetCountList(), out int inputvalue))
                {
                    BasketManager.basketManager.PrintProductInfo(inputvalue);
                }
            }
        }
        else
        {
            Errors.DoubleEmpty();
        }
    }
}
class PrintInfoCategoryProduct : ICommandBasket
{
    public void Run()
    {
        Array collection = Enum.GetValues(typeof(CategoryProduct));
        if (BasketManager.basketManager.GetCountList() != 0)
        {
            BasketManager.basketManager.PrintInfoCategoryProduct();
            if (InputHelper.Input("Выберите категорию о которой хотите получить инфу: \n", 1, collection.Length, out int inputvalue))
            {
                List<string> Products = new List<string>();
                foreach (var item in BasketManager.basketManager.GetListProduct())
                {
                    if (item.Category == (CategoryProduct)inputvalue)
                    {
                        Products.Add(item.NameProdurt);
                        Console.WriteLine($"{item.NameProdurt}--{item.Price}");
                    }
                }
            }
        }
        else
        {
            Errors.DoubleEmpty();
        }
    }
}
class GetProductRangePrice : ICommandBasket
{
    public void Run()
    {
        BasketManager.basketManager.GetProductRangePrice();
    }
}
public enum CategoryProduct
{
    Vegetables = 1,
    Bakery,
    Fruits
}
