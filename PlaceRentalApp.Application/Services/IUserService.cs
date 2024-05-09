using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceRentalApp.Application.Services
{
    public interface IUserService
    {
        User? GetById(int id);
        int Insert(CreateUserInputModel model);

        ResultViewModel<LoginViewModel?> Login(loginInputModel model);
    }
}
