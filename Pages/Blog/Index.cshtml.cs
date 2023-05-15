using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor9_identity.Models;

namespace Razor9_identity.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly Razor9_identity.Models.MyBlogContext _context;

        public IndexModel(Razor9_identity.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;
        [BindProperty(SupportsGet =true, Name ="SearchString")]
        public string SearchString {set;get;}

        [BindProperty(SupportsGet =true, Name ="p")]
        public int currentPage {set;get;}
        public int countPages {set;get;}
        public const int NUM_PER_PAGE =10;

        public async Task OnGetAsync()
        {
            if (_context.articles != null)
            {
                // Article = await _context.articles.ToListAsync();
                var totalArticle = await _context.articles.CountAsync();
                countPages = (int)Math.Ceiling((double)totalArticle/NUM_PER_PAGE);
                if(currentPage> countPages)
                    currentPage = countPages;
                if(currentPage<1)
                    currentPage=1;
                int skip =(currentPage-1)*NUM_PER_PAGE;

                var qr = from a in _context.articles
                        orderby a.Created descending
                        select a; 

                if(!string.IsNullOrEmpty(SearchString))
                {
                    Article = await qr.Where(a=>a.Title.Contains(SearchString)).ToListAsync();
                    totalArticle = Article.Count;
                    countPages = (int)Math.Ceiling((double)totalArticle/NUM_PER_PAGE);
                    
                    skip =(currentPage-1)*NUM_PER_PAGE;
                    Article = await qr.Where(a=>a.Title.Contains(SearchString))
                                      .Skip(skip).Take(NUM_PER_PAGE)
                                      .ToListAsync() ;
                }
                else
                {

                     Article = await qr.Skip(skip).Take(NUM_PER_PAGE).ToListAsync();
                }
            }
        }
    }
}
