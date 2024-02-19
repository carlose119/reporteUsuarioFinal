using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace reporte.DataSetSp
{
    public class sp
    {
        private readonly IConfiguration configuration;
        public sp(IConfiguration config)
        {
            configuration = config;
        }
        public DataSet getSpProductos()
        {
            //con sqlserver
            /* using (SqlConnection conn = new SqlConnection(connection))
            {
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("MyProcedure", conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                return dataset;
            } */

            //con mysql
            using (MySqlConnection conn = new MySqlConnection(configuration.GetConnectionString("cnDB")))
            {
                DataSet dataset = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //se puede pasar parametros aqui.
                /* command.Parameters.Add("@usn", VarChar).Value = textBoxUsername.Text;
                command.Parameters.Add("@pass", VarChar).Value = textBoxPassword.Text; */
                adapter.SelectCommand = new MySqlCommand("obtenerProductos", conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                return dataset;
            }
        }
    }
}
