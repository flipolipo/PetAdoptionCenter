using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.DTOs.AddressDTOs;
using SimpleWebDal.DTOs.AnimalDTOs;
using SimpleWebDal.DTOs.CalendarDTOs;
using SimpleWebDal.DTOs.CalendarDTOs.ActivityDTOs;
using SimpleWebDal.DTOs.ShelterDTOs;
using SimpleWebDal.DTOs.WebUserDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.BasicInformationDTOs;
using SimpleWebDal.DTOs.WebUserDTOs.CredentialsDTOs;
using SimpleWebDal.Models.Animal;
using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.WebUser;
using SimpleWebDal.Repository.UserRepo;
using SImpleWebLogic.Configuration;

namespace PetAdoptionCenter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private readonly ValidatorFactory _validatorFactory;

        public UserController(IUserRepository userRepository, IMapper mapper, ValidatorFactory validatorFactory)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
        }

        [HttpPost("{id}/calendar/activities/create")]
        public async Task<ActionResult<Activity>> AddActivity(Guid userId, string activityName, DateTime activityDate)
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
                await _userRepository.AddActivity(userId, activityName, activityDate);
                return Ok(activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating an activity: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(string username, string password,
            string name, string surname, string phone, string email,
            string street, string houseNumber, int flatNumber, string postalCode, string city)
        {
            var userCreateDTO = new UserCreateDTO()
            {
                Credentials = new CredentialsCreateDTO()
                {
                    Username = username,
                    Password = password
                },
                BasicInformation = new BasicInformationCreateDTO()
                {
                    Name = name,
                    Surname = surname,
                    Phone = phone,
                    Email = email,
                    Address = new AddressCreateDTO()
                    {
                        Street = street,
                        HouseNumber = houseNumber,
                        FlatNumber = flatNumber,
                        PostalCode = postalCode,
                        City = city
                    }
                },
                UserCalendar = new CalendarActivityCreateDTO()
                {
                    DateWithTime = DateTime.UtcNow
                }
            };

            var userValidator = _validatorFactory.GetValidator<UserCreateDTO>();
            var userCredentialsValidator = _validatorFactory.GetValidator<CredentialsCreateDTO>();
            var userBasicInformationValidator = _validatorFactory.GetValidator<BasicInformationCreateDTO>();
            var userAddressValidator = _validatorFactory.GetValidator<AddressCreateDTO>();

            var validationResult = userValidator.Validate(userCreateDTO);
            var validationResultCredentials = userCredentialsValidator.Validate(userCreateDTO.Credentials);
            var validationResultBasicInformation = userBasicInformationValidator.Validate(userCreateDTO.BasicInformation);
            var validationResultAddress = userAddressValidator.Validate(userCreateDTO.BasicInformation.Address);

            if (!validationResult.IsValid || !validationResultCredentials.IsValid ||
                !validationResultBasicInformation.IsValid || !validationResultAddress.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = _mapper.Map<SimpleWebDal.Models.WebUser.User>(userCreateDTO);

            try
            {
                await _userRepository.AddUser(username, password, name, surname, phone, email,
            street, houseNumber, flatNumber, postalCode, city);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Błąd podczas tworzenia usera: " + ex.Message);

            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelter>>> GetAllShelters()
        {
            var shelters = await _userRepository.GetAllShelters();
            var sheltersDto = _mapper.Map<IEnumerable<ShelterReadDTO>>(shelters);
            if (sheltersDto != null)
            {
                return Ok(sheltersDto);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelter>>> GetAllPets()
        {
            var pets = await _userRepository.GetAllPets();
            var petsDto = _mapper.Map<IEnumerable<PetReadDTO>>(pets);
            if (petsDto != null)
            {
                return Ok(petsDto);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelter>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var usersDto = _mapper.Map<IEnumerable<UserReadDTO>>(users);
            if (usersDto != null)
            {
                return Ok(usersDto);
            }
            return BadRequest();
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpGet("{id}/pets")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterDogsOrCats(Guid shelterId, PetType type)
        {
            var pets = await _userRepository.GetAllShelterDogsOrCats(shelterId, type);
            if (pets != null)
            {
                return Ok(pets);
            }
            return BadRequest();
        }

        [HttpGet("{id}/pets/type")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllShelterPets(Guid shelterId)
        {
            var pets = await _userRepository.GetAllShelterPets(shelterId);
            if (pets != null)
            {
                return Ok(pets);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            bool deleted = await _userRepository.DeleteUser(userId);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/activities/{activityId}")]
        public async Task<IActionResult> DeleteActivity(Guid userId, Guid activityId)
        {
            bool deleted = await _userRepository.DeleteActivity(userId, activityId);

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
        public async Task<IActionResult> UpdateActivity(Guid userId, Guid activityId, string name, DateTime date)
        {
            bool updated = await _userRepository.UpdateActivity(userId, activityId, name, date);

            if (updated)
            {
                var updatedActivity = await _userRepository.GetUserActivityById(userId, activityId);
                return Ok(updatedActivity);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid userId, string username, string password, string name, string surname, string phone, string email,
        string street, string houseNumber, int flatNumber, string postalCode, string city)
        {
            bool updated = await _userRepository.UpdateUser(userId, username, password, name, surname, phone, email, street, houseNumber, flatNumber, postalCode, city);

            if (updated)
            {
                var updatedUser = await _userRepository.GetUserById(userId);
                return Ok(updatedUser);
            }

            return NotFound();
        }

    }
}
