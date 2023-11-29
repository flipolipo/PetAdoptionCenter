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
using SimpleWebDal.DTOs.TemporaryHouseDTOs;
using SimpleWebDal.DTOs.AdoptionDTOs;
using SimpleWebDal.Models.AdoptionProccess;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.Models.WebUser;
using System.Data;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.VaccinationDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.DiseaseDTOs;
using SimpleWebDal.DTOs.AnimalDTOs.BasicHealthInfoDTOs;

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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShelterReadDTO>>> GetAllShelters()
    {
        var shelters = await _shelterRepository.GetAllShelters();
        var sheltersDto = _mapper.Map<IEnumerable<ShelterReadDTO>>(shelters);
        var updatedSheltersDto = sheltersDto.Select(shelterDto =>
        {
            var matchingshelter = shelters.FirstOrDefault(shelter => shelter.Id == shelterDto.Id);
            if (matchingshelter != null)
            {
                shelterDto.ImageBase64 = Convert.ToBase64String(matchingshelter.Image);
            }
            return shelterDto;
        }).ToList();
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
        shelterDto.ImageBase64 = Convert.ToBase64String(shelter.Image);
        return Ok(shelterDto);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost, ActionName(nameof(CreateShelter))]
    public async Task<ActionResult<ShelterReadDTO>> CreateShelter([FromForm] ShelterCreateDTO shelterCreateDTO)
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
        if (shelterCreateDTO.ImageFile != null && shelterCreateDTO.ImageFile.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await shelterCreateDTO.ImageFile.CopyToAsync(memoryStream);
            shelter.Image = memoryStream.ToArray();
        }


        var newShelter = await _shelterRepository.CreateShelter(shelter);

        var readDto = _mapper.Map<ShelterReadDTO>(shelter);
        return CreatedAtRoute(nameof(GetShelterById), new { shelterId = readDto.Id }, readDto);


    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}")]
    public async Task<IActionResult> UpdateShelter(Guid shelterId, string name, string description, string street, string houseNumber, string postalCode, string city, string phone, string bankNumber, IFormFile image)
    {
        bool updated = await _shelterRepository.UpdateShelter(shelterId, name, description, street, houseNumber, postalCode, city, phone, bankNumber, image);
        
        if (updated)
        {
            var updatedShelter = await _shelterRepository.GetShelterById(shelterId);
            return Ok(updatedShelter);
        }

        return NotFound();
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

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{shelterId}/calendar/activities")]
    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetAllActivities(Guid shelterId)
    {
        var activities = await _shelterRepository.GetShelterActivities(shelterId);
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
        var activity = await _shelterRepository.GetShelterActivityById(shelterId, activityId);  
        var activityDto = _mapper.Map<ActivityReadDTO>(activity);
        if (activityDto != null)
        {
            return Ok(activityDto);
        }
        return BadRequest();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/calendar/activities")]
    public async Task<ActionResult<ActivityReadDTO>> AddActivityToCalendar(Guid shelterId, ActivityCreateDTO activityCreateDTO)
    {
        var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        var validationResult = activityValidator.Validate(activityCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var activity = _mapper.Map<Activity>(activityCreateDTO);
        await _shelterRepository.AddActivityToCalendar(shelterId, activity);
        return Ok(activity);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}/activities/{activityId}")]
    public async Task<IActionResult> UpdateActivity(Guid shelterId, Guid activityId, ActivityCreateDTO activityCreateDTO)
    {
        var foundShelter = await _shelterRepository.GetShelterById(shelterId);
        var foundActivity = await _shelterRepository.GetShelterActivityById(shelterId, activityId);
        if (foundShelter == null || foundActivity == null)
        {
            return NotFound();
        }
        var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        var validationResult = activityValidator.Validate(activityCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }
        _mapper.Map(activityCreateDTO, foundActivity);
        bool updated = await _shelterRepository.UpdateShelterActivity(shelterId, foundActivity);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
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
    public async Task<ActionResult<UserReadDTO>> GetShelterUserById(Guid shelterId, Guid userId)
    {
        var user = await _shelterRepository.GetShelterUserById(shelterId, userId);
        var userDto = _mapper.Map<UserReadDTO>(user);
        if (userDto != null)
        {
            return Ok(userDto);
        }
        return NotFound();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/users")]
    public async Task<ActionResult<UserReadDTO>> AddUser(Guid shelterId, [FromQuery] Guid userId, RoleCreateDTO roleCreateDTO)
    {
        var foundUser = await _shelterRepository.FindUserById(userId);
        var roleModel = _mapper.Map<Role>(roleCreateDTO);
        var updated = await _shelterRepository.AddShelterUser(shelterId, userId, roleModel);
        var userReadDto = _mapper.Map<UserReadDTO>(foundUser);
        return Ok(userReadDto);
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{shelterId}/users/{userId}")]
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
    public async Task<ActionResult<PetReadDTO>> GetShelterPetById(Guid shelterId, Guid petId)
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

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/pets")]
    public async Task<ActionResult<PetReadDTO>> AddPet([FromForm] PetCreateDTO petCreateDTO, Guid shelterId)
    {
        var pet = _mapper.Map<Pet>(petCreateDTO);

        var petValidator = _validatorFactory.GetValidator<PetCreateDTO>();
        var validationResult = petValidator.Validate(petCreateDTO);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

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

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}/pets/{petId}")]
    public async Task<IActionResult> UpdateShelterPet(Guid shelterId, Guid petId, PetGender gender, PetType type, string description, PetStatus status, bool avaibleForAdoption, IFormFile image)
    {
        bool updated = await _shelterRepository.UpdateShelterPet(shelterId, petId, gender, type, description, status, avaibleForAdoption, image);

        if (updated)
        {
            var updatedPet = await _shelterRepository.GetShelterActivityById(shelterId, petId);
            return Ok(updatedPet);
        }

        return NotFound();
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/pets/{petId}/basicHealthInfo")]
    public async Task<ActionResult<BasicHealthInfoReadDTO>> GetPetBasicHealthInfo(Guid shelterId, Guid petId)
    {
        var basicHealthInfo = await _shelterRepository.GetPetBasicHealthInfo(shelterId, petId);
        var basicHealthInfoDTO = _mapper.Map<BasicHealthInfoReadDTO>(basicHealthInfo);
        if (basicHealthInfoDTO != null)
        {
            return Ok(basicHealthInfo);
        }
        return BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/pets/{petId}/basicHealthInfo/{basicHealthInfoId}", Name = "GetPetBasicHealthInfoById")]
    public async Task<ActionResult<BasicHealthInfoReadDTO>> GetPetBasicHealthInfoById(Guid shelterId, Guid petId, Guid basicHealthInfoId)
    {
        var basicHealthInfo = await _shelterRepository.GetPetBasicHealthInfoById(shelterId, petId, basicHealthInfoId);
        var basicHealthInfoDTO = _mapper.Map<BasicHealthInfoReadDTO>(basicHealthInfo);
        if (basicHealthInfoDTO != null)
        {
            return Ok(basicHealthInfo);
        }
        return BadRequest();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}/pets/{petId}/basicHealthInfo/{basicHelthInfoId}")]
    public async Task<IActionResult> UpdatePetBasicHealthInfo(Guid shelterId, Guid petId, string name, int age, Size size, bool isNeutred)
    {
        bool updated = await _shelterRepository.UpdatePetBasicHealthInfo(shelterId, petId, name, age, size, isNeutred);

        if (updated)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpGet("{shelterId}/pets/{petId}/vaccinations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VaccinationReadDTO>>> GetAllPetVaccinations(Guid shelterId, Guid petId)
    {
        var vaccinations = await _shelterRepository.GetAllPetVaccinations(shelterId, petId);
        return Ok(_mapper.Map<IEnumerable<VaccinationReadDTO>>(vaccinations));
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
        var vaccinationReadDto = _mapper.Map<VaccinationReadDTO>(vaccination);
        return CreatedAtRoute(nameof(GetPetVaccinationById), new { shelterId, petId, vaccinationId = addedVaccination.Id }, vaccinationReadDto);

    }
    [HttpPut("{shelterId}/pets/{petId}/vaccinations/{vaccinationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdatePetVaccination(Guid shelterId, Guid petId, Guid vaccinationId, VaccinationCreateDTO vaccinationCreateDTO)
    {
        var foundVaccination = await _shelterRepository.GetPetVaccinationById(shelterId, petId, vaccinationId);
        var vaccinationValidator = _validatorFactory.GetValidator<VaccinationCreateDTO>();
        var validationResult = vaccinationValidator.Validate(vaccinationCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        _mapper.Map(vaccinationCreateDTO, foundVaccination);

        bool updated = await _shelterRepository.UpdatePetVaccination(shelterId, petId, foundVaccination);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{shelterId}/pets/{petId}/vaccinations/{vaccinationId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePetVaccination(Guid shelterId, Guid petId, Guid vaccinationId)
    {
        bool deleted = await _shelterRepository.DeletePetVaccination(shelterId, petId, vaccinationId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("{shelterId}/pets/{petId}/diseases")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DiseaseReadDTO>>> GetAllPetDiseases(Guid shelterId, Guid petId)
    {
        var diseases = await _shelterRepository.GetAllPetDiseases(shelterId, petId);
        return Ok(_mapper.Map<IEnumerable<DiseaseReadDTO>>(diseases));
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
        var diseaseReadDto = _mapper.Map<DiseaseReadDTO>(disease);
        return CreatedAtRoute(nameof(GetPetDiseaseById), new { shelterId, petId, diseaseId = addedDisease.Id }, diseaseReadDto);

    }
    [HttpPut("{shelterId}/pets/{petId}/diseases/{diseaseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdatePetDisease(Guid shelterId, Guid petId, Guid diseaseId, DiseaseCreateDTO diseaseCreateDTO)
    {
        var foundDisease = await _shelterRepository.GetPetDiseaseById(shelterId, petId, diseaseId);
        var diseaseValidator = _validatorFactory.GetValidator<DiseaseCreateDTO>();
        var validationResult = diseaseValidator.Validate(diseaseCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        _mapper.Map(diseaseCreateDTO, foundDisease);

        bool updated = await _shelterRepository.UpdatePetDisease(shelterId, petId, foundDisease);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }
    [HttpDelete("{shelterId}/pets/{petId}/diseases/{diseaseId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePetDisease(Guid shelterId, Guid petId, Guid diseaseId)
    {
        bool deleted = await _shelterRepository.DeletePetDisease(shelterId, petId, diseaseId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("{shelterId}/pets/{petId}/calendar/activities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetAllPetActivities(Guid shelterId, Guid petId)
    {
        var petCalendar = await _shelterRepository.GetAllPetActivities(shelterId, petId);
        if (petCalendar != null)
        {
            return Ok(_mapper.Map<IEnumerable<ActivityReadDTO>>(petCalendar));
        }
        return NotFound();
    }


    [HttpGet("{shelterId}/pets/{petId}/calendar/activities/{activityId}", Name = "GetPetActivityById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActivityReadDTO>> GetPetActivityById(Guid shelterId, Guid petId, Guid activityId)
    {
        var petActivity = await _shelterRepository.GetPetActivityById(shelterId, activityId, petId);
        if (petActivity != null)
        {
            return Ok(_mapper.Map<ActivityReadDTO>(petActivity));
        }
        return NotFound();
    }


    [HttpPost("{shelterId}/pets/{petId}/calendar/activities")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ActivityReadDTO>> AddPetActivity(Guid shelterId, Guid petId, ActivityCreateDTO activityCreateDTO)
    {
        var foudShelter = await _shelterRepository.GetShelterById(shelterId);
        var foundPet = await _shelterRepository.GetShelterPetById(shelterId, petId);
        var activityModel = _mapper.Map<Activity>(activityCreateDTO);

        //var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        //var validationResult = activityValidator.Validate(activityCreateDTO);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest();
        //}

        var addedActivity = await _shelterRepository.AddPetActivityToCalendar(shelterId, petId, activityModel);
        var activityReadDTO = _mapper.Map<ActivityReadDTO>(activityModel);

        return CreatedAtRoute(nameof(GetPetActivityById), new { shelterId = foudShelter.Id, petId = foundPet.Id, activityId = addedActivity.Id }, activityReadDTO);
    }


    [HttpPut("{shelterId}/pets/{petId}/calendar/activities/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdatePetActivity(Guid shelterId, Guid petId, Guid activityId, ActivityCreateDTO activityCreateDTO)
    {
        var foundPet = await _shelterRepository.GetShelterPetById(shelterId, petId);
        var foundActivity = await _shelterRepository.GetPetActivityById(shelterId, activityId, petId);
        if (foundPet == null || foundActivity == null)
        {
            return NotFound();
        }
        var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        var validationResult = activityValidator.Validate(activityCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        _mapper.Map(activityCreateDTO, foundActivity);

        bool updated = await _shelterRepository.UpdatePetActivity(shelterId, petId, foundActivity);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{shelterId}/pets/{petId}/calendar/activities/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePetActivity(Guid shelterId, Guid petId, Guid activityId)
    {
        bool deleted = await _shelterRepository.DeletePetActivity(shelterId, petId, activityId);

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
    [HttpGet("{shelterId}/tempHouses/{tempHouseId}", Name = "GetTempHouseById")]
    public async Task<ActionResult<TempHouseReadDTO>> GetTempHouseById(Guid shelterId, Guid tempHouseId)
    {
        var tempHouse = await _shelterRepository.GetTempHouseById(shelterId, tempHouseId);
        var temphouseDto = _mapper.Map<TempHouseReadDTO>(tempHouse);
        if (temphouseDto != null)
        {
            return Ok(temphouseDto);
        }
        return NotFound();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{shelterId}/temporary-houses/pets/{petId}/users/{userId}")]
    public async Task<ActionResult<TempHouseReadDTO>> InitializeTempHouse(Guid shelterId, Guid userId, Guid petId, TempHouseCreateDTO tempHouseCreateDTO)
    {
        var foundShelter = await _shelterRepository.GetShelterById(shelterId);
        var tempHouseModel = _mapper.Map<TempHouse>(tempHouseCreateDTO);
        //var tempHouseValidator = _validatorFactory.GetValidator<TempHouseCreateDTO>();
        //var validationResult = tempHouseValidator.Validate(tempHouseCreateDTO);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest();
        //}
        var addedTemphouse = await _shelterRepository.InitializeTempHouseForPet(shelterId, userId, petId, tempHouseModel);
        var tempHouseReadDto = _mapper.Map<TempHouseReadDTO>(tempHouseModel);
        return CreatedAtRoute(nameof(GetTempHouseById), new { shelterId = foundShelter.Id, tempHouseId = addedTemphouse.Id }, tempHouseReadDto);
    }

    [HttpPost("temporary-houses/{tempHouseId}/pets/{petId}/calendar/activities/{activityId}/users/meetings-temporary-house")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TempHouseReadDTO>> ChooseMeetingDatesForTempHouse(Guid petId, Guid tempHouseId, Guid activityId)
    {
        var tempHouse = await _shelterRepository.ChooseMeetingDatesForTempHouseProcess(petId, tempHouseId, activityId);
        var tempHouseReadDTO = _mapper.Map<TempHouseReadDTO>(tempHouse);
        return Ok(tempHouseReadDTO);
    }

    [HttpPost("temporary-houses/{tempHouseId}/pets/{petId}/confirm")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TempHouseReadDTO>> ConfirmYourChooseForTempHouse(Guid tempHouseId, Guid petId)
    {
        var tempHouse = await _shelterRepository.ConfirmYourChooseForTempHouse(tempHouseId, petId);
        var tempHouseReadDTO = _mapper.Map<TempHouseReadDTO>(tempHouse);
        return Ok(tempHouseReadDTO);
    }

    [HttpPost("{shelterId}/temporary-houses/{tempHouseId}/pets/{petId}/calendar/activities/{activityId}/users/{userId}/meetings-another-pet-temporary-house")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TempHouseReadDTO>> ChooseMeetingDatesForKnowAnotherYourPet(Guid shelterId, Guid petId, Guid userId, Guid tempHouseId, Guid activityId)
    {
        var tempHouse = await _shelterRepository.ChooseMeetingDatesForKnowAnotherPet(shelterId, petId, userId, tempHouseId, activityId);
        var tempHouseReadDTO = _mapper.Map<TempHouseReadDTO>(tempHouse);
        return Ok(tempHouseReadDTO);
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("temporary-houses/{tempHouseId}/pets/{petId}/calendar/activities/users/add-pet")]
    public async Task<ActionResult<TempHouseReadDTO>> ConfirmToAddAnotherPetToTempHouse(Guid petId, Guid tempHouseId)
    {
        var addPetToTempHouse = await _shelterRepository.ConfirmToAddAnotherPetToTempHouse(tempHouseId, petId);
        var tempHouseReadDto = _mapper.Map<TempHouseReadDTO>(addPetToTempHouse);
        return Ok(tempHouseReadDto);
    }

    [HttpPut("temporary-houses/{tempHouseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateTempHouse(Guid tempHouseId, TempHouseCreateDTO tempHouseCreateDTO)
    {
        var foundTempHouse = await _shelterRepository.GetTempHouse(tempHouseId);
        if (foundTempHouse == null)
        {
            return NotFound();
        }
        var tempHouseCreateDto = _mapper.Map(tempHouseCreateDTO, foundTempHouse);

        bool updated = await _shelterRepository.UpdateTempHouse(foundTempHouse);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{shelterId}/tempHouses/{tempHouseId}/pets/{petId}/users/{userId}")]
    public async Task<IActionResult> DeleteTempHouse(Guid tempHouseId, Guid shelterId, Guid petId, Guid userId)
    {
        bool deleted = await _shelterRepository.DeleteTempHouse(tempHouseId, shelterId, petId, userId);

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
    [HttpGet("{shelterId}/adoptions")]
    public async Task<ActionResult<IEnumerable<AdoptionReadDTO>>> GetAllAdoptions(Guid shelterId)
    {
        var adoptions = await _shelterRepository.GetAllShelterAdoptions(shelterId);
        return Ok(_mapper.Map<IEnumerable<AdoptionReadDTO>>(adoptions));
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{shelterId}/adoptions/{adoptionId}", Name = "GetAdoptionById")]
    public async Task<ActionResult<AdoptionReadDTO>> GetAdoptionById(Guid shelterId, Guid adoptionId)
    {
        var adoption = await _shelterRepository.GetShelterAdoptionById(shelterId, adoptionId);
        if (adoption != null)
        {
            return Ok(_mapper.Map<AdoptionReadDTO>(adoption));
        }
        return NotFound();


    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("/adoptions/{adoptionId}", Name = "GetAdoptionFromDataBaseById")]
    public async Task<ActionResult<AdoptionReadDTO>> GetAdoptionFromDataBaseById(Guid adoptionId)
    {
        var adoption = await _shelterRepository.GetAdoptionFromDataBaseById(adoptionId);
        if (adoption != null)
        {
            return Ok(_mapper.Map<AdoptionReadDTO>(adoption));
        }
        return NotFound();
    }
    [HttpPost("{shelterId}/pets/{petId}/users/{userId}/adoptions/inizialize-adoption")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AdoptionReadDTO>> InizializePetAdoption(Guid shelterId, Guid petId, Guid userId, AdoptionCreateDTO adoptionCreateDto)
    {
        var foundShelter = await _shelterRepository.GetShelterById(shelterId);
        var adoptionModel = _mapper.Map<Adoption>(adoptionCreateDto);

        //var adoptionValidator = _validatorFactory.GetValidator<AdoptionCreateDTO>();
        //var validationResult = adoptionValidator.Validate(adoptionCreateDTO);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest();
        //}

        var addedAdoption = await _shelterRepository.InitializePetAdoption(shelterId, petId, userId, adoptionModel);
        var adoptionReadDTO = _mapper.Map<AdoptionReadDTO>(adoptionModel);
        return Ok(adoptionReadDTO);
    }

    [HttpPost("{shelterId}/pets/{petId}/calendar/activities/{activityId}/users/{userId}/adoptions/{adoptionId}/meetings-adoption")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AdoptionReadDTO>> ChooseMeetingDatesForAdoption(Guid shelterId, Guid petId, Guid userId, Guid adoptionId, Guid activityId)
    {
        var addedAdoption = await _shelterRepository.ChooseMeetingDatesForAdoption(shelterId, petId, userId, adoptionId, activityId);
        var adoptionReadDTO = _mapper.Map<AdoptionReadDTO>(addedAdoption);
        return Ok(adoptionReadDTO);
    }


    [HttpPost("adoptions/{adoptionId}/meetings-adoption-done")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AdoptionReadDTO>> PetAdoptionMeetingsDone(Guid adoptionId)
    {
        var addedAdoption = await _shelterRepository.PetAdoptionMeetingsDone(adoptionId);
        var adoptionReadDTO = _mapper.Map<AdoptionReadDTO>(addedAdoption);
        return Ok(adoptionReadDTO);
    }

    [HttpPost("{shelterId}/pets/{petId}/users/{userId}/adoptions/{adoptionId}/contract-adoption")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AdoptionReadDTO>> ContractForPetAdoption(Guid shelterId, Guid petId, Guid userId, Guid adoptionId, string contractAdoption)
    {
        var addedAdoption = await _shelterRepository.ContractForPetAdoption(shelterId, petId, userId, adoptionId, contractAdoption);
        var adoptionReadDTO = _mapper.Map<AdoptionReadDTO>(addedAdoption);
        return Ok(adoptionReadDTO);
    }



    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("{shelterId}/adoptions/{adoptionId}")]
    public async Task<IActionResult> UpdateAdoption(Guid shelterId, Guid adoptionId, Guid userId, AdoptionCreateDTO adoptionCreateDTO)
    {
        var foundShelter = await _shelterRepository.GetShelterById(shelterId);
        var foundUser = await _shelterRepository.FindUserById(userId);
        var foundAdoption = await _shelterRepository.GetShelterAdoptionById(shelterId, adoptionId);
        if (foundShelter == null || foundUser == null || foundAdoption == null)
        {
            return NotFound();
        }
        var activityValidator = _validatorFactory.GetValidator<AdoptionCreateDTO>();
        var validationResult = activityValidator.Validate(adoptionCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        var activityCreate = _mapper.Map(adoptionCreateDTO, foundAdoption);

        bool updated = await _shelterRepository.UpdateAdoption(shelterId, userId, foundAdoption);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{shelterId}/adoptions/{adoptionId}/pets/{petId}/users/{userId}")]
    public async Task<IActionResult> DeleteAdoption(Guid shelterId, Guid adoptionId, Guid petId, Guid userId)
    {
        bool deleted = await _shelterRepository.DeleteAdoption(shelterId, adoptionId, petId, userId);
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
    [HttpGet("{shelterId}/pets/avaible")]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllAvaiblePets(Guid shelterId)
    {
        var pets = await _shelterRepository.GetAllAvaiblePets(shelterId);
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
}


