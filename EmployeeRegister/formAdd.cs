using EmployeeRegister.Data;
using EmployeeRegister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeRegister
{
    public partial class formAdd : Form
    {
        Form1 form;
        public formAdd(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os arquivos|*.*";
            openFile.Title = "Escolha uma imagem de perfil";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string path = openFile.FileName;

                try
                {
                   textBox1.Text = path;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
               
                using (var _context = new MyDbContext())
                {
                    Image image;
                    if (textBox1.Text.Length != 0)
                        image = new System.Drawing.Bitmap(textBox1.Text);

                    else
                        image = form.DefaultImage();

                    Employee em = new Employee
                    {
                        Name = txtName.Text,
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        Address = rtxtAddress.Text,
                        User = _context.Users.Find(form.getUser().Id),
                        UserPicture = form.ImageToArray(image)
                        

                    };
                    _context.Employees.Add(em);
                    _context.SaveChanges();


                    MessageBox.Show("Employee registred");

                    form.loadDataGridEmployees();
                    this.Close();
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Handle exceptions related to entity validation
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Erro de validação: {validationError.ErrorMessage}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void formAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
