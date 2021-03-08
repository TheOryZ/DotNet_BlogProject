using System.Threading.Tasks;
using BlogProjectFront.ApiServices.Interfaces;
using BlogProjectFront.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogProjectFront.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        public int Id { get; set; }
        public BlogImageType BlogImageType { get; set; } = BlogImageType.BlogHome;

        private readonly IImageApiService _imageApiService;
        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);
            string html = string.Empty;
            if(BlogImageType == BlogImageType.BlogHome)
                html = $"<img src='{blob}' class='card-img-top'>";
            else
                html = $"<img src='{blob}' class='img-fluid rounded'>";
            output.Content.SetHtmlContent(html);
        }
    }
}