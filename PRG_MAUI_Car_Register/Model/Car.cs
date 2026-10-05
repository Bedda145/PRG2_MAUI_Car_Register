namespace PRG_MAUI_Car_Register.Model
{
    internal class Car : Vehicle
    {
        private int doors;

        public int Doors
        {
            get { return doors; }

            set
            {
                if (value < 2 || value > 5)
                    throw new ArgumentException("En bil måste ha mellan 2 och 5 dörrar.");

                doors = value;
            }
        }

        public override string getDescription()
        {
            return $"{RegistrationNumber}\tBil\t{Manufacturer}\t{Model}\t{Year}";
        }
    }
}
