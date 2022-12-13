using TPro.Models.ResponseDtos;

namespace TPro.Business
{
    public class BaseService
    {
        public ResponseModel Success(dynamic data = null, string message = "Success")
        {
            return new ResponseModel() { Code = 200, Data = data, Message = message };
        }

        public ResponseModel Fail(int code = 0, dynamic data = null, string message = "Failure")
        {
            return new ResponseModel() { Code = code, Data = data, Message = message };
        }

        public ResponseModel Fail(string message)
        {
            return new ResponseModel() { Code = 0, Data = null, Message = message };
        }
    }
}