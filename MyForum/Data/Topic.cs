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
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(180)]
        public string TopicDescription { get; set; }
        public DateTime AddTopic { get; set; }
    }
}
