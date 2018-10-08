using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data
{
  public interface ICampRepository
  {
    // General 
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveChangesAsync();

    // Camps
    Task<Camp[]> GetAllCampsAsync(bool includeTalks = false);
    Task<Camp> GetCampAsync(string moniker, bool includeTalks = false);
    Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);

    // Talks
    Task<Talk> GetTalkByMonikerAsync(string moniker, int talkId, bool includeSpeakers = false);
    Task<Talk[]> GetTalksByMonikerAsync(string moniker, bool includeSpeakers = false);

    // Speakers
    Task<Speaker[]> GetSpeakersByMonikerAsync(string moniker);
    Task<Speaker> GetSpeakerAsync(int speakerId);
    Task<Speaker[]> GetAllSpeakersAsync();

  }
}