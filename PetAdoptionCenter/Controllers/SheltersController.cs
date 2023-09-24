using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.WebUser;
using SImpleWebLogic.Configuration;
using SImpleWebLogic.Repository.ShelterRepo;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;

namespace PetAdoptionCenter.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SheltersController : ControllerBase
    {

        private IShelterRepository _shelterRepository;
        private IMapper _mapper;
        private readonly ValidatorFactory _validatorFactory;

        public SheltersController(IShelterRepository shelterRepository, IMapper mapper, ValidatorFactory validatorFactory)
        {
            _shelterRepository = shelterRepository;
            _mapper = mapper;
            _validatorFactory = validatorFactory;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelter>>> GetAllShelters()
        {
            var shelters = await _shelterRepository.GetAllShelters();
            var sheltersDto = _mapper.Map<IEnumerable<ShelterReadDTO>>(shelters);
            if (sheltersDto != null)
            {
                return Ok(sheltersDto);
            }
            return BadRequest();
        }
        [HttpGet("{id}", Name = "GetShelterById")]
        public async Task<ActionResult<Shelter>> GetShelterById(Guid shelterId)
        {
            var shelter = await _shelterRepository.GetShelterById(shelterId);
            if (shelter != null)
            {
                return Ok(shelter);
            }
            return BadRequest();
        }
        [HttpGet("{id}/pets")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterDogsOrCats(Guid shelterId, PetType type)
        {
            var pets = await _shelterRepository.GetAllShelterDogsOrCats(shelterId, type);
            if (pets != null)
            {
                return Ok(pets);
            }
            return BadRequest();
        }
        [HttpGet("{id}/pets/type")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterPets(Guid shelterId)
        {
            var pets = await _shelterRepository.GetAllShelterPets(shelterId);
            if (pets != null)
            {
                return Ok(pets);
            }
            return BadRequest();
        }
        [HttpGet("{id}/pets/{petId}")]
        public async Task<ActionResult<Pet>> GetShelterPetById(Guid shelterId, Guid petId)
        {
            var pet = await _shelterRepository.GetShelterPetById(shelterId, petId);
            var petDto = _mapper.Map<PetReadDTO>(pet);
            if (petDto != null)
            {
                return Ok(petDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/users/workers")]
        public async Task<ActionResult<IEnumerable<User>>> GetShelterWorkers(Guid shelterId)
        {
            var workers = await _shelterRepository.GetShelterWorkers(shelterId);
            var workersDto = _mapper.Map<UserReadDTO>(workers);
            if (workersDto != null)
            {
                return Ok(workersDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/users/workers/{workerId}")]
        public async Task<ActionResult<User>> GetShelterWorkerById(Guid shelterId, Guid workerId)
        {
            var worker = await _shelterRepository.GetShelterWorkerById(shelterId, workerId);
            var workerDto = _mapper.Map<UserReadDTO>(worker);
            if (workerDto != null)
            {
                return Ok(workerDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/users/contributors")]
        public async Task<ActionResult<IEnumerable<User>>> GetShelterContributors(Guid shelterId)
        {
            var contributors = await _shelterRepository.GetShelterContributors(shelterId);
            var contributorsDTO = _mapper.Map<UserReadDTO>(contributors);
            if (contributorsDTO != null)
            {
                return Ok(contributorsDTO);
            }
            return NotFound();
        }
        [HttpGet("{id}/users/contributors/{contributorId}")]
        public async Task<ActionResult<User>> GetShelterContributorById(Guid shelterId, Guid contributorId)
        {
            var contributor = await _shelterRepository.GetShelterContributorById(shelterId, contributorId);
            var contributorDto = _mapper.Map<UserReadDTO>(contributor);
            if (contributorDto != null)
            {
                return Ok(contributorDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/pets/adopted")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllAdoptedPets(Guid shelterId)
        {
            var pets = await _shelterRepository.GetAllAdoptedPets(shelterId);
            var petsDto = _mapper.Map<PetReadDTO>(pets);
            if (petsDto != null)
            {
                return Ok(petsDto);
            }
            return BadRequest();
        }
        [HttpGet("{id}/tempHouses")]
        public async Task<ActionResult<IEnumerable<TempHouse>>> GetAllTempHouses(Guid shelterId)
        {
            var tempHouses = await _shelterRepository.GetAllTempHouses(shelterId);
            var temphousesDto = _mapper.Map<UserReadDTO>(tempHouses);
            if (temphousesDto != null)
            {
                return Ok(temphousesDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/tempHouse/{temphouseId}")]
        public async Task<ActionResult<TempHouse>> GetTempHouseById(Guid shelterId, Guid tempHouseId)
        {
            var tempHouse = await _shelterRepository.GetTempHouseById(shelterId, tempHouseId);
            var temphouseDto = _mapper.Map<UserReadDTO>(tempHouse);
            if (temphouseDto != null)
            {
                return Ok(temphouseDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/tempHouse/{tempHouseId}/pets")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterTempHousesPets(Guid shelterId)
        {
            var pets = await _shelterRepository.GetAllShelterTempHousesPets(shelterId);
            var petsDto = _mapper.Map<UserReadDTO>(pets);
            if (petsDto != null)
            {
                return Ok(petsDto);
            }
            return NotFound();
        }
        [HttpGet("{id}/tempHouse/{tempHouseId}/pets/{petId}")]
        public async Task<ActionResult<Pet>> GetTempHousePetById(Guid shelterId, Guid petId, Guid tempHouseId)
        {
            var pet = await _shelterRepository.GetTempHousePetById(shelterId, tempHouseId, petId);
            var petDto = _mapper.Map<UserReadDTO>(pet);
            if (petDto != null)
            {
                return Ok(petDto);
            }
            return NotFound();
        }
        [HttpDelete("{id}/activities/{activityId}")]
        public async Task<IActionResult> DeleteActivity(Guid shelterId, Guid activityId)
        {
            bool deleted = await _shelterRepository.DeleteActivity(shelterId, activityId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/contributors/{contributorId}")]
        public async Task<IActionResult> DeleteContributor(Guid shelterId, Guid userId)
        {
            bool deleted = await _shelterRepository.DeleteContributor(shelterId, userId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelter(Guid shelterId)
        {
            bool deleted = await _shelterRepository.DeleteShelter(shelterId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/pets/{petId}")]
        public async Task<IActionResult> DeleteShelterPet(Guid shelterId, Guid petId)
        {
            bool deleted = await _shelterRepository.DeleteShelterPet(shelterId, petId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/tempHouses/{tempHouseId}")]
        public async Task<IActionResult> DeleteTempHouse(Guid shelterId, Guid tempHouseId)
        {
            bool deleted = await _shelterRepository.DeleteTempHouse(tempHouseId, shelterId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/workers/{userId}")]
        public async Task<IActionResult> DeleteWorker(Guid shelterId, Guid userId)
        {
            bool deleted = await _shelterRepository.DeleteWorker(shelterId, userId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}/activities/{activityId}")]
        public async Task<IActionResult> UpdateActivity(Guid shelterId, Guid activityId, string name, DateTime date)
        {
            bool updated = await _shelterRepository.UpdateActivity(shelterId, activityId, name, date);

            if (updated)
            {
                var updatedActivity = await _shelterRepository.GetShelterActivityById(shelterId, activityId);
                return Ok(updatedActivity);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city)
        {
            bool updated = await _shelterRepository.UpdateShelter(shelterId, name, description, street, houseNumber, postalCode, city);

            if (updated)
            {
                var updatedShelter = await _shelterRepository.GetShelterById(shelterId);
                return Ok(updatedShelter);
            }

            return NotFound();
        }

        [HttpPut("{id}/pets/{petId}")]
        public async Task<IActionResult> UpdateShelterPet(Guid shelterId, Guid petId, PetType type, string description, PetStatus status, bool avaibleForAdoption)
        {
            bool updated = await _shelterRepository.UpdateShelterPet(shelterId, petId, type, description, status, avaibleForAdoption);

            if (updated)
            {
                var updatedPet = await _shelterRepository.GetShelterActivityById(shelterId, petId);
                return Ok(updatedPet);
            }

            return NotFound();
        }

        [HttpPut("{id}/pets/basicHealthInfo/{basicHelthInfoId}")]
        public async Task<IActionResult> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size)
        {
            bool updated = await _shelterRepository.UpdatePetBasicHealthInfo(shelterId, petId, name, age, size);

            if (updated)
            {
                var updatedPet = await _shelterRepository.GetShelterPetById(shelterId, petId);
                return Ok(updatedPet);
            }

            return NotFound();
        }
        [HttpPost("create")]
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
                await _shelterRepository.CreateShelter(name, description, street, houseNumber, postalCode, city);
                return Ok(shelter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd podczas tworzenia schroniska: " + ex.Message);
            }
        }
        [HttpPost("{id}/pets/create")]
        public async Task<ActionResult<Pet>> AddPet(Guid shelterId, PetType type, string description, PetStatus status, bool avaibleForAdoption)
        {
            var petDto = new PetCreateDTO()
            {
                Id = Guid.NewGuid(),
                Type = type,
                Description = description,
                Status = status,
                AvaibleForAdoption = avaibleForAdoption
            };
            var petValidator = _validatorFactory.GetValidator<PetCreateDTO>();
            var validationResult = petValidator.Validate(petDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var pet = _mapper.Map<Pet>(petDto);
            try
            {
                await _shelterRepository.AddPet(shelterId, type, description, status, avaibleForAdoption);
                return Ok(pet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating a pet: " + ex.Message);
            }
        }
        [HttpPost("{id}/calendar/activities/create")]
        public async Task<ActionResult<Activity>> AddActivityToCalendar(Guid shelterId, string activityName, DateTime activityDate)
        {
            var activityDto = new ActivityCreateDTO()
            {
                ActivityDate = activityDate,
                Name = activityName
            };
            var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
            var validationResult = activityValidator.Validate(activityDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var activity = _mapper.Map<Pet>(activityDto);
            try
            {
                await _shelterRepository.AddActivityToCalendar(shelterId, activityName, activityDate);
                return Ok(activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating an activity: " + ex.Message);
            }
        }
        [HttpPost("{id}/temphouses/create")]
        public async Task<ActionResult<TempHouse>> AddTempHouse(Guid shelterId, Guid userId, DateTime startDate) 
        {
            var tempHouseDto = new TempHouseCreateDTO()
            {
                StartOfTemporaryHouseDate = startDate
            };
            var tempHouseValidator = _validatorFactory.GetValidator<TempHouseCreateDTO>();
            var validationResult = tempHouseValidator.Validate(tempHouseDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var tempHouse = _mapper.Map<Pet>(tempHouseDto);
            try
            {
                await _shelterRepository.AddTempHouse(shelterId, userId, startDate);
                return Ok(tempHouse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating an activity: " + ex.Message);
            }
        }
        [HttpPut("{id}/users/{workerId}")]
        public async Task<IActionResult> AddWorker(Guid shelterId, Guid userId) 
        {
            var updated = await _shelterRepository.AddWorker(shelterId, userId);
            if (updated)
            {
                var updatedWorker = await _shelterRepository.GetShelterWorkerById(shelterId, userId);
                return Ok(updatedWorker);
            }

            return NotFound();
        }
        [HttpPut("{id}/users/{contributorId}")]
        public async Task<IActionResult> AddContributor(Guid shelterId, Guid userId)
        {
            var updated = await _shelterRepository.AddContributor(shelterId, userId);
            if (updated)
            {
                var updatedWorker = await _shelterRepository.GetShelterContributorById(shelterId, userId);
                return Ok(updatedWorker);
            }

            return NotFound();
        }
    }


}

