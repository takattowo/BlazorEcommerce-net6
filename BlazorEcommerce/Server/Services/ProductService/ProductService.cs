namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                .Include(o => o.Variants)
                .ThenInclude(v => v.ProductType)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                response.Success = false;
                response.Message = "Product does not exist!";
            }
            else
            {
                response.Data = product;
            }
 

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.Include(o => o.Variants).ToListAsync()
            };

            return response;
        }

		public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
		{
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).Include(o => o.Variants).ToListAsync()
            };

            return response;
        }

		public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string worlds, int page)
		{
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductsByText(worlds)).Count / pageResults);
            var products = await _context.Products
                            .Where(p => p.Title.ToLower().Contains(worlds.ToLower())
                            ||
                            p.Description.ToLower().Contains(worlds.ToLower()))
                            .Include(o => o.Variants)
                            .Skip((page - 1) * (int)pageResults) 
                            .Take((int)pageResults)
                            .ToListAsync();


            var response = new ServiceResponse<ProductSearchResult>()
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    Pages = (int)pageCount,
                    CurrentPage = page

                }
            };

            return response;
        }

		public async Task<ServiceResponse<List<string>>> SearchProductSearchSuggestions(string worlds)
		{
            var product = await FindProductsByText(worlds);
			var st = new List<string>();

            foreach (var item in product) 
            {
                if(item.Title.Contains(worlds, StringComparison.OrdinalIgnoreCase))
                    st.Add(item.Title);

                if(item.Description != null)
				{
                    var punchtuation = item.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = item.Description.Split().Select(s => s.Trim(punchtuation));

                    foreach (var word in words)
					{
                        st.Add(word);
					}
				}
			}

			return new ServiceResponse<List<string>>{ Data = st.Distinct().ToList() };
		}

		private async Task<List<Product>> FindProductsByText(string worlds)
		{
			return await _context.Products
							.Where(p => p.Title.ToLower().Contains(worlds.ToLower())
							||
							p.Description.ToLower().Contains(worlds.ToLower())).Include(o => o.Variants).ToListAsync();
		}
	}
}