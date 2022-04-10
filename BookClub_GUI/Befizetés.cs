using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BookClub_GUI
{
    public partial class Befizetés : Form
    {
        MySqlConnection connection = null;
        MySqlCommand sql = null;
         List<Tag> tagList = new List<Tag>() ;
        public Befizetés()
        {
            InitializeComponent();
        }
       
  
        private void adatokBetoltese()
        {
            listTagok.Items.Clear();
            listTagok.MultiColumn = false;
            dateTimePicker1.Value = DateTime.Today.AddDays(0);
            try
            {
               string myMessage = "";
                connection.Open();
                sql.CommandText = "SELECT * FROM tagok;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myMessage += dr.GetString("csaladnev") + dr.GetString("utonev") + "\n";
                        Tag tmpTag = new Tag(dr.GetInt32("id"), dr.GetString("csaladnev"), dr.GetString("utonev"));
                        tagList.Add(tmpTag);
                        listTagok.Items.Add(tmpTag);

                     
                        //   Haz uj = new Haz(dr.GetString("cim"), dr.GetInt32("id"), dr.GetInt32("alapterulet"), dr.GetString("epitesianyag"), dr.GetDateTime("mkezdete"), dr.GetDateTime("mvege"));
                        //listBox_hazak.Items.Add(uj);
                    }
                   // MessageBox.Show(myMessage);
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_rogzit_Click(object sender, EventArgs e)
        {
            
            if(listTagok.SelectedIndex == -1)
            {
              
                MessageBox.Show("Válasszon ki tagot!");
            }else if (numericUpDown1.Value < 1)
            {
               
                MessageBox.Show("Befizetett összeg nem lehet negatív");
            }
            else
            {
                
                var kiv = listTagok.SelectedItem;
                Tag kivTag = (Tag)kiv;
                string logmessage =kivTag.Id + " "+ kivTag.Csaladnev + " " + kivTag.Utonev + " " + numericUpDown1.Value + " " + dateTimePicker1.Value;
               // MessageBox.Show(logmessage);
                connection.Open();
                try
                {
                    
                    sql.CommandText = "INSERT INTO befizetes(id, datum, befizetes) VALUES(@id,  @datum, @befizetes);";
                    sql.Parameters.AddWithValue("@id", kivTag.Id);
                    sql.Parameters.AddWithValue("@datum", dateTimePicker1.Value);
                    sql.Parameters.AddWithValue("@befizetes", numericUpDown1.Value);
                    sql.ExecuteNonQuery();
                    MessageBox.Show("Sikeres hozzáadás!");
                    

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
                numericUpDown1.Value = 0;
                listTagok.SelectedIndex = -1;


            }
        }

        private void Befizetés_Load_1(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "bookclub";
            sb.CharacterSet = "utf8";
            connection = new MySqlConnection(sb.ToString());
            sql = connection.CreateCommand();

            adatokBetoltese();

        }
    }
}
