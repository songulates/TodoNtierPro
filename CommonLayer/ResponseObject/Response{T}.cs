using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.ResponseObject
{
    //responseun data tasıyan versiyonunu yapalım
    public class Response<T>:Response, IResponse<T>
    {
        public T Data { get; set; }
        //responsetype alsın base e göndersin
        public Response(ResponseType responseType, T data) :base(responseType)
        {
            Data = data;
        }
        public Response(ResponseType responseType, string message) : base(responseType, message)
        {
            //hata mesajı gönderme
        }

        public Response(ResponseType responseType, T data, List<CustomValidationError> errors) : base(responseType)
        {
            ValidationErrors = errors;
            Data = data;
        }
        public List<CustomValidationError> ValidationErrors { get; set; }
    }
}
