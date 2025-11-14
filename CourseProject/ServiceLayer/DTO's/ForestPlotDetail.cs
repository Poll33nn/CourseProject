using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class ForestPlotDetail
    {
        public int PlotId { get; set; }
        public string Responsible { get; set; }
        public string Address { get; set; }
        List<TreesNumberDto> TreesComposition{ get; set; } = new();
        List<>

    }
}
