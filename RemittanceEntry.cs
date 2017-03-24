/* 

REMITTANCE ENTRY

 */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication2
{
    public partial class RemittanceEntry : Form
    {

        public static string connection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\MichaelMaur\Documents\Visual Studio 2015\Projects\IT420_MAGDADARO\CapstoneDatabase.mdb";
        OleDbConnection DBConnection = new OleDbConnection(connection);
       
        
        string returnNo;
        string remittanceNo;
        string apprEmpNo;

        Boolean updateRemAmount = false;
        DateTime returnDate;
        int indexer = 0;
        int rowDuplicate = 0;
        

        double bonus = 0;
        double genericSales = 0;
        double brandedSales = 0;
        double genericCom = 0;
        double brandedCom = 0;
        double expectedSale = 0;
        double totalAmountRemit = 0;
        double totalNetSales = 0;
        double totalComission = 0;
        double totalBalance = 0;

        public RemittanceEntry()
        {
            InitializeComponent();
        }

        private void remittanceEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            this.Hide();
            RemittanceEntry RemittanceEntryForm = new RemittanceEntry();
            RemittanceEntryForm.Show();
           
        }

        private void remittanceUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            RemittanceUpdate RemittanceUpdateForm = new RemittanceUpdate();
            RemittanceUpdateForm.Show();       
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBConnection.Close();
            this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        private void txtReturnNo_KeyPress_1(object sender, KeyPressEventArgs e){

            if (e.KeyChar == (char)Keys.Enter){
                dataGridView1.Rows.Clear();
                returnNo = txtReturnNo.Text.ToUpper().Trim();
                checkReturnNo(returnNo);
            }
        }
        public void checkReturnNo(string returnNo)
        {

            //EMPTY FIELD
            if (String.IsNullOrEmpty(returnNo))
            {
                MessageBox.Show("Please enter the Return Number the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtReturnNo.Clear();
            }
            //NOT EMPTY
            else if (!String.IsNullOrEmpty(returnNo))
            {
                //CHECK FOR SPECIAL CHARACTERS
                if (checkSpecialChar(returnNo))
                {
                    MessageBox.Show("Please enter a Return No. without any special characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtReturnNo.Clear();
                }
                else
                {

                    DBConnection.Open();

                    checkReturnHeader(returnNo);

                    DBConnection.Close();
                }
            }
        }

        public void checkReturnHeader(string returnNo)
        {

            Boolean retNoFlag = true;

            //CHECK RETURNHEADER IF RETURN NUMBER IS USED
            string commandRetHead = "Select * from RETURNHEADERFILE WHERE RETHEADCODE='" + returnNo + "'";
            OleDbCommand cmdRetHead = new OleDbCommand(commandRetHead, DBConnection);
            OleDbDataReader RetHeadReader = cmdRetHead.ExecuteReader();
            
            while (RetHeadReader.Read())
            {
                retNoFlag = false;

                if (RetHeadReader.GetValue(10).ToString().ToUpper().Trim().Equals("OP"))
                {

                    if (RetHeadReader.GetValue(2) == DBNull.Value)
                    {
                        MessageBox.Show("Return Date is missing. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReturnNo.Clear();
                    }
                    else if (RetHeadReader.GetDateTime(2) > dateTimePicker1.Value)
                    {
                        MessageBox.Show("Return Date is recent than the current date. Please update the Return transaction first before performing the Remittance Transaction", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtReturnNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(3).ToString().Trim().Equals(""))
                    {
                        MessageBox.Show("There is no record of the agent who performed the return transaction. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReturnNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(4).ToString().Trim().Equals("") || int.Parse(RetHeadReader.GetValue(4).ToString()) < 0)
                    {
                        MessageBox.Show("The total quantity dispensed is invalid. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReturnNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(5).ToString().Trim().Equals("") || int.Parse(RetHeadReader.GetValue(5).ToString()) < 0)
                    {
                        MessageBox.Show("The total quantity returned is invalid. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReturnNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(6).ToString().Trim().Equals(""))
                    {
                        MessageBox.Show("There is no record of the employee who verified the transaction. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReturnNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(9).ToString().Trim().Equals("") || int.Parse(RetHeadReader.GetValue(9).ToString()) < 0)
                    {
                        MessageBox.Show("The total quantity sold is invalid. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtReturnNo.Clear();
                    }

                    else
                    {

                        returnDate = RetHeadReader.GetDateTime(2);

                        if (!RetHeadReader.GetValue(7).ToString().Equals(""))
                        {
                            string updateEmpNo = RetHeadReader.GetValue(7).ToString().ToUpper().Trim();

                            if (checkUpdateRetEmp(updateEmpNo, returnDate))
                            {
                                if (checkRetCheckEmp(RetHeadReader.GetValue(6).ToString().ToUpper().Trim(), returnDate))
                                {
                                    int qtyOut = int.Parse(RetHeadReader.GetValue(4).ToString());
                                    int qtySold = int.Parse(RetHeadReader.GetValue(9).ToString());
                                    int qtyReturn = int.Parse(RetHeadReader.GetValue(5).ToString());

                                    string empNo = RetHeadReader.GetValue(3).ToString().ToUpper();

                                    if (qtyOut == qtyReturn + qtySold)
                                    {
                                        if (checkRetDetail(returnNo, qtyOut, qtySold, qtyReturn))
                                            checkAgent(empNo, returnDate);
                                        else
                                            break;
                                    }
                                    else if (qtyOut != qtyReturn + qtySold)
                                    {
                                        MessageBox.Show("The quantity dispense does not match with the quantity sold and quantity returned. Please update the return transaction first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        txtReturnNo.Clear();
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (checkRetCheckEmp(RetHeadReader.GetValue(6).ToString().ToUpper().Trim(), returnDate))
                            {
                                int qtyOut = int.Parse(RetHeadReader.GetValue(4).ToString());
                                int qtySold = int.Parse(RetHeadReader.GetValue(9).ToString());
                                int qtyReturn = int.Parse(RetHeadReader.GetValue(5).ToString());

                                string empNo = RetHeadReader.GetValue(3).ToString().ToUpper();

                                if (qtyOut == qtyReturn + qtySold)
                                {
                                    if (checkRetDetail(returnNo, qtyOut, qtySold, qtyReturn))
                                        checkAgent(empNo, returnDate);
                                    else
                                        break;
                                }
                                else if (qtyOut != qtyReturn + qtySold)
                                {
                                    MessageBox.Show("The quantity dispense does not match with the quantity sold and quantity returned. Please update the return transaction first", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtReturnNo.Clear();
                                }

                            }
                        }

                    }
                }
                else if (RetHeadReader.GetValue(10).ToString().ToUpper().Trim().Equals("CL"))
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("This return transaction has already been remitted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReturnNo.Clear();
                }
                else
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The status of this return transaction cannot be identified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReturnNo.Clear();
                }

            }//WHILE RETURNHEADER

            if (retNoFlag)
            {
                txtReturnNo.Clear();
                MessageBox.Show("There is no record for the Return transaction. Return Number not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtReturnNo.Clear();
            }

        }//END checkReturnHeader

        public void checkAgent(string empNo, DateTime returnDate)
        {
            Boolean empFlag = true;
            string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + empNo + "'";
            OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
            OleDbDataReader EmpReader = cmdEmp.ExecuteReader();

            while (EmpReader.Read())
            {
                empFlag = false;

                if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("AC"))
                {

                    //CHECK FOR EMPTYFIELDS HERE FNAME+LNAME EMPPOS+EMPRIGHTS HIREDATE  
                    if (EmpReader.GetValue(1).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("Employee's first name is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("Employee's last name is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("Employee's position is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("Employee's hire date is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("Employee's rights are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString();

                        //IF EMPLOYEE IS AN AGENT
                        if (EmpReader.GetDateTime(7) > dateTimePicker1.Value)
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("Employee's hiredate is recent than the current date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("Employee's hiredate is recent than the return transaction date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (empRights == 3 && empPos.ToUpper().Equals("AGENT"))
                        {
                            if (!EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(3).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(4).ToString().ToUpper().Trim().Equals(""))
                            {
                                lblEmpNo.Text = empNo;
                                lblEmpName.Text = EmpReader.GetValue(1).ToString().ToUpper().Trim() + " " + EmpReader.GetValue(3).ToString().ToUpper().Trim() + ". " + EmpReader.GetValue(2).ToString().ToUpper().Trim();
                                txtRemittanceNo.Focus();
                            }
                        }
                        else  if (empRights != 3 || !empPos.ToUpper().Equals("AGENT"))
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("Employee record shows that  the employee is not an agent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("Employee is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("Employee status cannot be retrieved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtReturnNo.Clear();
                MessageBox.Show("Employee Record is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public Boolean checkRetCheckEmp(string empNo, DateTime returnDate)
        {

            Boolean empFlag = true;
            Boolean errorFlag = false;
            string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + empNo + "'";
            OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
            OleDbDataReader EmpReader = cmdEmp.ExecuteReader();

            while (EmpReader.Read())
            {
                empFlag = false;

                if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("AC"))
                {

                    //CHECK FOR EMPTYFIELDS HERE FNAME+LNAME EMPPOS+EMPRIGHTS HIREDATE  
                    if (EmpReader.GetValue(1).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The first name of the employee who approved the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The last name of the employee who approved the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The role / position of the employee who approved the return transaction is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The hire date of the employee who approved the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The rights of the employee who approved the return transaction are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString().ToUpper().Trim();

                        //IF EMPLOYEE IS AN AGENT
                        if (EmpReader.GetDateTime(7) > dateTimePicker1.Value)
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("The hire date of the employee who approved the return transaction is recent than the current date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (EmpReader.GetDateTime(7) > returnDate)
                        {                   
                            txtReturnNo.Clear();
                            MessageBox.Show("The hire date of the employee who approved the return transaction is recent than the return transaction date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if ((empRights == 1 && empPos.Equals("OWNER")) || (empRights == 2 && empPos.Equals("MANAGER")))
                        {
                            if (!EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(3).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(4).ToString().ToUpper().Trim().Equals(""))
                            {
                                return errorFlag = true;
                            }
                        }
                        else if (!(empRights == 1 && empPos.Equals("OWNER")) || !(empRights == 2 && empPos.Equals("MANAGER")))
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("The employee who checked the return transaction has no right to process the transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("Employee is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The status of employee who approved the return transaction is not specified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtReturnNo.Clear();
                MessageBox.Show("The employee who approved the return transaction record is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return errorFlag;
        }

        public Boolean checkUpdateRetEmp(string empNo, DateTime returnDate)
        {

            Boolean empFlag = true;
            Boolean errorFlag = false;
            string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + empNo + "'";
            OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
            OleDbDataReader EmpReader = cmdEmp.ExecuteReader();

            while (EmpReader.Read())
            {
                empFlag = false;

                if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("AC"))
                {

                    //CHECK FOR EMPTYFIELDS HERE FNAME+LNAME EMPPOS+EMPRIGHTS HIREDATE  
                    if (EmpReader.GetValue(1).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The first name of the employee who updated the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The last name of the employee who updated the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The role / position of the employee who updated the return transaction is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The hiredate of the employee who updated the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The rights of the employee who updated the return transaction are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString().ToUpper().Trim();

                        //IF EMPLOYEE IS AN AGENT
                        if (EmpReader.GetDateTime(7) > dateTimePicker1.Value)
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("The hire date of the employee who updated the return transaction is recent than the current date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("The hire date of the employee who updated the return transaction is recent than the return date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if ((empRights == 1 && empPos.Equals("OWNER")) || (empRights == 2 && empPos.Equals("MANAGER")))
                        {
                            if (!EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(3).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(4).ToString().ToUpper().Trim().Equals(""))
                            {
                                return errorFlag = true;
                            }
                        }
                        else if (!(empRights == 1 && empPos.Equals("OWNER")) || !(empRights == 2 && empPos.Equals("MANAGER")))
                        {
                            txtReturnNo.Clear();
                            MessageBox.Show("The employee who updated the return transaction is not eligible to process the transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The status of the employee who updated the return transaction is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The status of mployee who updated the return transaction is not speciefied in the record ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtReturnNo.Clear();
                MessageBox.Show("The employee who updated the record is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return errorFlag;
        }

        public Boolean checkRetDetail(string returnNo, int totOut, int totSold, int totReturn)
        {
            Boolean retDeFlag = true;
            Boolean errorFlag = false;
            int drgOut = 0;
            int drgReturn = 0;
            int drgSold = 0;

            //CHECK RETURNDETAIL IF RETURN NUMBER IS USED
            string commandRetDetail = "Select * from RETURNDETAILFILE WHERE RETDETTRANSNO='" + returnNo + "'";
            OleDbCommand cmdRetDetail = new OleDbCommand(commandRetDetail, DBConnection);
            OleDbDataReader RetDetailReader = cmdRetDetail.ExecuteReader();


            while (RetDetailReader.Read())
            {
                retDeFlag = false;
                if (RetDetailReader.GetValue(5).ToString().ToUpper().Trim().Equals("OP") || RetDetailReader.GetValue(5).ToString().ToUpper().Trim().Equals("CL"))
                {
                    if (RetDetailReader.GetValue(1).ToString().Trim().Equals(""))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The drug code in the return file is missing", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    else if (!checkDrugHeader(RetDetailReader.GetValue(1).ToString().ToUpper().Trim()))
                    {
                        txtReturnNo.Clear();
                        errorFlag = false;
                        break;
                    }
                    if (RetDetailReader.GetValue(2).ToString().Trim().Equals("") || int.Parse(RetDetailReader.GetValue(2).ToString()) < 0)
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The drug quantity dispensed is invalid. Please update return transaction first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    else if (RetDetailReader.GetValue(3).ToString().Trim().Equals("") || int.Parse(RetDetailReader.GetValue(3).ToString()) < 0)
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The drug quantity returned is invalid. Please update return transaction first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    else if (RetDetailReader.GetValue(4).ToString().Trim().Equals("") || int.Parse(RetDetailReader.GetValue(4).ToString()) < 0)
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The drug quantity sold is invalid. Please update return transaction first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    //IF QTY !=QTYSOLD + QTYRETURN
                    else if (double.Parse(RetDetailReader.GetValue(2).ToString()) != double.Parse(RetDetailReader.GetValue(3).ToString()) + double.Parse(RetDetailReader.GetValue(4).ToString()))
                    {
                        txtReturnNo.Clear();
                        MessageBox.Show("The sum of the drugs sold and returned is not equal to the quantity dispensed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    else
                    {
                      
                            drgOut = drgOut + int.Parse(RetDetailReader.GetValue(2).ToString());
                            drgReturn = drgReturn + int.Parse(RetDetailReader.GetValue(3).ToString());
                            drgSold = drgSold + int.Parse(RetDetailReader.GetValue(4).ToString());
                            errorFlag = true;
                    }
                }// IF RETURN DETAIL IS OPEN
                else
                {
                    txtReturnNo.Clear();
                    errorFlag = false;
                    MessageBox.Show("The status of return detail cannot be identified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }//WHILE RETURNDETAIL

          
            if (retDeFlag)
            {
                txtReturnNo.Clear();
                MessageBox.Show("The return detail records cannot be retrieved. Please update the return transaction first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (errorFlag)
            {
                if (drgOut != totOut)
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The quantity dispensed in the return header does not match the quantity of return detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFlag = false;
                }
                else if (drgReturn != totReturn)
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The quantity returned in the return header does not match the quantity of return detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFlag = false;
                }
                else if (drgSold != totSold)
                {
                    txtReturnNo.Clear();
                    MessageBox.Show("The quantity sold in the return header does not match the quantity of return detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFlag = false;
                }

            }

            return errorFlag;
        }

        private void txtDrugCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string drugCode = txtDrugCode.Text.ToUpper().Trim();
                checkDrugCode(drugCode);
            }
        }

        public void checkDrugCode(string drugCode)
        {

            Boolean retDetailFlag = true;
            double retailPrice = 0;
            int qtySold = 0;

            // if textbox is empty
            if (String.IsNullOrEmpty(drugCode))
            {
                MessageBox.Show("Please enter the Drug Code in the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDrugCode.Clear();
                txtDrugCode.Focus();
            }
            //user has not yet entered the return no
            else if (String.IsNullOrEmpty(returnNo))
            {
                MessageBox.Show("Please enter the Return No. in the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtReturnNo.Focus();
                txtDrugCode.Clear();
            }
            // if textbox is not empty
            else if (!String.IsNullOrEmpty(drugCode))
            {
                //check for special characters
                if (checkSpecialChar(drugCode))
                {
                    MessageBox.Show("Please enter a Drug Code without any special characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                }
                //else if (indexer != 0 && dataGridView1.Rows[indexer].Cells[0].Value != null && dataGridView1.Rows[indexer].Cells[6].Value == null)
                else if (indexer >= 1 && dataGridView1.Rows[indexer].Cells[0].Value != null && dataGridView1.Rows[indexer].Cells[6].Value == null)
                {
                    MessageBox.Show("Please input remittance amount first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRemAmount.Focus();                     
                }
                else if ((indexer ==0 && dataGridView1.Rows[indexer].Cells[0].Value != null))
                {
                    MessageBox.Show("Please input the remittance amount first before adding another drug", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDrugCode.Text = dataGridView1.Rows[indexer].Cells[0].Value.ToString();
                    txtRemAmount.Focus();               
                }
                else
                {
                    DBConnection.Open();
                    //CHECK DRUGDETAIL
                    if (checkDrugHeader(drugCode))
                    {
                        if (checkDuplicate(drugCode))
                        {

                            DialogResult dr = MessageBox.Show("Drug code is in the flexgrid, Do you want to update?", "Missing information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.Yes)
                            {

                                updateRemAmount = true;
                                txtRemAmount.Focus();
                            }

                            if (dr == DialogResult.No)
                            {
                                txtDrugCode.Clear();
                            }
                        }
                        else
                        {
                            string commandReturn = "Select * from RETURNDETAILFILE WHERE RETDETTRANSNO='" + returnNo + "'And RETDETDRUGCODE='" + drugCode + "'";
                            OleDbCommand cmdRet = new OleDbCommand(commandReturn, DBConnection);
                            OleDbDataReader RetReader = cmdRet.ExecuteReader();

                            while (RetReader.Read())
                            {
                                retDetailFlag = false;

                                if (RetReader.GetValue(5).ToString().ToUpper().Trim().Equals("OP"))
                                {                                    
                                    //if QTYDISPENSE= 0
                                    if (RetReader.GetValue(2).ToString().Equals("") || int.Parse(RetReader.GetValue(2).ToString()) < 0)
                                    {
                                        txtDrugCode.Clear();
                                        MessageBox.Show("Drug quantity dispensed is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }// QTYSOLD AND QTYRETURN
                                    else if (RetReader.GetValue(3).ToString().Equals("") || int.Parse(RetReader.GetValue(3).ToString()) < 0)
                                    {
                                        txtDrugCode.Clear();
                                        MessageBox.Show("Drug quantity returned is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                    else if (RetReader.GetValue(4).ToString().Equals("") || int.Parse(RetReader.GetValue(4).ToString()) < 0)
                                    {
                                        txtDrugCode.Clear();
                                        MessageBox.Show("Drug quantity sold is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                    //IF QTY !=QTYSOLD + QTYRETURN
                                    else if (int.Parse(RetReader.GetValue(2).ToString()) != int.Parse(RetReader.GetValue(3).ToString()) + int.Parse(RetReader.GetValue(4).ToString()))
                                    {
                                        MessageBox.Show("Drug quantity sold and quantity returned is not equal to the quantity dispensed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtDrugCode.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Add();

                                        string commandDrug = "Select * from DRUGINVENTORYHEADERFILE WHERE DRUGHEADCODE='" + drugCode + "'";
                                        OleDbCommand cmdDrug = new OleDbCommand(commandDrug, DBConnection);
                                        OleDbDataReader DrugReader = cmdDrug.ExecuteReader();

                                        while (DrugReader.Read())
                                        {
                                            qtySold = int.Parse(RetReader.GetValue(4).ToString().ToUpper().Trim());
                                            retailPrice = double.Parse(DrugReader.GetValue(6).ToString().ToUpper().Trim());
                                            dataGridView1.Rows[indexer].Cells[0].Value = DrugReader.GetValue(0).ToString().ToUpper().Trim();
                                            dataGridView1.Rows[indexer].Cells[1].Value = DrugReader.GetValue(2).ToString().ToUpper().Trim();
                                            dataGridView1.Rows[indexer].Cells[2].Value = DrugReader.GetValue(7).ToString().ToUpper().Trim();
                                            dataGridView1.Rows[indexer].Cells[3].Value = RetReader.GetValue(4).ToString().ToUpper().Trim();
                                            dataGridView1.Rows[indexer].Cells[4].Value = DrugReader.GetValue(6).ToString().ToUpper().Trim();
                                            dataGridView1.Rows[indexer].Cells[5].Value = qtySold * retailPrice;
                                        }


                                        txtRemAmount.Focus();
                                        break;
                                    }
                                }
                                else if (RetReader.GetValue(5).ToString().ToUpper().Trim().Equals("CL"))
                                {
                                    MessageBox.Show("Drug code has already been remitted. If you want to make changes please update", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDrugCode.Clear();
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Please update this drug code in the return transaction before processing the remittance", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDrugCode.Clear();
                                    break;
                                }

                            }//END WHILE                                   
                            //ERROR CHECKING
                            if (retDetailFlag)
                            {
                                txtDrugCode.Clear();
                                txtDrugCode.Focus();
                                MessageBox.Show("Drug Code is not on Return File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    
                }

                DBConnection.Close();
            }
        }
        public Boolean checkDrugHeader(string drugCode)
        {
            Boolean drugCodeFlag = true;
            Boolean flag = false;

            string commandDrug = "Select * from DRUGINVENTORYHEADERFILE WHERE DRUGHEADCODE='" + drugCode + "'";
            OleDbCommand cmdDrug = new OleDbCommand(commandDrug, DBConnection);
            OleDbDataReader DrugReader = cmdDrug.ExecuteReader();

            while (DrugReader.Read())
            {
                drugCodeFlag = false;
                //CHECK IF THERE IS UNIT PRICE AND RETAIL PRICE 
                if (DrugReader.GetValue(2).ToString().Trim().Equals(""))
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The drug name is not in record", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (DrugReader.GetValue(1).ToString().Trim().Equals("") || double.Parse(DrugReader.GetValue(1).ToString().Trim()) < 0)
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The drug quantity is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DrugReader.GetValue(5).ToString().Trim().Equals("") || double.Parse(DrugReader.GetValue(5).ToString().Trim()) <= 0)
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The drug unit price is not in record ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DrugReader.GetValue(6).ToString().Trim().Equals("")  || double.Parse(DrugReader.GetValue(6).ToString().Trim()) <= 0)
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The drug retail price is not in record ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (DrugReader.GetValue(7).ToString().Trim().Equals(""))
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The drug type is not specified in record", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!DrugReader.GetValue(7).ToString().Trim().Equals("BRANDED") && !DrugReader.GetValue(7).ToString().Trim().Equals("GENERIC"))
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("Drug type is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!DrugReader.GetValue(8).ToString().Trim().Equals("AV") && !DrugReader.GetValue(8).ToString().Trim().Equals("UN"))
                {
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The status of the drug is not specified", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    if (checkDrugDetail(drugCode))
                        flag = true;
                    else
                        flag = false;
                }

            }
            if (drugCodeFlag)
            { 
                    txtDrugCode.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("The drug code is not found in the record", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            return flag;
        }
        public Boolean checkDrugDetail(string drugCode)
        {
            Boolean drugCodeFlag = true;
            Boolean flag = false;

            string commandDrug = "Select * from DRUGINVENTORYDETAILFILE WHERE DRUGDETCODE='" + drugCode + "'";
            OleDbCommand cmdDrug = new OleDbCommand(commandDrug, DBConnection);
            OleDbDataReader DrugReader = cmdDrug.ExecuteReader();

            while (DrugReader.Read())
            {
                drugCodeFlag = false;

                if (DrugReader.GetValue(2).ToString().Trim().Equals(""))
                {
                    flag = false;   
                    MessageBox.Show("The drug expiry is missing", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                else if (DrugReader.GetValue(1).ToString().Trim().Equals("") || double.Parse(DrugReader.GetValue(1).ToString().Trim()) < 0)
                {
                    flag = false;   
                    MessageBox.Show("The drug quantity in the detail file is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else if (!DrugReader.GetValue(3).ToString().Trim().Equals("AV") && !DrugReader.GetValue(3).ToString().Trim().Equals("UN"))
                {
                    flag = false;   
                    MessageBox.Show("The status of the drug in the detail file is not specified", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                else
                {
                    flag = true;
                }

            }
            if (drugCodeFlag)
            {
                txtDrugCode.Clear();
                txtDrugCode.Focus();
                MessageBox.Show("The drug code  is not found in the record", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return flag;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DatePickerCheck();
        }
        public void DatePickerCheck()
        {
            Boolean checkDateFlag = false;

            // condition to check if the date selected by the user is not  the Date Today
            if (dateTimePicker1.Value < DateTime.Now || dateTimePicker1.Value > DateTime.Now)
                checkDateFlag = true;


            if (checkDateFlag)
            {
                dateTimePicker1.Value = DateTime.Now;
                MessageBox.Show("Please choose the current date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtRemittanceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               remittanceNo = txtRemittanceNo.Text.ToUpper().Trim();
               checkRemittanceNo(remittanceNo);
            }
        }

        public void checkRemittanceNo(string remittanceNo)
        {

            Boolean remNoFlag = false;
            Boolean remDetFlag = false;

            // if textbox is empty
            if (String.IsNullOrEmpty(remittanceNo))
            {
                MessageBox.Show("Please enter the Remittance Number the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            // if textbox is not empty
            else if (!String.IsNullOrEmpty(remittanceNo))
            {
                //check for special characters
                if (checkSpecialChar(remittanceNo))
                {
                    MessageBox.Show("Please enter a Remittance No. without any special characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRemittanceNo.Clear();
                    txtRemittanceNo.Focus();
                }
                else
                {
                    DBConnection.Open();
                    string commandRem = "Select * from REMITTANCEHEADERFILE WHERE REMHEADTRANSNO='" + remittanceNo + "'";
                    OleDbCommand cmdRem = new OleDbCommand(commandRem, DBConnection);
                    OleDbDataReader RemReader = cmdRem.ExecuteReader();

                    while (RemReader.Read())
                    {
                            remNoFlag = true;
         
                    }

                    //CHECK IF REMITTANCE NUMBER IS IN DETAIL
                    string commandRemDe = "Select * from REMITTANCEDETAILFILE WHERE REMDETTRANSNO='" + remittanceNo + "'";
                    OleDbCommand cmdRemDe = new OleDbCommand(commandRemDe, DBConnection);
                    OleDbDataReader RemDeReader = cmdRemDe.ExecuteReader();

                    while (RemDeReader.Read())
                    {
                        if (RemDeReader.GetValue(0).ToString().ToUpper().Trim().Equals(remittanceNo))
                            remDetFlag = true;
                    }

                    DBConnection.Close();

                    //ERROR CHECKING
                    if (remNoFlag || remDetFlag)
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("Remittance Number is already used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        txtDrugCode.Focus();
                }
            }
            
        }

        public Boolean checkSpecialChar(string check)
        {

            Boolean flag = false;

            // convert each character of the string value in the textbox to ASCII
            byte[] ASCII = Encoding.ASCII.GetBytes(check);
            //foreach loop to check each value 
            foreach (byte b in ASCII)
            {
                //condition to check and filter for special characters entered
                if (b < 48 || (b > 75 && b < 65) || (b > 90 && b < 97) || b > 122)
                    flag = true;
            }

            return flag;
        }

        public Boolean checkDuplicate(string drugcode)
        {

            Boolean found = false;

            for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
            {
                if (drugcode.Equals(dataGridView1.Rows[x].Cells[0].Value.ToString().ToUpper().Trim()))
                {
                    found = true;
                    rowDuplicate = x;
                }
            }

            return found;
        }
        private void txtRemAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string remAmount = txtRemAmount.Text.ToUpper().Trim();
                checkRemAmount(remAmount);
            }
        }

        public void checkRemAmount(String remAmount)
        {

            Boolean drugCodeEmptyFlag = false;
            string checkDrugType = "";
            double amount = 0;
            double salesAmount = 0;
            double dOutput = 0;

            if (Double.TryParse(remAmount, out dOutput))
            {
                // if textbox is empty
                if (String.IsNullOrEmpty(remAmount))
                {
                    MessageBox.Show("Please enter the Quantity on the Box provided", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (String.IsNullOrEmpty(txtDrugCode.Text))
                {
                    txtRemAmount.Clear();
                    txtDrugCode.Focus();
                    MessageBox.Show("Please enter the drug code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //user has not yet entered the return no
                else if (String.IsNullOrEmpty(returnNo))
                {
                    MessageBox.Show("Please enter the Return No. in the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtReturnNo.Focus();
                    txtRemAmount.Clear();
                }
                else if (double.Parse(remAmount) < 0)
                {
                    MessageBox.Show("Amount should be greater than zero", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRemAmount.Clear();
                }

                //add trap if no drug code yet
                // if textbox is not empty
                else if (!String.IsNullOrEmpty(remAmount))
                {

                    if (updateRemAmount)
                    {
                        dataGridView1.Rows[rowDuplicate].Cells[6].Value = remAmount;

                        updateRemAmount = false;
                        txtDrugCode.Clear();
                        txtRemAmount.Clear();
                        txtDrugCode.Focus();

                        amount = 0;

                        genericSales = 0;
                        brandedSales = 0;
                        expectedSale = 0;
                        totalAmountRemit = 0;
                        genericCom = 0;
                        brandedCom = 0;
                        totalNetSales = 0;
                        totalComission = 0;
                        totalBalance = 0;

                        for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
                        {
                            amount = double.Parse(dataGridView1.Rows[x].Cells[6].Value.ToString().Trim());
                            checkDrugType = dataGridView1.Rows[x].Cells[2].Value.ToString().ToUpper().Trim();
                            salesAmount = double.Parse(dataGridView1.Rows[x].Cells[5].Value.ToString().ToUpper().Trim());

                            if (checkDrugType.Equals("BRANDED"))
                            {
                                brandedSales = brandedSales + amount;
                                brandedCom = brandedCom + (amount * .30);
                            }
                            else if (checkDrugType.Equals("GENERIC"))
                            {
                                genericSales = genericSales + amount;
                                genericCom = genericCom + (amount * .20);
                            }
                            totalAmountRemit = totalAmountRemit + amount;
                            expectedSale = expectedSale + salesAmount;
                            totalComission = genericCom + brandedCom;
                            totalNetSales = totalAmountRemit - (genericCom + brandedCom);
                            //totalBalance = expectedSale - totalAmountRemit;

                        }
                       
                        lblExpectedAmt.Text = expectedSale + "";
                        lblTotalRemAmt.Text = totalAmountRemit + "";

                        lblGenSales.Text = genericSales + "";
                        lblBrandedSales.Text = brandedSales + "";
                        lblBrandedCom.Text = brandedCom + "";
                        lblGenCom.Text = genericCom + "";
                        lblNetSales.Text = totalNetSales + "";
                        lblTotalCom.Text = totalComission + "";
                    }
                    else if (dataGridView1.Rows[indexer].Cells[0].Value == null)
                    {
                        drugCodeEmptyFlag = true;
                        MessageBox.Show("Please enter the drug code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (dataGridView1.Rows[indexer].Cells[0].Value != null)
                    {
                        dataGridView1.Rows[indexer].Cells[6].Value = remAmount;

                        amount = double.Parse(remAmount);

                        checkDrugType = dataGridView1.Rows[indexer].Cells[2].Value.ToString().ToUpper().Trim();
                        salesAmount = double.Parse(dataGridView1.Rows[indexer].Cells[5].Value.ToString().ToUpper().Trim());

                        //compute the bonus once
                        if (lblBonus.Text.Trim() == "")
                        {
                            //bonus = calculateBonus(empNo);
                            lblBonus.Text = bonus + "";
                        }


                        if (checkDrugType.Equals("BRANDED"))
                        {
                            brandedSales = brandedSales + amount;
                            brandedCom = brandedCom + (amount * .30);
                        }
                        else if (checkDrugType.Equals("GENERIC"))
                        {
                            genericSales = genericSales + amount;
                            genericCom = genericCom + (amount * .20);
                        }

                        //compute the bonus once
                        if (lblBonus.Text.Trim() == "")
                        {
                            //bonus = calculateBonus(empNo);
                            lblBonus.Text = bonus + "";
                        }

                        //ALREADY OKAY
                        totalAmountRemit = totalAmountRemit + amount;
                        expectedSale = expectedSale + salesAmount;
                        lblExpectedAmt.Text = expectedSale + "";
                        lblTotalRemAmt.Text = totalAmountRemit + "";


                        totalNetSales = totalAmountRemit - (genericCom + brandedCom + bonus);
                        totalComission = genericCom + brandedCom + bonus;
                        //totalBalance = expectedSale - totalAmountRemit;

                        //totalNetSales = totalAmountRemit - (genericCom + brandedCom + bonus);
                        //totalComission = genericCom + brandedCom + bonus;

                        indexer = indexer + 1;


                        DBConnection.Close();

                        lblGenSales.Text = genericSales + "";
                        lblBrandedSales.Text = brandedSales + "";
                        lblBrandedCom.Text = brandedCom + "";
                        lblGenCom.Text = genericCom + "";
                        lblNetSales.Text = totalNetSales + "";
                        lblTotalCom.Text = totalComission + "";


                        txtDrugCode.Clear();
                        txtRemAmount.Clear();
                        txtDrugCode.Focus();

                        if (drugCodeEmptyFlag)
                        {
                            txtRemAmount.Clear();
                            txtDrugCode.Focus();
                            MessageBox.Show("Please enter the specific Drug Code first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Input a valid numeric number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }         
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar == (char)Keys.Enter)
            {
                string remarks = txtRemarks.Text.ToUpper().Trim();
                checkRemarks(remarks);
            }
        }

        private void txtApprEmpCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                apprEmpNo = txtApprEmpCode.Text.ToUpper().Trim();
                checkApprEmp(apprEmpNo);
            }
        }

        public void checkApprEmp(string apprEmpNo)
        {
            Boolean empFlag = true;

            DBConnection.Open();
            string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + apprEmpNo + "'";
            OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
            OleDbDataReader EmpReader = cmdEmp.ExecuteReader();

            while (EmpReader.Read())
            {
                empFlag = false;

                if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("AC"))
                {

                    //CHECK FOR EMPTYFIELDS HERE FNAME+LNAME EMPPOS+EMPRIGHTS HIREDATE  
                    if (EmpReader.GetValue(1).ToString().Trim().Equals(""))
                    {
                        txtApprEmpCode.Clear();
                        lblApprvByName.Text= ""; 
                        MessageBox.Show("The first name of the employee is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtApprEmpCode.Clear();
                        lblApprvByName.Text= ""; 
                        MessageBox.Show("The last name of the employee is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtApprEmpCode.Clear();
                        lblApprvByName.Text= ""; 
                        MessageBox.Show("The role / position of the employee is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtApprEmpCode.Clear();
                        lblApprvByName.Text= ""; 
                        MessageBox.Show("The hire date of the employee  is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtApprEmpCode.Clear();
                        lblApprvByName.Text= ""; 
                        MessageBox.Show("The rights of the employee are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString().ToUpper().Trim();

                        //IF EMPLOYEE IS AN AGENT
                        if (EmpReader.GetDateTime(7) > dateTimePicker1.Value)
                        {
                            txtApprEmpCode.Clear();
                            lblApprvByName.Text= ""; 
                            MessageBox.Show("The employee's hire date is recent than the current date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtApprEmpCode.Clear();
                            lblApprvByName.Text= ""; 
                            MessageBox.Show("Employee's hire date is recent than the return transaction date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if ((empRights == 1 && empPos.Equals("OWNER")) || (empRights == 2 && empPos.Equals("MANAGER")))
                        {
                            if (!EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(3).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(4).ToString().ToUpper().Trim().Equals(""))
                            {
                                lblApprvByName.Text = EmpReader.GetValue(1).ToString().ToUpper().Trim()+ " " +EmpReader.GetValue(3).ToString().ToUpper().Trim() + ". "+ EmpReader.GetValue(2).ToString().ToUpper().Trim();
                            }
                        }
                        else if (!(empRights == 1 && empPos.Equals("OWNER")) || !(empRights == 2 && empPos.Equals("MANAGER")))
                        {
                            txtApprEmpCode.Clear();
                            lblApprvByName.Text= ""; 
                            MessageBox.Show("The employee who checked the return transaction has no right to process the transaction", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtApprEmpCode.Clear();
                    lblApprvByName.Text= ""; 
                    MessageBox.Show("Employee is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtApprEmpCode.Clear();
                    lblApprvByName.Text= ""; 
                    MessageBox.Show("The status of employee who approved the return transaction is not specified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtApprEmpCode.Clear();
                lblApprvByName.Text = "";
                MessageBox.Show("The employee who approved the return transaction record is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                txtRemarks.Focus();

            DBConnection.Close();

        }

        public void checkRemarks(string remarks)
        {
         // if textbox is empty
            if (String.IsNullOrEmpty(remarks))
            {
                txtRemarks.Focus();
                MessageBox.Show("Please provide remarks for easy understandability and for future use", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Save() 
        {
            //ADD CHECK TO TEXTFIELDS
            if (returnNo != txtReturnNo.Text.ToUpper().Trim() || returnNo.Trim().Equals(""))
            {
                checkReturnNo(returnNo);
            }
            else if (remittanceNo != txtRemittanceNo.Text.ToUpper().Trim() || remittanceNo.Trim().Equals(""))
            {
                checkRemittanceNo(remittanceNo);
            }
            else if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("Please add data to the data grid", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (indexer >= 1 && dataGridView1.Rows[indexer].Cells[0].Value != null && dataGridView1.Rows[indexer].Cells[6].Value == null)
            {
                MessageBox.Show("Please add remittance amount first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRemAmount.Focus();
            }
            else if ((indexer == 0 && dataGridView1.Rows[indexer].Cells[0].Value != null))
            {
                MessageBox.Show("Please input the remittance amount first before adding another drug", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDrugCode.Text = dataGridView1.Rows[indexer].Cells[0].Value.ToString();
                txtRemAmount.Focus();
            }
            else if (string.IsNullOrEmpty(txtApprEmpCode.Text) || lblApprvByName.Text.Trim() == "")
            {
                MessageBox.Show("Please add the approved employee", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //SAVE REMITTANCE HEADER
                DBConnection.Open();
                String sqlRemHeader = "Select * from REMITTANCEHEADERFILE WHERE REMHEADTRANSNO='" + remittanceNo + "'";
                OleDbDataAdapter thisAdapterRemHeader = new OleDbDataAdapter(sqlRemHeader, connection);
                OleDbCommandBuilder cmdBuilderRemHeader = new OleDbCommandBuilder(thisAdapterRemHeader);

                DataSet thisDataSetRemHeader = new DataSet();

                thisAdapterRemHeader.Fill(thisDataSetRemHeader, "REMITTANCEHEADERFILE");

                //SET THE PRIMAREY KEYS
                DataColumn[] keysRemHeader = new DataColumn[2];
                keysRemHeader[0] = thisDataSetRemHeader.Tables["REMITTANCEHEADERFILE"].Columns["REMHEADTRANSNO"];
                keysRemHeader[1] = thisDataSetRemHeader.Tables["REMITTANCEHEADERFILE"].Columns["REMHEADRETNO"];

                //LET THE DataSet RECOGNIZE THE KEYS
                thisDataSetRemHeader.Tables["REMITTANCEHEADERFILE"].PrimaryKey = keysRemHeader;

                String[] searchValueRemHeader = new String[2];
                searchValueRemHeader[0] = remittanceNo;
                searchValueRemHeader[1] = returnNo;

                DataRow findRowRemHeader = thisDataSetRemHeader.Tables["REMITTANCEHEADERFILE"].Rows.Find(searchValueRemHeader);

                if (findRowRemHeader == null)
                {

                    totalBalance = expectedSale - totalAmountRemit;


                    DataRow thisRemHeaderRow = thisDataSetRemHeader.Tables["REMITTANCEHEADERFILE"].NewRow();
                    thisRemHeaderRow[0] = remittanceNo;
                    thisRemHeaderRow[1] = returnNo;
                    thisRemHeaderRow[2] = lblEmpNo.Text.ToString().ToUpper().Trim();
                    thisRemHeaderRow[3] = dateTimePicker1.Value.ToShortDateString();
                    thisRemHeaderRow[4] = totalAmountRemit;
                    thisRemHeaderRow[5] = brandedSales;
                    thisRemHeaderRow[6] = genericSales;
                    thisRemHeaderRow[7] = totalComission;
                    thisRemHeaderRow[8] = expectedSale;
                    thisRemHeaderRow[9] = totalNetSales;
                    thisRemHeaderRow[10] = totalBalance;
                    thisRemHeaderRow[11] = apprEmpNo;
                    thisRemHeaderRow[13] = txtRemarks.Text.ToString().ToUpper();
                    thisRemHeaderRow[14] = "OP";

                    thisDataSetRemHeader.Tables["REMITTANCEHEADERFILE"].Rows.Add(thisRemHeaderRow);

                }
                thisAdapterRemHeader.Update(thisDataSetRemHeader, "REMITTANCEHEADERFILE");
                thisAdapterRemHeader.AcceptChangesDuringUpdate = true;
                DBConnection.Close();

                string tempExpectedSale;
                string tempAmountRem;

                //SAVE REMITTANCEDETAIL
                DBConnection.Open();
                String sqlRemDetail = "Select * from REMITTANCEDETAILFILE WHERE REMDETTRANSNO='" + remittanceNo + "'";
                OleDbDataAdapter thisAdapterRemDetail = new OleDbDataAdapter(sqlRemDetail, connection);
                OleDbCommandBuilder cmdBuilderRemDetail = new OleDbCommandBuilder(thisAdapterRemDetail);

                DataSet thisDataSetRemDetail = new DataSet();

                thisAdapterRemDetail.Fill(thisDataSetRemDetail, "REMITTANCEDETAILFILE");

                //SET THE PRIMAREY KEYS
                DataColumn[] keysRemDetail = new DataColumn[2];
                keysRemDetail[0] = thisDataSetRemDetail.Tables["REMITTANCEDETAILFILE"].Columns["REMDETTRANSNO"];
                keysRemDetail[1] = thisDataSetRemDetail.Tables["REMITTANCEDETAILFILE"].Columns["REMDETDRUGCODE"];

                //LET THE DataSet RECOGNIZE THE KEYS
                thisDataSetRemDetail.Tables["REMITTANCEDETAILFILE"].PrimaryKey = keysRemDetail;

                for (int n = 0; n < dataGridView1.Rows.Count - 1; n++)
                {

                    //SEARCH DRUG CODE HERE IN RETURNFILE IF FOUND UPDATE IF NOT ADD

                    string drgCode = dataGridView1.Rows[n].Cells[0].Value.ToString().ToUpper().Trim();
                    String[] searchValueRemDetail = new String[2];
                    searchValueRemDetail[0] = remittanceNo;
                    searchValueRemDetail[1] = drgCode;
                    DataRow findRowRemDetail = thisDataSetRemDetail.Tables["REMITTANCEDETAILFILE"].Rows.Find(searchValueRemDetail);

                    if (findRowRemDetail == null)
                    {

                        DataRow thisDetailRemRow = thisDataSetRemDetail.Tables["REMITTANCEDETAILFILE"].NewRow();

                        tempExpectedSale = dataGridView1.Rows[n].Cells[5].Value.ToString().ToUpper().Trim();
                        tempAmountRem = dataGridView1.Rows[n].Cells[6].Value.ToString().ToUpper().Trim();

                        thisDetailRemRow[0] = remittanceNo;
                        thisDetailRemRow[1] = dataGridView1.Rows[n].Cells[0].Value.ToString().ToUpper().Trim();
                        thisDetailRemRow[2] = Convert.ToDouble(dataGridView1.Rows[n].Cells[5].Value);
                        thisDetailRemRow[3] = Convert.ToDouble(dataGridView1.Rows[n].Cells[6].Value);
                        thisDetailRemRow[4] = "OP";

                        thisDataSetRemDetail.Tables["REMITTANCEDETAILFILE"].Rows.Add(thisDetailRemRow);
                    }

                    else
                    {
                        double amount = double.Parse(findRowRemDetail["REMDETAMTREMITTED"].ToString().Trim());
                        //DONT REPLACE THE VALUE. MUST ADD OR SUBTRACT
                        findRowRemDetail.BeginEdit();
                        findRowRemDetail["REMDETAMTREMITTED"] = amount + Convert.ToDouble(dataGridView1.Rows[n].Cells[6].Value);
                        findRowRemDetail.EndEdit();

                    }


                    thisAdapterRemDetail.Update(thisDataSetRemDetail, "REMITTANCEDETAILFILE");
                    thisAdapterRemDetail.AcceptChangesDuringUpdate = true;
                    DBConnection.Close();
                }


                //CHECK IF DRUG CODE WAS SUCESSFULLY REMITTED


                //CHECK VALID
                DBConnection.Open();

                Boolean allClose = true;
                double returnBalance = 0;
                double remAmount = 0;

                string commandRetHead = "Select * from RETURNHEADERFILE WHERE RETHEADCODE='" + returnNo + "'";
                OleDbCommand cmdRetHead = new OleDbCommand(commandRetHead, DBConnection);
                OleDbDataReader RetHeadReader = cmdRetHead.ExecuteReader();

                while (RetHeadReader.Read())
                {
                    string sqlRetDetail = "Select * from RETURNDETAILFILE WHERE RETDETTRANSNO='" + returnNo + "'";
                    OleDbCommand cmdRetDetail = new OleDbCommand(sqlRetDetail, DBConnection);
                    OleDbDataReader RetDetailReader = cmdRetDetail.ExecuteReader();

                    while (RetDetailReader.Read())
                    {
                        
                        //check remittance header for the same return no, check return, then check remittancedetail
                        string drugCode = RetDetailReader.GetValue(1).ToString().ToUpper().Trim();

                        string commandDrug = "Select * from DRUGINVENTORYHEADERFILE WHERE DRUGHEADCODE='" + drugCode + "'";
                        OleDbCommand cmdDrug = new OleDbCommand(commandDrug, DBConnection);
                        OleDbDataReader DrugReader = cmdDrug.ExecuteReader();

                        double price = 0;
                        double qtyReturned = int.Parse(RetDetailReader.GetValue(4).ToString().Trim());

                        while (DrugReader.Read())
                        {
                            price = double.Parse(DrugReader.GetValue(6).ToString().Trim());

                        }

                        returnBalance =  (price * qtyReturned);

                        string commandRemHead = "Select * from REMITTANCEHEADERFILE WHERE REMHEADRETNO='" + returnNo + "'";
                        OleDbCommand cmdRemHead = new OleDbCommand(commandRemHead, DBConnection);
                        OleDbDataReader RemHeadReader = cmdRemHead.ExecuteReader();


                        while (RemHeadReader.Read())
                        {

                            //CHECK BALANCE HERE
                            string commandRemDetail = "Select * from REMITTANCEDETAILFILE WHERE REMDETTRANSNO='" + RemHeadReader.GetValue(0).ToString().Trim() + "'And REMDETDRUGCODE='" + drugCode + "'";
                            OleDbCommand cmdRemDetail = new OleDbCommand(commandRemDetail, DBConnection);
                            OleDbDataReader RemDetailReader = cmdRemDetail.ExecuteReader();


                            while (RemDetailReader.Read())
                            {
                               
                                //GET TOTAL DRUG HERE RETURN AMOUNT ADD A NEW  VARIABLE
                                remAmount = remAmount + double.Parse(RemDetailReader.GetValue(3).ToString().Trim());
                            }

                           
                            //DO THE SUBTRACTION HERE
                            returnBalance = returnBalance - remAmount;
                            remAmount = 0;
                           
                        }

                        if (returnBalance != 0)
                        {
                            allClose = false;
                        }
                        else
                        {


                            string commandRemHead2 = "Select * from REMITTANCEHEADERFILE WHERE REMHEADRETNO='" + returnNo + "'";
                            OleDbCommand cmdRemHead2 = new OleDbCommand(commandRemHead2, DBConnection);
                            OleDbDataReader RemHeadReader2 = cmdRemHead2.ExecuteReader();


                            while (RemHeadReader2.Read())
                            {

                                //CHECK BALANCE HERE
                                string commandRemDetail2 = "Select * from REMITTANCEDETAILFILE WHERE REMDETTRANSNO='" + RemHeadReader2.GetValue(0).ToString().Trim() + "'And REMDETDRUGCODE='" + drugCode + "'";
                                OleDbCommand cmdRemDetail2 = new OleDbCommand(commandRemDetail2, DBConnection);
                                OleDbDataReader RemDetailReader2 = cmdRemDetail2.ExecuteReader();


                                while (RemDetailReader2.Read())
                                {
                                    string sqlDetail = "Select * from RETURNDETAILFILE where RETDETTRANSNO='" + returnNo + "'And RETDETDRUGCODE = '" + drugCode + "'";
                                    OleDbDataAdapter thisAdapter1 = new OleDbDataAdapter(sqlDetail, DBConnection);
                                    OleDbCommandBuilder thisBuilder1 = new OleDbCommandBuilder(thisAdapter1);
                                    DataSet thisDataSet1 = new DataSet();
                                    thisAdapter1.Fill(thisDataSet1, "RETURNDETAILFILE");

                                    foreach (DataRow thisDetailRow in thisDataSet1.Tables["RETURNDETAILFILE"].Rows)
                                    {


                                            thisDetailRow["RETDETSTATUS"] = "CL";
                                        

                                    }
                                    thisAdapter1.Update(thisDataSet1, "RETURNDETAILFILE");
                                    thisAdapter1.AcceptChangesDuringUpdate = true;

                                }

                            }

                            

                        }

                    }


                }
                

                if (allClose)
                {
                    string sqlHeader = "Select * from RETURNHEADERFILE where RETHEADCODE = '" + returnNo + "'";
                    OleDbDataAdapter thisAdapter = new OleDbDataAdapter(sqlHeader, DBConnection);
                    OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);
                    DataSet thisDataSet = new DataSet();
                    thisAdapter.Fill(thisDataSet, "RETURNHEADERFILE");

                    foreach (DataRow thisHeaderRow in thisDataSet.Tables["RETURNHEADERFILE"].Rows)
                    {

                            thisHeaderRow["RETHEADSTATUS"] = "CL";
                      
                    }
                        thisAdapter.Update(thisDataSet, "RETURNHEADERFILE");
                    thisAdapter.AcceptChangesDuringUpdate = true;

                   MessageBox.Show("Return Number has been sucessfully remitted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Data successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DBConnection.Close();

                clearAll();


                    
                
                }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void clearAll()
        {

            returnNo = "";
            remittanceNo = "";

            bonus = 0;
            indexer = 0;
            genericSales = 0;
            brandedSales = 0;
            totalAmountRemit = 0;
            genericCom = 0;
            brandedCom = 0;
            totalNetSales = 0;
            totalComission = 0;
            expectedSale = 0;
            

            txtReturnNo.Clear();
            txtRemittanceNo.Clear();
            txtDrugCode.Clear();
            txtRemAmount.Clear();
            txtApprEmpCode.Clear();
            txtRemarks.Clear();
            lblApprvByName.Text = "";
            lblBonus.Text = "";
            lblBrandedCom.Text = "";
            lblBrandedSales.Text = "";
            lblEmpName.Text = "";
            lblEmpNo.Text = "";
            lblExpectedAmt.Text = "";
            lblGenCom.Text = "";
            lblGenSales.Text = "";
            lblNetSales.Text = "";
            lblTotalCom.Text = "";
            lblTotalRemAmt.Text = "";
            dataGridView1.Rows.Clear();
        }
    
    }
    }


