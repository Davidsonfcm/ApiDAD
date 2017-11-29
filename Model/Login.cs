using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace Model
{
    public class Login
    {
        public UsuarioDTO buscarUsuario(string cpf, string senha)
        {
            // select from usuarios where cpf=cpf 

            //se senha nao for valida, incrementar tentativa

            //se tentativa >= 3, bloqueado = true

            //se senha valida, tentativa =0, bloqueado = false
            string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string command = "SELECT * FROM usuario WHERE cpf = @cpf and senha = @senha";
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    cmd.Parameters.AddWithValue("@cpf",cpf);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader!=null)
                    {
                        var usuario = new UsuarioDTO();
                        usuario.cpf = reader.GetString(0);
                        usuario.senha = reader.GetString(1);
                        usuario.tipo = reader.GetString(2);
                        usuario.tentativas = reader.GetInt32(3);
                        usuario.bloqueado = reader.GetBoolean(4);
                        return usuario;
                    }

                }

            }
            return null;


        }
    }
}
