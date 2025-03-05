namespace ApiDemo.Dtos.Shared;

public class ResponseDto
{
    public object? Data{ get; set; }
    public object? MetaData{ get; set; }
    public List<string>? Errors{ get; set; }

    public ResponseDto(object data, object? metaData=null)
    {
        Data = data;
        MetaData = metaData;
        Errors = null;
    }

    public ResponseDto(List<string> errors)
    {
        Data = null;
        MetaData = null;
        Errors = errors;
    }
}
