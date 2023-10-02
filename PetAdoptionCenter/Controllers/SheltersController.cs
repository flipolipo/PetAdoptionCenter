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
using SimpleWebDal.Models.WebUser.Enums;
using SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;

namespace PetAdoptionCenter.Controllers;


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

    //SHELTER 

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShelterReadDTO>>> GetAllShelters()
    {
        var shelters = await _shelterRepository.GetAllShelters();
        var sheltersDto = _mapper.Map<IEnumerable<ShelterReadDTO>>(shelters);
        if (sheltersDto != null)
        {
            return Ok(sheltersDto);
        }
        return BadRequest();
    }

    [HttpGet("{shelterId}", Name = "GetShelterById")]
    public async Task<ActionResult<ShelterReadDTO>> GetShelterById(Guid shelterId)
    {
        var shelter = await _shelterRepository.GetShelterById(shelterId);
        if (shelter == null)
        {
            return NotFound();
        }

        var shelterDto = _mapper.Map<ShelterReadDTO>(shelter);
        return Ok(shelterDto);
    }
    [HttpGet("{shelterId}/users")]
    public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetShelterUsers(Guid shelterId)
    {
        var users = await _shelterRepository.GetShelterUsers(shelterId);
        var usersDto = _mapper.Map<IEnumerable<UserReadDTO>>(users);
        if (usersDto != null)
        {
            return Ok(usersDto);
        }
        return NotFound();
    }
    [HttpGet("{shelterId}/users/{userId}")]
    public async Task<ActionResult<UserReadDTO>> GetShelterWorkerById(Guid shelterId, string userId)
    {
        var user = await _shelterRepository.GetShelterUserById(shelterId, userId);
        var userDto = _mapper.Map<UserReadDTO>(user);
        if (userDto != null)
        {
            return Ok(userDto);
        }
        return NotFound();
    }

    [HttpDelete("{shelterId}/activities/{activityId}")]
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

    [HttpDelete("{shelterId}/contributors/{contributorId}")]
    public async Task<IActionResult> DeleteUser(Guid shelterId, string userId)
    {
        bool deleted = await _shelterRepository.DeleteShelterUser(shelterId, userId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{shelterId}")]
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

    [HttpDelete("{shelterId}/pets/{petId}")]
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

    [HttpPut("{shelterId}/activities/{activityId}")]
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

    [HttpPut("{shelterId}")]
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
    [HttpPost("create"), ActionName(nameof(CreateShelter))]
    public async Task<ActionResult<ShelterReadDTO>> CreateShelter([FromBody] ShelterCreateDTO shelterCreateDTO)
    {

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
            var newShelter = await _shelterRepository.CreateShelter(shelter);

            var readDto = _mapper.Map<ShelterReadDTO>(shelter);
            return CreatedAtRoute(nameof(GetShelterById), new { shelterId = readDto.Id }, readDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Błąd podczas tworzenia schroniska: " + ex.Message);
        }
    }
    [HttpPost("{shelterId}/calendar/activities/create")]
    public async Task<ActionResult<ActivityReadDTO>> AddActivityToCalendar(Guid shelterId, ActivityCreateDTO activityCreateDTO)
    {

        //var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        //var validationResult = activityValidator.Validate(activityDto);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest(validationResult.Errors);
        //}
        var activity = _mapper.Map<Activity>(activityCreateDTO);
        try
        {
            await _shelterRepository.AddActivityToCalendar(shelterId, activity);
            return Ok(activity);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating an activity: " + ex.Message);
        }
    }

    [HttpPost("{shelterId}/users")]
    public async Task<ActionResult<UserReadDTO>> AddUser(Guid shelterId, string userId, RoleName role)
    {
        var foundUser = await _shelterRepository.FindUserById(userId);
        var userReadDto = _mapper.Map<UserReadDTO>(foundUser);
        var updated = await _shelterRepository.AddShelterUser(shelterId, userId, role);
        if (updated)
        {
            // var updatedWorker = await _shelterRepository.GetShelterUserById(shelterId, userId);
            return Ok(userReadDto);
        }

        return NotFound();
    }



    //PETS
    [HttpGet("{shelterId}/pets/type")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllShelterDogsOrCats(Guid shelterId, PetType type)
    {
        var pets = await _shelterRepository.GetAllShelterDogsOrCats(shelterId, type);
        if (pets != null)
        {
            return Ok(pets);
        }
        return BadRequest();
    }

    [HttpGet("{shelterId}/pets")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllShelterPets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllShelterPets(shelterId);
        var petsDto = _mapper.Map<IEnumerable<Pet>>(pets);
        if (pets != null)
        {
            return Ok(pets);
        }
        return BadRequest();
    }
    [HttpGet("{shelterId}/pets/{petId}", Name = "GetShelterPetById")]
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

    [HttpGet("{shelterId}/pets/adopted")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllAdoptedPets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllAdoptedPets(shelterId);
        var petsDto = _mapper.Map<IEnumerable<PetReadDTO>>(pets);
        if (petsDto != null)
        {
            return Ok(petsDto);
        }
        return BadRequest();
    }
    [HttpGet("{shelterId}/tempHouses")]
    public async Task<ActionResult<IEnumerable<TempHouseReadDTO>>> GetAllTempHouses(Guid shelterId)
    {
        var tempHouses = await _shelterRepository.GetAllTempHouses(shelterId);
        var temphousesDto = _mapper.Map<IEnumerable<TempHouseReadDTO>>(tempHouses);
        if (temphousesDto != null)
        {
            return Ok(temphousesDto);
        }
        return NotFound();
    }
    [HttpGet("{shelterId}/tempHouse/{temphouseId}")]
    public async Task<ActionResult<TempHouseReadDTO>> GetTempHouseById(Guid shelterId, Guid tempHouseId)
    {
        var tempHouse = await _shelterRepository.GetTempHouseById(shelterId, tempHouseId);
        var temphouseDto = _mapper.Map<UserReadDTO>(tempHouse);
        if (temphouseDto != null)
        {
            return Ok(temphouseDto);
        }
        return NotFound();
    }
    [HttpGet("{shelterId}/tempHouse/{tempHouseId}/pets")]
    public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterTempHousesPets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllShelterTempHousesPets(shelterId);
        var petsDto = _mapper.Map<IEnumerable<Pet>>(pets);
        if (petsDto != null)
        {
            return Ok(petsDto);
        }
        return NotFound();
    }
    [HttpGet("{shelterId}/tempHouse/{tempHouseId}/pets/{petId}")]
    public async Task<ActionResult<PetReadDTO>> GetTempHousePetById(Guid shelterId, Guid petId, Guid tempHouseId)
    {
        var pet = await _shelterRepository.GetTempHousePetById(shelterId, tempHouseId, petId);
        var petDto = _mapper.Map<UserReadDTO>(pet);
        if (petDto != null)
        {
            return Ok(petDto);
        }
        return NotFound();
    }

    [HttpDelete("{shelterId}/tempHouses/{tempHouseId}")]
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

    [HttpPut("{shelterId}/pets/{petId}")]
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

    [HttpPut("{shelterId}/pets/basicHealthInfo/{basicHelthInfoId}")]
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

    [HttpPost("{shelterId}/pets/create")]
    public async Task<ActionResult<PetReadDTO>> AddPet([FromBody] PetCreateDTO petCreateDTO, Guid shelterId)
    {

        var petValidator = _validatorFactory.GetValidator<PetCreateDTO>();
        var validationResult = petValidator.Validate(petCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var pet = _mapper.Map<Pet>(petCreateDTO);

        try
        {
            await _shelterRepository.AddPet(shelterId, pet);
            var map = _mapper.Map<PetReadDTO>(pet);

            return CreatedAtRoute(nameof(GetShelterPetById), new { shelterId = map.ShelterId, petId = map.Id }, map);
            //return Ok(map);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating a pet: " + ex.Message);
        }
    }

    [HttpPost("{shelterId}/temphouses/create")]
    public async Task<ActionResult<TempHouseReadDTO>> AddTempHouse(Guid shelterId, string userId, Guid petId, TempHouseCreateDTO tempHouseCreateDTO)
    {
        //var tempHouseDto = new TempHouseCreateDTO()
        //{
        //    StartOfTemporaryHouseDate = startDate
        //};
        //var tempHouseValidator = _validatorFactory.GetValidator<TempHouseCreateDTO>();
        //var validationResult = tempHouseValidator.Validate(tempHouseDto);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest(validationResult.Errors);
        //}
        var tempHouse = _mapper.Map<TempHouse>(tempHouseCreateDTO);
        try
        {
            await _shelterRepository.AddTempHouse(shelterId, userId, petId, tempHouse);
            var tempHouseReadDto = _mapper.Map<TempHouseReadDTO>(tempHouse);
            return Ok(tempHouseReadDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating an activity: " + ex.Message);
        }
    }
    [HttpGet("{shelterId}/calendar/activities")]
    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetAllActivities(Guid shelterId)
    {
        var activities = await _shelterRepository.GetAllActivities(shelterId);
        var activitiesDto = _mapper.Map<IEnumerable<ActivityReadDTO>>(activities);
        if (activitiesDto != null)
        {
            return Ok(activitiesDto);
        }
        return BadRequest();
    }
    [HttpGet("{shelterId}/calendar/activities/{activityId})")]
    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetActivityById(Guid shelterId, Guid activityId)
    {
        var activity = await _shelterRepository.GetActivityById(shelterId, activityId);
        var activityDto = _mapper.Map<ActivityReadDTO>(activity);
        if (activityDto != null)
        {
            return Ok(activityDto);
        }
        return BadRequest();
    }
    [HttpGet("{shelterId}/pets/{petId}/vaccinations/{vaccinationId}", Name = "GetPetVaccinationById")]
    public async Task<ActionResult<VaccinationReadDTO>> GetPetVaccinationById(Guid shelterId, Guid petId, Guid vaccinationId) 
    {
        var vaccination = await _shelterRepository.GetPetVaccinationById(shelterId, petId, vaccinationId);
        var vaccinationDTO = _mapper.Map<VaccinationReadDTO>(vaccination);
        if(vaccinationDTO != null) 
        {
            return Ok(vaccinationDTO);
        }
        return BadRequest();
    }

    [HttpPost("{shelterId}/pets/{petId}/vaccinations")]
    public async Task<ActionResult<VaccinationReadDTO>> AddVaccination(Guid shelterId, Guid petId, VaccinationCreateDTO vaccinationCreateDTO) 
    {
        var vaccination = _mapper.Map<Vaccination>(vaccinationCreateDTO);
        
            var addedVaccination = _shelterRepository.AddPetVaccination(shelterId, petId, vaccination);
            return CreatedAtRoute(nameof(GetPetVaccinationById), new {shelterId, petId, vaccinationId = addedVaccination.Id });
        
    }
    
}


