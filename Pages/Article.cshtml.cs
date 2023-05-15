using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor9_identity.Models;

namespace Razor9_identity.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly MyBlogContext myblogContext;
        private readonly ILogger<ArticleModel> logger;
        public ArticleModel (MyBlogContext _myblogContext, ILogger<ArticleModel> _logger)
        {
            myblogContext = _myblogContext;
            logger = _logger;
            logger.LogInformation("Tạo thành công article");
        }
        public void OnGet()
        {
            var posts = (from p in myblogContext.articles
                        orderby p.Title descending
                        select p).ToList();
            ViewData["posts"] = posts;
        }
    }
}
