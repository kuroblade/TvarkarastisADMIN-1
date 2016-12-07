﻿using System;
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
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=tvarkarastisdata");
        String[] kas = new String[50];
        String pasirinktas;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Width = 875 + monthCalendar1.Width + 10;
            selectedDateLabel.Width = monthCalendar1.Width;
            issaugotiButton.Visible = false;
            dinksta();

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

            string selectQuerry = "SELECT * FROM tvarkarastis WHERE `data` = '" + dataPicker.Text + "' AND `grupe` = '" + grupeDrop.Text + "' AND `kursas` = '" + kursasDrop.Text + "'";
            Array.Clear(kas, 0, kas.Length);
            rodymas(selectQuerry);
        }

        private void monthCalendar1_MouseUp(object sender, MouseEventArgs e)
        {
            Array.Clear(kas, 0, kas.Length);
            selectedDateLabel.Text = monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd");
            string selectQuerry = "SELECT * FROM tvarkarastis WHERE `data` = '" + selectedDateLabel.Text + "' AND `grupe` = '" + grupeDrop.Text + "' AND `kursas` = '" + kursasDrop.Text + "'";
            rodymas(selectQuerry);  
        }

        private void trintiButton_Click(object sender, EventArgs e)
        {
            radio();
            string deleteQuerry = "DELETE FROM tvarkarastis WHERE ID = " + pasirinktas + " ";
            MySqlCommand cmd = new MySqlCommand(deleteQuerry, connection);

                try
                {
                    connection.Open();

                    if (MessageBox.Show("Ar tikrai norit istrinti irasa?", "Istrinti", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sekmingai istrinta");
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }

            string selectQuerry = "SELECT * FROM tvarkarastis WHERE `data` = '" + selectedDateLabel.Text + "' AND `grupe` = '" + grupeDrop.Text + "' AND `kursas` = '" + kursasDrop.Text + "'";
            Array.Clear(kas, 0, kas.Length);
            rodymas(selectQuerry);
        }
        private void dinksta()
        {
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
        }
        private void rodymas(string sel)
        {
            MySqlCommand cmd = new MySqlCommand(sel, connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            int k = 0;
            dinksta();
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
            for(int j=0;j<k;j++)
            {
                if (j == 0) radioButton1.Visible = true;
                if (j==1) radioButton2.Visible = true;
                if(j==2) radioButton3.Visible = true;
                if(j==3) radioButton4.Visible = true;
                if(j==4) radioButton5.Visible = true;
            }
            radioButton1.Tag = kas[0];
            pask1.Text = kas[1];
            prad1.Text = kas[2];
            pab1.Text = kas[3];
            aud1.Text = kas[4];
            radioButton2.Tag = kas[5];
            pask2.Text = kas[6];
            prad2.Text = kas[7];
            pab2.Text = kas[8];
            aud2.Text = kas[9];
            radioButton3.Tag = kas[10];
            pask3.Text = kas[11];
            prad3.Text = kas[12];
            pab3.Text = kas[13];
            aud3.Text = kas[14];
            radioButton4.Tag = kas[15];
            pask4.Text = kas[16];
            prad4.Text = kas[17];
            pab4.Text = kas[18];
            aud4.Text = kas[19];
            radioButton5.Tag = kas[20];
            pask5.Text = kas[21];
            prad5.Text = kas[22];
            pab5.Text = kas[23];
            aud5.Text = kas[24];

            connection.Close();
        }

        private void radio()
        {
            if (radioButton1.Checked)
            {
                pasirinktas = radioButton1.Tag.ToString();
                
            }
            else if (radioButton2.Checked)
            {
                pasirinktas = radioButton2.Tag.ToString();
            }
            else if (radioButton3.Checked)
            {
                pasirinktas = radioButton3.Tag.ToString();
            }
            else if (radioButton4.Checked)
            {
                pasirinktas = radioButton4.Tag.ToString();
            }
            else if (radioButton5.Checked)
            {
                pasirinktas = radioButton5.Tag.ToString();
            };
        }

        private void issaugotiButton_Click(object sender, EventArgs e)
        {
            radio();
            string updatequary = " UPDATE tvarkarastis SET grupe ='" + grupeDrop.Text + "' , kursas='" + kursasDrop.SelectedItem + "' , data=  '" + dataPicker.Text + "' , dalykas='" + dalykasDrop.SelectedItem + "' , destytojas= '" + destytojasPicker.SelectedItem + "' , pradzia= '" + pradziaPicker.Text + "' , pabaiga = '" + pabaigaPicker.Text + "' ,  auditorija= '" + auditorijaDrop.SelectedItem + "' WHERE `id` = '" + pasirinktas + "'";
            MySqlCommand cmd = new MySqlCommand(updatequary, connection);
            try
            {
                connection.Open();

                if (MessageBox.Show("Ar tikrai norit pakeisti irasa?", "pakeisti", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Sekmingai pakeistas");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }

            string selectQuerry = "SELECT * FROM tvarkarastis WHERE `data` = '" + dataPicker.Text + "' AND `grupe` = '" + grupeDrop.Text + "' AND `kursas` = '" + kursasDrop.Text + "'";
            Array.Clear(kas, 0, kas.Length);
            rodymas(selectQuerry);
        }

        private void redaguotiButton_Click(object sender, EventArgs e)
        {
            radio();
            string inputquary = "SELECT * FROM tvarkarastis WHERE `id` = '" + pasirinktas + "'" ;
            MySqlCommand cmd = new MySqlCommand(inputquary, connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                grupeDrop.SelectedItem = reader.GetString("grupe");
                kursasDrop.SelectedItem = (reader.GetString("kursas"));
                dataPicker.Value = Convert.ToDateTime(reader.GetString("data"));
                dalykasDrop.SelectedItem = (reader.GetString("dalykas"));
                destytojasPicker.SelectedItem = (reader.GetString("destytojas"));
                pradziaPicker.Value = Convert.ToDateTime(reader.GetString("pradzia"));
                pabaigaPicker.Value = Convert.ToDateTime(reader.GetString("pabaiga"));
                auditorijaDrop.SelectedItem = (reader.GetString("auditorija"));
             
            }
            connection.Close();
            issaugotiButton.Visible = true;
        }
    }
}
