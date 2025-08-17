using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using System.Linq.Expressions;

namespace Corelia.DataLake.Dashboard.Domain.Specfication.Authentication
{
    public class ApplicationUserSpecification : Specification<ApplicationUser, string>
    {

        public ApplicationUserSpecification(Expression<Func<ApplicationUser, bool>>? expression) : base(expression)
        {
            AddIncludes();
        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
        }
    }
}
