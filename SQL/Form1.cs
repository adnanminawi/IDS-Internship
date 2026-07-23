using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL
{
    public partial class Form1 : Form
    {
        string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=University;Integrated Security=True;TrustServerCertificate=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();

                SqlDataAdapter daMajor = new SqlDataAdapter("SELECT Major_id, Major_Name FROM Major", conn);
                DataTable dtMajor = new DataTable();
                daMajor.Fill(dtMajor);
                cb_major.DisplayMember = "Major_Name";
                cb_major.ValueMember = "Major_id";
                cb_major.DataSource = dtMajor;

                SqlDataAdapter daCourse = new SqlDataAdapter("SELECT Course_id, Course_Name FROM Course", conn);
                DataTable dtCourse = new DataTable();
                daCourse.Fill(dtCourse);
                cb_course.DisplayMember = "Course_Name";
                cb_course.ValueMember = "Course_id";
                cb_course.DataSource = dtCourse;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load failed: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            int majorId = (int)cb_major.SelectedValue;
            int courseId = (int)cb_course.SelectedValue;
            int grade = (int)ngrade.Value;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a student name.");
                tbName.Focus();
                return;
            }

            if (cb_major.SelectedValue == null || cb_course.SelectedValue == null)
            {
                MessageBox.Show("Please select a major and a course.");
                return;
            }
            
            
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmdStudent = new SqlCommand(
                    "INSERT INTO Student (Student_Name, Major_id) VALUES (@name, @major); SELECT SCOPE_IDENTITY();", conn);
                cmdStudent.Parameters.AddWithValue("@name", name);
                cmdStudent.Parameters.AddWithValue("@major", majorId);
                int studentId = Convert.ToInt32(cmdStudent.ExecuteScalar());

                SqlCommand cmdEnroll = new SqlCommand(
                    "INSERT INTO Enrollment (Student_id,Course_id, Grade) VALUES(@student_id, @course_id, @grade);", conn);
                cmdEnroll.Parameters.AddWithValue("@student_id", studentId);
                cmdEnroll.Parameters.AddWithValue("@course_id", courseId);
                cmdEnroll.Parameters.AddWithValue("@grade", grade);
                cmdEnroll.ExecuteNonQuery();

                MessageBox.Show("Student added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Student SET AverageGrade = (SELECT AVG(Grade * 1.0) FROM Enrollment E WHERE E.Student_id= Student.Student_id);", conn);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter("SELECT Student_Name,AverageGrade FROM Student ORDER BY Student_Name;", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT S.Student_Name, C.Course_Name, M.Major_Name, E.Grade FROM Student S INNER JOIN Enrollment E ON E.Student_id= S.Student_id INNER JOIN Course C ON C.Course_id=E.course_id INNER JOIN Major M ON M.Major_id= S.Major_id ORDER BY S.Student_Name;", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed: " + ex.Message);
            }
            finally { conn.Close(); }
        }
    } 
}