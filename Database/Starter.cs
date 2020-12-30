using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Starter
    {
        const string dbName = "BankDB"; //Name of DB
        const string dataSource = @"(localdb)\MSSQLLocalDB"; //Name of DataSource for DB

        private static int DepartmentID = 0; //Counter for DepartmentID
        private static int ClientID = 0; //Counter for ClientID

        /// <summary>
        /// Method to Start DB Initialization   
        /// </summary>
        public static void Start()
        {
            CreateDatabase(dbName);
            AddTables();
        }

        /// <summary>
        /// Method to CREATE Sql Connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnectionStringBuilder ConnectionCreator()
        {
            SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder()
            {
                DataSource = dataSource,
                InitialCatalog = dbName,
                IntegratedSecurity = true,
                Pooling = true
            };

            return connection;
        }

        /// <summary>
        /// Method to CREATE DB
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dbName"></param>
        private static void CreateDatabase(string dbName)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand($"If(db_id(N'{dbName}') IS NULL) CREATE DATABASE [{dbName}]", connection))
                        cmd.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Method to ADD Tables in DB
        /// </summary>
        /// <param name="connectionString"></param>
        private static void AddTables()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand(CreateDepartmentsTable(), connection))
                        cmd.ExecuteNonQuery();

                    using (var cmd = new SqlCommand(CreateClientsTable(), connection))
                        cmd.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Method to CREATE String for Departments Table creation
        /// </summary>
        /// <returns></returns>
        private static string CreateDepartmentsTable()
        {
            return $"DROP TABLE IF EXISTS Departments;" +
                   $"CREATE TABLE [dbo].[Departments] ( " +
                   $"[DepartmentID] INTEGER PRIMARY KEY, " +
                   $"[DepartmentName] NVARCHAR(255));";
        }

        /// <summary>
        /// Method to CREATE String for Clients Table creation
        /// </summary>
        /// <returns></returns>
        private static string CreateClientsTable()
        {
            return $"DROP TABLE IF EXISTS Clients;" +
                   $"CREATE TABLE [dbo].[Clients] ( " +
                   $"[ClientID] INTEGER PRIMARY KEY, " +
                   $"[Status] NVARCHAR(255), " +
                   $"[Name] NVARCHAR(255), " +
                   $"[LastName] NVARCHAR(255), " +
                   $"[Deposit] REAL, " +
                   $"[Percent] REAL, " +
                   $"[Accummulation] REAL, " +
                   $"[Balance] REAL, " +
                   $"[DepartmentID] INT," +
                   $"[DateOfDeposit] DATETIME);";
        }

        public static void Insert(string name)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand(InsertDepartment(name),
                                                    connection))
                        cmd.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("---INSERT---");
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Method to INSERT Data in DB
        /// </summary>
        /// <param name="status"></param>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="deposit"></param>
        /// <param name="percent"></param>
        /// <param name="accummmulation"></param>
        /// <param name="balance"></param>
        /// <param name="departmentID"></param>
        public static void Insert(string status,
                                  string name,
                                  string lastName,
                                  int deposit,
                                  float percent,
                                  float accummmulation,
                                  float balance,
                                  int departmentID,
                                  DateTime dateOfDeposit)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(InsertClient(status,
                                                                 name,
                                                                 lastName,
                                                                 deposit,
                                                                 percent,
                                                                 accummmulation,
                                                                 balance,
                                                                 departmentID,
                                                                 dateOfDeposit),
                                                                 connection))
                        command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("---INSERT---");
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Method to INSERT Department to DB
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string InsertDepartment(string name)
        {
            DepartmentID++;

            return $"INSERT INTO [dbo].[Departments] (" +
                   $"[DepartmentID]," +
                   $"[DepartmentName]) " +
                   $"VALUES (" +
                   $"'{DepartmentID}'," +
                   $"'{name}')";
        }

        /// <summary>
        /// Method to INSERT Client in DB
        /// </summary>
        /// <param name="status"></param>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="deposit"></param>
        /// <param name="percent"></param>
        /// <param name="accummulation"></param>
        /// <param name="balance"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        private static string InsertClient(string status,
                                           string name,
                                           string lastName,
                                           float deposit,
                                           float percent,
                                           float accummulation,
                                           float balance,
                                           int departmentID,
                                           DateTime dateOfDeposit)
        {
            ClientID++;

            return $"INSERT INTO [dbo].[Clients] (" +
                   $"[ClientID]," +
                   $"[Status]," +
                   $"[Name]," +
                   $"[LastName]," +
                   $"[Deposit]," +
                   $"[Percent]," +
                   $"[Accummulation]," +
                   $"[Balance]," +
                   $"[DepartmentID]," +
                   $"[DateOfDeposit]) " +
                   $"VALUES (" +
                   $"'{ClientID}'," +
                   $"'{status}'," +
                   $"'{name}'," +
                   $"'{lastName}'," +
                   $"'{deposit}'," +
                   $"'{percent}'," +
                   $"'{accummulation}'," +
                   $"'{balance}'," +
                   $"'{departmentID}'," +
                   $"'{dateOfDeposit}')";
        }

        /// <summary>
        /// Method to SHOW data
        /// </summary>
        public static void Show()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SelectFromDB(), connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0],5} | " +
                                          $"{reader[1],10} |" +
                                          $"{reader[2],10} |" +
                                          $"{reader[3],10} |" +
                                          $"{reader[4],10} |" +
                                          $"{reader[5],5} |" +
                                          $"{reader[6],10} |" +
                                          $"{reader[7],10} |" +
                                          $"{reader[8],30} |" +
                                          $"{reader[9],10} |");
                    }

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.Write("---SHOW---");
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Method to CREATE String to Show Data
        /// </summary>
        /// <returns></returns>
        public static string SelectFromDB()
        {
            return $"SELECT" +
                   $"[dbo].[Clients].[ClientID] as 'ID'," +
                   $"[dbo].[Clients].[Status] as 'STATUS'," +
                   $"[dbo].[Clients].[Name] as 'NAME'," +
                   $"[dbo].[Clients].[LastName] as 'LASTNAME'," +
                   $"[dbo].[Clients].[Deposit] as 'DEPOSIT'," +
                   $"[dbo].[Clients].[Percent] as 'PERCENT'," +
                   $"[dbo].[Clients].[Accummulation] as 'ACCUMMULATION'," +
                   $"[dbo].[Clients].[Balance] as 'BALANCE'," +
                   $"[dbo].[Departments].[DepartmentName] as 'DEPARTMENT'," +
                   $"[dbo].[Clients].[DateOfDeposit] as 'DATEOFDEPOSIT'" +
                   $"FROM " +
                   $"[Departments], [Clients]" +
                   $"WHERE" +
                   $"[Clients].[DepartmentID] = [Departments].[DepartmentID]" +
                   $"ORDER BY [Clients].[Balance]";
        }

        /// <summary>
        /// Method to UPDATE Client Data in DB
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="deposit"></param>
        /// <param name="percent"></param>
        /// <param name="accummulation"></param>
        /// <param name="balance"></param>
        /// <param name="departmentID"></param>
        /// <param name="dateOfDeposit"></param>
        public static void UpdateClient(int clientID,
                                           string name,
                                           string lastName,
                                           float deposit,
                                           float percent,
                                           float accummulation,
                                           float balance,
                                           int departmentID,
                                           DateTime dateOfDeposit)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    var sql = $"UPDATE [dbo].[Clients] " +
                              $"SET " +
                              $"[Name] = '{name}'," +
                              $"[LastName] = '{lastName}'," +
                              $"[Deposit] = '{deposit}'," +
                              $"[Percent] = '{percent}'," +
                              $"[Accummulation] = '{accummulation}'," +
                              $"[Balance] = '{balance}'," +
                              $"[DepartmentID] = '{departmentID}'," +
                              $"[DateOfDeposit] = '{dateOfDeposit}' " +
                              $"WHERE [ClientID] = {clientID}";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("---UPDATE---");
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Method to REMOVE Client from DB
        /// </summary>
        /// <param name="ID">ID of Client to REMOVE</param>
        public static void RemoveClient(int ID)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    var sql = $"DELETE FROM [Clients] WHERE [ClientID] = {ID}";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("---REMOVE---");
                Console.WriteLine(exception.Message);
            }            
        }
    }
}
