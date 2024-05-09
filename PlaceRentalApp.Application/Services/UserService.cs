using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.Application.Exceptions;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Infraestructure.Auth;
using PlaceRentalApp.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceRentalApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly PlaceRentalDbContext _context;
        private readonly IAuthService _authService;

        public UserService(PlaceRentalDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public User? GetById(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user is null)
            {
                throw new NotFoundException();
            }

            return user;
        }

        public int Insert(CreateUserInputModel model)
        {
            var hash = _authService.ComputerHash(model.Password);
            var user = new User(model.FullName, model.Email, model.BirthDate, hash, model.Role);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public ResultViewModel<LoginViewModel?> Login(loginInputModel model)
        {
            var hash = _authService.ComputerHash(model.Password);

            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == hash);

            if(user is null)
            {
                return ResultViewModel<LoginViewModel?>.Error("Not found");
            }

            var token = _authService.GenerateToken(user.Email, user.Role);
            var viewModel = new LoginViewModel(token);

            return ResultViewModel<LoginViewModel?>.Success(viewModel);
        }
    }
}
