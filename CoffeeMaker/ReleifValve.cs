using System;

namespace CoffeeMaker
{
    public class ReleifValve : IObserver<WarmerPlateStatus>
    {
        private readonly ICoofeeMaker _hardware;

        public ReleifValve(ICoofeeMaker hardware)
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
            if (value == WarmerPlateStatus.WARMER_EMPTY)
                this._hardware.SetReliefValveState(ReliefValveState.OPEN);
            else
                this._hardware.SetReliefValveState(ReliefValveState.CLOSED);
        }
    }
}
