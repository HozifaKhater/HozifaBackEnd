using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.ViewModels
{
    public class ResponseResult
    {
        public bool State { get; set; }

        public string Errors { get; set; }

        public object Data { get; set; }


        public static ResponseResult SuccessWithMessage(string msg)
        {
            return new ResponseResult()
            {
                State = true,
                Data = new { Message = msg },
                Errors = ""
            };
        }

        public static ResponseResult Faild(string msg)
        {
            return new ResponseResult()
            {
                State = false,
                Data = new { },
                Errors = msg
            };
        }

        public static ResponseResult SuccessWithData(dynamic data)
        {
            return new ResponseResult()
            {
                State = true,
                Data = new { Result = data },
                Errors = ""
            };
        }
    }
}
