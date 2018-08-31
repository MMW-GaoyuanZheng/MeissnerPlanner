using JsonFx.Json;

namespace Youtrack.OutlookDesktop.Projects
{
    [JsonName("assignee")]
    public class Assignee
    {
        public string Login { get; set; }
    }
}