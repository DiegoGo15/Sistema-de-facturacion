﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MiLibreria;

namespace PracticaProyecto
{
    public partial class VentanaLogin : FormBase
    {
        public VentanaLogin()
        {
            InitializeComponent();
        }

        public static string Codigo = "";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Consulta para inicio de sesión
                string CMD = string.Format("Select * FROM Usuarios Where account='{0}' and password='{1}'", txtNomAcc.Text.Trim(), txtPass.Text.Trim());

                DataSet ds = Utilidades.Ejecutar(CMD);

                Codigo = ds.Tables[0].Rows[0]["id_usuario"].ToString().Trim();

                string cuenta = ds.Tables[0].Rows[0]["account"].ToString().Trim();
                string contra = ds.Tables[0].Rows[0]["password"].ToString().Trim();

                if (cuenta == txtNomAcc.Text.Trim() && contra == txtPass.Text.Trim())
                {
                    if(Convert.ToBoolean(ds.Tables[0].Rows[0]["Status_admin"]) == true)
                    {
                        VentanaAdmin VenAd = new VentanaAdmin();
                        this.Hide();
                        VenAd.Show();
                    }
                    else
                    {
                        VentanaUser VenUs = new VentanaUser();
                        this.Hide();
                        VenUs.Show();
                    }
                }
            }
#pragma warning disable CS0168 // La variable 'error' se ha declarado pero nunca se usa
            catch (Exception error)
#pragma warning restore CS0168 // La variable 'error' se ha declarado pero nunca se usa
            {
                MessageBox.Show("Usuario o contraseña incorrecta...");
            }
        }

        private void VentanaLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
