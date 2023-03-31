using System.IO.Compression;
using LogApi.Models;

namespace LogApi.Services
{
  public class LogFileReader : ILogFileReader
  {
    private readonly string _filePath;

    public LogFileReader(IConfiguration configuration)
    {
      _filePath = configuration.GetValue<string>("FilePath");
    }

    /// <summary>
    /// Ajouter la classe LogFileReader pour lire le fichier .tsv et retourner une liste d'objets LogEntry :
    /// </summary>
    /// <returns></returns>
    public List<LogEntry> ReadLogEntries()
    {
      var logEntries = new List<LogEntry>();

      using (var fileStream = File.OpenRead(_filePath))
      using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
      using (var streamReader = new StreamReader(gzipStream))
      {
        while (!streamReader.EndOfStream)
        {
          var line = streamReader.ReadLine();
          if (!string.IsNullOrEmpty(line))
          {
            var fields = line.Split('\t');
            var logEntry = new LogEntry
            {
              Date = DateTime.Parse(fields[0]),
              Request = fields[1]
            };
            logEntries.Add(logEntry);
          }
        }
      }

      
      return logEntries;
    }



  }
}
