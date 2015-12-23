using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WinceSQLite20120808
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 创建一个空数据库
            string DbName = "test.db";
            SQLiteConnection.CreateFile(DbName);
            MessageBox.Show("数据库创建完成。");

            // 连接数据库
            string connStr = "data source=" + DbName + ";Pooling=true;FailIfMissing=false";
            SQLiteConnection conn = new SQLiteConnection(connStr);
            conn.Open();
            MessageBox.Show("数据库连接完成。");

            // 创建表
            SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            string sql = "CREATE TABLE test(username varchar(20),password varchar(20))";
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            MessageBox.Show("数据表创建完成。");

            // 插入数据
            sql = "INSERT INTO test VALUES('dotnetthink','mypassword')";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("插入数据完成。");

            // 取出数据
            sql = "SELECT * FROM test";
            cmd.CommandText = sql;
            System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                sb.Append("username:").Append(reader.GetString(0)).Append("\n");
                sb.Append("password:").Append(reader.GetString(1));
            }
            MessageBox.Show(sb.ToString());

        }
    }
}