    using PRG_MAUI_Car_Register.Model;

    namespace PRG_MAUI_Car_Register.View
    {
    public partial class MainPage : ContentPage
    {
        List<Vehicle> vehicleList = new List<Vehicle>();

        public MainPage()
        {
            InitializeComponent();
            pickerType.SelectedIndex = 0;
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                Vehicle vehicle;

                switch (pickerType.SelectedIndex)
                {
                    case 0:
                        vehicle = new Car();
                        break;
                    case 1:
                        vehicle = new MC();
                        break;
                    case 2:
                        vehicle = new Truck();
                        break;
                    default:
                        throw new ArgumentException("Välj en fordonstyp.");
                }

                vehicle.RegistrationNumber = entryRegistrationNumber.Text; ;
                vehicle.Manufacturer = entryManufacturer.Text;
                vehicle.Model = entryModel.Text;
                vehicle.Year = entryYear.Text;

                vehicleList.Add(vehicle);
                listViewVehicles.ItemsSource = null;
                listViewVehicles.ItemsSource = vehicleList;

                ClearTextFields();
            }

            // här "fångas" eventuella felmeddelanden från Vehicle
            catch (ArgumentException ex)
            {
                DisplayAlert("Fel", ex.Message, "OK");
            }
        }

        private void OnRadioCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value != true) return;

            IEnumerable<Vehicle> filteredList;

            if (radioCar.IsChecked)
            {
                filteredList = vehicleList.OfType<Car>();
            }
            else if (radioMC.IsChecked)
            {
                filteredList = vehicleList.OfType<MC>();
            }
            else if (radioTruck.IsChecked)
            {
                filteredList = vehicleList.OfType<Truck>();
            }
            else
            {
                // Om "Alla" är vald, visa hela listan
                filteredList = vehicleList;
            }

            listViewVehicles.ItemsSource = filteredList;
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string searchTerm = entrySearchRegistrationNumber.Text?.ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                entrySearchRegistrationNumber.Text = "Ange ett registreringsnummer för att söka.";
                return;
            }

            var foundVehicle = vehicleList.FirstOrDefault(v => v.RegistrationNumber?.ToLower() == searchTerm);

            if (foundVehicle != null)
            {
                labelSearchResult.Text = $"Fordon hittat:\n" +
                                         $"Registreringsnummer: {foundVehicle.RegistrationNumber}\n" +
                                         $"Tillverkare: {foundVehicle.Manufacturer}\n" +
                                         $"Modell: {foundVehicle.Model}\n" +
                                         $"Årsmodell: {foundVehicle.Year}\n" +
                                         $"Typ: {foundVehicle.VehicleType}";
            }
            else
            {
                labelSearchResult.Text = "Inget fordon hittades med det registreringsnumret.";
            }
        }

        private void ClearTextFields()
        {
            entryRegistrationNumber.Text = string.Empty;
            entryManufacturer.Text = string.Empty;
            entryModel.Text = string.Empty;
            entryYear.Text = string.Empty;
        }
    }
}
