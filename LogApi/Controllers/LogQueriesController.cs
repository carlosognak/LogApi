using System.Collections.Generic;
using LogApi.Models;
using LogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LogApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LogQueriesController : Controller
  {
    private readonly Dictionary<string, int> queries;
    private readonly ILogger<LogQueriesController> _logger;
    private readonly ILogFileReader _logFileReader;
    public LogQueriesController( ILogger<LogQueriesController> logger, ILogFileReader logFileReader)
    {
      _logger = logger;
      _logFileReader = logFileReader;
       queries = new Dictionary<string, int>();
     
    }

    /// <summary>
    /// renvoie un JSON comptant le nombre de requête distincte sur la période
    /// </summary>
    /// <param name="datePrefix"></param>
    /// <returns></returns>
    [HttpGet("queries/count/{datePrefix}")]
    public IActionResult Count(string datePrefix)
    {
      try
      {
        var count = 0;
        Dictionary<string, int> mydict = new Dictionary<string, int>();
        var logEntries = _logFileReader.ReadLogEntries();
        if (logEntries != null)
        {
          foreach (var logEntry in logEntries)
          {
            var query = new LogEntry { Date = logEntry.Date, Request = logEntry.Request };
            if (query.Date.ToString("yyyy-MM-dd HH:mm:G").StartsWith(datePrefix) && !mydict.ContainsKey(query.Request))
            {
              count++;
              mydict.Add(query.Request, 0);
            }
          }
        }
        return Json(new{count});
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error while counting queries.");
        return StatusCode(500);
      }
      
    }

    /// <summary>
    /// renvoie un JSON contenant les <size> requêtes les plus populaires, ainsi que leur nombre
    /// </summary>
    /// <param name="datePrefix"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [HttpGet("queries/popular/{datePrefix}")]
    public  IActionResult Popular(string datePrefix, int size)
    {
      try
      {
        int count = 0;
        Dictionary<string, int> mydict = new Dictionary<string, int>();
        var logEntries = _logFileReader.ReadLogEntries();
        if (logEntries != null)
        {
          foreach (var logEntry in logEntries)
          {
            var query = new LogEntry { Date = logEntry.Date, Request = logEntry.Request };
            if (query.Date.ToString("yyyy-MM-dd HH:mm:G").StartsWith(datePrefix))
            {
              if (mydict.ContainsKey(query.Request))
              {
                mydict[query.Request]++;
              }
              else
              {
                mydict.Add(query.Request, 1);

              }
            }
          }

        }
        var popularQueries = mydict
          .OrderByDescending(q => q.Value)
          .Take(size)
          .Select(q => new { Query = q.Key, Count = q.Value })
          .ToList();

        var result = new { Queries = popularQueries };
        return Ok(result);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error while retrieving popular queries.");
        return StatusCode(500);
      };
    }



  }
}
