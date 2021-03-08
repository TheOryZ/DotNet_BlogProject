using BlogProjectFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectFront.ViewComponents
{
    public class LastFiveBlog : ViewComponent
    {
        private readonly IBlogApiService _blogApiService;
        public LastFiveBlog(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_blogApiService.GetLastFiveAsync().Result);
        }
    }
}