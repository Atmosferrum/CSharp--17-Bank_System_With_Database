using System;

namespace Bank_Independent
{
    public class Client : IEquatable<Client>
    {
        #region Fields;

        private static int ID = 0; //ID for all Clients
        private int clientID; //ID of Current Client
        private DateTime dateOfDeposit; //DateOfDeposit field

        #endregion Fields

        #region Constructor;

        /// <summary>
        /// Constructor for Client
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="LastName"></param>
        /// <param name="Deposit"></param>
        /// <param name="Percent"></param>
        /// <param name="DateOfDeposit"></param>
        public Client(string Name,
                      string LastName,
                      int Deposit,
                      float Percent,
                      DateTime DateOfDeposit)
        {
            this.clientID = NextID();
            this.Name = Name;
            this.LastName = LastName;
            this.Deposit = Deposit;
            this.Percent = Percent;
            this.DateOfDeposit = DateOfDeposit;
            this.Status = this.GetType().Name;
            this.DepartmentID = Status == "Royal" ? 3 : (Status == "Aristocrat" ? 2 : 1);
        }

        #endregion Constructor

        #region Properties;

        public int ClientID { get { return this.clientID; } } //Property to GET Client ID
        public int DepartmentID { get; private set; } //Property to GET or SET DepartmentID
        public string Status { get; private set; } //Property to GET or SET Status
        public string Name { get; set; } //Property to GET or SET Name
        public string LastName { get; set; } //Property to GET or SET LastName
        public int Deposit { get; set; } //Property to GET or SET Deposit
        public float Percent { get; set; } //Property to GET or SET Percent
        public float Accumulation { get; set; } //Property to GET or SET Accumulation
        public float Balance { get; set; } //Property to GET or SET Total Client's Balance        

        public DateTime DateOfDeposit //Department Property
        {
            get { return this.dateOfDeposit; }
            set { this.dateOfDeposit = value; CountAccumulation(); }
        }

        private int NextID()
        {
            ID++;
            return ID;
        }

        #endregion Properties

        #region Methods;

        /// <summary>
        /// Method to COUNT number of Months
        /// between DayOfDeposit and DateTime.Now
        /// </summary>
        /// <returns></returns>
        private int NumberOfMonths()
        {
            return Math.Abs(DateTime.Now.Month - this.dateOfDeposit.Month)
                           + 12 * (DateTime.Now.Year - this.dateOfDeposit.Year);
        }

        /// <summary>
        /// Methodto COUNT Acummulation
        /// </summary>
        private void CountAccumulation()
        {
            if (this.Deposit >= 1_000)
            {
                
                float total = this.Deposit;

                for (int i = 0; i < NumberOfMonths(); i++)
                {
                    float midTotal = 0;

                    midTotal += (total / 100 * Percent);
                    total += midTotal;
                }

                this.Accumulation = (float)Math.Round((total - this.Deposit) * 100) / 100;
            }
            else
            {
                float total = 0;

                for (int i = 0; i < NumberOfMonths(); i++)
                {
                    total += ((float)this.Deposit / 100 * Percent);
                }

                this.Accumulation = (float)Math.Round(total * 100) /100;
            }

            CountBalance();
        }

        private void CountBalance()
        {
            this.Balance = this.Deposit + this.Accumulation;
        }

        #endregion Methods

        #region Interfaces;

        /// <summary>
        /// Method for IEquatable Interface
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Client other)
        {
            return this.Name == other.Name
                && this.LastName == other.LastName
                && this.Deposit == other.Deposit
                && this.Percent == other.Percent
                && this.Accumulation == other.Accumulation
                && this.DateOfDeposit == other.DateOfDeposit;
        }

        #endregion Interfaces
    }
}
