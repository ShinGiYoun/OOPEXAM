using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exam0612
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            
            InitializeComponent();
        }
        int selectedRow;
        public void LoadData()
        {
            string sql = "";
            MySqlConnection con = new MySqlConnection(sql);
            MySqlCommand cmd_db = new MySqlCommand("SELECT * FROM sales_tb;", con);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd_db;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //텍스트 박스 초기화
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
            {
                string constring = "";
                string Query = "INSERT INTO pos_dataset.item_tb (i_name,i_price,i_count) value ('" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.textBox4.Text + "')";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
                MySqlDataReader myReader;

                try
                {
                    conDataBase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("재고 추가 완료");

                    while (myReader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("항목을 정확히 입력해주세요");
                    MessageBox.Show(ex.Message);
                }

                LoadData();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("검색 정보를 입력해주세요");
            }
            else
            {
                string valueToSearch = textBox1.Text.ToString();
                searchData(valueToSearch);
                //텍스트 박스 초기화
                textBox1.Text = "";
            }
        }
        private void dataGridView1_CellClick1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            textBox2.Text = row.Cells[0].Value.ToString();
            textBox3.Text = row.Cells[1].Value.ToString();
            textBox4.Text = row.Cells[2].Value.ToString();
            textBox5.Text = row.Cells[3].Value.ToString();
        }
        MySqlConnection connection = new MySqlConnection("");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table = new DataTable();
        public void searchData(string valueToSearch)
        {
            string query = "SELECT * FROM sales_tb WHERE CONCAT(`name`, `price`, `count`, `total`) like '%" + valueToSearch + "%'";
            command = new MySqlCommand(query, connection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {   
            string constring = "";

            if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("항목을 정확히 입력해주세요");
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            else
            {
                decimal price = decimal.Parse(textBox4.Text);
                decimal count = decimal.Parse(textBox5.Text);
                

                //업데이트를 통해 DB로 수정된 데이터 전송 - 기본키 기준으로 수정위치 탐색
                string Query = "update sales_tb set no ='" + this.textBox2.Text + "',name='" + this.textBox3.Text + "',price='" + this.textBox4.Text + "'," +
                    "count='" + this.textBox5.Text + "' where no ='" + this.textBox2.Text + "'";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
                MySqlDataReader myReader;

                try
                {
                    conDataBase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("수정완료");

                    while (myReader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string constring = "";
            if (textBox1.Text == "")
            {
                MessageBox.Show("삭제 할 항목을 찾지 못했습니다.");
            }
            else
            {
                //delete를 통해 DB로 삭제된 데이터 전송 - 기본키 기준으로 삭제위치 탐색
                string Query = "delete from sales_tb where no ='" + this.textBox1.Text + "';";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
                MySqlDataReader myReader;

                try
                {
                    conDataBase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("삭제완료");

                    while (myReader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                LoadData();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
    
}
