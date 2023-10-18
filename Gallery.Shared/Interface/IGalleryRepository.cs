using System.Linq.Expressions;
using Gallery.Shared.Entities;

namespace Gallery.Shared.Interface;

public interface IGalleryRepository
{
    Task<GalleryModel?> GetItem(Expression<Func<GalleryModel, bool>> predicate);

    Task<List<GalleryModel>> GetListByAsync(Guid userId);

    Task DeleteAsync(GalleryModel model);

    Task AddAsync(GalleryModel model);
}