using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceRentalApp.Infraestructure.Auth
{
    public interface IAuthService
    {
        string ComputerHash(string password);
        string GenerateToken(string email, string role);
    }
}
