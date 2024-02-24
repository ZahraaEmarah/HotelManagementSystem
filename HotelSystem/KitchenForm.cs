using HotelSystem.ReservationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem
{
    public partial class KitchenForm : Form
    {
        FRONTEND_RESERVATIONContext context;

        public KitchenForm()
        {
            InitializeComponent();
            context = new FRONTEND_RESERVATIONContext();
            this.FormClosed += (sender, e) => context?.Dispose();

            context.Reservation.Load();
        }

        private void KitchenForm_Load(object sender, EventArgs e)
        {
            var kitchenInfo = (from k in context.Reservation
                               where k.SupplyStatus == false
                              select k).ToList();
            listBoxOnline.DataSource = kitchenInfo;
            listBoxOnline.DisplayMember = kitchenInfo.ToString();

            firstnametxt.DataBindings.Add("Text", kitchenInfo, "FirstName");
            lastnametxt.DataBindings.Add("Text", kitchenInfo, "LastName");
            textBoxPhone.DataBindings.Add("Text", kitchenInfo, "PhoneNumber");
            RoomTypecomboBox.DataBindings.Add("Text", kitchenInfo, "RoomType");
            FloorcomboBox.DataBindings.Add("Text", kitchenInfo, "RoomFloor");
            roomNoComboBox.DataBindings.Add("Text", kitchenInfo, "RoomNumber");

            textBoxBQ.DataBindings.Add("Text", kitchenInfo, "BreakFast");
            textBoxLQ.DataBindings.Add("Text", kitchenInfo, "Lunch");
            textBoxDQ.DataBindings.Add("Text", kitchenInfo, "Dinner");

            checkBoxCleaning.DataBindings.Add("Checked", kitchenInfo, "Cleaning");
            checkBoxTowels.DataBindings.Add("Checked", kitchenInfo, "Towel");
            checkBoxSurprise.DataBindings.Add("Checked", kitchenInfo, "SSurprise");
            checkBoxFood.DataBindings.Add("Checked", kitchenInfo, "SupplyStatus");

            dataGridView1.DataSource = kitchenInfo;

            dataGridView1.Columns.OfType<DataGridViewColumn>().ToList().ForEach(col => col.Visible = false);

            dataGridView1.Columns["Id"].Visible = true;
            dataGridView1.Columns["FirstName"].Visible = true;
            dataGridView1.Columns["LastName"].Visible = true;
            dataGridView1.Columns["PhoneNumber"].Visible = true;
            dataGridView1.Columns["RoomType"].Visible = true;
            dataGridView1.Columns["RoomFloor"].Visible = true;
            dataGridView1.Columns["RoomNumber"].Visible = true;
            dataGridView1.Columns["BreakFast"].Visible = true;
            dataGridView1.Columns["Lunch"].Visible = true;
            dataGridView1.Columns["Dinner"].Visible = true;
            dataGridView1.Columns["Cleaning"].Visible = true;
            dataGridView1.Columns["Towel"].Visible = true;
            dataGridView1.Columns["SSurprise"].Visible = true;
            dataGridView1.Columns["SupplyStatus"].Visible = true;
            dataGridView1.Columns["FoodBill"].Visible = true;
        }

        private void buttonFoodSelection_Click(object sender, EventArgs e)
        {
            context.SaveChanges();
            MessageBox.Show("Updated Successfully");
        }
    }
}
