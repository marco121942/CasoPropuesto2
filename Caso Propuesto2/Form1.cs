using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace Caso_Propuesto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["caso2"].ConnectionString);
        private void Label1_Click(object sender, EventArgs e)
        {

        }
        public void ListaClientes()
        {
            using (SqlDataAdapter df = new SqlDataAdapter("Listado_Clientes", cn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Clientes");
                    DgClientes.DataSource = Da.Tables["Clientes"];
                    txtTotal.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }
        public void Listar()
        {
            using (SqlDataAdapter df = new SqlDataAdapter("ClientesSelect", cn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataTable Da = new DataTable())
                {
                    df.Fill(Da);


                    // clientes.DataSource = Da.Tables[0];
                    for (int i =0; i<Da.Rows.Count; i++) {
                        clientes.Items.Add(Da.Rows[i]["NombreContacto"]);
                    }
                   
                   // clientes.Text = Da.Tables["Clientes"];


                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes();
            Listar();
        }
        public void BusquedaClientes()
        {
            using (SqlDataAdapter Df = new SqlDataAdapter("Buscar_Cliente", cn))
            {
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.Value = clientes.Text;
                sqlParameter.SqlDbType = SqlDbType.VarChar;
                sqlParameter.Size = 100;
                sqlParameter.ParameterName = "@NombreContacto";

                Df.SelectCommand.Parameters.Add(sqlParameter);
                Df.SelectCommand.CommandType = CommandType.StoredProcedure;

                using (DataSet Da = new DataSet())
                {
                    Df.Fill(Da, "Clientes");
                    DgClientes.DataSource = Da.Tables["Clientes"];
                    txtTotal.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            BusquedaClientes();

        }
    }
}
