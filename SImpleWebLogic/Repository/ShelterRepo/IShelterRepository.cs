﻿using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SImpleWebLogic.Repository.ShelterRepo
{
    public interface IShelterRepository
    {
        //Get
        public bool SaveChange();
        public Task<IEnumerable<Shelter>> GetAllShelters();
        public Task<Shelter> GetShelterById(int shelterId); 
        public void UpdateShelter(Shelter shelter);
        public void DeleteShelter(int shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterPets(int shelterId);
        public Task<IEnumerable<Pet>> GetAllShelterDogs(int shelterId);
        public Task<Pet> GetShelterPetById(int shelterId, int petId);
        public Task<IEnumerable<User>> GetShelterWorkers(int shelterId);
        public Task<IEnumerable<User>> GetShelterContributors(int shelterId);
        public Task<IEnumerable<User>> GetShelterWorkersById(int shelterId, int workerId);




    }
}