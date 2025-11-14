using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class SilvicultureEventDto
    {
        public int EventId { get; set; }
        public string EventType { get; set; };
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int? TreesNumber { get; set; }
    }
}
