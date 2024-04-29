using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Interfaces;

public interface IVeiculoServico
{
    List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null, int? ano = null);
    Veiculo? BuscaPorId(int id);
    Veiculo? Incluir(Veiculo veiculo);
    Veiculo? Atualizar(int id, Veiculo veiculo);
    Veiculo? Apagar(Veiculo veiculo);
}