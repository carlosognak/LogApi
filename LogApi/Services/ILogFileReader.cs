using LogApi.Models;

namespace LogApi.Services
{
  public interface ILogFileReader
  {
    public List<LogEntry> ReadLogEntries();

  }
}
