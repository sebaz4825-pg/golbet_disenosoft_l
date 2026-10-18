// GolBet.Repositories/Interfaces/IMatchRepository.cs
using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Implementations;

namespace GolBet.Repositories.Interfaces;

public interface IMatchRepository : IGenericRepository<Match>
{
    // Agregamos el parámetro opcional 'search' para soportar la búsqueda por equipo
    Task<IEnumerable<Match>> GetAllWithTeamsAsync(MatchStatus? status = null, string? search = null); // Se modifica
    Task<Match?> GetByIdWithDetailsAsync(int id);
}


