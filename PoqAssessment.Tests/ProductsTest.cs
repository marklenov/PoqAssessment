using AutoMapper;
using Microsoft.Extensions.Options;
using PoqAssessment.Options;
using PoqAssessment.Services;
using PoqAssessment.Tests.Mocks;

namespace PoqAssessment.Tests;

public class ProductsTest
{
    private readonly ProductClientMock _clientMock;
    private readonly IMapper _mapperMock;
    private readonly IOptions<MostCommonWordsOptions> _wordsOptions;
    private readonly IOptions<HighlightTagsOptions> _highlightOptions;

    public ProductsTest()
    {
        _clientMock = new ProductClientMock();
        _mapperMock = MapperMock.MockMapper();

        var wordOpts = new MostCommonWordsOptions() { SkipMostCommonCount = 5, TakeMostCommonCount = 10 };
        _wordsOptions = Microsoft.Extensions.Options.Options.Create(wordOpts);

        var highlightOpts = new HighlightTagsOptions() { OpeningTag = "<em>", ClosingTag = "</em>" };
        _highlightOptions = Microsoft.Extensions.Options.Options.Create(highlightOpts);
    }

    [Fact]
    public void OnRequestProductWithoutParametersShouldReturnAllFiveProducts()
    {
        //arrange
        var expected = 5;
        var products = _clientMock.GetAllProducts();

        //act 
        var productServiceResponse = new ProductsService(_mapperMock, _highlightOptions, _wordsOptions).GetProductsFiltered(new DTOs.ProductsRequestDTO(), products);

        //assert
        Assert.Equal(expected, productServiceResponse.Products.Count);
    }

    [Fact]
    public void OnRequestProductWithMinSizeTenShouldReturnMinSizeTen()
    {
        //arrange
        var expected = true;
        var products = _clientMock.GetAllProducts();

        //act 
        var productServiceResponse = new ProductsService(_mapperMock, _highlightOptions, _wordsOptions)
            .GetProductsFiltered(new DTOs.ProductsRequestDTO() { MinPrice = 10 }, products);

        //assert
        Assert.Equal(expected, !productServiceResponse.Products.Any(x => x.Price < 10));
    }

    [Fact]
    public void OnRequestHighlightRedShouldReturnDescriptionWithHtmlTag()
    {
        //arrange
        var expected = true;
        var products = _clientMock.GetAllProducts();

        //act 
        var productServiceResponse = new ProductsService(_mapperMock, _highlightOptions, _wordsOptions)
            .GetProductsFiltered(new DTOs.ProductsRequestDTO() { Highlight = "red", MaxPrice = 30 }, products);

        var isLessThanThirty = !productServiceResponse.Products.Any(x => x.Price > 30);
        var isRedTagged = productServiceResponse.Products.Where(x => x.Description.Contains("red"))
            .All(x => x.Description.Contains("<em>red</em>"));

        //assert
        Assert.Equal(expected, isLessThanThirty && isRedTagged);
    }
}
