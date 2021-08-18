using System;

namespace CoffeeMaker
{
    public class Boiler : IObserver<BrewButtonStatus>, IObserver<BoilerStatus>
    {
        private readonly ICoofeeMaker _hardware;
        private bool hasWater;

        public Boiler(ICoofeeMaker hardware)
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

            if (!this.hasWater)
                this._hardware.SetBoilerState(BoilerState.OFF);
        }

        public void OnNext(BrewButtonStatus value)
        {
            if (this.hasWater && value == BrewButtonStatus.PUSHED)
                this._hardware.SetBoilerState(BoilerState.ON);
        }
    }
}
