namespace FaceID.Core.Entities
{
    public class Vectors : BaseEntity
    {
        public Employee Employee { get; set; }
        public string Vector { get; set; }
    }
}
