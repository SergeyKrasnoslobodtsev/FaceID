using System.Collections.Generic;

namespace FaceID.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public string Photo { get; set; }
        public virtual ICollection<Vectors> Vectors { get; set; }
    }
}
