using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.BlogModels
{
    public class PostListViewModel
    {
        public IEnumerable<PostModel> Posts { get; set; }
        public PostPagingInfo PagingInfo { get; set; }
    }
}