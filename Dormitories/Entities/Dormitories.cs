namespace Dormitories.Entities
{
    public class Dormitories
    {
        public int Id { get; set; }

        public string Code {  get; set; }
        public string Name { get; set; }
        public int CurrentCapacity { get; set; }
        public int MaxCapacity { get; set; }

        public List<Students> StudentList = new List<Students>();
    }
}
