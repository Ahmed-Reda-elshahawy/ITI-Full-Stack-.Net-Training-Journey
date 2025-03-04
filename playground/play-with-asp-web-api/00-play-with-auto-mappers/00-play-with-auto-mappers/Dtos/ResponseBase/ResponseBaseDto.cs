using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _00_play_with_auto_mappers.Dtos.ResponseBase
{
    public class ResponseBaseDto
    {
        public object? Data { get; set; }
        public object? MetaData{ get; set; }
        public List<string>? Errors{ get; set; }

        public ResponseBaseDto(object? data, object? metaData = null)
        {
            Data = data;
            MetaData = metaData;
            Errors = null;
        }

        public ResponseBaseDto(List<string> errors)
        {
            Data = null;
            MetaData = null;
            Errors = errors;
        }
    }
}
