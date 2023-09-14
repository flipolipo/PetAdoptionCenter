using SimpleWebDal.Models.Animal.Enums;
using SimpleWebDal.Models.PetShelter;
using SimpleWebDal.Models.CalendarModel;
using SimpleWebDal.Models.WebUser;
using System.ComponentModel.DataAnnotations;
using SimpleWebDal.Models.TemporaryHouse;
using SimpleWebDal.Models.ProfileUser;
using SimpleWebDal.Models.AdoptionProccess;

namespace SimpleWebDal.Models.Animal;

public class Pet
{

    public int PetId { get; init; }
    public PetType Type { get; init; }
    public BasicHealthInfo BasicHealthInfo { get; set; }

    public string Description { get; set; }
    public CalendarModelClass Callendar { get; set; }
    public PetStatus Status { get; set; }
    public Shelter Shelter { get; set; }
    public bool AvaibleForAdoption { get; set; }
    public ICollection<User> PatronUsers { get; set; }

    //Navigation properties:

    public int AdoptionId { get; set; }
    public Adoption Adoption { get; set; }
    public int TempHouseId { get; set; }
    public TempHouse TempHouse { get; set; }

    // Foreign key for the first relationship (FavouriteListPets)
    public int ProfileId { get; set; }
    public ProfileModel Profile { get; set; }

    // Foreign key for the second relationship (VirtualAdoptionPetsList)
    public int ProfileIdForVirtualAdoptionId { get; set; }
    public ProfileModel ProfileForVirtualAdoption { get; set; }

    // Foreign key for the first relationship (ListOfPetsAdopted)
    public int ShelterListOfPetsAdoptedId { get; set; }
    public Shelter ShelterListOfPetsAdopted { get; set; }

    // Foreign key for the second relationship (ListOfPets)
    public int ShelterListOfPetsId { get; set; }
    public Shelter ShelterListOfPets { get; set; }

    // Foreign key for the second relationship (ListOfPets)
    public int ActivityId { get; set; }
    public Activity Activity { get; set; }
}
