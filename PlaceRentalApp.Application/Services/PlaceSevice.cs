﻿using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.Application.Exceptions;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Core.Repositories;
using PlaceRentalApp.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceRentalApp.Application.Services
{
    public class PlaceSevice : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceSevice(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public ResultViewModel Book(int id, CreateBookInputModel model)
        {
            var place = _placeRepository.GetById(id);
            if (place is null)
            {
                return ResultViewModel.Error("Not Found");
            }

            var book = new PlaceBook(model.IdUser, model.IdPlace, model.StartDate, model.EndDate, model.Comments);

            _placeRepository.AddBook(book);

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var place = _placeRepository.GetById(id);
            if (place is null)
            {
                //throw new NotFoundException();
                return ResultViewModel.Error("Not Found");
            }

            place.SetAsDeleted();

            _placeRepository.Delete(place);

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<PlaceViewModel>> GetAllAvailable(string search, DateTime startDate, DateTime endDate)
        {
            var availablePlaces = _placeRepository.GetAllAvailable(search, startDate, endDate);

            var model = availablePlaces.Select(PlaceViewModel.FromEntity).ToList(); 

            return ResultViewModel<List<PlaceViewModel>>.Success(model);
        }

        public ResultViewModel<PlaceDetailsViewModel?> GetById(int id)
        {
            var place = _placeRepository.GetById(id);
            
            return place is null ? ResultViewModel<PlaceDetailsViewModel?>.Error("Not found") : ResultViewModel<PlaceDetailsViewModel?>.Success(PlaceDetailsViewModel.FromEntity(place));
        }

        public ResultViewModel<int> Insert(CreatePlaceInputModel model)
        {
            var address = new Address(
                model.Address.Street,
                model.Address.Number,
                model.Address.ZipCode,
                model.Address.District,
                model.Address.City,
                model.Address.State,
                model.Address.Country
                );

            var place = new Place(
                model.Title,
                model.Description,
                model.DailyPrice,
                address,
                model.AllowedNumberPerson,
                model.AllowPets,
                model.CreatedBy
                );

            _placeRepository.Add( place );

            return ResultViewModel<int>.Success(place.Id);
        }

        public ResultViewModel InsertAmenity(int id, CreatePlaceAmenityInputModel model)
        {
            var place = _placeRepository.GetById(id);
            if (place is null)
            {
                //throw new NotFoundException();
                return ResultViewModel.Error("Not Found");
            }

            var amenity = new PlaceAmenity(model.Description, id);
            _placeRepository.AddAmenity(amenity);

            return ResultViewModel.Success();
        }

        public ResultViewModel Update(int id, UpdatePlaceInputModel model)
        {
            var place = _placeRepository.GetById(id);
            if (place is null)
            {
                //throw new NotFoundException();
                return ResultViewModel.Error("Not Found");
            }

            place.Update(model.Title, model.Description, model.DailyPrice);
            _placeRepository.Update(place);

            return ResultViewModel.Success();
        }
    }
}
