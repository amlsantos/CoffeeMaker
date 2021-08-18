using System;

namespace CoffeeMaker
{
    public class WarmerPlate : IObserver<WarmerPlateStatus>
    {
        private readonly ICoffeeMaker _hardware;
        public WarmerPlate(ICoffeeMaker hardware)
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

        public void OnNext(WarmerPlateStatus value)
        {
            if (value == WarmerPlateStatus.POT_NOT_EMPTY)
                this._hardware.SetWarmerState(WarmerState.ON);
            else
                this._hardware.SetWarmerState(WarmerState.OFF);
        }
    }
}
