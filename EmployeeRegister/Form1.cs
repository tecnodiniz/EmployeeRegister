using EmployeeRegister.Data;
using EmployeeRegister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeRegister
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
            clock.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    MessageBox.Show("successfully connected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Error: "+ex.Message);
                }
            }
        }
        private void loadDataGridEmployees()
        {
            using (var _context = new MyDbContext())
            {
                var employeesWithUser = _context.Employees.Include(u => u.User).ToList();

                List<Employee> employees = _context.Employees.ToList();

                dataGridViewEmployees.DataSource = employees;

                dataGridViewEmployees.Columns["Id"].Visible = false;
                dataGridViewEmployees.Columns["UserId"].Visible = false;
                dataGridViewEmployees.Columns["User"].HeaderText = "Registrado por";

                dataGridViewEmployees.CellFormatting += (sender, e) =>
                {

                    if (e.ColumnIndex == dataGridViewEmployees.Columns["User"].Index && e.RowIndex >= 0 && e.RowIndex < employees.Count)
                    {
                        Employee employee = employees[e.RowIndex];  
                        if (employee.User is EmployeeRegister.Model.User)
                        {
                            e.Value = ((EmployeeRegister.Model.User)employee.User).UserEmail;

                        }

                    }
                };
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           loadDataGridEmployees();
            using(var context = new MyDbContext())
            {
                try
                {

                    User user = context.Users.Find(1);
                    lbUserLogin.Text = user.UserEmail;
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            DateTime timer = DateTime.Now;
            lbDateTime.Text = timer.ToString("hh:mm:ss");

        }
    }
}
