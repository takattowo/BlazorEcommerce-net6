namespace BlazorEcommerce.Client.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		public List<Category> Categories { get; set; } = new List<Category>();
		private readonly HttpClient _http;

		public CategoryService(HttpClient http)
		{
			_http = http;
		}

		public async Task GetCategories()
		{
			var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
			if(result != null && result.Data != null)
				Categories = result.Data;
		}

		public Task<ServiceResponse<Category>> GetCategory(int id)
		{
			throw new NotImplementedException();
		}
	}
}
