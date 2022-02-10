namespace BlazorEcommerce.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartServiceResponse>>> GetCart(List<CartItem> cartItems);
    }
}
