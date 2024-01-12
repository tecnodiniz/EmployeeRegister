using EmployeeRegister.Data;
using EmployeeRegister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeRegister
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var _context = new MyDbContext())
                {
                    if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                    {
                        User user = _context.Users.Where(u => u.userLogin == textBox1.Text && u.UserPassword == textBox2.Text).First();
                        if (user != null)
                        {
                            Form1 form = new Form1(user);
                            form.Show();
                            this.Hide();
                            
                        }
                    }
                    else
                        MessageBox.Show("Preencha todos os campos");

                
                        
                }
            }catch (Exception ex)
            {
                MessageBox.Show("User or Password invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Text = null;
                textBox2.Text = null;
            }
        }
    }
}
