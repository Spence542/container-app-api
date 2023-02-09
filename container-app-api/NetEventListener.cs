using System.Diagnostics.Tracing;

namespace container_app_api
{
    public class NetEventListener : EventListener
    {
        private readonly ILogger<NetEventListener> _logger;
        public NetEventListener(ILogger<NetEventListener> logger)
        {
            _logger = logger;
        }
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name.StartsWith("System.Net"))
                EnableEvents(eventSource, EventLevel.Informational);
        }
        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventName == "ResolutionStart")
            {
                _logger.LogInformation(eventData.EventName + " - " + eventData.Payload[0]);
            }
            else if (eventData.EventName == "RequestStart")
            {
                _logger.LogInformation(eventData.EventName + " - " + eventData.Payload[1]);
            }
        }
    }
}
