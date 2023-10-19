using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.TemporaryHouse;
using SImpleWebLogic.Configuration;
using SImpleWebLogic.Repository.ShelterRepo;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SimpleWebDal.Models.WebUser.Enums;
using SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.DiseaseDTOs;
using SimpleWebDal.DTOs.AdoptionDTOs;

namespace PetAdoptionCenter.Controllers;


[ApiController]
[Route("[controller]")]
public class SheltersController : ControllerBase
{

    private IShelterRepository _shelterRepository;
    private IMapper _mapper;
    private readonly ValidatorFactory _validatorFactory;
    private readonly ILogger<SheltersController> _logger;

    public SheltersController(IShelterRepository shelterRepository, IMapper mapper, ValidatorFactory validatorFactory, ILogger<SheltersController> logger)
    {
        _shelterRepository = shelterRepository;
        _mapper = mapper;
        _validatorFactory = validatorFactory;
        _logger = logger;

    }

    #region //SHELTERS 
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/users/{userId}")]
    public async Task<ActionResult<UserReadDTO>> GetShelterWorkerById(Guid shelterId, Guid userId)
    {
        var user = await _shelterRepository.GetShelterUserById(shelterId, userId);
        var userDto = _mapper.Map<UserReadDTO>(user);
        if (userDto != null)
        {
            return Ok(userDto);
        }
        return NotFound();
    }
    
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{shelterId}/contributors/{contributorId}")]
    public async Task<IActionResult> DeleteUser(Guid shelterId, Guid userId)
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost, ActionName(nameof(CreateShelter))]
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

            var newShelter = await _shelterRepository.CreateShelter(shelter);

            var readDto = _mapper.Map<ShelterReadDTO>(shelter);
            return CreatedAtRoute(nameof(GetShelterById), new { shelterId = readDto.Id }, readDto);

       
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/calendar/activities")]
    public async Task<ActionResult<ActivityReadDTO>> AddActivityToCalendar(Guid shelterId, ActivityCreateDTO activityCreateDTO)
    {
        var activity = _mapper.Map<Activity>(activityCreateDTO);
            await _shelterRepository.AddActivityToCalendar(shelterId, activity);
            return Ok(activity);
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/users")]
    public async Task<ActionResult<UserReadDTO>> AddUser(Guid shelterId, Guid userId, RoleName role)
    {
        var foundUser = await _shelterRepository.FindUserById(userId);
        var userReadDto = _mapper.Map<UserReadDTO>(foundUser);
        var updated = await _shelterRepository.AddShelterUser(shelterId, userId, role);
        if (updated)
        {
            return Ok(userReadDto);
        }
        return NotFound();
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/temphouses")]
    public async Task<ActionResult<TempHouseReadDTO>> AddTempHouse(Guid shelterId, Guid userId, Guid petId, TempHouseCreateDTO tempHouseCreateDTO)
    {
        var tempHouse = _mapper.Map<TempHouse>(tempHouseCreateDTO);

        var addedTemphouse = await _shelterRepository.AddTempHouse(shelterId, userId, petId, tempHouse);
        var tempHouseReadDto = _mapper.Map<TempHouseReadDTO>(addedTemphouse);
        return CreatedAtRoute(nameof(GetTempHouseById), new { shelterId, petId }, tempHouseReadDto);

    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{shelterId}/adoptions")]
    public async Task<ActionResult<IEnumerable<AdoptionReadDTO>>> GetAllAdoptions(Guid shelterId)
    {
        var adoptions = await _shelterRepository.GetAllShelterAdoptions(shelterId);
        var adoptionsDTO = _mapper.Map<AdoptionReadDTO>(adoptions);
        if (adoptionsDTO != null) { return Ok(adoptionsDTO); }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/adoptions/{adoptionId}")]
    public async Task<ActionResult<AdoptionReadDTO>> GetAdoptionById(Guid shelterId, Guid adoptionId)
    {
        var adoption = await _shelterRepository.GetShelterAdoptionById(shelterId, adoptionId);
        var adoptionDTO = _mapper.Map<AdoptionReadDTO>(adoption);
        if (adoptionDTO != null)
        { return Ok(adoptionDTO); }
        return BadRequest();

    }
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/tempHouse/{temphouseId}", Name = "GetTempHouseById")]
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
    #endregion


    #region //PETS
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{shelterId}/pets")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllShelterPets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllShelterPets(shelterId);
        var petsDto = _mapper.Map<IEnumerable<PetReadDTO>>(pets);
        var updatedPetsDto = petsDto.Select(petDto =>
        {
            var matchingPet = pets.FirstOrDefault(pet => pet.Id == petDto.Id);
            if (matchingPet != null)
            {
                petDto.ImageBase64 = Convert.ToBase64String(matchingPet.Image);
            }
            return petDto;
        }).ToList();

        if (updatedPetsDto != null)
        {
            return Ok(updatedPetsDto);
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/pets/{petId}", Name = "GetShelterPetById")]
    public async Task<ActionResult<Pet>> GetShelterPetById(Guid shelterId, Guid petId)
    {
        var pet = await _shelterRepository.GetShelterPetById(shelterId, petId);
        var petDto = _mapper.Map<PetReadDTO>(pet);
        petDto.ImageBase64 = Convert.ToBase64String(pet.Image);
        if (petDto != null)
        {
            return Ok(petDto);
        }
        return NotFound();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{shelterId}/pets/adopted")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllAdoptedPets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllAdoptedPets(shelterId);
        var petsDto = _mapper.Map<IEnumerable<PetReadDTO>>(pets);
        var updatedPetsDto = petsDto.Select(petDto =>
        {
            var matchingPet = pets.FirstOrDefault(pet => pet.Id == petDto.Id);
            if (matchingPet != null)
            {
                petDto.ImageBase64 = Convert.ToBase64String(matchingPet.Image);
            }
            return petDto;
        }).ToList();

        if (updatedPetsDto != null)
        {
            return Ok(updatedPetsDto);
        }
        return BadRequest();
    }
    
   
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{shelterId}/tempHouse/{tempHouseId}/pets")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllShelterTempHousesPets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllShelterTempHousesPets(shelterId);
        var petsDto = _mapper.Map<IEnumerable<PetReadDTO>>(pets);
        var updatedPetsDto = petsDto.Select(petDto =>
        {
            var matchingPet = pets.FirstOrDefault(pet => pet.Id == petDto.Id);
            if (matchingPet != null)
            {
                petDto.ImageBase64 = Convert.ToBase64String(matchingPet.Image);
            }
            return petDto;
        }).ToList();

        if (updatedPetsDto != null)
        {
            return Ok(updatedPetsDto);
        }
        return NotFound();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/tempHouse/{tempHouseId}/pets/{petId}")]
    public async Task<ActionResult<PetReadDTO>> GetTempHousePetById(Guid shelterId, Guid petId, Guid tempHouseId)
    {
        var pet = await _shelterRepository.GetTempHousePetById(shelterId, tempHouseId, petId);
        var petDto = _mapper.Map<PetReadDTO>(pet);
        petDto.ImageBase64 = Convert.ToBase64String(pet.Image);
        if (petDto != null)
        {
            return Ok(petDto);
        }
        return NotFound();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}/pets/{petId}")]
    public async Task<IActionResult> UpdateShelterPet(Guid shelterId, Guid petId, PetGender gender, PetType type, string description, PetStatus status, bool avaibleForAdoption)
    {
        bool updated = await _shelterRepository.UpdateShelterPet(shelterId, petId, gender, type, description, status, avaibleForAdoption);

        if (updated)
        {
            var updatedPet = await _shelterRepository.GetShelterActivityById(shelterId, petId);
            return Ok(updatedPet);
        }

        return NotFound();
    }
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}/pets/basicHealthInfo/{basicHelthInfoId}")]
    public async Task<IActionResult> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred)
    {
        bool updated = await _shelterRepository.UpdatePetBasicHealthInfo(shelterId, petId, name, age, size, isNeutred);

        if (updated)
        {
            var updatedPet = await _shelterRepository.GetShelterPetById(shelterId, petId);
            return Ok(updatedPet);
        }

        return NotFound();
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/pets")]
    public async Task<ActionResult<PetReadDTO>> AddPet([FromForm] PetCreateDTO petCreateDTO, Guid shelterId)
    {

        var petValidator = _validatorFactory.GetValidator<PetCreateDTO>();
        var validationResult = petValidator.Validate(petCreateDTO);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var pet = _mapper.Map<Pet>(petCreateDTO);
       
        if (petCreateDTO.ImageFile != null && petCreateDTO.ImageFile.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await petCreateDTO.ImageFile.CopyToAsync(memoryStream);
            pet.Image = memoryStream.ToArray();
        }
            await _shelterRepository.AddPet(shelterId, pet);
            var map = _mapper.Map<PetReadDTO>(pet);
            return CreatedAtRoute(nameof(GetShelterPetById), new { shelterId = map.ShelterId, petId = map.Id }, map);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/pets/{petId}/vaccinations/{vaccinationId}", Name = "GetPetVaccinationById")]
    public async Task<ActionResult<VaccinationReadDTO>> GetPetVaccinationById(Guid shelterId, Guid petId, Guid vaccinationId)
    {
        var vaccination = await _shelterRepository.GetPetVaccinationById(shelterId, petId, vaccinationId);
        var vaccinationDTO = _mapper.Map<VaccinationReadDTO>(vaccination);
        if (vaccinationDTO != null)
        {
            return Ok(vaccinationDTO);
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/pets/{petId}/vaccinations")]
    public async Task<ActionResult<VaccinationReadDTO>> AddVaccination(Guid shelterId, Guid petId, VaccinationCreateDTO vaccinationCreateDTO)
    {
        var vaccination = _mapper.Map<Vaccination>(vaccinationCreateDTO);

        var addedVaccination = await _shelterRepository.AddPetVaccination(shelterId, petId, vaccination);
        return CreatedAtRoute(nameof(GetPetVaccinationById), new { shelterId, petId, vaccinationId = addedVaccination.Id });

    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/pets/{petId}/diseases/{diseaseId}", Name = "GetPetDiseaseById")]
    public async Task<ActionResult<DiseaseReadDTO>> GetPetDiseaseById(Guid shelterId, Guid petId, Guid diseaseId)
    {
        var disease = await _shelterRepository.GetPetDiseaseById(shelterId, petId, diseaseId);
        var diseaseDTO = _mapper.Map<DiseaseReadDTO>(disease);
        if (diseaseDTO != null)
        {
            return Ok(diseaseDTO);
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/pets/{petId}/diseases")]
    public async Task<ActionResult<DiseaseReadDTO>> AddDisease(Guid shelterId, Guid petId, DiseaseCreateDTO diseaseCreateDTO)
    {
        var disease = _mapper.Map<Disease>(diseaseCreateDTO);

        var addedDisease = await _shelterRepository.AddPetDisease(shelterId, petId, disease);
        return CreatedAtRoute(nameof(GetPetDiseaseById), new { shelterId, petId, diseaseId = addedDisease.Id });

    }
    #endregion
}


