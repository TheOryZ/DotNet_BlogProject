using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;

namespace BlogProjectFront.ApiServices.Concrete
{
    public class ImageApiManager : IImageApiService
    {
        private readonly HttpClient _httpClient;
        public ImageApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:60231/api/images/");
        }

        public async Task<string> GetBlogImageByIdAsync(int id)
        {
            //GetBlogImageById
            var responseMessage = await _httpClient.GetAsync($"GetBlogImageById/{id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                return $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            }
            return null;
        }

    }
}