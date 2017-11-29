using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
            this.Success = false;
        }
   
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Contents { get; set; }
    }
}

