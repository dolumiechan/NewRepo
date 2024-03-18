public class Product
{
    public string NameProdurt { get; }
    public double Price { get; }
    public CategoryProduct Category { get; }

    public Product(string name_product, double price, CategoryProduct Category)
    {
        NameProdurt = name_product;
        Price = price;
        this.Category = Category;
    }
}
