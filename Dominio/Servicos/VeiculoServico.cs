using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly DbContexto _contexto;
        public VeiculoServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null, int? ano = null)
        {
            var query = _contexto.Veiculos.AsQueryable();
            if(!string.IsNullOrEmpty(nome))
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));

            int itensPorPagina = 5;

            if(pagina != null)
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina );

            return query.ToList();
        }

        public Veiculo? BuscaPorId(int id)
        {
            return _contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
        }

        public Veiculo? Incluir(Veiculo veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
            return veiculo;
        }

        public Veiculo? Atualizar(int id, Veiculo veiculo)
        {
            var veiculoAtual = _contexto.Veiculos.Find(id);
            if (veiculoAtual == null)
            {
                return null;
            }
            else
            {
                veiculoAtual.Nome = veiculo.Nome;
                veiculoAtual.Marca = veiculo.Marca;
                veiculoAtual.Ano = veiculo.Ano;
                _contexto.SaveChanges();
                return veiculoAtual;
            }

        }

        public Veiculo? Apagar(Veiculo veiculo)
        {
            _contexto.Veiculos.Remove(veiculo);
            _contexto.SaveChanges();
            return veiculo;
        }
    }
}