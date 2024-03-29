﻿using AutoFixture.Xunit2;
using Moq;
using System;
using Xunit;

namespace CoffeeMaker.UnitTests
{
    public class IndicatorTests
    {
        [Theory, TestConventions]
        public void SutIsObververOfBoilerStatus(IndicatorLight sut)
        {
            Assert.IsAssignableFrom<IObserver<BoilerStatus>>(sut);
        }

        [Theory, TestConventions]
        public void OnCompletedDoesNotThrow(IndicatorLight sut)
        {
            sut.OnCompleted();
        }

        [Theory, TestConventions]
        public void OnErrorDoesNotThrowNotImplementedException(
            IndicatorLight sut,
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
        public void OnNextWarmerEmptyDoesNotThrow(IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.NOT_EMPTY);
        }

        [Theory, TestConventions]
        public void SutIsObserverOfBrewButtonStatus(IndicatorLight sut)
        {
            Assert.IsAssignableFrom<IObserver<BrewButtonStatus>>(sut);
        }

        [Theory, TestConventions]
        public void OnNextBrewButtonNotPushedDoesNotThrow(IndicatorLight sut)
        {
            sut.OnNext(BrewButtonStatus.NOT_PUSHED);
        }

        [Theory, TestConventions]
        public void StartBrewCycleTurnsOffIndicator(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.NOT_EMPTY);
            sut.OnNext(BrewButtonStatus.PUSHED);

            hardwareMock.Verify(hw => hw.SetIndicatorState(IndicatorState.OFF));
        }

        [Theory, TestConventions]
        public void PushButtonDoesNothingWhenBoilerStateIsUnknown(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BrewButtonStatus.PUSHED);

            hardwareMock.Verify(
                hw => hw.SetIndicatorState(It.IsAny<IndicatorState>()),
                Times.Never());
        }

        [Theory, TestConventions]
        public void PushButtonDoesNothingWhenBoilerIsEmpty(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.EMPTY);
            sut.OnNext(BrewButtonStatus.PUSHED);

            hardwareMock.Verify(
                hw => hw.SetIndicatorState(It.IsAny<IndicatorState>()),
                Times.Never());
        }

        [Theory, TestConventions]
        public void NoPushOnButtonDoesNothingEvenWhenBoilerIsNotEmpty(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.NOT_EMPTY);
            sut.OnNext(BrewButtonStatus.NOT_PUSHED);

            hardwareMock.Verify(
                hw => hw.SetIndicatorState(It.IsAny<IndicatorState>()),
                Times.Never());
        }

        [Theory, TestConventions]
        public void CompletingBrewCycleTurnsOnIndicator(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.NOT_EMPTY);
            sut.OnNext(BrewButtonStatus.PUSHED);
            sut.OnNext(BoilerStatus.EMPTY);

            hardwareMock.Verify(hw => hw.SetIndicatorState(IndicatorState.ON));
        }

        [Theory, TestConventions]
        public void OnNextNonEmptyBoilerDoesNotTurnOnIndicatorWhileBrewing(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.NOT_EMPTY);
            sut.OnNext(BrewButtonStatus.PUSHED);
            sut.OnNext(BoilerStatus.NOT_EMPTY);

            hardwareMock.Verify(
                hw => hw.SetIndicatorState(IndicatorState.ON),
                Times.Never());
        }

        [Theory, TestConventions]
        public void Test(
            [Frozen] Mock<ICoffeeMaker> hardwareMock,
            IndicatorLight sut)
        {
            sut.OnNext(BoilerStatus.NOT_EMPTY);
            sut.OnNext(BrewButtonStatus.PUSHED);
            sut.OnNext(BoilerStatus.EMPTY);
            sut.OnNext(BoilerStatus.EMPTY);

            hardwareMock.Verify(
                hw => hw.SetIndicatorState(IndicatorState.ON),
                Times.Once());
        }
    }
}
