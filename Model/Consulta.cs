using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Consulta
    {
        public int identificador { get; set;}
        public DateTime data { get; set; }
        public string animal { get; set; }
        public string descricao { get; set; }
        public string diagnostico { get; set; }
        public string usuarioCpf { get; set; }
    }
}
