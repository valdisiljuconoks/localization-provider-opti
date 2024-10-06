using DbLocalizationProvider.Cache;

namespace DbLocalizationProvider.EPiServer;

/// <summary>
/// Configuration context for DbLocalizationProvider.EPiServer.
/// </summary>
public class DbLocalizationConfigurationContext
{
    /// <summary>
    /// Allows to override internal cache implementation, if problems with default EPiServer implementation arise.
    /// </summary>
    public ICache InnerCache { get; set; } = new EPiServerCache();
}
