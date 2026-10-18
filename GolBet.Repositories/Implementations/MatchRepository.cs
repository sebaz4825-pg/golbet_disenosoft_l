// GolBet.Repositories/Implementations/MatchRepository.cs
using GolBet.Entities;
using GolBet.Entities.Enums;
using GolBet.Repositories.Data;
using GolBet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GolBet.Repositories.Implementations;

public class MatchRepository : GenericRepository<Match>, IMatchRepository
{
    public MatchRepository(AppDbContext context) : base(context) { }

    // Se agrega el parámetro opcional search
     public async Task<IEnumerable<Match>> GetAllWithTeamsAsync(MatchStatus? status = null, string? search = null) // se modifica
     {
        var query = _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Where(m => m.IsActive)
            .AsNoTracking()
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(m => m.Status == status.Value);

        // Se agrega la composición del filtro de búsqueda en SQL
        if (!string.IsNullOrWhiteSpace(search)) // se modifica 
        {
            string searchLower = search.Trim().ToLower(); // se modifica 
            query = query.Where(m =>
                m.HomeTeam.Name.ToLower().Contains(searchLower) ||
                m.AwayTeam.Name.ToLower().Contains(searchLower)); // se modifica 
        }

        return await query.OrderBy(m => m.Date).ToListAsync();
     }

    public async Task<Match?> GetByIdWithDetailsAsync(int id)
        => await _dbSet
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Bets)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
}
