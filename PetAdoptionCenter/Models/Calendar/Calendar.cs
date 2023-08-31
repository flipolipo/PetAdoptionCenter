namespace PetAdoptionCenter.Models.TimeTable

{
    public class TimeTable<T>
    {
        public uint Id { get; init; }
        public T Role { get; set; }
        public DateTime DateWithTime { get; set; }
        public Activity Activity { get; set; }
    }
}
