using System.Collections.Generic;

namespace AL.Atendimento.SobConsulta.Fronteiras.Dtos.Entidades
{
    public class DtoBase<TEntidade, TDto>
    {
        protected DtoBase()
        {
        }

        public static TDto CriarAPartirDeEntidade(TEntidade entidade)
        {
            return MapeadorDto.Mapear<TDto>(entidade);
        }

        public static List<TDto> CriarAPartirDeEntidade(List<TEntidade> lista)
        {
            if (lista == null)
                return null;

            return lista.ConvertAll(dto => CriarAPartirDeEntidade(dto));
        }
    }
}