using FluentValidation.Results;

namespace ProductCRUD.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(ValidationFailure failure) : this()
    {
        Errors = new Dictionary<string, string[]>
        {
            { failure.PropertyName, new string[] { failure.ErrorMessage } }
        };
    }

    public ValidationException(string propertyName, string errorMessage)
    {
        Errors = new Dictionary<string, string[]>
        {
            { propertyName, new string[] { errorMessage } }
        };
    }

    public IDictionary<string, string[]> Errors { get; }
}
