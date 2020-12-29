using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private static SqlConnectionStringBuilder ConnectionCreator()
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
                   $"[DepartmentID] INT);";
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

        public static void Insert(string status,
                                  string name,
                                  string lastName,
                                  int deposit,
                                  float percent,
                                  float accummmulation,
                                  float balance,
                                  int departmentID)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();
                  
                        using (var cmd = new SqlCommand(InsertClient(status,
                                                                     name,
                                                                     lastName,
                                                                     deposit,
                                                                     percent,
                                                                     accummmulation,
                                                                     balance,
                                                                     departmentID),
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

        private static string InsertDepartment(string name)
        {
            DepartmentID++;

            return $"INSERT INTO [dbo].[Departments] (" +
                   $"[DepartmentID]," +
                   $"[DepartmentName]) " +
                   $"VALUES (" +
                   $"{DepartmentID}," +
                   $"'{name}')";
        }

        private static string InsertClient(string status,
                                           string name,
                                           string lastName,
                                           float deposit,
                                           float percent,
                                           float accummulation,
                                           float balance,
                                           int departmentID)
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
                   $"[DepartmentID]) " +
                   $"VALUES (" +
                   $"'{ClientID}'," +
                   $"'{status}'," +
                   $"'{name}'," +
                   $"'{lastName}'," +
                   $"{deposit}," +
                   $"{percent}," +
                   $"{accummulation}," +
                   $"{balance}," +
                   $"{departmentID})";
        }

        private static void Show()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionCreator().ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(SelectDepartment(), connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0],4}" +
                                          $"{reader[1],10}");
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

        private static string SelectDepartment()
        {
            return $"SELECT" +
                   $"[dbo].[Departments].[DepartmentID] as 'ID'," +
                   $"[dbo].[Departments].[DepartmentName] as 'NAME'" +
                   $"FROM [dbo].[Departments];";
        }
    }
}
