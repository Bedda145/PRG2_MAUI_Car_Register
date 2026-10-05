namespace PRG_MAUI_Car_Register.Model
{
    internal class Truck : Vehicle
    {
        private double loadCapacity;

        public double LoadCapacity
        {
            get { return loadCapacity; }

            set
            {
                if (value <= 0)
                    throw new ArgumentException("Lastkapaciteten måste vara större än 0 ton.");

                loadCapacity = value;
            }
        }

        public override string GetDescription()
        {
            return $"{RegistrationNumber}\tLastbil\t{Manufacturer}\t{Model}\t{Year}";
        }
    }
}
