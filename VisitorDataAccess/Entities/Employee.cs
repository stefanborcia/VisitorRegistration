

namespace VisitorDataAccess.Entities
{
    public class Employee : SoftDelete
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CompanyId { get; set; }

    }
}
