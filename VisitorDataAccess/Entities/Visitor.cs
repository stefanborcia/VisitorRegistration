
namespace VisitorDataAccess.Entities
{
    public class Visitor : SoftDelete
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Company { get; set; } 
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
