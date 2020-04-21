using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string TopicName { get; set; }
    }
}
