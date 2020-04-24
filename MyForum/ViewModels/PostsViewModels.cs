using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.ViewModels
{
    public class PostsViewModels
    {
            public IEnumerable<Data.Post> Posts { get; set; }
            public IEnumerable<Data.User> Users { get; set; }
    }
}
