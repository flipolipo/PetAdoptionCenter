using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.RoleDTOs;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Repository.UserRepo;
using SImpleWebLogic.Configuration;

namespace PetAdoptionCenter.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private readonly ValidatorFactory _validatorFactory;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserRepository userRepository, IMapper mapper, ValidatorFactory validatorFactory, ILogger<UsersController> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validatorFactory = validatorFactory;
        _logger = logger;
    }
    #region //Endpoints for users
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        _logger.LogInformation($"Metoda HTTP: {HttpContext.Request.Method}");
        return Ok(_mapper.Map<IEnumerable<UserReadDTO>>(users));
    }
    //[HttpGet("test")]
    //public IActionResult Test()
    //{
    //    throw new Exception("This is a simulated exception.");
    //}

    [HttpGet("{id}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadDTO>> GetUserById(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user != null)
        {
            return Ok(_mapper.Map<UserReadDTO>(user));
        }
        return NotFound();
    }



    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult<UserReadDTO>> AddUser(UserCreateDTO userCreateDTO)
    //{
    //    var userModel = _mapper.Map<User>(userCreateDTO);

    //    var userBasicInformationValidator = _validatorFactory.GetValidator<BasicInformationCreateDTO>();
    //    var userAddressValidator = _validatorFactory.GetValidator<AddressCreateDTO>();


    //    var validationResultBasicInformation = userBasicInformationValidator.Validate(userCreateDTO.BasicInformation);
    //    var validationResultAddress = userAddressValidator.Validate(userCreateDTO.BasicInformation.Address);

    //    if (
    //        !validationResultBasicInformation.IsValid || !validationResultAddress.IsValid)
    //    {
    //        return BadRequest();
    //    }

    //    var addedUser = await _userRepository.AddUser(userModel);
    //    var userReadDTO = _mapper.Map<UserReadDTO>(userModel);

    //    return CreatedAtRoute(nameof(GetUserById), new { id = userReadDTO.Id }, userReadDTO);
    //}


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUser(Guid id, UserCreateDTO userCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        if (foundUser == null)
        {
            return NotFound();
        }
        if (!IsUserCreateDTOValid(userCreateDTO))
        {
            return BadRequest();
        }
        var userCreateDto = _mapper.Map(userCreateDTO, foundUser);

        bool updated = await _userRepository.UpdateUser(foundUser);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }
    private bool IsUserCreateDTOValid(UserCreateDTO userCreateDTO)
    {
        var userValidator = _validatorFactory.GetValidator<UserCreateDTO>();
        var userBasicInformationValidator = _validatorFactory.GetValidator<BasicInformationCreateDTO>();
        var userAddressValidator = _validatorFactory.GetValidator<AddressCreateDTO>();
        var userCalendarValidator = _validatorFactory.GetValidator<CalendarActivityCreateDTO>();

        var validationResultBasicInformation = userBasicInformationValidator.Validate(userCreateDTO.BasicInformation);
        var validationResultAddress = userAddressValidator.Validate(userCreateDTO.BasicInformation.Address);
        var validationResultCalendarActivity = userCalendarValidator.Validate(userCreateDTO.UserCalendar);
        var validateResultRole = userValidator.Validate(userCreateDTO);

        return validationResultBasicInformation.IsValid && validationResultAddress.IsValid && validationResultCalendarActivity.IsValid
            && validateResultRole.IsValid;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        bool deleted = await _userRepository.DeleteUser(id);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }


    [HttpGet("{id}/calendar/activities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetAllActivities(Guid id)
    {
        var userCalendar = await _userRepository.GetUserActivities(id);
        if (userCalendar != null)
        {
            return Ok(_mapper.Map<IEnumerable<ActivityReadDTO>>(userCalendar));
        }
        return NotFound();
    }


    [HttpGet("{id}/calendar/activities/{activityId}", Name = "GetActivityById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActivityReadDTO>> GetActivityById(Guid id, Guid activityId)
    {
        var userActivity = await _userRepository.GetUserActivityById(id, activityId);
        if (userActivity != null)
        {
            return Ok(_mapper.Map<ActivityReadDTO>(userActivity));
        }
        return NotFound();
    }


    [HttpPost("{id}/calendar/activities")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ActivityReadDTO>> AddActivity(Guid id, ActivityCreateDTO activityCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var activityModel = _mapper.Map<Activity>(activityCreateDTO);

        var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        var validationResult = activityValidator.Validate(activityCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        var addedActivity = await _userRepository.AddActivity(id, activityModel);
        var activityReadDTO = _mapper.Map<ActivityReadDTO>(activityModel);

        return CreatedAtRoute(nameof(GetActivityById), new { id = foundUser.Id, activityId = addedActivity.Id }, activityReadDTO);
    }


    [HttpPut("{id}/calendar/activities/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUserActivity(Guid id, Guid activityId, ActivityCreateDTO activityCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var foundActivity = await _userRepository.GetUserActivityById(id, activityId);
        if (foundUser == null || foundActivity == null)
        {
            return NotFound();
        }
        var activityValidator = _validatorFactory.GetValidator<ActivityCreateDTO>();
        var validationResult = activityValidator.Validate(activityCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        var activityCreate = _mapper.Map(activityCreateDTO, foundActivity);

        bool updated = await _userRepository.UpdateActivity(id, foundActivity);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}/activities/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteActivity(Guid id, Guid activityId)
    {
        bool deleted = await _userRepository.DeleteActivity(id, activityId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("{id}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<RoleReadDTO>>> GetAllUserRoles(Guid id)
    {
        var userRoles = await _userRepository.GetAllUserRoles(id);
        if (userRoles != null)
        {
            return Ok(_mapper.Map<IEnumerable<RoleReadDTO>>(userRoles));
        }
        return NotFound();
    }

    [HttpGet("{id}/roles/{roleId}", Name = "GetUserRoleById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleReadDTO>> GetUserRoleById(Guid id, Guid roleId)
    {
        var userRole = await _userRepository.GetUserRoleById(id, roleId);
        if (userRole != null)
        {
            return Ok(_mapper.Map<RoleReadDTO>(userRole));
        }
        return NotFound();
    }

    [HttpPost("{id}/roles")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleReadDTO>> AddUserRole(Guid id, RoleCreateDTO roleCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var roleModel = _mapper.Map<Role>(roleCreateDTO);

        var roleValidator = _validatorFactory.GetValidator<RoleCreateDTO>();
        var validationResult = roleValidator.Validate(roleCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        var addedUserRole = await _userRepository.AddRole(id, roleModel);
        var roleReadDTO = _mapper.Map<RoleReadDTO>(roleModel);

        return CreatedAtRoute(nameof(GetUserRoleById), new { id = foundUser.Id, roleId = addedUserRole.Id }, roleReadDTO);
    }

    [HttpDelete("{id}/roles/{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUserRole(Guid id, Guid roleId)
    {
        bool deleted = await _userRepository.DeleteUserRole(id, roleId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/roles/{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUserRole(Guid id, Guid roleId, RoleCreateDTO roleCreateDTO)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var foundRole = await _userRepository.GetUserRoleById(id, roleId);
        if (foundUser == null || foundRole == null)
        {
            return NotFound();
        }
        var roleValidator = _validatorFactory.GetValidator<RoleCreateDTO>();
        var validationResult = roleValidator.Validate(roleCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        var activityCreate = _mapper.Map(roleCreateDTO, foundRole);

        bool updated = await _userRepository.UpdateUserRole(id, foundRole);
        if (updated)
        {
            return NoContent();
        }
        else
        {
            return StatusCode(500);
        }
    }
    #endregion
    #region //Endpoints for pets
    [HttpGet("pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllPets()
    {
        var pets = await _userRepository.GetAllPets();
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
        return Ok(updatedPetsDto);
    }

    [HttpGet("pets/{id}", Name = "GetPetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetReadDTO>> GetPetById(Guid id)
    {
        var pet = await _userRepository.GetPetById(id);
        var petDto = _mapper.Map<PetReadDTO>(pet);
        petDto.ImageBase64 = Convert.ToBase64String(pet.Image);
        if (pet != null)
        {
            return Ok(petDto);
        }
        return NotFound();
    }

    [Authorize]
    [HttpGet("{id}/pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllFavouritePets(Guid id)
    {
        var pets = await _userRepository.GetAllFavouritePets(id);
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
        return Ok(updatedPetsDto);
    }

    [HttpGet("{id}/pets/{petId}", Name = "GetFavouritePetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetReadDTO>> GetFavouritePetById(Guid id, Guid petId)
    {
        var pet = await _userRepository.GetFavouritePetById(id, petId);
        var petDto = _mapper.Map<PetReadDTO>(pet);
        petDto.ImageBase64 = Convert.ToBase64String(pet.Image);
        if (pet != null)
        {
            return Ok(petDto);
        }
        return NotFound();
    }

    [HttpDelete("{id}/pets/{petId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteFavouritePet(Guid id, Guid petId)
    {
        bool deleted = await _userRepository.DeleteFavouritePet(id, petId);

        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }


    [HttpPost("{id}/pets/{petId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetReadDTO>> AddExistingPetToUser(Guid id, Guid petId)
    {
        var foundUser = await _userRepository.GetUserById(id);
        var addedPet = await _userRepository.AddFavouritePet(id, petId);
        var addedPetReadDto = _mapper.Map<PetReadDTO>(addedPet);

        if (addedPet != null)
        {
            return CreatedAtRoute(nameof(GetFavouritePetById), new { id = foundUser.Id, petId = addedPetReadDto.Id }, addedPetReadDto);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("pets/adopted")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllAdoptedPets()
    {
        var pets = await _userRepository.GetAllAdoptedPet();
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
        return Ok(updatedPetsDto);
    }

    [HttpGet("pets/adopted/{id}", Name = "GetAdoptedPetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetReadDTO>> GetAdoptedPetById(Guid id)
    {
        var pet = await _userRepository.GetAdoptedPetById(id);
        var petDto = _mapper.Map<PetReadDTO>(pet);
        petDto.ImageBase64 = Convert.ToBase64String(pet.Image);
        if (pet != null)
        {
            return Ok(petDto);
        }
        return NotFound();
    }

    [HttpGet("pets/available-to-adoption")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllPetsAvailableToAdoption()
    {
        var pets = await _userRepository.GetAllPetsAvailableForAdoption();
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
        return Ok(updatedPetsDto);
    }
    #endregion
}