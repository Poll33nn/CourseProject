using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class ForestPlotDto
    {
        public int Id { get; set; }
        public string Responsible { get; set; }
        public int Compartment {  get; set; }
        public int Subcompartment {  get; set; }

    }
}
