using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyForum.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string PostTitle { get; set; }
        [Required]
        public string PostBody { get; set; }
        public DateTime AddPost { get; set; }
        //public string UserId { get; set; }
        //public virtual User User { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
