using System.Collections.Generic;
using BusinessLayer.BusinessModelsDTO;

namespace WebUI.Models.BlogModels
{
    public class PostListViewModel
    {
        public IEnumerable<PostDTO> Posts { get; set; }
        public PostPagingInfo PagingInfo { get; set; }
    }
}