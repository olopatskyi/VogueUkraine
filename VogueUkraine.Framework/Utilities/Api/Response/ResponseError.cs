using System.Text.Json.Serialization;
using FluentValidation.Results;
using Newtonsoft.Json;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Framework.Extensions.Enum;
using VogueUkraine.Framework.Extensions.String;

namespace VogueUkraine.Framework.Utilities.Api.Response;

public class ResponseError : ResponseError<ResponseErrorItem, string>
{
    public ResponseError() {}
    public ResponseError(string status, string message = null, string source = null)
        : base(status, message, source)
    {
            
    }

    public ResponseError(ServiceResponse<ValidationResult> serviceResponse)
        : this(serviceResponse.Errors, serviceResponse.Status.GetDescription())
    {

    }

    public ResponseError(ValidationResult fluentValidationResult, string status = null)
    {
        foreach (var group in fluentValidationResult.Errors.GroupBy(x => x.PropertyName, x => x.ErrorMessage))
            if (string.IsNullOrEmpty(group.Key))
            {
                if(group.Any())
                    this.AddOneError((status ?? "INVALID_INPUT_MODEL").ToUpperInvariant(),
                            string.Join(", ", group));
            }
            else
            {
                var lowerKey = group.Key.ToLowerCamelcase();

                if(group.Any())
                    this.AddOneError(
                        (status ?? "INVALID_ATTRIBUTE").ToUpperInvariant(),
                        group.Select(e =>
                            string.IsNullOrEmpty(e)
                                ? $"The {lowerKey} field has wrong value."
                                : e.Replace(group.Key, lowerKey)),
                        lowerKey);
            }
    }
}

public class ResponseError<T, TStatus> where T : ResponseErrorItem<TStatus>, new()
{
    [JsonProperty("errors"), JsonPropertyName("errors")]
    public T[] Errors
    {
        get => ErrorItems?.ToArray();
        set=> ErrorItems = value?.ToList();
    }
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
    // ReSharper disable once MemberCanBePrivate.Global
    public List<T> ErrorItems { get; set; }
    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
    public bool HasErrors => ErrorItems.Count > 0;

    // constructors
    public ResponseError()
    {
        ErrorItems = new List<T>();
    }
    public ResponseError(T item)
    {
        ErrorItems = new List<T>();
        AddOneError(item);
    }
    public ResponseError(TStatus status, string message = null, string source = null)
    {
        ErrorItems = new List<T>();
        AddOneError(status, message, source);
    }
    // methods
    // ReSharper disable once MemberCanBePrivate.Global
    public void AddOneError(T item)
    {
        ErrorItems.Add(item);
    }
    public void AddOneError(TStatus status, string message = null, string source = null)
    {
        AddOneError(new T
        {
            Messages = new List<string>{message},
            Source = source,
            Status = status
        });
    }
    public void AddOneError(TStatus status, IEnumerable<string> messages, string source = null)
    {
        AddOneError(new T
        {
            Messages = messages?.ToList(),
            Source = source,
            Status = status
        });
    }
    public void AddManyErrors(IEnumerable<T> items)
    {
        ErrorItems.AddRange(items);
    }
}