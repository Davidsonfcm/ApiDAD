﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usuario
    {
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string senha { get; set; }
        public string tipo { get; set; }
        public int tentativas { get; set; }
        public bool bloqueado { get; set; }

    }
}

