namespace Domain.Responses;

public class TraceModel
{
    public bool HasErrors() => Errors.Count > 0;
    private IList<Exception> Errors { get; set; } = [];

    protected void AddError(Exception exception)
    {
        Errors.Add(exception);
    }

    public IList<Exception> GetAllErrors()
    {
        return Errors;
    }

    public List<string> GetAllMessageErrors()
    {
        return Errors.Select(e => e.Message).ToList();
    }
}