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
using System.Collections;

namespace AdminUI
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=Projektas7;database=tvarkarastisdata");
        String[] kas = new String[50];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void duombaze_Click(object sender, EventArgs e)
        {

        }

        private void idetiButton_Click(object sender, EventArgs e)
        {
            string insertQuerry = "INSERT INTO tvarkarastis ( `grupe`,`kursas`, `data`, `dalykas`,`destytojas`,`pradzia`,`pabaiga`,`auditorija` ) VALUES ('" + grupeDrop.Text + "','" + kursasDrop.SelectedItem + "', '" + dataPicker.Text + "', '" + dalykasDrop.SelectedItem + "','" + destytojasPicker.SelectedItem + "', '" + pradziaPicker.Text + "','" + pabaigaPicker.Text + "','" + auditorijaDrop.SelectedItem + "');";

            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuerry, connection);
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Irasas ikeltas");
            }
            else
            {
                MessageBox.Show("Irasas neikeltas");
            }

            connection.Close();
        }

        private void ziuretiButton_Click(object sender, EventArgs e)
        {
            string selectQuerry = "SELECT * FROM tvarkarastis WHERE `data` = '" + dateTimePicker1.Text + "' AND `grupe` = '" + grupeDrop.Text + "' AND `kursas` = '" + kursasDrop.Text + "'";
            MySqlCommand cmd = new MySqlCommand(selectQuerry, connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            int k = 0;
            while (reader.Read())
            {
                kas[i] = (reader.GetString("id"));
                i++;
                kas[i] = (reader.GetString("dalykas"));
                i++;
                kas[i] = (reader.GetString("pradzia"));
                i++;
                kas[i] = (reader.GetString("pabaiga"));
                i++;
                kas[i] = (reader.GetString("auditorija"));
                i++;
                k++;
            }

            pask1.Text = kas[1];
            pask2.Text = kas[6];
            pab1.Text = kas[3];
            pab2.Text = kas[8];
            prad1.Text = kas[2];
            prad2.Text = kas[7];
            aud1.Text = kas[4];
            aud2.Text = kas[9];


            connection.Close();
        }

        private void pasirinktiButton_Click(object sender, EventArgs e)
        {


            string deleteQuerry = "DELETE FROM tvarkarastis WHERE ID = " + kas[0] + " ";
            MySqlCommand cmd = new MySqlCommand(deleteQuerry, connection);
            if (checkBox1.Checked == true)
            {
                try
                {
                    connection.Open();


                    if (MessageBox.Show("Ar tikrai norit istrinti irasa?", "Istrinti", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sekmingai istrinta");
                            ;
                        }
                    }

                    connection.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();

                }
            }
        }
    }
}
