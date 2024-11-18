﻿

namespace VisitorDataAccess.Entities
{
    public class Company : SoftDelete
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
