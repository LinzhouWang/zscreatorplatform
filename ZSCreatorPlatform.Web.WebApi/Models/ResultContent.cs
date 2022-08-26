using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Models
{
    public class ResultContent
    {

        public int Code { get; set; }

        public string Msg { get; set; }

        public Object Data { get; set; }


        public static ResultContent Result(int code,string msg,Object obj)
        {
            return new ResultContent 
            {
                Code = code,
                Msg = msg,
                Data = obj??default(Object)
            };
        }


    }
}
