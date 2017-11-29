using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Mail
    {
        public string destinatario { get; set; }
        public string remetente { get; set; }
        public string assunto { get; set; }
        public string corpo { get; set; }
    }
}
