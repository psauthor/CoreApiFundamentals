using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreCodeCamp.Data
{
  public class CampRepository : ICampRepository
  {
    private readonly CampContext _context;
    private readonly ILogger<CampRepository> _logger;

    public CampRepository(CampContext context, ILogger<CampRepository> logger)
    {
      _context = context;
      _logger = logger;
    }

    public void Add<T>(T entity) where T : class 
    {
      _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T: class
    {
      _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
      _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
      _logger.LogInformation($"Attempitng to save the changes in the context");

      // Only return success if at least one row was changed
      return (await _context.SaveChangesAsync()) > 0;
    }

    public async Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
    {
      _logger.LogInformation($"Getting all Camps");

      IQueryable<Camp> query = _context.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query
          .Include(c => c.Talks)
          .ThenInclude(t => t.Speaker);
      }

      // Order It
      query = query.OrderByDescending(c => c.EventDate)
        .Where(c => c.EventDate.Date == dateTime.Date);

      return await query.ToArrayAsync();
    }

    public async Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
    {
      _logger.LogInformation($"Getting all Camps");

      IQueryable<Camp> query = _context.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query
          .Include(c => c.Talks)
          .ThenInclude(t => t.Speaker);
      }

      // Order It
      query = query.OrderByDescending(c => c.EventDate);

      return await query.ToArrayAsync();
    }

    public async Task<Camp> GetCampAsync(string moniker, bool includeTalks = false)
    {
      _logger.LogInformation($"Getting a Camp for {moniker}");

      IQueryable<Camp> query = _context.Camps
          .Include(c => c.Location);

      if (includeTalks)
      {
        query = query.Include(c => c.Talks)
          .ThenInclude(t => t.Speaker);
      }

      // Query It
      query = query.Where(c => c.Moniker == moniker);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false)
    {
      _logger.LogInformation($"Getting all Talks for a Camp");

      IQueryable<Talk> query = _context.Talks;

      if (includeSpeakers)
      {
        query = query
          .Include(t => t.Speaker);
      }

      // Add Query
      query = query
        .Where(t => t.Camp.Moniker == moniker)
        .OrderByDescending(t => t.Title);

      return await query.ToArrayAsync();
    }

    public async Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false)
    {
      _logger.LogInformation($"Getting all Talks for a Camp");

      IQueryable<Talk> query = _context.Talks;

      if (includeSpeakers)
      {
        query = query
          .Include(t => t.Speaker);
      }

      // Add Query
      query = query
        .Where(t => t.TalkId == talkId && t.Camp.Moniker == moniker);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker)
    {
      _logger.LogInformation($"Getting all Speakers for a Camp");

      IQueryable<Speaker> query = _context.Talks
        .Where(t => t.Camp.Moniker == moniker)
        .Select(t => t.Speaker)
        .Where(s => s != null)
        .OrderBy(s => s.LastName)
        .Distinct();

      return await query.ToArrayAsync();
    }

    public async Task<Speaker[]> GetAllSpeakersAsync()
    {
      _logger.LogInformation($"Getting Speaker");

      var query = _context.Speakers
        .OrderBy(t => t.LastName);

      return await query.ToArrayAsync();
    }


    public async Task<Speaker> GetSpeakerAsync(int speakerId)
    {
      _logger.LogInformation($"Getting Speaker");

      var query = _context.Speakers
        .Where(t => t.SpeakerId == speakerId);

      return await query.FirstOrDefaultAsync();
    }
  }
}
