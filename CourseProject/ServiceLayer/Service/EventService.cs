using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;

namespace ServiceLayer.Service
{
    public class EventService
    {
        private readonly ForestryContext _context;
        public EventService(ForestryContext context)
        {
            _context = context;
        }
        public async Task<List<EventTypeDto>> GetEventTypeNameAsync()
        {
            try
            {
                var eventTypes = await _context.EventTypes.ToListAsync();

                if (eventTypes == null)
                    return null;

                return eventTypes.Select(eventType => new EventTypeDto
                {
                    EventTypeId = eventType.EventTypeId,
                    Name = eventType.Name,
                }).ToList();
            }
            catch (DbException)
            {
                throw;
            }
        }
        public async Task<List<ForestEventReportDto>> GetAllSilvicultureEventAsync()
        {
            try
            {
                var forestryEvents = await _context.SilvicultureEvents
                .Include(e => e.EventType)
                .Include(e => e.TreeType)
                .ToListAsync();

                if (forestryEvents == null)
                    return null;

                return forestryEvents.Select(forestryEvent => new ForestEventReportDto
                {
                    EventId = forestryEvent.EventId,
                    EventType = forestryEvent.EventType.Name,
                    PlotId = forestryEvent.PlotId,
                    TreeType = forestryEvent.TreeType.Name,
                    Date = forestryEvent.Date,
                    Description = forestryEvent.Description,
                    TreesNumber = forestryEvent.TreesNumber,
                }).ToList();
            }
            catch (DbException)
            {
                throw;
            }            
        }
        public async Task<List<ForestEventReportDto>> GetPlotSilvicultureEventAsync(int PlotId)
        {
            try
            {
                var plotEvents = await _context.SilvicultureEvents
                .Include(e => e.EventType)
                .Include(e => e.TreeType)
                .Where(e => e.PlotId == PlotId)
                .ToListAsync();

                if (plotEvents == null)
                    return null;

                return plotEvents.Select(plotEvent => new ForestEventReportDto
                {
                    EventId = plotEvent.EventId,
                    EventType = plotEvent.EventType.Name,
                    PlotId = PlotId,
                    TreeType = plotEvent.TreeType.Name,
                    Date = plotEvent.Date,
                    Description = plotEvent.Description,
                    TreesNumber = plotEvent.TreesNumber,
                }).ToList();
            }
            catch (DbException)
            {
                throw;
            } 
        }
        public async Task<bool> CreateSilvicultureEventAsync(CreateSilvicultureEventDto silvicultureEvent)
        {
            try
            {
                var forestEvent = new SilvicultureEvent
                {
                    PlotId = silvicultureEvent.PlotId,
                    EventTypeId = silvicultureEvent.EventTypeId,
                    TreeTypeId = silvicultureEvent.TreeTypeId,
                    Description = silvicultureEvent.Description,
                    Date = DateOnly.FromDateTime(silvicultureEvent.Date),
                    TreesNumber= silvicultureEvent.TreesNumber,
                };

                if (forestEvent == null)
                    return false;

                _context.SilvicultureEvents.Add(forestEvent);
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
