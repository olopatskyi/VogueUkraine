using System.Collections.Generic;
using System.Linq;
// ReSharper disable MemberCanBeProtected.Global

namespace VogueUkraine.Framework.Utilities.Api.Response;

public class ResponseErrorItem : ResponseErrorItem<string>
{
    public ResponseErrorItem()
    { } 

    public ResponseErrorItem(string status, string message = null, string source = null)
        : base(status, message, source) { }

    public ResponseErrorItem(string status, IReadOnlyCollection<string> messages, string source = null)
        : base(status, messages, source) { }
}
public class ResponseErrorItem<T>
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public T Status { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public List<string> Messages { get; set; } = new();
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Source { get; set; }
        
    public ResponseErrorItem(){} 

    public ResponseErrorItem(T status, string message = null, string source = null)
    {
        Status = status;
        Source = source;
        if (message != null)
            Messages.Add(message);

           
    }
    public ResponseErrorItem(T status, IReadOnlyCollection<string> messages, string source = null)
    {
        Status = status;
        Source = source;
        if (messages.Any())
            Messages.AddRange(messages);
    }
}