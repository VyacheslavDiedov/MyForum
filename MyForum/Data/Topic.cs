using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyForum.Data
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string TopicName { get; set; }
    }
}
