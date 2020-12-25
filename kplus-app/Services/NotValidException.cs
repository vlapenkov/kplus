using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kplus_app.Services
{
    public class NotValidException : Exception
    {
        public string FieldName { get; set; }
        public NotValidException(string message, string fieldName) : this(message)
        {
            FieldName = fieldName;
        }
        public NotValidException(string message) : base(message)
        {
            
        }
    }
       
    
}
