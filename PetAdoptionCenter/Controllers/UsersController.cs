using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.Models.CalendarModel;
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

    public UsersController(IUserRepository userRepository, IMapper mapper, ValidatorFactory validatorFactory)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validatorFactory = validatorFactory;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return Ok(_mapper.Map<IEnumerable<UserReadDTO>>(users));
    }

    [HttpGet("{id}", Name = "GetUserById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user != null)
        {
            return Ok(_mapper.Map<UserReadDTO>(user));
        }
        return NotFound();
    }

    //[HttpPost]
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
    public async Task<ActionResult> UpdateUser(string id, UserCreateDTO userCreateDTO)
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
    public async Task<IActionResult> DeleteUser(string id)
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

    public async Task<ActionResult<IEnumerable<ActivityReadDTO>>> GetAllActivities(string id)
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
    public async Task<ActionResult<ActivityReadDTO>> GetActivityById(string id, Guid activityId)
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
    public async Task<ActionResult<ActivityReadDTO>> AddActivity(string id, ActivityCreateDTO activityCreateDTO)
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
    public async Task<ActionResult> UpdateUserActivity(string id, Guid activityId, ActivityCreateDTO activityCreateDTO)
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
    public async Task<ActionResult> DeleteActivity(string id, Guid activityId)
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
    [HttpGet("pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PetReadDTO>>> GetAllPets()
    {
        var pets = await _userRepository.GetAllPets();
        return Ok(_mapper.Map<IEnumerable<PetReadDTO>>(pets));
    }

    [HttpGet("pets/{id}", Name = "GetPetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetReadDTO>> GetPetById(Guid id)
    {
        var pets = await _userRepository.GetPetById(id);
        if (pets != null)
        {
            return Ok(_mapper.Map<PetReadDTO>(pets));
        }
        return NotFound();
    }

    //[HttpGet("{id}/pets")]
    //public async Task<ActionResult<IEnumerable<string>>> GetAllFavouritePets(Guid id)
    //{
    //    var pets = await _userRepository.GetAllFavouritePets(id);
    //    if (pets != null)
    //    {
    //        return Ok(pets);
    //    }
    //    return NotFound();
    //}

    //[HttpGet("{id}/pets/{petId}", Name = "GetFavouritePetById")]
    //public async Task<ActionResult<string>> GetFavouritePetById(Guid id, Guid petId)
    //{
    //    var pet = await _userRepository.GetFavouritePetById(id, petId);
    //    if (pet != null)
    //    {
    //        return Ok(pet);
    //    }
    //    return NotFound();
    //}

    //[HttpDelete("{id}/pets/{petId}")]
    //public async Task<IActionResult> DeleteFavouritePet(Guid id, Guid petId)
    //{
    //    bool deleted = await _userRepository.DeleteFavouritePet(id, petId);

    //    if (deleted)
    //    {
    //        return NoContent();
    //    }
    //    else
    //    {
    //        return NotFound();
    //    }
    //}


    //[HttpPatch("{id}")]
    //public async Task<ActionResult> PartialUserUpdate(Guid id, JsonPatchDocument<IEnumerable<string>> patchDoc)
    //{
    //    var user = await _userRepository.GetUserById(id);
    //    var petsList = await _userRepository.GetAllFavouritePets(id);

    //    if (petsList == null)
    //    {
    //        return NotFound();
    //    }

    //    patchDoc.ApplyTo(petsList, ModelState);

    //    foreach (var operation in patchDoc.Operations)
    //    {
    //        if (operation.op == "add" && operation.path == "/Id")
    //        {
    //            var newId = operation.value.ToString();
    //            petsList.Append(newId);
    //        }
    //    }

    //    if (!TryValidateModel(patchDoc))
    //    {
    //        return ValidationProblem(ModelState);
    //    }
    //    await _userRepository.PartialUpdateUser(user);

    //    return NoContent();
    //}
}





//[HttpGet("{id}/pets/type")]
//public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterPets(Guid shelterId)
//{
//    var pets = await _userRepository.GetAllShelterPets(shelterId);
//    if (pets != null)
//    {
//        return Ok(pets);
//    }
//    return BadRequest();
//}









