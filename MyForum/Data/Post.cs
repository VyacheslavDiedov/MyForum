﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyForum.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(180)]
        public string PostTitle { get; set; }
        [MaxLength(500)]
        public string PostBody { get; set; }
        public DateTime AddPost { get; set; }
        public string UserName { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
