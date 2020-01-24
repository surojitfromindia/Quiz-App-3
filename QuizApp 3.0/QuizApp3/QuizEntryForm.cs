using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp3
{
    public partial class QuizEntryForm : Form
    {
        private int lastIndexPos = 0;// store  last selected index of lstQList. (default "zero")
        OleDbConnection conn;
        OleDbDataAdapter adpt;
        OleDbCommand cmd;
        DataSet ds;
        private string type;

       
        /**entry code for Multiple question type datas*/
        private void edForM(String key)
        {
            string q, o1, o2, o3, o4, oC, id; //store encripted text/values
            q = encript(txtQ.Text, key); //encripted value of quesion
            o1 = encript(txtO1.Text, key);//encripted value of option 1
            o2 = encript(txtO2.Text, key);//encripted value of option 2
            o3 = encript(txtO3.Text, key);//encripted value of option 3
            o4 = encript(txtO4.Text, key);//encripted value of option 4
            oC = encript(txtCorrectOption.Text, key);//encripted value of correnct ans
            id = tID.Text;// question id/order (not encripted)
            string query = //insert sql query
                "insert into qTable values(" +
                id + ",'" + //id
                q + "','" +//question
                o1 + "','" +//option 1
                o2 + "','" +//option 2
                o3 + "','" +//option 3
                o4 + "','" +//option 4
                oC + "','M')";//Correct Ans ,
            //'M' is the Type of Quesion, 'M' For Multiple Choice, 'TF' For true/false
            cmd = new OleDbCommand(query, conn);//create the command for query 
            cmd.ExecuteNonQuery();//execute the sql query
            updateDsTable();//(update the database, after new entry) >>look the method
            clean();//clear the field >>look the method
        }
        /**entry code for True/False question type datas*/
        private void edForTF(String key)
        {
            //same look method >>'edForM'
            string q, o1, o2, oC, id; 
            q = encript(txtQ.Text, key); 
            o1 = encript(txtO1.Text, key);
            o2 = encript(txtO2.Text, key);  
            oC = encript(txtCorrectOption.Text, key);
            id = tID.Text;
            string query = 
                "insert into qTable (ID,Qes,op1,op2,corr,qty) values(" +
                id + ",'" +
                q + "','" +
                o1 + "','" +
                o2 + "','" +              
                oC + "','TF')";
            cmd = new OleDbCommand(query, conn);
            cmd.ExecuteNonQuery();
            updateDsTable();
            clean();
        }
        /**data showing code for Multiple type question*/
        private void showForTF()
        {
            string mQ, mo1, mo2, mCo, mId;//store the values from database/dataset
            string key = "new";// for encription/decrp key
            string id = lstQList.SelectedItem.ToString();//store the current index of listbox
            checkBox3.Enabled = false; // for true/false type question disable other two textbox
            checkBox4.Enabled = false;// for true/false type question disable other two textbox
            txtO3.Enabled = false;// for true/false type question disable other two textbox
            txtO4.Enabled = false;// for true/false type question disable other two textbox
            string query = "select * from qTable where id =" + id + "";//sql
            adpt = new OleDbDataAdapter(query, conn);
            ds = new DataSet();
            adpt.Fill(ds);
            mId = ds.Tables[0].Rows[0][0].ToString();//store the values in variables
            mQ = ds.Tables[0].Rows[0][1].ToString();//store the values in variables
            mo1 = ds.Tables[0].Rows[0][2].ToString();//store the values in variables
            mo2 = ds.Tables[0].Rows[0][3].ToString();//store the values in variables
            mCo = ds.Tables[0].Rows[0][6].ToString();//store the values in variables
            txtQ.Text = decript(mQ, key);//set/show the value of textboxes
            txtO1.Text = decript(mo1, key);//set/show the value of textboxes
            txtO2.Text = decript(mo2, key);//set/show the value of textboxes
            txtO3.Text = "";//null/empty because True/False only have two option
            txtO4.Text = "";//null/empty because True/False only have two option
            txtCorrectOption.Text = decript(mCo, key);//set/show the value of textboxes
            tID.Text = id;//id
            lastIndexPos = lstQList.SelectedIndex;//store current index in a global variable
        }
        /**data showing code for True/False type question*/
        private void showForM()
        {
            string mQ, mo1, mo2, mo3, mo4, mCo, mId;//store the values from database/dataset
            string key = "new";// for encription/decrp key
            string id = lstQList.SelectedItem.ToString();//store the current index of listbox
            checkBox3.Enabled = true;//enable all checkboxes and textbox , if it was disable by 'showForTF()' 
            checkBox4.Enabled = true; ;//enable all checkboxes and textbox , if it was disable by 'showForTF()' 
            txtO3.Enabled = true; ;//enable all checkboxes and textbox , if it was disable by 'showForTF()' 
            txtO4.Enabled = true; ;//enable all checkboxes and textbox , if it was disable by 'showForTF()' 
            string query = "select * from qTable where id =" + id + "";//sql
            adpt = new OleDbDataAdapter(query, conn);
            ds = new DataSet();
            adpt.Fill(ds);
            mId = ds.Tables[0].Rows[0][0].ToString();//store the values in variables
            mQ = ds.Tables[0].Rows[0][1].ToString();//store the values in variables
            mo1 = ds.Tables[0].Rows[0][2].ToString();//store the values in variables
            mo2 = ds.Tables[0].Rows[0][3].ToString();//store the values in variables
            mo3 = ds.Tables[0].Rows[0][4].ToString();//store the values in variables
            mo4 = ds.Tables[0].Rows[0][5].ToString();//store the values in variables
            mCo = ds.Tables[0].Rows[0][6].ToString();//store the values in variables
            txtQ.Text = decript(mQ, key);//set/show the value of textboxes
            txtO1.Text = decript(mo1, key);//set/show the value of textboxes
            txtO2.Text = decript(mo2, key);//set/show the value of textboxes
            txtO3.Text = decript(mo3, key);//set/show the value of textboxes
            txtO4.Text = decript(mo4, key);//set/show the value of textboxes
            txtCorrectOption.Text = decript(mCo, key);//set/show the value of textboxes
            tID.Text = id;//id
            lastIndexPos = lstQList.SelectedIndex;//store current index in a global variable
        }
        /**update code for multiple type question*/
        private void updateforM()
        {
            string query = "update qTable set Qes='" + encript(txtQ.Text, "new") + "', "
               + " op1='" + encript(txtO1.Text, "new") +
               "', op2='" + encript(txtO2.Text, "new") +
               "', op3='" + encript(txtO3.Text, "new") +
               "' ,op4='" + encript(txtO4.Text, "new") +
               "', corr='" + encript(txtCorrectOption.Text, "new") +
               "' where id=" + tID.Text + "";
            if ((checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked) == true)

            {
                cmd = new OleDbCommand(query, conn);
                cmd.ExecuteNonQuery();
                CheckBox[] chk = { checkBox1, checkBox2, checkBox3, checkBox4 };
                uncheckedAll(chk);
                MessageBox.Show("Upadated");
            }
            else
            {
                MessageBox.Show("Fill All And Choose At Least One Option");
            }
        }
        /**update code for True/False type question*/
        private void updateforTF()
        {
            string query = "update qTable set Qes='" + encript(txtQ.Text, "new") + "', "
              + " op1='" + encript(txtO1.Text, "new") +
              "', op2='" + encript(txtO2.Text, "new") +
              "', corr='" + encript(txtCorrectOption.Text, "new") +
              "' where id=" + tID.Text + "";
            if ((checkBox1.Checked || checkBox2.Checked) == true)
            {
                cmd = new OleDbCommand(query, conn);
                cmd.ExecuteNonQuery();
                CheckBox[] chk = { checkBox1, checkBox2 };
                uncheckedAll(chk);
                MessageBox.Show("Upadated");
            }
            else
            {
                MessageBox.Show("Fill All And Choose At Least One Option");
            }
        }
        /**clean all text from all textbox*/
        private void clean()
        {
            txtQ.Clear();
            txtO1.Clear();
            txtO2.Clear();
            txtO3.Clear();
            txtO4.Clear();
            txtCorrectOption.Clear();
            CheckBox[] k = { checkBox1, checkBox2, checkBox3, checkBox4 };
            uncheckedAll(k);
        }
        /** set the correct answear*/
        private void setCorrect(CheckBox correct, TextBox txtCorrect, CheckBox[] otherbox)
        {
            if (correct.Checked)
            {
                txtCorrectOption.Text = "";
                txtCorrectOption.Text = txtCorrect.Text;
                uncheckedAll(otherbox);
            }
        }
        /** unchecked all checkboxs*/
        private void uncheckedAll(CheckBox[] otherbox)
        {
            foreach (CheckBox chk in otherbox)  chk.Checked = false;
        }
        /** update dataset*/
        private void updateDsTable()
        {
            adpt = new OleDbDataAdapter("select * from qTable", conn);
            ds = new DataSet();
            adpt.Fill(ds);
            tID.Text = (ds.Tables[0].Rows.Count + 1).ToString();

            lstQList.Items.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lstQList.Items.Add(ds.Tables[0].Rows[i][0]);
            }
           
        }

        /*all bool method*/
        private bool checkForMultiple()
        {
            bool ans = true;

            if (string.IsNullOrEmpty(txtQ.Text) || string.IsNullOrEmpty(txtO1.Text) || string.IsNullOrEmpty(txtO2.Text) ||
                string.IsNullOrEmpty(txtO3.Text) || string.IsNullOrEmpty(txtO4.Text) || string.IsNullOrEmpty(txtCorrectOption.Text))
                ans = false;
            else
                ans = true;
            return ans;

        }
        private bool checkForTF()
        {
            bool ans = true;

            if (string.IsNullOrEmpty(txtQ.Text) || string.IsNullOrEmpty(txtO1.Text) || string.IsNullOrEmpty(txtO2.Text) ||
                string.IsNullOrEmpty(txtCorrectOption.Text))
                ans = false;
            else
                ans = true;
            return ans;

        }

        /*all string method*/
        private string encript(String msg, String key)
        {
            string encpS = "";
            int inK = ((int)key[0] + (int)key[key.Length - 1]) - key.Length;
            for (int i = 0; i < msg.Length; i++)
            {
                int n = (int)msg[i] + inK + i;
                int p = n.ToString().Length;
                encpS += p + "" + n + "";
            }
            return encpS;
        }
        private string decript(String msg, string key)
        {
            string des = "";
            if (msg != "") { 

           
            int jump = Int16.Parse(msg[0] + "") + 1;
            int sp = jump - 1;
            int inK = ((int)key[0] + (int)key[key.Length - 1]) - key.Length;

            for (int i = 0; i < msg.Length - 1; i += jump)
            {

                int it = Int16.Parse(msg.Substring(i + 1, sp));
                des += (char)(it - inK - (i / jump));

            }
        }
            return des;
        
        }



        /* all event*/
        public QuizEntryForm()
        {
            InitializeComponent();
        }

        private void QuizEntryForm_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("provider=Microsoft.JET.oledb.4.0;data source=quizDB.mdb");
            try
            {
                conn.Open();
                updateDsTable();
            }
            catch (OleDbException)
            {
                Console.WriteLine("Catch");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox[] j = { checkBox2, checkBox3, checkBox4 };
            setCorrect(checkBox1, txtO1, j);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox[] j = { checkBox1, checkBox3, checkBox4 };
            setCorrect(checkBox2, txtO2, j);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox[] j = { checkBox2, checkBox1, checkBox4 };
            setCorrect(checkBox3, txtO3, j);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox[] j = { checkBox2, checkBox3, checkBox1 };
            setCorrect(checkBox4, txtO4, j);
        }

        private void lstQList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = lstQList.SelectedItem.ToString();
             type = "";

            string query = "select qty from qTable where id =" + id + "";
            adpt = new OleDbDataAdapter(query, conn);
            ds = new DataSet();
            adpt.Fill(ds);
            type = ds.Tables[0].Rows[0][0].ToString();

            if (type.Equals("TF")) showForTF();
            if (type.Equals("M")) showForM();


        }  

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkTF.Checked) { edForTF("new"); };
            if (checkForMultiple() && !chkTF.Checked) edForM("new");
          
           
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            lstQList.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true; btnCloseUp.Enabled = true;
            btnEdit.Enabled = false;
            lstQList.SelectedIndex = lastIndexPos;// select the last selected item item of list on first load
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clean();
        }

      

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (type.Equals("M")) updateforM();
            if (type.Equals("TF")) updateforTF();
           
        }

        private void btnCloseUp_Click(object sender, EventArgs e)
        {
            lstQList.Enabled = false;
            btnSave.Enabled = true;
            btnEdit.Enabled = true;
            btnUpdate.Enabled = false; btnCloseUp.Enabled = false;
            clean();
            updateDsTable();
        }

        private void chkTF_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTF.Checked) { txtO3.Enabled = false; txtO4.Enabled = false; checkBox3.Enabled = false;checkBox4.Enabled = false;txtO3.Clear();txtO4.Clear(); }
            else { txtO3.Enabled = true; txtO4.Enabled = true; checkBox3.Enabled = true; checkBox4.Enabled = true; }
        }
    }
}
