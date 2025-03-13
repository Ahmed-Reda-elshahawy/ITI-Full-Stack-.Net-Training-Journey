using observer.Models;

namespace observer.Services.Products;

class ProductCreatedEventArgs:EventArgs
{
    public ProductCreatedEventArgs(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}
