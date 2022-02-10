using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }


        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null) 
                cart = new List<CartItem>();

            var sameItem = cart.Find(x => x.ProductTypeId == cartItem.ProductTypeId && x.ProductId == cartItem.ProductId);
            if (sameItem == null)
                cart.Add(cartItem);
            else
                sameItem.Quantity += 1;

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetAll()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null) cart = new List<CartItem>();

            return cart;
        }

        public async Task<List<CartServiceResponse>> GetProducts()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var result = await _http.PostAsJsonAsync("api/cart/products", cart);
            var cartProducts = await result.Content.ReadFromJsonAsync<ServiceResponse<List<CartServiceResponse>>>();
            return cartProducts.Data;
        }

        public async Task RemoveFromCart(int productId, int productTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
                return;

            var cartItem = cart.Find(x => x.ProductId == productId && x.ProductTypeId == productTypeId);

            if(cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateQty(CartServiceResponse product)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
                return;

            var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);

            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }
    }
}
