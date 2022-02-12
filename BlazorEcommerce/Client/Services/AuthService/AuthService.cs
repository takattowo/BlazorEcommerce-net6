namespace BlazorEcommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<int>> Register(UserRegister userRegister)
        {
            var aa = await _http.PostAsJsonAsync("api/auth/register", userRegister);

            return await aa.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
