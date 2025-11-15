using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ForestPlotService
    {
        private readonly ForestryContext _context = new();

        public async Task<List<ForestPlotDto>> GetForestPlotsAsync()
        {
            var forestPlots = await _context.ForestPlots.ToListAsync<ForestPlotDto>();
            var forestPlotsDto = new ForestPlotDto()
            {
               
            };
            return forestPlotsDto;
        }

        public async Task CreateForestPlotAsync(CreateForestPlotDto createPlotDto)
        {
            var forestPlot = new CreateForestPlotDto()
            {
               
            };
        }
    }
}
