using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ForestPlotService
    {
        private readonly ForestryContext _context;
        public ForestPlotService(ForestryContext context)
        {
            _context = context;
        }

        public async Task<List<ForestPlotDto>> GetForestPlotsAsync()
        {
            try
            {
                var forestPlots = await _context.ForestPlots
                .Include(fp => fp.User)
                .Include(fp => fp.TreesNumbers)
                .ThenInclude(tn => tn.TreeType)
                .ToListAsync();

                if (forestPlots == null)
                    return null;

                return forestPlots.Select(forestPlot => new ForestPlotDto
                {
                    PlotId = forestPlot.PlotId,
                    UserId = forestPlot.UserId,
                    Responsible = $"{forestPlot.User.LastName} {forestPlot.User.Name} {forestPlot.User.Patronymic}".Trim(),
                    Compartment = forestPlot.Compartment,
                    Subcompartment = forestPlot.Subcompartment,
                    TreesComposition = forestPlot.TreesNumbers.Select(treesNumber => new TreesNumberDto
                    {
                        TreeType = treesNumber.TreeType.Name,
                        Amount = treesNumber.Amount,
                    }).ToList()
                }).ToList();
            }
            catch (DbException)
            {
                throw;
            }    
        }

        public async Task<bool> CreateForestPlotAsync(CreateForestPlotDto createPlotDto)
        {
            try
            {
                var forestPlot = new ForestPlot
                {
                    PlotId = createPlotDto.PlotId,
                    UserId = createPlotDto.UserId,
                    Compartment = Convert.ToByte(createPlotDto.Compartment),
                    Subcompartment = Convert.ToByte(createPlotDto.Subcompartment),
                    TreesNumbers = createPlotDto.TreeComposition.Select(t => new TreesNumber
                    {
                        TreeTypeId = t.TreeTypeId,
                        Amount = t.Amount,
                    }).ToList()
                };

                if (forestPlot == null)
                    return false;

                _context.ForestPlots.Add(forestPlot);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                throw;
            } 
        }
        public async Task<bool> UpdateForestPlotAsync(UpdateForestPlotDto updatePlotDto)
        {
            try
            {
                var existingForestPlot = await _context.ForestPlots
                                .FirstOrDefaultAsync(fp => fp.PlotId == updatePlotDto.PlotId);

                if (existingForestPlot == null)
                    return false;

                existingForestPlot.PlotId = updatePlotDto.PlotId;
                existingForestPlot.UserId = updatePlotDto.UserId;
                existingForestPlot.Compartment = Convert.ToByte(updatePlotDto.Compartment);
                existingForestPlot.Subcompartment = Convert.ToByte(updatePlotDto.Subcompartment);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                throw;
            }
        }
        public async Task<bool> DeleteForestPlotAsync(int plotId)
        {
            try
            {
                var forestPlot = await _context.ForestPlots
                .Include(fp => fp.TreesNumbers)
                .Include(fp => fp.SilvicultureEvents)
                .FirstOrDefaultAsync(fp => fp.PlotId == plotId);

                if (forestPlot == null)
                    return false;

                _context.SilvicultureEvents.RemoveRange(forestPlot.SilvicultureEvents);
                _context.TreesNumbers.RemoveRange(forestPlot.TreesNumbers);
                _context.ForestPlots.Remove(forestPlot);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                throw;
            }   
        }
    }
}
