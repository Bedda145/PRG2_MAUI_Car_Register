using System.Text.RegularExpressions;

namespace PRG_MAUI_Car_Register.Model
{
    abstract class Vehicle
    {
        // Medlemsvariabler
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;

        private const int FirstProductionYear = 1895;

        private int year;

        // Konstruktor (en metod med samma namn som klassen, som returnerar ett objekt)
        protected Vehicle() // en konstruktor kan, men måste inte, ta parametrar
        {
        }

        // Get-Set för att hålla variablerna privata, och för att validera inkommande värden från UI (user interface, användargränssnittet)
        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ett registeringsnummer måste anges.");

                if (value.Length != 6)
                    throw new ArgumentException("Ett registeringsnummer måste bestå av exakt 6 tecken, tre bokstäver, två siffror och en siffra eller bokstav.");

                for (int i = 0; i < 3; i++)
                {
                    if (!char.IsLetter(value[i]))
                        throw new ArgumentException("Inkorrekt registreringsnummer: De första tre tecknen måste vara bokstäver.");
                }

                for (int i = 3; i < 5; i++)
                {
                    if (!char.IsDigit(value[i]))
                        throw new ArgumentException("Inkorrekt registreringsnummer: Det fjärde och femte tecknet måste vara siffror.");
                }
                if (!char.IsDigit(value[5]) && !char.IsLetter(value[5]))
                    throw new ArgumentException("Inkorrekt registeringsnummer: Det sjätte tecknet måste vara en siffra eller en bokstav.");

                registrationNumber = value.ToUpper();
            }
        }

        

        public string Model
        {
            get { return model; }
            set { model = ValidateName(value, "Modell"); }
        }

        public string Year
        {
            get { return year == 0 ? string.Empty : year.ToString(); }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("En årsmodell måste anges, till exempel 2024.");

                string input = value.Trim();

                if (!Regex.IsMatch(input, @"^[1-2][0-9][0-9][0-9]$"))
                    throw new ArgumentException("Årsmodellen måste skrivar som fyra siffror, till exempel 2024.");

                int inputYear = int.Parse(input);
                int currentYear = DateTime.Now.Year;

                if (inputYear < FirstProductionYear || inputYear > currentYear)
                    throw new ArgumentException($"Årsmodellen måste ligga mellan {FirstProductionYear} och {currentYear}.");

                year = inputYear;


            }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = ValidateName(value, "Märke"); }
        }

        private string ValidateName(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} måste anges.");

            string input = value.Trim();

            if (input.Length > 30)
                throw new ArgumentException($"{fieldName} får vara högst 30 tecken");

            bool hasLetter = false;

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                    hasLetter = true;
                else if (!char.IsDigit(c) && c != ' ' && c != '-')
                    throw new ArgumentException($"{fieldName} får bara innehålla bokstäver, siffror, mellanslag och bindestreck. Tecknet '{c}' är inte tillåtet.");
            }

            if (!hasLetter)
                throw new ArgumentException($"{fieldName} måste innehålla minst en bokstav.");

            return input;
        }

        public abstract string getDescription();
        public override string ToString()
        {
            return $"{registrationNumber}\t{manufacturer}\t{model}\t{Year}";
        }
    }
}
