using System.ComponentModel.DataAnnotations;

namespace ZincTicket.Api.Models
{
    /// <summary>Anyone who logs into the system (supported user, staff, owners, managers, etc.).</summary>
    public class User
    {
        public long Id { get; set; }
        
    }
}