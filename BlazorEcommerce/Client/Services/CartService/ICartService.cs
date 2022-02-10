namespace BlazorEcommerce.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem cartItem);
        Task RemoveFromCart (int productId, int productTypeId);
        Task UpdateQty(CartServiceResponse product);
        Task<List<CartItem>> GetAll();
        Task<List<CartServiceResponse>> GetProducts();
    }
}
