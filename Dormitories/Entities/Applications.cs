using System.ComponentModel.DataAnnotations.Schema;

namespace Dormitories.Entities
{
    public class Applications
    {
        public int Id { get; set; }
        public int StudentsId { get; set; }
        public Students Students { get; set; }
        public int AnnouncementsId { get; set; }
        public Announcements Announcements { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
