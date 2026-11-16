using System.Collections.ObjectModel;
using System.Windows.Input;
using PRG_MAUI_Car_Register.Model;

namespace PRG_MAUI_Car_Register.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Vehicle> allVehicles = new ObservableCollection<Vehicle>();

        private string registrationNumber = string.Empty;
        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set { SetProperty(ref registrationNumber, value); }
        }

        private string vehicleModel = string.Empty;
        public string VehicleModel
        {
            get { return vehicleModel; }
            set { SetProperty(ref vehicleModel, value); }
        }

        private string year = string.Empty;
        public string Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        public List<string> Brands { get; } = new List<string>
        {
            "Volvo (Bil)",
            "Fiat (Bil)",
            "Yamaha (MC)",
            "Ducati (MC)",
            "Scania (Lastbil)",
            "Mercedes-Benz (Lastbil)"
        };

        private int selectedBrandIndex = 0;
        public int SelectedBrandIndex
        {
            get { return selectedBrandIndex; }
            set { SetProperty(ref selectedBrandIndex, value); }
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get { return searchText; }
            set { SetProperty(ref searchText, value); }
        }

        private IEnumerable<Vehicle> visibleVehicles;
        public IEnumerable<Vehicle> VisibleVehicles
        {
            get { return visibleVehicles; }
            set { SetProperty(ref visibleVehicles, value); }
        }

        private string searchResult = "Sökresultat visas här...";
        public string SearchResult
        {
            get { return searchResult; }
            set { SetProperty(ref searchResult, value); }
        }

        private string errorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand FilterCommand { get; }

        public MainPageViewModel()
        {
            visibleVehicles = allVehicles;

            RegisterCommand = new Command(Register);
            SearchCommand = new Command(Search);
            FilterCommand = new Command<string>(Filter);
        }

        private void Register()
        {
            try
            {
                Vehicle vehicle = CreateVehicle(SelectedBrandIndex);

                vehicle.RegistrationNumber = RegistrationNumber;
                vehicle.Model = VehicleModel;
                vehicle.Year = Year;

                allVehicles.Add(vehicle);
                Filter("Alla");
                ClearFields();
                ErrorMessage = string.Empty;
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private Vehicle CreateVehicle(int brandIndex)
        {
            switch (brandIndex)
            {
                case 0:
                    return new Volvo();
                case 1:
                    return new Fiat();
                case 2:
                    return new Yamaha();
                case 3:
                    return new Ducati();
                case 4:
                    return new Scania();
                case 5:
                    return new MercedesBenz();
                default:
                    throw new ArgumentException("Välj ett märke.");
            }
        }

        private void Filter(string category)
        {
            switch (category)
            {
                case "Bil":
                    VisibleVehicles = allVehicles.OfType<Car>();
                    break;
                case "MC":
                    VisibleVehicles = allVehicles.OfType<MC>();
                    break;
                case "Lastbil":
                    VisibleVehicles = allVehicles.OfType<Truck>();
                    break;
                default:
                    VisibleVehicles = allVehicles;
                    break;
            }
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                SearchResult = "Ange ett registreringsnummer för att söka.";
                return;
            }

            Vehicle? foundVehicle = allVehicles.FirstOrDefault(
                v => v.RegistrationNumber.Equals(SearchText.Trim(), StringComparison.OrdinalIgnoreCase));

            if (foundVehicle != null)
            {
                SearchResult = $"Fordon hittat:\n{foundVehicle.GetDescription()}\n" +
                               $"Antal hjul: {foundVehicle.GetWheelCount()}\n" +
                               $"Körkort som krävs: {foundVehicle.GetRequiredLicense()}";
            }
            else
            {
                SearchResult = "Inget fordon hittades med det registreringsnumret.";
            }
        }

        private void ClearFields()
        {
            RegistrationNumber = string.Empty;
            VehicleModel = string.Empty;
            Year = string.Empty;
        }
    }
}