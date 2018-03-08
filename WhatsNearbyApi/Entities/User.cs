using Microsoft.AspNetCore.Identity;

namespace WhatsNearbyApi.Entities
{
    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        //public string FingerPrintId { get; set; }
    }
}
