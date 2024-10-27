using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT8
{
    public partial class Form1 : Form
    {
        private int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.studentsTableAdapter.Fill(this.schoolDBDataSet.Students);
            UpdateStudentDetails();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentName = textBox1.Text;
            int studentAge = int.Parse(textBox2.Text);
            string studentMajor = comboBox1.SelectedItem.ToString();

            var newRow = schoolDBDataSet.Students.NewStudentsRow();
            newRow.FullName = studentName;
            newRow.Age = studentAge;
            newRow.Major = studentMajor;

            schoolDBDataSet.Students.Rows.Add(newRow);

            studentsTableAdapter.Update(schoolDBDataSet.Students);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = schoolDBDataSet.Students;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int studentId = (int)row.Cells[0].Value;
                    var studentRow = schoolDBDataSet.Students.FindByStudentId(studentId);
                    if (studentRow != null)
                    {
                        studentRow.Delete();
                    }
                }
                studentsTableAdapter.Update(schoolDBDataSet.Students);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = schoolDBDataSet.Students;
            }
            else
            {
                MessageBox.Show("Hãy chọn SV để xóa.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int studentId = (int)selectedRow.Cells[0].Value;
                var studentRow = schoolDBDataSet.Students.FindByStudentId(studentId);

                if (studentRow != null)
                {
                    textBox1.Text = studentRow.FullName;
                    textBox2.Text = studentRow.Age.ToString();
                    comboBox1.SelectedItem = studentRow.Major;

                    studentRow.FullName = textBox1.Text;
                    studentRow.Age = int.Parse(textBox2.Text);
                    studentRow.Major = comboBox1.SelectedItem.ToString();

                    studentsTableAdapter.Update(schoolDBDataSet.Students);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = schoolDBDataSet.Students;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn SV để sửa");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentIndex < schoolDBDataSet.Students.Rows.Count - 1)
            {
                currentIndex++;
                UpdateStudentDetails();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateStudentDetails();
            }
        }

        private void UpdateStudentDetails()
        {
            if (schoolDBDataSet.Students.Rows.Count > 0)
            {
                var studentRow = schoolDBDataSet.Students[currentIndex];
                textBox1.Text = studentRow.FullName;
                textBox2.Text = studentRow.Age.ToString();
                comboBox1.SelectedItem = studentRow.Major;

                dataGridView1.ClearSelection();
                dataGridView1.Rows[currentIndex].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = currentIndex;
            }
        }
    }
}
