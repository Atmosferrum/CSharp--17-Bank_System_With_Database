using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bank_Independent
{
    public class Department<T> : ICollection<T>, IEnumerable<T>
        where T : Client
    {
        public ObservableCollection<Department<Client>> Departments { get; set; } //Collection of inner Departments

        protected T[] items; //Array of Items in this.Collection.Generic
        protected int size; //Size of this.Colletion.Generic

        public event Action<Client, Client, float> TransferNotification;

        #region Constructor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name"></param>
        public Department(string Name)
        {
            this.Name = Name;
            this.items = new T[0];
            this.size = 0;
            this.Departments = new ObservableCollection<Department<Client>>();
        }

        #endregion Constructor

        #region Properties;

        /// <summary>
        /// Property to GET Name
        /// </summary>
        public string Name { get; }

        #endregion Properties

        #region Indexers;

        /// <summary>
        /// Indexer by [i] = int
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index > size)
                    throw new IndexOutOfRangeException("Index is out of range !");
                else
                    return this.items[index];
            }
            set
            {
                if (index > size)
                    throw new IndexOutOfRangeException("Index is out of range !");
                else
                    this.items[index] = value;
            }
        }

        /// <summary>
        /// Indexer by Client Data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="deposit"></param>
        /// <param name="percent"></param>
        /// <param name="dateOfDeposit"></param>
        /// <returns></returns>
        public Client this[string name, string lastName, int deposit, double percent, DateTime dateOfDeposit]
        {
            get
            {
                Client client = null;
                foreach (var item in this.items)
                {
                    if ((item as Client).Name == name
                     && (item as Client).LastName == lastName
                     && (item as Client).Deposit == deposit
                     && (item as Client).Percent == percent
                     && (item as Client).DateOfDeposit == dateOfDeposit)
                    { client = item as Client; break; }
                }
                return client;
            }
        }

        #endregion Indexers

        #region Methods;

        /// <summary>
        /// Method to ADD new Item in this.Collection.Generic
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (size >= items.Length)
                ResizeArray();

            this.items[size] = item;
            this.size++;
        }

        /// <summary>
        /// Method to Resize this.Collection.Generic
        /// </summary>
        private void ResizeArray()
        {
            Array.Resize(ref this.items, size + 1);
        }

        /// <summary>
        /// Method to CLEAR this.Collection.Generic
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                this.items[i] = default(T);
            }

            this.size = 0;
            T[] temp = new T[size];

            this.items = temp;
        }

        /// <summary>
        /// Method to REMOVE Item from this.Collection.Generic
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            int tempI = 0;

            for (int i = 0; i < size; i++)
            {
                if (items[i].Equals(item))
                {
                    items[i] = default(T);
                    tempI = i;
                    size--;
                }
            }

            for (int i = tempI; i < size; i++)
            {
                items[i] = items[i + 1];
            }

            items[size] = default(T);
        }

        /// <summary>
        /// Method to EDIT Item in this.Collection.Generic
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="newItem"></param>
        public void Edit(T oldItem, T newItem)
        {
            for (int i = 0; i < size; i++)
            {
                if (items[i].Equals(oldItem))
                    items[i] = newItem;
            }
        } 

       /// <summary>
       /// Method to TRANSFER money between given Clients
       /// </summary>
       /// <param name="fromClient">Client taht GIVES Transfer</param>
       /// <param name="toClient">Client that TAKES Transfer</param>
       /// <param name="amount">Amlount of Transfer</param>
        public void Transfer(Client fromClient, Client toClient, float amount)
        {
            fromClient.Balance -= amount;
            toClient.Balance += amount;

            TransferNotification?.Invoke(fromClient, toClient, amount);
        }

        /// <summary>
        /// Methd to FIND Client in this.Collection.Generic
        /// </summary>
        /// <param name="clientToFind"></param>
        /// <returns></returns>
        public Client Find(Client clientToFind)
        {
            Client defaultClient = default(T);

            foreach(Client currentClient in items)
            {
                if (currentClient.Name == clientToFind.Name
                && currentClient.LastName == clientToFind.LastName
                && currentClient.Deposit == clientToFind.Deposit
                && currentClient.Percent == clientToFind.Percent
                && currentClient.Accumulation == clientToFind.Accumulation
                && currentClient.DateOfDeposit == clientToFind.DateOfDeposit)
                    defaultClient = currentClient;
            }

            return defaultClient;
        }

        #endregion Methods

        #region Interfaces;

        /// <summary>
        /// IEnumerator implementation
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return items.Take(size).GetEnumerator();
        }

        /// <summary>
        /// IEnumerator implementation
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        #endregion Interfaces       
    }
}
