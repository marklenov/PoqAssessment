using AutoMapper;
using Microsoft.Extensions.Options;
using PoqAssessment.Clients.Models;
using PoqAssessment.DTOs;
using PoqAssessment.Interfaces;
using PoqAssessment.Options;

namespace PoqAssessment.Services;

public class ProductsService : IProductsService
{
    private readonly IMapper _mapper;
    private readonly HighlightTagsOptions _highlightTagsOptions;
    private readonly MostCommonWordsOptions _mostCommonWordsOptions;
    public ProductsService(IMapper mapper, IOptions<HighlightTagsOptions> options, 
        IOptions<MostCommonWordsOptions> mostCommonWordsOptions)
    {
        _mapper = mapper;
        _highlightTagsOptions = options.Value;
        _mostCommonWordsOptions = mostCommonWordsOptions.Value;
    }

    public ProductsResponseDTO GetProductsFiltered(ProductsRequestDTO requestDTO, ClientProducts products)
    {
        var clientProds = products.Products;

        var filterObjects = new FilterObjectResponseDTO
        {
            CommonWords = GetMostCommonWords(clientProds),
            MinPrice = clientProds.Select(x => x.Price).Min(),
            MaxPrice = clientProds.Select(x => x.Price).Max(),
            Sizes = GetDistinctSizes(clientProds)
        };

        if (requestDTO.MinPrice.HasValue)
        {
            clientProds = clientProds.Where(x => x.Price >= requestDTO.MinPrice);
        }

        if (requestDTO.MaxPrice.HasValue)
        {
            clientProds = clientProds.Where(x => x.Price <= requestDTO.MaxPrice);
        }

        if (!string.IsNullOrWhiteSpace(requestDTO.Size))
        {
            clientProds = clientProds.Where(x => x.Sizes.Contains(requestDTO.Size, StringComparer.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(requestDTO.Highlight))
        {
            var highlightedWords = requestDTO.Highlight.Split(",");

            var changedProds = new List<ProductsDetails>();
            foreach (var prod in clientProds)
            {
                foreach (var word in highlightedWords)
                {
                    prod.Description = prod.Description.Replace(word, 
                        $"{_highlightTagsOptions.OpeningTag}{word}{_highlightTagsOptions.ClosingTag}");
                }
                changedProds.Add(prod);
            }

            clientProds = changedProds;
        }

        var filteredProducts = _mapper.Map<List<ProductsDetailedResponseDTO>>(clientProds);

        return new ProductsResponseDTO()
        {
            Products = filteredProducts,
            FilterObjects = filterObjects
        };
    }

    private IEnumerable<string> GetMostCommonWords(IEnumerable<ProductsDetails> clientProds)
    {
        var allWords = new List<string> { };

        foreach (var product in clientProds)
            allWords.AddRange(product.Description.Split(" "));

        var mostCommonWords = allWords
            .GroupBy(x => x)
            .Select(x => new
            {
                KeyField = x.Key,
                Count = x.Count()
            })
          .OrderByDescending(x => x.Count)
          .Skip(_mostCommonWordsOptions.SkipMostCommonCount)
          .Take(_mostCommonWordsOptions.TakeMostCommonCount)
          .Select(x => x.KeyField.Replace(".", string.Empty));

        return mostCommonWords.Take(10);
    }

    private IEnumerable<string> GetDistinctSizes(IEnumerable<ProductsDetails> clientProds)
    {
        var allSizes = new List<string>{ };

        foreach(var product in clientProds)
            allSizes.AddRange(product.Sizes);

        return allSizes.Distinct();
    }
}
