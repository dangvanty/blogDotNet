using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Razor9_identity.Helper
{
    public class PagingModel
    {
        public int currentPage {set;get;}
        public int countPages {set;get;}
        public string SearchString {set;get;}
        public Func<int? ,string?,string> generateUrl{set;get;}
      
    }
}