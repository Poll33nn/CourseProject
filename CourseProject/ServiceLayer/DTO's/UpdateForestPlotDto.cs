using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class UpdateForestPlotDto
    {
        public int PlotId { get; set; }
        public int UserId { get; set; }
        public int Compartment { get; set; }
        public int Subcompartment { get; set; }
        public List<CreateTreesNumberDto> TreeComposition { get; set; } = new();
    }
}
