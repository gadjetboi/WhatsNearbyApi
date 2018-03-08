using WhatsNearbyApi.Models;
using System.Collections.Generic;

namespace WhatsNearbyApi.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<CustomUserDto> GetUsers();
        CustomUserDto GetUserByUsername(string username);
    }
}
