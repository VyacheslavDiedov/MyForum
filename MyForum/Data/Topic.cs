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
        public string Name { get; set; }
        public string TopicDescription { get; set; }
        public int TypeTopicId { get; set; }
        public virtual TypeTopic TypeTopic { get; set; }
    }
}
