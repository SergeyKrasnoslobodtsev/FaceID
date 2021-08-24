namespace FaceID.Core.Specifications.Employee
{
    public class EmployeeWithNameAndPhotoSpecification : BaseSpecification<Entities.Employee>
    {
        public EmployeeWithNameAndPhotoSpecification() : base()
        {
            AddInclude(b => b.Name);
            AddInclude(b => b.Photo);
            ApplyOrderBy(b => b.Name);
        }
    }
}
