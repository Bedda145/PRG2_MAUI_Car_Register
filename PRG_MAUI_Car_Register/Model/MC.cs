namespace PRG_MAUI_Car_Register.Model
{
    internal class MC : Vehicle
    {
        private string category = string.Empty;

        public string Category
        {
            get { return category; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("En MC måste ha en kategori, till exempel Sport eller Touring.");

                category = value.Trim();
            }
        }

        public override string GetDescription()
        {
            return $"{RegistrationNumber}\tMC\t{Manufacturer}\t{Model}\t{Year}";
        }
    }
}
