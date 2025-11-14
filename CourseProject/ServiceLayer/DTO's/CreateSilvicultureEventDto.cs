using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class CreateSilvicultureEventDto
    {
        public int PlotId { get; set; }
        public int EventTypeId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int? TreesNumber { get; set; }
    }
}
