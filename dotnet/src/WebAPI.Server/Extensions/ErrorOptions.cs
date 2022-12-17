namespace JIL;

public static class ErrorOptionsExtensions
{
    public static ErrorOptions UseJILResponseBuilder(this ErrorOptions options)
    {
        options.ResponseBuilder = (failures, context, statusCode) => {
            context.Response.Headers.Add(ApiHeaders.ResponseObjectType, SchemaIdAttribute.ValidationFailure);
            return new ValidationProblemDetails(failures.GroupBy(failure => failure.PropertyName).ToDictionary(failure => failure.Key, failure => failure.Select(failure => failure.ErrorMessage).ToArray()))
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = statusCode,
                Instance = context.Request.Path,
                Extensions = { { "traceId", context.TraceIdentifier } }
            };
        };
        return options;
    }
}