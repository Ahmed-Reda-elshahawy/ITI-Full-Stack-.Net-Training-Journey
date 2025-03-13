using observer.Models;

namespace observer.Services.Products;

class ProductsManagementService
{
    private readonly List<Product> _products;

    public event EventHandler<ProductCreatedEventArgs> ProductCreated;

    public ProductsManagementService()
    {
        _products = new List<Product>
        {
            new Product { Name = "IPhone 16 Pro Max", Price = 1500 },
            new Product { Name = "Macbook Pro", Price = 1800 }
        };
    }

    protected virtual void OnProductCreated(ProductCreatedEventArgs e)
    {
        ProductCreated?.Invoke(this, e);
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public Product? GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void CreateProduct(Product product)
    {
        _products.Add(product);
        OnProductCreated(new ProductCreatedEventArgs(product));
    }
}