using System;

namespace CoffeeMaker
{
    public class IndicatorLight : IObserver<BoilerStatus>, IObserver<BrewButtonStatus>
    {
        private readonly ICoffeeMaker _hardware;
        private bool hasWater;
        private bool isBrewing;

        public IndicatorLight(ICoffeeMaker hardware)
        {
            if (hardware == null)
                throw new ArgumentNullException("hardware");

            this._hardware = hardware;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(BoilerStatus value)
        {
            this.hasWater = (value == BoilerStatus.NOT_EMPTY);

            if (this.isBrewing && value == BoilerStatus.EMPTY)
            {
                this._hardware.SetIndicatorState(IndicatorState.ON);  // the coffee is ready
                this.isBrewing = false;
            }
        }

        public void OnNext(BrewButtonStatus value)
        {
            if (this.hasWater && value == BrewButtonStatus.PUSHED)
            {
                this._hardware.SetIndicatorState(IndicatorState.OFF);  // the coffee is not ready
                this.isBrewing = true;
            }
        }
    }
}
