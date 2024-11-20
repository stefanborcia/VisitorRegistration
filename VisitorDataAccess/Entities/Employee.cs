

namespace VisitorDataAccess.Entities
{
    public class Employee : SoftDelete
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
