using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace CoffeeMaker.UnitTests
{
    public class TestConventionsAttribute : AutoDataAttribute
    {
        public TestConventionsAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
