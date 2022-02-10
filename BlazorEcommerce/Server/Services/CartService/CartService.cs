namespace BlazorEcommerce.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<CartServiceResponse>>> GetCart(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartServiceResponse>>
            {
                Data = new List<CartServiceResponse>()
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products.Where(p=>p.Id == item.ProductId).FirstOrDefaultAsync();

                if (product == null)
                    continue;

                var pvaoramt = await _context.ProductVariants
                    .Where(v=>v.ProductId == item.ProductId && v.ProductTypeId == item.ProductTypeId)
                    .Include(v=>v.ProductType)
                    .FirstOrDefaultAsync();

                if (pvaoramt == null)
                    continue;

                var cartProduct = new CartServiceResponse
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    ProductTypeId = pvaoramt.ProductTypeId,
                    ProductType = pvaoramt.ProductType.Name,
                    Price = pvaoramt.Price,
                    Quantity = item.Quantity,
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }
    }
}
