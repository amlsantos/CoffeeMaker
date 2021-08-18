using CoffeeMaker;

namespace MakeCoffee
{
    public class CoffeeMakerInMemory : ICoofeeMaker
    {
        public WarmerPlateStatus WarmerPlateStatus { get; set; }
        public BoilerStatus BoilerStatus { get; set; }
        public BrewButtonStatus BrewButtonStatus { get; set; }
        public BoilerState BoilerState { get; set; }
        public IndicatorState IndicatorState { get; set; }
        public WarmerState WarmerState { get; set; }
        public ReliefValveState ReliefValveState { get; set; }

        public WarmerPlateStatus GetWarmerPlateStatus()
        {
            return this.WarmerPlateStatus;
        }

        public BoilerStatus GetBoilerStatus()
        {
            return this.BoilerStatus;
        }

        public BrewButtonStatus GetBrewButtonStatus()
        {
            var output = this.BrewButtonStatus;
            this.BrewButtonStatus = BrewButtonStatus.NOT_PUSHED;

            return output;
        }

        public void SetBoilerState(BoilerState s)
        {
            this.BoilerState = s;
        }

        public void SetWarmerState(WarmerState s)
        {
            this.WarmerState = s;
        }

        public void SetIndicatorState(IndicatorState s)
        {
            this.IndicatorState = s;
        }

        public void SetReliefValveState(ReliefValveState s)
        {
            this.ReliefValveState = s;
        }

    }
}
