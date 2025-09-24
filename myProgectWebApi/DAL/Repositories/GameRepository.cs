using Microsoft.EntityFrameworkCore;
using myProgectWebApi.DAL.Entities;

namespace myProgectWebApi.DAL.Repositories
{
    public class GameRepository:IGameRepository
    {
        
            private readonly GameDbContext _context;

            public GameRepository(GameDbContext context)
            {
                _context = context;
            }

            public async Task<Game> CreateAsync(Game game)
            {
                _context.Games.Add(game);
                await _context.SaveChangesAsync();
                return game;
            }

            public async Task<Game?> UpdateAsync(Game game)
            {
                var existingGame = await _context.Games.FindAsync(game.Id);
                if (existingGame == null) return null;

                existingGame.Name = game.Name;
                existingGame.Genre = game.Genre;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.Price = game.Price;

                await _context.SaveChangesAsync();
                return existingGame;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var game = await _context.Games.FindAsync(id);
                if (game == null) return false;

                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<Game?> GetByIdAsync(int id)
            {
                return await _context.Games.FindAsync(id);
            }

            public async Task<IEnumerable<Game>> GetAllAsync()
            {
                return await _context.Games.ToListAsync();
            }

            public async Task<IEnumerable<Game>> GetByGenreAsync(string genre)
            {
                return await _context.Games
                    .Where(g => g.Genre.ToLower() == genre.ToLower())
                    .ToListAsync();
            }
        }
    }

