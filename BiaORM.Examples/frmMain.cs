using BiaORM.Examples.Classes;
using BiaORM.MySQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiaORM.Examples
{
    public partial class frmMain : Form
    {
        IConnector connector = null;
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["BiaORM"];
            connector = new MyConnection(connectionString);

            Global.DB = (MyConnection)connector;
            Global.DB.OnExecuteQuery += DB_OnExecuteQuery;
            Global.DB.OnOpenConnection += DB_OnOpenConnection;
            Global.DB.OnCloseConnection += DB_OnCloseConnection;
            Global.DB.OnCommited += DB_OnCommited;
            Global.DB.OnRollBack += DB_OnRollBack;
        }

        private void DB_OnRollBack()
        {
            txtOutput.AppendText("Rollback" + Environment.NewLine);
        }

        private void DB_OnCommited()
        {
            txtOutput.AppendText("Comitado" + Environment.NewLine);
        }

        private void DB_OnCloseConnection()
        {
            txtOutput.AppendText("Conexão fechada" + Environment.NewLine);
        }

        private void DB_OnOpenConnection()
        {
            txtOutput.AppendText("Conexão aberta" + Environment.NewLine);
        }

        private void DB_OnExecuteQuery(string query)
        {
            txtOutput.AppendText(query + Environment.NewLine);
        }

        private void BtnCreateSchool_Click(object sender, EventArgs e)
        {
            try
            {
                School school = new School();
                school.Name = "Centro Educacional Nossa Senhora das Graças 2";
                school.CreateOrUpdate();
                Global.DB.Commit();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
            
        }
    }
}
