using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Dormitories.Entities
{
    public class Announcements
    {
        public int Id { get; set; }
        public int? DormitoriesId { get; set; }
        public Dormitories? Dormitories { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsActive { get; set; }
        public List<Applications>? Applications { get; set; }
    }
}
