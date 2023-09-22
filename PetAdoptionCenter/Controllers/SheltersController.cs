using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using SImpleWebLogic.Configuration;
using SImpleWebLogic.Repository.ShelterRepo;
using SImpleWebLogic.Validations.ShelterCreateDTOValidation;

namespace PetAdoptionCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheltersController : ControllerBase
    {
        private readonly IShelterRepository _shelterRepo;
        private readonly IMapper _mapper;
        private readonly ValidatorFactory _validatorFactory;


        public SheltersController(IShelterRepository shelterRepo, IMapper mapper, ValidatorFactory validatorFactory)
        {
            _shelterRepo = shelterRepo;
            _mapper = mapper;
            _validatorFactory = validatorFactory;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelter>>> GetAllShelters()
        {
            var foundShelters = await _shelterRepo.GetAllShelters();
            var sheltersDTO = _mapper.Map<IEnumerable<ShelterReadDTO>>(foundShelters);

            if (sheltersDTO != null)
            {
                return Ok(sheltersDTO);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Shelter>> CreateShelter(string name, string description, string street, string houseNumber, string postalCode, string city)
        {
            var shelterCreateDTO = new ShelterCreateDTO()
            {
                Name = name,
                ShelterCalendar = new CalendarActivityCreateDTO()
                {
                    DateWithTime = DateTime.UtcNow
                },
                ShelterDescription = description,
                ShelterAddress = new AddressCreateDTO()
                {
                    City = city,
                    Street = street,
                    HouseNumber = houseNumber,
                    PostalCode = postalCode
                },
            };

            var shelterValidator = _validatorFactory.GetValidator<ShelterCreateDTO>();
            var shelterAddress = _validatorFactory.GetValidator<AddressCreateDTO>();
            var validationResult = shelterValidator.Validate(shelterCreateDTO);
            var validationResultAddress = shelterAddress.Validate(shelterCreateDTO.ShelterAddress);


            if (!validationResult.IsValid || !validationResultAddress.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var shelter = _mapper.Map<Shelter>(shelterCreateDTO);

            try
            {
                
                await _shelterRepo.CreateShelter(name, description, street, houseNumber, postalCode, city);
                return Ok(shelter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd podczas tworzenia schroniska: " + ex.Message);
            }
        }

    }
}
