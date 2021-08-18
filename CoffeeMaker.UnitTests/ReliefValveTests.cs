using AutoFixture.Xunit2;
using Moq;
using System;
using Xunit;

namespace CoffeeMaker.UnitTests
{
    public class ReliefValveTests
    {
        [Theory, TestConventions]
        public void SutIsObserverOfWarmerPlateStatus(ReleifValve sut)
        {
            Assert.IsAssignableFrom<IObserver<WarmerPlateStatus>>(sut);
        }

        [Theory, TestConventions]
        public void OnCompletedDoesNotThrow(ReleifValve sut)
        {
            sut.OnCompleted();
        }

        [Theory, TestConventions]
        public void OnErrorDoesNotThrowNotImplementedException(
            ReleifValve sut,
            Exception e)
        {
            try
            {
                sut.OnError(e);
            }
            catch (NotImplementedException)
            {
                Assert.True(false, "NotImplementedException thrown.");
            }
        }

        [Theory, TestConventions]
        public void OnNextWarmerEmptyDoesNotThrow(ReleifValve sut)
        {
            sut.OnNext(WarmerPlateStatus.WARMER_EMPTY);
        }

        [Theory, TestConventions]
        public void OpenValveWhenRemovingPotFromWarmerPlate(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            ReleifValve sut)
        {
            sut.OnNext(WarmerPlateStatus.WARMER_EMPTY);

            hardwareMock.Verify(
                hw => hw.SetReliefValveState(ReliefValveState.OPEN));
        }

        [Theory, TestConventions]
        public void CloseValveWhenEmptyPotIsPresent(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            ReleifValve sut)
        {
            sut.OnNext(WarmerPlateStatus.POT_EMPTY);

            hardwareMock.Verify(
                hw => hw.SetReliefValveState(ReliefValveState.CLOSED));
        }

        [Theory, TestConventions]
        public void CloseValveWhenPotIsPresent(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            ReleifValve sut)
        {
            sut.OnNext(WarmerPlateStatus.POT_NOT_EMPTY);

            hardwareMock.Verify(
                hw => hw.SetReliefValveState(ReliefValveState.CLOSED));
        }
    }
}
