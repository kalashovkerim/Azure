using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace Nimb.Controllers
{
    [Route("error")]
    public class ErrorPagesController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public ErrorPagesController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("404")]
        public IActionResult NotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            _telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            {
                ["originalPath"] = originalPath
            });
            return View();
        }
    }
}
