namespace PlaceRentalApp.Application.Models
{
    public class CreateCommentInputModel
    {
        public int IdUser { get; private set; }
        public int IdPlace { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Comments { get; private set; }
    }
}
