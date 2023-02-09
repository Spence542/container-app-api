using Microsoft.AspNetCore.Mvc;

namespace container_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallFunctionController : ControllerBase
    {
        
        private readonly ILogger<CallFunctionController> _logger;
        private readonly HttpClient _httpClient;
        private readonly NetEventListener _netEventListener;

        public CallFunctionController(ILogger<CallFunctionController> logger, HttpClient httpClient, NetEventListener netEventListener)
        {
            _logger = logger;
            _httpClient = httpClient;
            _netEventListener = netEventListener;
        }

        [HttpGet("GetDataFromFunctionsAppSameDomain")]
        public async Task<IActionResult> GetDataFromFunctionsAppSameDomain()
        {
            _logger.LogInformation("Attempting api call to functions app with neonhealthsolutions domain");
            var result = await _httpClient.GetAsync("https://givemedata.neonhealthsolutions.com/api/givemedata?requestUrl=neonhealthsolutions");
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"response: {result.StatusCode}  for request with neonhealthsolutions domain");
            }
            else
            {
                _logger.LogError($"response: {result.StatusCode}  for request with neonhealthsolutions domain");
            }
            return Ok($"request to domain: https://givemedata.neonhealthsolutions.com/api/givemedata?requestUrl=neonhealthsolutions \nresults in: {result.StatusCode}");
        }

        [HttpGet("GetDataFromFunctionsAppDifferentDomain")]
        public async Task<IActionResult> GetDataFromFunctionsAppDifferentDomain()
        {
            _logger.LogInformation("Attempting api call to functions app with azurewebsites domain");
            var result = await _httpClient.GetAsync("https://give-me-data-test-app.azurewebsites.net/api/givemedata?requestUrl=azurewebsites");
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"response: {result.StatusCode} for request with azurewebsites domain");
            }
            else
            {
                _logger.LogError($"response: {result.StatusCode} for request with azurewebsites domain");
            }
            return Ok($"request to domain: https://give-me-data-test-app.azurewebsites.net/api/givemedata?requestUrl=azurewebsites \nresults in: {result.StatusCode}");
        }
    }
}