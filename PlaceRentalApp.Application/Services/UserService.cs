using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.Application.Exceptions;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
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

        public UserService(PlaceRentalDbContext context)
        {
            _context = context;
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
            var user = new User(model.FullName, model.Email, model.BirthDate);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }
    }
}
