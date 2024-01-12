using EmployeeRegister.Data;
using EmployeeRegister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeRegister
{
    public partial class Form1 : Form
    {

        private User user;
        private Employee employee;
        
        public Form1(User user)
        {
            this.user = user;
            employee = new Employee();

            InitializeComponent();
            loadDataGridEmployees();
            selectUser();
            clock.Start();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
        
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            formAdd form = new formAdd(this);
            form.Show();
            /*
            try
            {

                using (var _context = new MyDbContext())
                {
                    
                   
                    Employee em = new Employee
                    {
                        Name = txtName.Text,
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        Address = rtxtAddress.Text,
                        User = _context.Users.Find(user.Id),
                        UserPicture = ImageToArray(pictureBox1.Image)

                    };
                    _context.Employees.Add(em);
                    _context.SaveChanges();
                    

                    MessageBox.Show("Employee registred");
                    
                    loadDataGridEmployees();
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
            ClearFields();
            */
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var _context = new MyDbContext())
                {
                    Employee employee = _context.Employees.Find(this.employee.Id);
                    if (employee != null)
                    {
                        employee.Name = txtName.Text;
                        employee.Email = txtEmail.Text;
                        employee.Phone = txtPhone.Text;
                        employee.Address = rtxtAddress.Text;

                        if (picSelected)
                            employee.UserPicture = ImageToArray(pictureBox1.Image);

                    }
                    else return;

                    _context.SaveChanges();
                    loadDataGridEmployees();


                    txtName.Enabled = false;
                    txtEmail.Enabled = false;
                    txtPhone.Enabled = false;
                    rtxtAddress.Enabled = false;
                    pnUpdate.Visible = true;
                    pnSaveChanges.Location = new System.Drawing.Point(119, 525);
                    pnSaveChanges.Visible = false;
                    MessageBox.Show("Updated");
                    ClearFields();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            rtxtAddress.Enabled = false;
            pnUpdate.Visible = true;
            pnSaveChanges.Location = new System.Drawing.Point(119, 525);
            pnSaveChanges.Visible = false;
        }
        private bool userSelected = false;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (userSelected)
            {
                try
                {
                    var result = MessageBox.Show("Are you sure?", "Delete ?",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (var _context = new MyDbContext())
                        {
                            Employee employee = _context.Employees.Find(this.employee.Id);
                            _context.Employees.Remove(employee);
                            _context.SaveChanges();

                            MessageBox.Show("Employee removed");
                            loadDataGridEmployees();
                            ClearFields();

                        }

                    }
                    else return;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
                MessageBox.Show("Select the employee to be deleted");

        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            using (var _context = new MyDbContext())
            {
                List<Employee> employees = _context.Employees
                    .Where(em => em.Name.Contains(txtSearch.Text) ||
                    em.Email.Contains(txtSearch.Text) ||
                    em.Phone.Contains(txtSearch.Text) ||
                    em.Address.Contains(txtSearch.Text)).ToList();

                dataGridViewEmployees.DataSource = employees;
            }
        }

        //Browse Image
        private bool picSelected = false;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os arquivos|*.*";
            openFile.Title = "Escolha uma imagem de perfil";

            if(openFile.ShowDialog() == DialogResult.OK)
            {
                string path = openFile.FileName;

                try
                {
                    pictureBox1.Image = new System.Drawing.Bitmap(path);
                    picSelected = true;

                }
                catch(Exception ex )
                {
                    MessageBox.Show("Erro ao carregar imagem: "+ ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
       
        //Select content in DataGrid
        private void dataGridViewEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selected = dataGridViewEmployees.Rows[e.RowIndex];
                this.employee = (Employee)selected.DataBoundItem as Employee;

                txtName.Text = this.employee.Name;
                txtEmail.Text = this.employee.Email;
                txtPhone.Text = this.employee.Phone;
                rtxtAddress.Text = this.employee.Address;
                userSelected = true;
                if (this.employee.UserPicture != null)
                    pictureBox1.Image = ByteToArray(employee.UserPicture);
                else
                    pictureBox1.Image = DefaultImage();
            }
            

        }
   

        //Date Time
        private void clock_Tick(object sender, EventArgs e)
        {
            DateTime timer = DateTime.Now;
            lbDateTime.Text = timer.ToString("hh:mm:ss");

        }

        //Methods

        private void selectUser()
        {
           using (var _context = new MyDbContext())
            {
                user = _context.Users.Find(user.Id);
                lbUserLogin.Text = user.UserEmail;

            }
           
        }
        public User getUser()
        {
            return (User)this.user;
        }
        public void loadDataGridEmployees()
        {
            dataGridViewEmployees.DataSource = null;
            dataGridViewEmployees.Columns.Clear();
            dataGridViewEmployees.Rows.Clear();

            using (var _context = new MyDbContext())
            {
                var employeesWithUser = _context.Employees.Include(u => u.User).ToList();

                List<Employee> employees = _context.Employees.ToList();

                dataGridViewEmployees.DataSource = employees;

                dataGridViewEmployees.Columns["Id"].Visible = false;
                dataGridViewEmployees.Columns["UserId"].Visible = false;
                dataGridViewEmployees.Columns["UserPicture"].Visible = false;
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

        private void ClearFields()
        {

            txtName.Text = null;
            txtEmail.Text = null;
            txtPhone.Text = null;
            rtxtAddress.Text = null;
            pictureBox1.Image = DefaultImage();
            picSelected = false;
            employee = new Employee();
            userSelected = false;
        }
        public Image DefaultImage()
        {
            string path = Path.Combine(Application.StartupPath, "Assets/images", "Default-welcomer.png");
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            else
            {
                MessageBox.Show("Caminho não encontrado");
                return null;
            }
        }
        public byte[] ImageToArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (image != null)
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    return ms.ToArray();
                }
                else { return null; }

            }
        }
        private Image ByteToArray(byte[] data)
        {
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    Image image = Image.FromStream(ms);

                    return image;
                }

            }
            else
            {
                return null;
            }

        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.Text = "";

        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            txtSearch.Text = "Name, Email or Phone";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtName.MaxLength > 0)
            {
                txtName.Enabled = true;
                txtEmail.Enabled = true;
                txtPhone.Enabled = true;
                rtxtAddress.Enabled = true;
                pnUpdate.Visible = false;
                pnSaveChanges.Location = new System.Drawing.Point(389, 525);
                pnSaveChanges.Visible = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
