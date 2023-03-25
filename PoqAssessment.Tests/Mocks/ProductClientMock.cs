using PoqAssessment.Clients.Models;

namespace PoqAssessment.Tests.Mocks;

public class ProductClientMock
{
    private readonly ClientProducts _products;

    public ProductClientMock()
    {
        _products = new ClientProducts();

        InitializeMockRepo();
    }

    private void InitializeMockRepo()
    {
        _products.Products = new List<ProductsDetails>
        {
            new ProductsDetails
            {
                Title = "SomeTitle1",
                Description = "Description red",
                Sizes = new List<string>{ "medium", "large"},
                Price = 12
            },
            new ProductsDetails
            {
                Title = "Description blue",
                Description = "Description",
                Sizes = new List<string>{ "medium", "small"},
                Price = 16
            },
            new ProductsDetails
            {
                Title = "Description red, green",
                Description = "Description",
                Sizes = new List<string>{ "large"},
                Price = 5
            },
            new ProductsDetails
            {
                Title = "trousers blue",
                Description = "Description green",
                Sizes = new List<string>{ "large"},
                Price = 7
            },
            new ProductsDetails
            {
                Title = "Description red, green",
                Description = "Description",
                Sizes = new List<string>{ "large"},
                Price = 35
            }
        };
    }

    public ClientProducts GetAllProducts()
    {
        return _products;
    }

    public IEnumerable<ProductsDetails> GetProductsWithMinSizeTen()
    {
        return _products.Products.Where(x => x.Price < 10);
    }
}
