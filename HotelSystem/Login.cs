using Dapper;
using HotelSystem.LoginContext;
using HotelSystem.LoginEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem
{
    public partial class Login : Form
    {
        LOGINContext context;

        public Login()
        {
            InitializeComponent();
            context = new LOGINContext();
            this.FormClosed += (sender, e) => context?.Dispose();
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {
            var admin = context.Frontend.Select(a => a).ToList();
            var kitchen = context.Kitchen.Select(k => k).ToList();

            //Dapper Throws a SQLException Login failed for user "."
            //SqlConnection SqlCn = new SqlConnection("Data Source=.; Initial Catalog=LOGIN_MANAGER; Integrated Security=true");
            //List<Kitchen> kitchenList = SqlCn.Query<Kitchen>("SELECT * FROM kitchen")?.ToList() ?? new();


            foreach (var str in admin)
            {
                if(usernametxtbox.Text == str.UserName)
                {
                    if(passwordtxtbox.Text == str.PassWord)
                    {
                        this.Hide();
                        FrontendForm f = new FrontendForm();
                        f.Show();                  
                    }
                }
            }

            foreach (var str in kitchen)
            {
                if (usernametxtbox.Text == str.UserName)
                {
                    if (passwordtxtbox.Text == str.PassWord)
                    {
                        this.Hide();
                        KitchenForm f = new KitchenForm();
                        f.Show();
                    }
                }
            }

            ErrorMsgLabel.Visible = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            context.Frontend.Load();
            context.Kitchen.Load();


        }
    }
}
