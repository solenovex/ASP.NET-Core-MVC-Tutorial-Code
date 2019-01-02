using System.Collections.Generic;
using System.Threading.Tasks;
using Heavy.Web.Data;
using Heavy.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Services
{
    public class AlbumEfService : IAlbumService
    {
        private readonly HeavyContext _context;

        public AlbumEfService(HeavyContext context)
        {
            _context = context;
        }

        public async Task<List<Album>> GetAllAsync()
        {
            return await _context.Albums.ToListAsync();
        }

        public Task<Album> GetByIdAsync(int id)
        {
            return _context.Albums.FindAsync(id);
        }

        public async Task<Album> AddAsync(Album model)
        {
            _context.Albums.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task UpdateAsync(Album model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Album model)
        {
            _context.Albums.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
