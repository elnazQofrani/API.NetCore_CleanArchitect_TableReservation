using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   
       
    public record Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType Type { get; set; }

        public Error(string code, string description = null, ErrorType errorType = ErrorType.NotFound)
        {
            Code = code;
            Description = description;
            Type = errorType;
        }

        public static Error NotFound(string key, string value) => new(nameof(NotFound),
        $"[{key}]:[{value}] was not found",
        ErrorType.NotFound);

        public static Error NotFound<TValue>(object value) =>
        NotFound(nameof(NotFound), $"[{typeof(TValue).Name}]:[{value}] was not found");

        public static Error NotFound(string key, object value, string code = null) =>
        NotFound(code ?? nameof(NotFound), $"[{key}]:[{value}] was not found");


        public static Error Validation(string code, string description) =>
           new(code, description, ErrorType.Validation);


    }

    public enum ErrorType
    {
        NotFound,
        Validation,
        Forbidden,
        Conflict
    }
}

