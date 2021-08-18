using AutoFixture.Idioms;
using System.Linq;
using Xunit;

namespace CoffeeMaker.UnitTests
{
    public class Invariants
    {
        [Theory, TestConventions]
        public void ConstructorsHaveAppropriateGuardClauses(
            GuardClauseAssertion assertion)
        {
            var representativeType = typeof(ICoffeeMaker);
            assertion.Verify(representativeType.Assembly
                .GetExportedTypes()
                .SelectMany(t => t.GetConstructors()));
        }
    }
}
