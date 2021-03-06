﻿using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ConsultaServices : Services<Consulta>
    {
        public ResponseDTO BuscarTodos()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                response.Contents = this.All().Where(c =>
                (c.diagnostico == "" || c.diagnostico == null)
                && c.data.Year >= DateTime.Now.Year
                && c.data.Month >= DateTime.Now.Month
                && c.data.Day >= DateTime.Now.Day)
                .OrderBy(c => c.data)
                .AsEnumerable()
                .Select(c => new {
                    identificador = c.identificador,
                    animal = c.animal,
                    data = c.data.Day.ToString() 
                    +"/"+ c.data.Month.ToString()
                    + "/"+ c.data.Year.ToString()
                    +" "+ c.data.Hour.ToString() +":"+ c.data.Minute.ToString(),
                    diagnostico = c.diagnostico,
                    usuarioCpf = c.usuarioCpf,
                    proprietario = this.context.Usuarios.Where(a => a.cpf == c.usuarioCpf).FirstOrDefault().nome
                }).ToList();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO BuscarConsulta(int identificador)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<Consulta> consultas = this.context.Consulta.Where(c => c.identificador.Equals(identificador)).ToList();

                if (consultas.Count == 0)
                {
                    response.Message = "Consulta não encontrada";
                    return response;
                }

                response.Contents = consultas;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO Buscar(string cpf)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<Consulta> consultas = this.context.Consulta.Where(c => c.usuarioCpf.Equals(cpf)).ToList();

                if (consultas.Count == 0)
                {
                    response.Message = "Você não possui agendamentos";
                    return response;
                }

                response.Contents = consultas;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO AgendarConsulta(Consulta consulta)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                this.context.Consulta.Add(consulta);
                this.context.SaveChanges();

                response.Message = "Agendamento feito com sucesso";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO Diagnosticar(Consulta consulta)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Consulta diagnosticar = this.context.Consulta.First(c => c.identificador.Equals(consulta.identificador));
                diagnosticar.diagnostico = consulta.diagnostico;
                Update(diagnosticar);
                this.context.SaveChanges();

                Usuario usuario = this.context.Usuarios.FirstOrDefault(c => c.cpf.Equals(consulta.usuarioCpf));

                new MailServices().EnviaMensagemEmail(usuario.email, "Diagnóstico do seu animal", consulta.diagnostico);

                response.Message = "Diagnóstico efetuado com sucesso," +
                    " foi enviado um email com o diagnóstico para o associado. ";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDTO CancelarConsulta(int identificador)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                Delete(identificador);
                this.context.SaveChanges();

                response.Message = "Agendamento cancelado com sucesso";
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
