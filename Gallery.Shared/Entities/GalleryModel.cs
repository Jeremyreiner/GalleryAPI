using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Shared.Entities
{
    [Table("GalleryModels")]

    public class GalleryModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public string FullName { get; set; }
        
        public string AvatarUrl { get; set; }

    }
}
