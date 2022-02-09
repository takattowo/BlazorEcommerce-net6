namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Product> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task <ServiceResponse<Product>> GetProduct (int id);
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task SearchProducts(string words, int page);
        Task<List<string>> GetProductSearchSuggestions(string words);
    }
}
