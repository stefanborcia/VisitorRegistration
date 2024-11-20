﻿

namespace VisitorDTOs
{
    public class SignInVisitorDTO
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Company { get; set; }

        public long VisitingCompanyId { get; set; }

        public long AppointmentWithId { get; set; }
        public SignInVisitorDTO()
        {
            Name = string.Empty; 
            Email = string.Empty;
        }
    }
}
