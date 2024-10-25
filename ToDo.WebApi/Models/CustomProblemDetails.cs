using Microsoft.AspNetCore.Mvc;

namespace ToDo.WebApi.Models
{
    // CustomProblemDetails class extends the ProblemDetails class to provide additional error details.
    public class CustomProblemDetails : ProblemDetails
    {
        // Dictionary to hold validation errors or other field-specific error messages.
        // The key is the field name, and the value is an array of error messages for that field.
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
