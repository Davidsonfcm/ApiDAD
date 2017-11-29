using Model;
using Model.DTO;
using System;
using System.Linq;

namespace Services
{
    public class UsuarioServices : Services<Usuario>
    {
        public ResponseDTO Buscar(string cpf)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                response.Contents = this.context.Usuarios.FirstOrDefault(x => x.cpf == cpf);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO BuscarTodos()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                
                response.Contents = this.All().ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO ValidarUsuario(string cpf, string senha)
        {
            ResponseDTO response = new ResponseDTO();

            Usuario usuario = this.context.Usuarios.FirstOrDefault(x => x.cpf.Equals(cpf));
            if (usuario != null)
            {
                if (usuario.bloqueado)
                {
                    response.Message = "Número de tentativas excedido, seu usuário foi bloqueado";
                }
                else
                {
                    if (usuario.senha != senha)
                    {
                        //se senha for invalida, aumentar numero de tentativas e bloquear se ja tiver 3
                        usuario.tentativas += 1;
                        if (usuario.tentativas >= 3)
                            usuario.bloqueado = true;
                        this.context.SaveChanges();

                        response.Message = "Senha inválida";
                    }
                    else
                    {
                        //se senha for valida, zerar tentativas
                        if (usuario.tentativas != 0)
                        {
                            usuario.tentativas = 0;
                            this.context.SaveChanges();
                        }

                        response.Contents = usuario;
                        response.Success = true;
                    }
                }
            }
            else
            {
                response.Message = "Login inválido.";
            }

            return response;
        }

        public ResponseDTO NovoUsuario(string cpf, string senha)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (Buscar(cpf) == null)
                {
                    response.Message = "Usuário inexistente";
                }
                else
                {
                    Usuario u = new Usuario { cpf = cpf, senha = senha, tipo = "Associado", bloqueado = false, tentativas = 0 };
                    this.context.Usuarios.Add(u);
                    this.context.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


        public ResponseDTO NovoUsuario(Usuario u)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (Buscar(u.cpf) == null)
                {
                    response.Message = "Usuário inexistente";
                }
                else
                {
                    this.context.Usuarios.Add(u);
                    this.context.SaveChanges();

                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public ResponseDTO EditarUsuario(Usuario u)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (Buscar(u.cpf) == null)
                {
                    response.Message = "Usuário inexistente";
                }
                else
                {
                    Usuario usuario = this.context.Usuarios.FirstOrDefault(x => x.cpf.Equals(u.cpf)); ;
                    usuario.email = u.email;
                    usuario.nome = u.nome;
                    usuario.senha = u.senha;
                    Update(usuario);
                    this.context.SaveChanges();

                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO DeleteUsuario(int i)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Delete(i);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

    }

}
