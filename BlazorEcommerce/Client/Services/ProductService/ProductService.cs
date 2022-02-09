namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

		public event Action ProductsChanged;

		public ProductService(HttpClient http)
        {
            _http = http;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; } = "Fetching infomation...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") 
                : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (result != null && result.Data != null)
                Products = result.Data;

            ProductsChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
            return result;
        }

		public async Task SearchProducts(string words, int page)
		{
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{words}/{page}");

            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage=  result.Data.CurrentPage;
                PageCount = result.Data.Pages;
                LastSearchText = words;
            }

            if (Products.Count == 0)
                Message = "No item was found.";

            ProductsChanged.Invoke();
        }

		public async Task<List<string>> GetProductSearchSuggestions(string words)
		{
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsussgesstion/{words}");

            return result.Data;
        }
	}
}
