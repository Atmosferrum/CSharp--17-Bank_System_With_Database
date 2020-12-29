using System;
using System.Windows;
using Bank_Independent;

namespace Bank_System.Windows
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        private int clientClassIndex; //Calss index for new Client
        private MainWindow mainWindow; //MainWindow reference

        private int deposit; //Variable to get PARSED Deposit Data

        private double percent; //Variable to get PARSED Percent Data

        private bool parsedDeposit => Int32.TryParse(TB_AddClientDeposit.Text, out deposit); //Bool to PARSE Deposit Data

        private bool parsedPercent => Double.TryParse(TB_AddClientPercent.Text, out percent); //Bool to PARSE Percent Data

        private bool depositIsValid => parsedDeposit //Bool to CHECK if Deposit Data is correct
                                    && deposit >= Bank.minDeposit
                                    && deposit <= Bank.maxDeposit;

        private bool percentIsValid => parsedPercent //Bool to CHECK if Percent Data is correct
                                    && percent >= Bank.minPercent
                                    && percent <= Bank.maxPercent;

        private bool inputDataIsCorrect => TB_AddClientName.Text != ""  //Bool to CHECK if input Data is correct
                                        && TB_AddClientLastName.Text != ""
                                        && depositIsValid
                                        && percentIsValid;

        #region Constructor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="clientClassIndex"></param>
        public AddClientWindow(MainWindow mainWindow,
                               int clientClassIndex)
        {
            InitializeComponent();

            this.clientClassIndex = clientClassIndex;
            this.mainWindow = mainWindow;
        }

        #endregion Constructor

        /// <summary>
        /// Method to ADD new Client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Clients_AddClient(object sender, RoutedEventArgs e)
        {
            

            try
            {
                if (TB_AddClientName.Text == "") throw new FormatException();

                if(TB_AddClientLastName.Text == "") throw new FormatException();

                if (!depositIsValid) throw new MyIncorrectDataException("Invalid Deposit!");

                if (!percentIsValid) throw new MyIncorrectDataException("Invalid Percent!");
            }
            catch (FormatException exception)
            {
                MessageBox.Show(exception.Message,
                                $"{AddClientWindow.TitleProperty.Name}",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch(MyIncorrectDataException exception)
            {
                MessageBox.Show(exception.Message,
                                $"{AddClientWindow.TitleProperty.Name}",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                               $"{AddClientWindow.TitleProperty.Name}",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }

            if (inputDataIsCorrect)
            {
                Bank.AddNewClient(clientClassIndex,
                                  TB_AddClientName.Text,
                                  TB_AddClientLastName.Text,
                                  Convert.ToString(deposit),
                                  Convert.ToString(percent),
                                  Convert.ToString(DateTime.Now));
                CloseWindow();
            }
        }

        /// <summary>
        /// Method to CLOSE this Window
        /// </summary>
        private void CloseWindow()
        {
            mainWindow.LoadClientsToLV();
            this.Close();
        }
    }
}
