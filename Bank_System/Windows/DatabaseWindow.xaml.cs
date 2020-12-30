using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Database;

namespace Bank_System.Windows
{
    /// <summary>
    /// Interaction logic for DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter dataAdapter;
        DataTable dataTable;

        public DatabaseWindow()
        {
            InitializeComponent();
            ConnectToDB();
        }

        public void ConnectToDB()
        {
            connection = new SqlConnection(Starter.ConnectionCreator().ConnectionString);
            dataTable = new DataTable();
            dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = new SqlCommand(Starter.SelectFromDB(), connection);

            dataAdapter.Fill(dataTable);

            DG_Database_View.DataContext = dataTable.DefaultView;
        }
    }
}
