namespace PlaceRentalApp.Core.Entities
{
    public class Place : BaseEntity
    {
        protected Place() { }
        public Place(string title, string description, decimal dailyPrice, Address address, int allowedNumberPerson, bool allosPets, int createdBy) : base()
        {
            Title = title;
            Description = description;
            DailyPrice = dailyPrice;
            Address = address;
            AllowedNumberPerson = allowedNumberPerson;
            AllosPets = allosPets;
            CreatedBy = createdBy;

            Status = PlaceStatus.Active;
            Books = new List<PlaceBook>();
            Amenities = new List<PlaceAmenity>();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal DailyPrice { get; private set; }
        public Address Address { get; private set; }
        public int AllowedNumberPerson { get; private set; }
        public bool AllosPets { get; private set; }
        public int CreatedBy { get; private set; }
        public User User { get; private set; }
        public PlaceStatus Status { get; private set; }
        public List<PlaceBook> Books { get; private set; }
        public List<PlaceAmenity> Amenities { get; private set; }

        public void Update(string title, string description, decimal dailyPrice)
        {
            Title = title;
            Description = description;
            DailyPrice = dailyPrice;
        }
    }
}
