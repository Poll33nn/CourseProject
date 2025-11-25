using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class ForestEventReportDto
    {
        public int EventId { get; set; }
        public string EventType { get; set; }
        public int PlotId { get; set; }
        public string TreeType { get; set; }
        public string? Description { get; set; }
        public DateOnly Date { get; set; }
        public int? TreesNumber { get; set; }
    }
}
