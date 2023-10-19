using Gallery.DataBase.Infrastructure.MySql;
using Gallery.Shared.Entities;
using System.Linq.Expressions;
using Gallery.Shared.Interface;
using Microsoft.EntityFrameworkCore;

namespace Gallery.DataBase.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        readonly ApplicationDbContext _DbContext;

        public GalleryRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddAsync(GalleryModel model)
        {
            await _DbContext.GalleryItems.AddAsync(model);

            await _DbContext.SaveChangesAsync();
        }

        public async Task<GalleryModel?> GetItem(Expression<Func<GalleryModel, bool>> predicate) =>
            await _DbContext.GalleryItems.FirstOrDefaultAsync(predicate);

        public async Task<List<GalleryModel>> GetListByAsync(Guid userId)
        {
            var items = await _DbContext.GalleryItems
                .Where(g => g.UserId == userId)
                .ToListAsync();

            return items.Any() 
                ? items 
                : Enumerable.Empty<GalleryModel>().ToList();
        }

        public async Task DeleteAsync(GalleryModel model)
        {
            _DbContext.GalleryItems.Remove(model);

            await _DbContext.SaveChangesAsync();
        }
    }
}
