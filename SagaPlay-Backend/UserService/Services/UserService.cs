using RestSharp;
using RestSharp.Extensions;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.DTOs;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserPreferences> GetPreferences(Guid Id)
        {
            return await _repository.GetUserPreferencesByUserIdAsync(Id);
        }

        public async Task<UserProfile> GetProfile(Guid UserId)
        {
           return await _repository.GetUserProfileByUserIdAsync(UserId);
        }

        public async Task<Guid> GetUserId(string username)
        {
            var user = await _repository.GetUserbyUserNameAsync(username);
            return user.Id;
        }

        public async Task<Guid> Register(RegisterDTO registerDTO)
        {
            string accessToken = await GetAccessTokenForUserCreation();

            //Replacing with Okta Auth0 call. User details will be stored in Okta.
             var client = new RestClient("https://dev-sagaplay.eu.auth0.com/api/v2/users");
             var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken);

            var body = new
            {
                connection = "Username-Password-Authentication",
                email = registerDTO.UserEmail,   // use email here, not "username",
                username = registerDTO.UserName,
                password = registerDTO.Password
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                //Add user in database so that userpreferences and userprofile can be created later.
                User user = new User
                {
                    UserName = registerDTO.UserName                    
                };

                Guid userId = await _repository.AddUserAsync(user);             
     

                return userId;
               
            }
            else
            {
                return Guid.Empty;
            }
        }

        public async Task<UserPreferences> SetPreferences(UserPreferences preferences)
        {
            return await _repository.UpdateUserPreferencesAsync(preferences);
        }
        

        public async Task<UserProfile> UpdateProfile(UserProfile userProfile)
        {
           return await _repository.UpdateUserProfileAsync(userProfile);
        }

        private async Task<string> GetAccessTokenForUserCreation()
        {
            var client = new RestClient("https://dev-sagaplay.eu.auth0.com/oauth/token");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("content-type", "application/json");
            request.AddStringBody(@"{
              ""client_id"": ""8yYC8t63VjfR8wGmPfeCc2UE86FcvLoF"",
              ""client_secret"": ""m6_Zyscv5d2GkPMZB65LlkKvQmsCeY6x-71BjxPNpPXAad8J7dEjz0aK_QJnHkC3"",
              ""audience"": ""https://dev-sagaplay.eu.auth0.com/api/v2/"",
              ""grant_type"": ""client_credentials""
            }", DataFormat.Json);

            var response = await client.ExecuteAsync(request);

            var jsonResponse = response.Content;
            var AuthTokenResponse = JsonSerializer.Deserialize<Auth0TokenResponse>(jsonResponse);

            return AuthTokenResponse.AccessToken;

        }
    }
}
