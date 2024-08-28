using SimplePOSWeb.Process.Abstraction;

namespace SimplePOSWeb.Process.Implementation
{
    public class HelperProcess : IHelperProcess
    {
        public string GetActivityClass(int i)
        {
            List<string> textClasses = ["text-success", "text-danger", "text-primary", "text-info", "text-warning", "text-muted"];
            var index = (i - 1) % textClasses.Count; // Adjusting index to start from 0

            return textClasses[index];
        }
    }
}
