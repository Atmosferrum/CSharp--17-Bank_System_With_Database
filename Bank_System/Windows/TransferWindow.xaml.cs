using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Bank_Independent;

namespace Bank_System.Windows
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        private MainWindow mainWindow; //MainWindow Reference
        private Client fromClient; //Client to Transfer from
        private int clientClassIndex; //Client index to GET its Department

        private float Amount; //Amount to TRANSSFER
        private float From; //Deposit FROM wich will GIVE Transfer
        private float FromResult; //Result for Client wich GIVES Transfer
        private float To; //Deposit TO wich will GET transfer
        private float ToResult; //Result for Client wich GETS Transfer

        private bool parsedAmount => float.TryParse(TB_AmountToTransfer.Text, out Amount); //Bool to PARSE Amount

        private bool amountIsValid => parsedAmount //Bool to CHECK if Amount Data is correct
                                   && Amount > 0
                                   && Amount <= From;

        private bool selectedClient => CB_ToClient.SelectedIndex > -1; //Bool to CHECK if there's selected Client

        private bool inputDataIsCorrect => selectedClient  //Bool to CHECK if input Data is correct
                                        && amountIsValid;
        //&& TB_AmountToTransfer.Text != null
        //&& TB_AmountToTransfer.Text != "";

        private Department<Client> allClients = new Department<Client>("Temp"); //List of ALL Clients for ComboBox


        #region Constructor;

        /// <summary>
        /// Constgructor for TransferWindow
        /// </summary>
        /// <param name="MainWindow"></param>
        /// <param name="FromClient"></param>
        /// <param name="ClientClassIndex"></param>
        public TransferWindow(MainWindow MainWindow,
                  Client FromClient,
                  int ClientClassIndex)
        {
            InitializeComponent();

            this.mainWindow = MainWindow;
            this.fromClient = FromClient;
            this.clientClassIndex = ClientClassIndex;

            TB_FromClient.Text = $"{fromClient.Name} {fromClient.LastName} {fromClient.Balance} $";

            foreach (Department<Client> allDepartments in Bank.Departments[0].Departments)
            {
                for (int i = 0; i < allDepartments.Count(); i++)
                {
                    allClients.Add(allDepartments[i]);
                }
            }

            From = fromClient.Balance;

            CB_ToClient.ItemsSource = allClients;
        }

        #endregion Constructor

        #region Element's Mthods;

        /// <summary>
        /// Button Method to TRANSFER money
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Clients_Transfer(object sender, RoutedEventArgs e)
        {
            if (inputDataIsCorrect)
            {
                Bank.Departments[0].Departments[clientClassIndex].Transfer(fromClient,
                                                                          (CB_ToClient.SelectedItem as Client),
                                                                          Amount);
                CloseWindow();
            }
            else
            {
                string message = !parsedAmount ? "Please input only NUMBERS!"
                               : !amountIsValid ? "Please input MORE than 0 and LESS then deposit of Client you're trying to transfer from!"
                               : !selectedClient ? "Please selec Client to recive transfer!"
                               : "The DATA you are entering is wrong!";

                MessageBox.Show(message,
                $"{TransferWindow.TitleProperty.Name}",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// ComboBox Method on SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_ToClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            To = (CB_ToClient.SelectedItem as Client).Balance;

            if (TB_AmountToTransfer.Text != null &&
                TB_AmountToTransfer.Text != "")
                ShowResults();
        }

        /// <summary>
        /// TextBlock Method on SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_AmountToTransfer_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TB_AmountToTransfer.Text != null &&
                TB_AmountToTransfer.Text != "")
                ShowResults();
        }

        #endregion Element's Mthods

        #region Methods;

        /// <summary>
        /// Method to SHOW Transfer Results
        /// </summary>
        private void ShowResults()
        {
            try
            {
                if (!selectedClient) throw new FormatException();
                else if (!parsedAmount) throw new MyIncorrectDataException("Please input only NUMBERS!");
                else if (!amountIsValid) throw new MyIncorrectDataException("Please input MORE than 0 and LESS then deposit of Client you're trying to transfer from!");
            }
            catch (FormatException exception)
            {
                MessageBox.Show(exception.Message,
                                $"{AddClientWindow.TitleProperty.Name}",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            catch (MyIncorrectDataException exception)
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
                FromResult = From - Amount;
                ToResult = To + Amount;
                TB_AmountResult.Text = $"{From} - {Amount} = {FromResult} -> {To} + {Amount} = {ToResult}";
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

        #endregion Methods
    }
}
