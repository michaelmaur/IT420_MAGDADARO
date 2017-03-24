/*

REMITTANCE UPDATE

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
    public partial class RemittanceUpdate : Form
    {

        public static string connection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\MichaelMaur\Documents\Visual Studio 2015\Projects\IT420_MAGDADARO\CapstoneDatabase.mdb";
        OleDbConnection DBConnection = new OleDbConnection(connection);

        string returnNo;
        string remittanceNo;
        string empNo;
        string checkEmp;
        string empSame;
        string updateEmp;

        Boolean updateRemAmount = false;
        DateTime remittanceDate;
        DateTime returnDate;
        int indexer = 0;
        int rowDuplicate = 0;
        int countDuplicate = 0;

        double bonus = 0;
        double genericSales = 0;
        double brandedSales = 0;
        double expectedSale = 0;
        double totalAmountRemit = 0;
        double genericCom = 0;
        double brandedCom = 0;
        double totalNetSales = 0;
        double totalComission = 0;
        double totalBalance = 0;



        public RemittanceUpdate()
        {
            InitializeComponent();

        }
        private void txtRemittanceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dataGridView1.Rows.Clear();
                indexer = 0;
                remittanceNo = txtRemittanceNo.Text.ToUpper().Trim();
                checkRemittanceNo(remittanceNo);
            }
        }
        public void checkRemittanceNo(string remittanceNo)
        {
            Boolean remNoFlag = false;


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
                }

                else
                {

                    DBConnection.Open();

                    //CHECK REMITTANCENUMBER IF ALREADY USED
                    string commandRemHead = "Select * from REMITTANCEHEADERFILE WHERE REMHEADTRANSNO='" + remittanceNo + "'";
                    OleDbCommand cmdRemHead = new OleDbCommand(commandRemHead, DBConnection);
                    OleDbDataReader RemHeadReader = cmdRemHead.ExecuteReader();

                    while (RemHeadReader.Read())
                    {
                        //CREATE AN OBJECT FOR REMITTANCEFILE TO CHECK IF THE VALUES ARE VALID
                        remNoFlag = true;
                        returnNo = RemHeadReader.GetValue(1).ToString().Trim();

                            if (RemHeadReader.GetValue(14).ToString().ToUpper().Trim().Equals("OP"))
                            {

                            if (RemHeadReader.GetValue(1).ToString().Trim().Equals(""))
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The Return number cannot be retrieved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);                         
                            }
                            else if (RemHeadReader.GetValue(2).ToString().Trim().Equals(""))
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("Employee number cannot be retrieved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }                            
                            else if (RemHeadReader.GetValue(3) == DBNull.Value)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The date of the remittance transaction cannot be retrieved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(4).ToString().Trim().Equals("") || double.Parse(RemHeadReader.GetValue(4).ToString()) < 0)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The total amount remitted is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(5).ToString().Trim().Equals("") || double.Parse(RemHeadReader.GetValue(5).ToString()) < 0)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The total branded drug sales is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(6).ToString().Trim().Equals("") || double.Parse(RemHeadReader.GetValue(6).ToString()) < 0)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The total generic drug sales is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(7).ToString().Trim().Equals("") || double.Parse(RemHeadReader.GetValue(7).ToString()) < 0)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The total commission is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(8).ToString().Trim().Equals("")|| double.Parse(RemHeadReader.GetValue(8).ToString()) < 0)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The total sales amount is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(9).ToString().Trim().Equals("") || double.Parse(RemHeadReader.GetValue(9).ToString()) < 0)
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The total net sales amount is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (RemHeadReader.GetValue(11).ToString().Trim().Equals(""))
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The Employee number of the employee responsible for checking cannot be retrieved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {


                                double amountRem = double.Parse(RemHeadReader.GetValue(4).ToString().Trim());
                                double bSale = double.Parse(RemHeadReader.GetValue(5).ToString().Trim());
                                double gSale = double.Parse(RemHeadReader.GetValue(6).ToString().Trim());
                                double totalcom = double.Parse(RemHeadReader.GetValue(7).ToString().Trim());                             
                                double expectedSale = double.Parse(RemHeadReader.GetValue(8).ToString().Trim());
                                double nSale = double.Parse(RemHeadReader.GetValue(9).ToString().Trim());


                                if (amountRem == bSale + gSale)
                                {

                                    empNo = RemHeadReader.GetValue(2).ToString().Trim();
                                    remittanceDate = RemHeadReader.GetDateTime(3);
                                    checkEmp = RemHeadReader.GetValue(11).ToString().Trim();

                                    //RETURNHEADERFILE -> RETURNDETAIL -> REMITTANCE HEADER FILE -> REMITTANCEDETAIL
                                    //Pass remittance date to check if its recent that return date
                                    if (checkReturnFile(returnNo, remittanceDate))
                                    {
                                        if (!empSame.Equals(empNo))
                                        {
                                            txtRemittanceNo.Clear();
                                            MessageBox.Show("The Employee number of the agent does not match in the returnfile and remittance file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        }
                                        else
                                        {

                                            string checkEmp = RemHeadReader.GetValue(11).ToString().ToUpper().Trim();
                                            string agent = RemHeadReader.GetValue(2).ToString().ToUpper().Trim();

                                            if (!RemHeadReader.GetValue(12).ToString().Trim().Equals(""))
                                            {
                                                string updateEmp = RemHeadReader.GetValue(12).ToString().ToUpper().Trim();

                                                //CHECK EMPLOYEES HERE.
                                                if (checkRemEmployee(updateEmp, returnDate, "UPDATE"))
                                                {
                                                    if (checkRemEmployee(checkEmp, returnDate, "CHECK"))
                                                    {
                                                        //THEN CHECK REMITTANCE DETAIL IF VALID
                                                        if (!checkRemDetail(remittanceNo, returnNo, bSale, gSale, amountRem, expectedSale, totalcom , nSale))
                                                        {
                                                            //PRINT AGENT NAME HERE
                                                            printAgent(agent, checkEmp);
                                                            lblRetNo.Text = returnNo;
                                                            lblDate.Text = remittanceDate.ToString();

                                                            addDrugFlexGrid(remittanceNo);

                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {

                                                if (checkRemEmployee(checkEmp, returnDate, "CHECK"))
                                                {
                                                    //THEN CHECK REMITTANCE DETAIL IF VALID
                                                    if (!checkRemDetail(remittanceNo, returnNo, bSale, gSale, amountRem, expectedSale, totalcom, nSale))
                                                    {
                                                        //PRINT AGENT NAME HERE
                                                        printAgent(agent, checkEmp);
                                                        lblRetNo.Text = returnNo;
                                                        lblDate.Text = remittanceDate.ToString();

                                                        addDrugFlexGrid(remittanceNo);

                                                        //DISPLAY ALL DRUGS HERE
                                                    }

                                                }

                                            }

                                        }
                                    }
                                    else
                                        clearAll();

                                    //here
                                }
                                else 
                                {
                                    txtRemittanceNo.Clear();
                                    MessageBox.Show("The total sales does not match with the sum of the branded drug sales and generic drug sales", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }



                            }
                            else if (RemHeadReader.GetValue(14).ToString().ToUpper().Trim().Equals("CL"))
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The remittance number has already been remitted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                            {
                                txtRemittanceNo.Clear();
                                MessageBox.Show("The remittance status cannot be identified", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }


                    }
                    if (!remNoFlag)
                        {
                            txtRemittanceNo.Clear();
                            MessageBox.Show("Remittance Number is not yet used. Cannot retrieve any information ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    DBConnection.Close();

                    
                }
            }
        }

        public Boolean checkReturnFile(string returnNo, DateTime remittanceDate)
        {

            Boolean retNoFlag = true;
            Boolean checkReturnFlag = true;

            //CHECK RETURNHEADER IF RETURN NUMBER IS USED
            string commandRetHead = "Select * from RETURNHEADERFILE WHERE RETHEADCODE='" + returnNo + "'";
            OleDbCommand cmdRetHead = new OleDbCommand(commandRetHead, DBConnection);
            OleDbDataReader RetHeadReader = cmdRetHead.ExecuteReader();

            while (RetHeadReader.Read())
            {
                retNoFlag = false;

                if (RetHeadReader.GetValue(10).ToString().ToUpper().Trim().Equals("OP") || RetHeadReader.GetValue(10).ToString().ToUpper().Trim().Equals("CL"))
                {

                    if (RetHeadReader.GetValue(2) == DBNull.Value)
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("Return Date is missing. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
                    }
                    else if (RetHeadReader.GetDateTime(2) > remittanceDate)
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("The return date is recent that the return date", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
                    }

                    else if (RetHeadReader.GetValue(3).ToString().Trim().Equals(""))
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("There is no record of the agent who performed the return transaction. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(4).ToString().Trim().Equals("") || int.Parse(RetHeadReader.GetValue(4).ToString()) < 0)
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("The total quantity dispensed is invalid. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(5).ToString().Trim().Equals("") || int.Parse(RetHeadReader.GetValue(5).ToString()) < 0)
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("The total quantity returned is invalid. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(6).ToString().Trim().Equals(""))
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("There is no record of the employee who verified the transaction. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
                    }
                    else if (RetHeadReader.GetValue(9).ToString().Trim().Equals("") || int.Parse(RetHeadReader.GetValue(9).ToString()) < 0)
                    {
                        checkReturnFlag = false;
                        MessageBox.Show("The total quantity sold is invalid. Please update the Return transaction first before performing the Remittance Transaction", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemittanceNo.Clear();
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
                                            checkReturnFlag = false;
                                            break;
                                    }
                                    else if (qtyOut != qtyReturn + qtySold)
                                    {
                                        MessageBox.Show("The quantity dispense does not match with the quantity sold and quantity returned. Please update the return transaction first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        txtRemittanceNo.Clear();
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
                                        checkReturnFlag = false;
                                    break;
                                }
                                else if (qtyOut != qtyReturn + qtySold)
                                {
                                    MessageBox.Show("The quantity dispense does not match with the quantity sold and quantity returned. Please update the return transaction first", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtRemittanceNo.Clear();
                                }

                            }
                            else
                            {
                                checkReturnFlag = false;
                            }
                        }

                    }
                }
                else
                {

                    MessageBox.Show("The status of this return transaction cannot be identified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkReturnFlag = false;
                    txtRemittanceNo.Clear();
                }

            }//WHILE RETURNHEADER

            if (retNoFlag)
            {
                MessageBox.Show("There is no record for the Return transaction. Return Number not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRemittanceNo.Clear();
            }

            return checkReturnFlag;
        }

        public Boolean checkRetDetail(string returnNo, int totOut, int totSold, int totReturn)
        {
            Boolean retDeFlag = true;
            Boolean errorFlag = false;
            Boolean quantityFlag = false;

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
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The drug code in the return file is missing", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    else if (!checkDrugHeader(RetDetailReader.GetValue(1).ToString().ToUpper().Trim()))
                    {
                        txtRemittanceNo.Clear();
                        errorFlag = false;
                        break;
                    }
                    if (RetDetailReader.GetValue(2).ToString().Trim().Equals("") || int.Parse(RetDetailReader.GetValue(2).ToString()) < 0)
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The drug quantity dispensed is invalid", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    else if (RetDetailReader.GetValue(3).ToString().Trim().Equals("") || int.Parse(RetDetailReader.GetValue(3).ToString()) < 0)
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The drug quantity returned is missing", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    else if (RetDetailReader.GetValue(4).ToString().Trim().Equals("") || int.Parse(RetDetailReader.GetValue(4).ToString()) < 0)
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The drug quantity sold is missing", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = false;
                        break;
                    }
                    //IF QTY !=QTYSOLD + QTYRETURN
                    else if (double.Parse(RetDetailReader.GetValue(2).ToString()) != double.Parse(RetDetailReader.GetValue(3).ToString()) + double.Parse(RetDetailReader.GetValue(4).ToString()))
                    {
                        quantityFlag = true;
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
                    txtRemittanceNo.Clear();
                    errorFlag = false;
                    MessageBox.Show("The status of return detail cannot be identified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }//WHILE RETURNDETAIL


            if (retDeFlag)
            {
                txtRemittanceNo.Clear();
                MessageBox.Show("The return detail records cannot be retrieved. It might have been deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (quantityFlag)
            {
                txtRemittanceNo.Clear();
                MessageBox.Show("The sum of the drugs sold and returned is not equal to the quantity dispensed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!quantityFlag)
            {
                if (drgOut != totOut)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The quantity dispensed in the return header does not match the quantity of return detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFlag = false;
                }
                else if (drgReturn != totReturn)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The quantity returned in the return header does not match the quantity of return detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFlag = false;
                }
                else if (drgSold != totSold)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The quantity sold in the return header does not match the quantity of return detail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFlag = false;
                }

            }

            return errorFlag;
        }

        public Boolean checkRemDetail(string remittanceNo, string returnNo, double bSale, double gSale, double amountRem, double expectedSale, double totalcom , double nSale)
        {
            Boolean remDetFlag = true;
            Boolean errorFlag = false;

            double detailBrandedSale = 0;
            double detailGenSale = 0;

            double detailExBrandedSale = 0;
            double detailExGenSale =0;

            double brandedcom = 0;
            double genericcom = 0;


            string commandRemDe = "Select * from REMITTANCEDETAILFILE WHERE REMDETTRANSNO='" + remittanceNo + "'";
            OleDbCommand cmdRemDe = new OleDbCommand(commandRemDe, DBConnection);
            OleDbDataReader RemDetReader = cmdRemDe.ExecuteReader();

            while (RemDetReader.Read())
            {
                remDetFlag = false;

                if (RemDetReader.GetValue(4).ToString().ToUpper().Trim().Equals("OP"))
                {
                    string drugCode = RemDetReader.GetValue(1).ToString().ToUpper().Trim();

                    //checkreturndetail here. return detail already checked the drugs
                    if (!checkReturnDetail(drugCode, returnNo))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The Remittance Detial drug was not returned", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = true;
                        break;
                    }
                    else if (RemDetReader.GetValue(2).ToString().ToUpper().Trim().Equals(""))
                    {
                        MessageBox.Show("The expected sales amount in the return detail file is missing", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        errorFlag = true;
                        break;
                    }
                    else if (RemDetReader.GetValue(3).ToString().ToUpper().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The amount remitted in the return detail file is missing", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    else
                    {

                        //checkdrugcode here
                        
                       

                        string commandDrug = "Select * from DRUGINVENTORYHEADERFILE WHERE DRUGHEADCODE='" + drugCode + "'";
                        OleDbCommand cmdDrug = new OleDbCommand(commandDrug, DBConnection);
                        OleDbDataReader DrugReader = cmdDrug.ExecuteReader();

                        while (DrugReader.Read())
                        {

                            if (DrugReader.GetValue(7).ToString().Trim().Equals("BRANDED"))
                            {
                                
                                detailBrandedSale = detailBrandedSale + double.Parse(RemDetReader.GetValue(2).ToString());
                                detailExBrandedSale = detailExBrandedSale + double.Parse(RemDetReader.GetValue(3).ToString());
                            }
                            else if (DrugReader.GetValue(7).ToString().Trim().Equals("GENERIC"))
                            {
                                detailGenSale = detailGenSale + double.Parse(RemDetReader.GetValue(2).ToString());
                                detailExGenSale = detailExGenSale + double.Parse(RemDetReader.GetValue(3).ToString());                              
                            }
                        }
                    }

                }
                else if (RemDetReader.GetValue(4).ToString().ToUpper().Trim().Equals("CL"))
                {
                    txtRemittanceNo.Clear();
                    errorFlag = true;
                    MessageBox.Show("The Remittance Detial file is closed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;                   
                }
                else
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The status of Remittance Detial file cannot be identified", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }


            brandedcom = detailExBrandedSale * .30;
            genericcom = detailExGenSale * .20;

            //ERROR CHECKING
            if (remDetFlag)
            {
                txtRemittanceNo.Clear();
                MessageBox.Show("Remittance Number is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorFlag = true;
            }

            if (!errorFlag)
            {
                if (nSale != amountRem - totalcom)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The net sale does not match from the difference of the amount remitted and the total commision", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorFlag = true;
                }
                else if (expectedSale != detailBrandedSale + detailGenSale)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The total sales from the headerfile does not match from the detail file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorFlag = true;
                }
                else if (amountRem != detailExBrandedSale + detailExGenSale)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The total amount remitted from the headerfile does not match from the detail file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorFlag = true;
                }
                else if (bSale != detailExBrandedSale)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The total branded sales from the header file does not match from retail file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorFlag = true;
                }
                else if (gSale != detailExGenSale)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The total generic sales from the header file does not match from retail file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorFlag = true;
                }
                else if (totalcom != genericcom + brandedcom)
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The total commission from the header file does not match from retail file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorFlag = true;
                }
                
            }
            



            return errorFlag;
                 
        }

        public Boolean checkReturnDetail(string drugCode, string returnNo)
        {
            Boolean flag = false;


            string commandReturn = "Select * from RETURNDETAILFILE WHERE RETDETTRANSNO='" + returnNo + "'And RETDETDRUGCODE='" + drugCode + "'";
            OleDbCommand cmdRet = new OleDbCommand(commandReturn, DBConnection);
            OleDbDataReader RetReader = cmdRet.ExecuteReader();

            while (RetReader.Read())
            {
                flag = true;
                break;
            }

            return flag;

        }
        public void addDrugFlexGrid(string remittanceNo)
        {

            int qtySold = 0;
            double retailPrice = 0;
            double salesAmount = 0;
            double remAmount = 0;

            string drugCode = "";
            string checkDrugType = "";

            brandedCom = 0;
            genericCom = 0;
            brandedSales = 0;
            genericSales = 0;


            string commandRemDe = "Select * from REMITTANCEDETAILFILE WHERE REMDETTRANSNO='" + remittanceNo + "'";
            OleDbCommand cmdRemDe = new OleDbCommand(commandRemDe, DBConnection);
            OleDbDataReader RemDetReader = cmdRemDe.ExecuteReader();

            while (RemDetReader.Read())
            {

                drugCode = RemDetReader.GetValue(1).ToString().ToUpper().Trim();

                string commandReturn = "Select * from RETURNDETAILFILE WHERE RETDETTRANSNO='" + returnNo + "'And RETDETDRUGCODE='" + drugCode + "'";
                OleDbCommand cmdRet = new OleDbCommand(commandReturn, DBConnection);
                OleDbDataReader RetReader = cmdRet.ExecuteReader();

                while (RetReader.Read())
                {

                    dataGridView1.Rows.Add();

                    string commandDrug = "Select * from DRUGINVENTORYHEADERFILE WHERE DRUGHEADCODE='" + drugCode + "'";
                    OleDbCommand cmdDrug = new OleDbCommand(commandDrug, DBConnection);
                    OleDbDataReader DrugReader = cmdDrug.ExecuteReader();

                    

                    while (DrugReader.Read())
                    {
                        qtySold = int.Parse(RetReader.GetValue(4).ToString().ToUpper().Trim());
                        retailPrice = double.Parse(DrugReader.GetValue(6).ToString().ToUpper().Trim());
                        checkDrugType = DrugReader.GetValue(7).ToString().ToUpper().Trim();
                        remAmount = double.Parse(RemDetReader.GetValue(3).ToString().ToUpper().Trim());
                        //open remittanceheader and detailhere

                        dataGridView1.Rows[indexer].Cells[0].Value = DrugReader.GetValue(0).ToString().ToUpper().Trim();
                        dataGridView1.Rows[indexer].Cells[1].Value = DrugReader.GetValue(2).ToString().ToUpper().Trim();
                        dataGridView1.Rows[indexer].Cells[2].Value = DrugReader.GetValue(7).ToString().ToUpper().Trim();
                        dataGridView1.Rows[indexer].Cells[3].Value = RetReader.GetValue(4).ToString().ToUpper().Trim();
                        dataGridView1.Rows[indexer].Cells[4].Value = DrugReader.GetValue(6).ToString().ToUpper().Trim();
                        dataGridView1.Rows[indexer].Cells[5].Value = qtySold * retailPrice;
                        dataGridView1.Rows[indexer].Cells[6].Value = RemDetReader.GetValue(3).ToString().ToUpper().Trim();

                        salesAmount = qtySold * retailPrice;
                    }



                    if (checkDrugType.Equals("BRANDED"))
                    {
                        brandedSales = brandedSales + remAmount;
                        brandedCom = brandedCom + (remAmount * .30);
                    }
                    else if (checkDrugType.Equals("GENERIC"))
                    {
                        genericSales = genericSales + remAmount;
                        genericCom = genericCom + (remAmount * .20);
                    }

                }

                indexer = indexer + 1;
            }


            string commandRemHead = "Select * from REMITTANCEHEADERFILE WHERE REMHEADTRANSNO='" + remittanceNo + "'";
            OleDbCommand cmdRemHead = new OleDbCommand(commandRemHead, DBConnection);
            OleDbDataReader RemHeadReader = cmdRemHead.ExecuteReader();


            while (RemHeadReader.Read())
            {

                lblExpectedAmt.Text = RemHeadReader.GetValue(8).ToString().ToUpper().Trim() + "";
                lblTotalRemAmt.Text = RemHeadReader.GetValue(4).ToString().ToUpper().Trim() + "";
                lblGenSales.Text = RemHeadReader.GetValue(6).ToString().ToUpper().Trim() + "";
                lblBrandedSales.Text = RemHeadReader.GetValue(5).ToString().ToUpper().Trim() + "";
                lblBrandedCom.Text = brandedCom + "";
                lblGenCom.Text = genericCom + "";
                lblNetSales.Text = RemHeadReader.GetValue(9).ToString().ToUpper().Trim() + "";
                lblTotalCom.Text = RemHeadReader.GetValue(7).ToString().ToUpper().Trim() + "";
                lblBalance.Text = RemHeadReader.GetValue(10).ToString().ToUpper().Trim() + "";
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
                else if (DrugReader.GetValue(6).ToString().Trim().Equals("") || double.Parse(DrugReader.GetValue(6).ToString().Trim()) <= 0)
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
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The first name of the employee who approved the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The last name of the employee who approved the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The role / position of the employee who approved the return transaction is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The hire date of the employee who approved the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The rights of the employee who approved the return transaction are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString().ToUpper().Trim();

                        //IF EMPLOYEE IS AN AGENT
                       
                         if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtRemittanceNo.Clear();
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
                            txtRemittanceNo.Clear();
                            MessageBox.Show("The employee who checked the return transaction has no right to process the transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("Employee is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The status of employee who approved the return transaction is not specified", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtRemittanceNo.Clear();
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
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The first name of the employee who updated the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The last name of the employee who updated the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The role / position of the employee who updated the return transaction is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The hiredate of the employee who updated the return transaction is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The rights of the employee who updated the return transaction are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString().ToUpper().Trim();

                        //IF EMPLOYEE IS AN AGENT
                         if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtRemittanceNo.Clear();
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
                            txtRemittanceNo.Clear();
                            MessageBox.Show("The employee who updated the return transaction is not eligible to process the transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The status of the employee who updated the return transaction is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The status of mployee who updated the return transaction is not speciefied in the record ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtRemittanceNo.Clear();
                MessageBox.Show("The employee who updated the record is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return errorFlag;
        }

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
                        txtRemittanceNo.Clear();
                        MessageBox.Show("Employee's first name is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("Employee's last name is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("Employee's position is not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("Employee's hire date is not in record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("Employee's rights are not specified in the record", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString();

                        //IF EMPLOYEE IS AN AGENT
                       
                        if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtRemittanceNo.Clear();
                            MessageBox.Show("Employee's hiredate is recent than the return transaction date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (empRights == 3 && empPos.ToUpper().Equals("AGENT"))
                        {
                            if (!EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(3).ToString().ToUpper().Trim().Equals("") || !EmpReader.GetValue(4).ToString().ToUpper().Trim().Equals(""))
                            {
                                //lblEmpNo.Text = empNo;
                                //lblEmpName.Text = EmpReader.GetValue(1).ToString().ToUpper().Trim() + " " + EmpReader.GetValue(3).ToString().ToUpper().Trim() + ". " + EmpReader.GetValue(2).ToString().ToUpper().Trim();
                                empSame = empNo;

                            }
                        }
                        else if (empRights != 3 || !empPos.ToUpper().Equals("AGENT"))
                        {
                            txtRemittanceNo.Clear();
                            MessageBox.Show("Employee record shows that  the employee is not an agent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("Employee is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("Employee status cannot be retrieved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtRemittanceNo.Clear();
                MessageBox.Show("Employee Record is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public Boolean checkRemEmployee(string empNo, DateTime returnDate, string empType)
        {

            Boolean empFlag = true;
            Boolean errorFlag = false;
            string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + empNo + "'";
            OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
            OleDbDataReader EmpReader = cmdEmp.ExecuteReader();

            string text = "";

            if (empType.Equals("CHECK"))
                text = "Appoved employee ";

            else if (empType.Equals("UPDATE"))

                text = "Update employee ";


            while (EmpReader.Read())
            {
                empFlag = false;

                if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("AC"))
                {

                    //CHECK FOR EMPTYFIELDS HERE FNAME+LNAME EMPPOS+EMPRIGHTS HIREDATE  
                    if (EmpReader.GetValue(1).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The first name of the employee is not in record", text + "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(2).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The last name of the employee is not in record", text + "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(5).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The role / position of the employee is not specified in the record", text + "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(7).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The hire date of the employee is not in record", text + "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (EmpReader.GetValue(8).ToString().Trim().Equals(""))
                    {
                        txtRemittanceNo.Clear();
                        MessageBox.Show("The access rights of the employee is not specified in the record", text + "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    //CHECK HIREDATE>CURRENTDATE HIREDATE>RETURNDATE 
                    else
                    {
                        int empRights = int.Parse(EmpReader.GetValue(8).ToString());
                        string empPos = EmpReader.GetValue(5).ToString().ToUpper().Trim();

                        //IF EMPLOYEE IS AN AGENT

                        if (EmpReader.GetDateTime(7) > returnDate)
                        {
                            txtRemittanceNo.Clear();
                            MessageBox.Show("Employee's hire date is recent than the return transaction date", text + "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            txtRemittanceNo.Clear();
                            MessageBox.Show("The employee has no right to process the transaction.", text + "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("Employee is currently inactive", text + "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtRemittanceNo.Clear();
                    MessageBox.Show("The status of employee is not specified", text + "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//WHILE EMP

            if (empFlag)
            {
                txtRemittanceNo.Clear();
                MessageBox.Show("The employee is not found", text + "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return errorFlag;
        }

        public void printAgent(string agent, string checkEmp)
        {

            string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + agent + "'";
            OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
            OleDbDataReader EmpReader = cmdEmp.ExecuteReader();



            while (EmpReader.Read())
            {
                lblEmpNo.Text = EmpReader.GetValue(0).ToString().ToUpper().Trim();
                lblEmpName.Text = EmpReader.GetValue(1).ToString().ToUpper().Trim() + " " + EmpReader.GetValue(3).ToString().ToUpper().Trim() + ". " + EmpReader.GetValue(2).ToString().ToUpper().Trim();
            }


            string commandEmpCheck = "Select * from EMPLOYEEFILE WHERE EMPNO='" + checkEmp + "'";
            OleDbCommand cmdEmpCheck = new OleDbCommand(commandEmpCheck, DBConnection);
            OleDbDataReader EmpReaderCheck = cmdEmpCheck.ExecuteReader();



            while (EmpReaderCheck.Read())
            {
                lblApprvByName.Text = EmpReaderCheck.GetValue(1).ToString().ToUpper().Trim() + " " + EmpReaderCheck.GetValue(3).ToString().ToUpper().Trim() + ". " + EmpReaderCheck.GetValue(2).ToString().ToUpper().Trim();
                txtDrugCode.Focus();
            }
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


            // if textbox is empty
            if (String.IsNullOrEmpty(drugCode))
            {
                MessageBox.Show("Please enter the Drug Code in the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDrugCode.Clear();
                txtDrugCode.Focus();
            }
            //user has not yet entered the return no
            else if (String.IsNullOrEmpty(remittanceNo))
            {
                MessageBox.Show("Please enter the Remittance No. in the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRemittanceNo.Focus();
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
                else if ((indexer == 0 && dataGridView1.Rows[indexer].Cells[0].Value != null))
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
                        string commandReturn = "Select * from RETURNDETAILFILE WHERE RETDETTRANSNO='" + returnNo + "'And RETDETDRUGCODE='" + drugCode + "'";
                        OleDbCommand cmdRet = new OleDbCommand(commandReturn, DBConnection);
                        OleDbDataReader RetReader = cmdRet.ExecuteReader();

                        while (RetReader.Read())
                        {
                            retDetailFlag = false;
                           
                        }//END WHILE 

                        if (retDetailFlag)
                        {
                            txtDrugCode.Clear();
                            txtDrugCode.Focus();
                            MessageBox.Show("Drug Code is not on Return File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (checkDuplicate(drugCode))
                            {

                                DialogResult dr = MessageBox.Show("Do you want to update " + drugCode + "?", "Missing information", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (dr == DialogResult.Yes)
                                {

                                    updateRemAmount = true;
                                    txtRemAmount.Focus();
                                }

                                if (dr == DialogResult.No)
                                {
                                    txtDrugCode.Clear();
                                }

                                // remove yes and no
                            }
                            else
                            {
                                MessageBox.Show("No record of this drug code for this Remittance Number. Please create a new remittance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtDrugCode.Clear();
                                txtDrugCode.Focus();

                            }
                        }
                        
                    }

                }

                DBConnection.Close();
            }
        }
        private void txtApprEmpCode_KeyPress(object sender, KeyPressEventArgs e)
        {//
            if(e.KeyChar == (char)Keys.Enter)
            {
                DBConnection.Open();
                updateEmp = txtApprEmpCode.Text.ToUpper().Trim();
                if (checkRemEmployee(updateEmp, returnDate, "UPDATE"))
                {

                    string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + updateEmp + "'";
                    OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
                    OleDbDataReader EmpReader = cmdEmp.ExecuteReader();



                    while (EmpReader.Read())
                    {
                        lblUpdateEmp.Text = EmpReader.GetValue(1).ToString().ToUpper().Trim() + " " + EmpReader.GetValue(3).ToString().ToUpper().Trim() + ". " + EmpReader.GetValue(2).ToString().ToUpper().Trim();
                    }

                }
                
                DBConnection.Close();
            }
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
                else if (String.IsNullOrEmpty(remittanceNo))
                {
                    MessageBox.Show("Please enter the Remittance No. in the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRemittanceNo.Focus();
                    txtRemAmount.Clear();
                }
                else if (double.Parse(remAmount) < 0)
                {
                    MessageBox.Show("Amount should be not lesser than zero", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            totalBalance = expectedSale - totalAmountRemit;

                        }

                        lblExpectedAmt.Text = expectedSale + "";
                        lblTotalRemAmt.Text = totalAmountRemit + "";
                        lblBalance.Text = totalBalance + "";
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
                        totalBalance = expectedSale - totalAmountRemit;

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
        public Boolean checkDuplicate(string drugcode)
        {

            Boolean found = false;
            countDuplicate = 0;

            for (int x = 0; x < dataGridView1.Rows.Count - 1; x++)
            {
                if (drugcode.Equals(dataGridView1.Rows[x].Cells[0].Value.ToString().ToUpper().Trim()))
                {
                    found = true;
                    countDuplicate = countDuplicate + 1;
                    rowDuplicate = x;
                }
            }

            return found;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void txtUpdateEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string updateEmp = txtApprEmpCode.Text.ToUpper().Trim();
                checkUpdateEmployee(updateEmp);
            }
        }
        public void checkUpdateEmployee(string updateEmp)
        {
            Boolean empApprFlag = true;

            // if textbox is empty
            if (String.IsNullOrEmpty(updateEmp))
            {
                MessageBox.Show("Please enter the Return Number the box Provided", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                txtUpdateEmpNo.Clear();
                lblUpdateEmpName.Text = "";
            }
            // if textbox is not empty
            else if (!String.IsNullOrEmpty(updateEmp))
            {
                //check for special characters
                if (checkSpecialChar(updateEmp))
                {
                    MessageBox.Show("Please enter a Employee No. without any special characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUpdateEmpNo.Clear();
                    lblUpdateEmpName.Text = "";
                }
                else
                {

                    DBConnection.Open();

                    string commandEmp = "Select * from EMPLOYEEFILE WHERE EMPNO='" + updateEmp + "'";
                    OleDbCommand cmdEmp = new OleDbCommand(commandEmp, DBConnection);
                    OleDbDataReader EmpReader = cmdEmp.ExecuteReader();

                    while (EmpReader.Read())
                    {
                        empApprFlag = false;
                        if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("AC"))
                        {
                            //CHECK HIREDATE
                            DateTime Hiredate = DateTime.Parse(EmpReader.GetValue(7).ToString());

                            //HIREDATE IS GREATER THAN CURRENT DATE
                            if (Hiredate > DateTime.Now)
                            {
                                txtUpdateEmpNo.Clear();
                                lblUpdateEmpName.Text = "";
                                lblEmpNo.Text = "";
                                lblEmpName.Text = "";
                                MessageBox.Show("Employee's hire date is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //CHECK RIGHTS
                                int empRights;
                                empRights = int.Parse(EmpReader.GetValue(8).ToString());

                                //IF EMPLOYEE IS AN AGENT
                                if (empRights == 1 || empRights == 2)
                                {
                                    //EMPLOYEE LAST NAME IS EMPTY
                                    if (EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals("") || EmpReader.GetValue(3).ToString().ToUpper().Trim().Equals("") || EmpReader.GetValue(2).ToString().ToUpper().Trim().Equals(""))
                                    {
                                        txtUpdateEmpNo.Clear();
                                        lblUpdateEmpName.Text = "";
                                        MessageBox.Show("Employee Name on Return File not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {

                                        lblUpdateEmpName.Text = EmpReader.GetValue(1).ToString().ToUpper().Trim() + " " + EmpReader.GetValue(3).ToString().ToUpper().Trim() + ". " + EmpReader.GetValue(2).ToString().ToUpper().Trim();
                                        txtRemittanceNo.Focus();

                                    }

                                }
                                //EMPRIGHTS != 1 || EMPLOYEE IS NOT AN OWNER / MANAGER
                                else if (empRights != 1 || empRights != 2)
                                {
                                    txtUpdateEmpNo.Clear();
                                    lblUpdateEmpName.Text = "";
                                    MessageBox.Show("Employee is not authrorized to process the transaction", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        //EMPLOYEE IS INACTIVE
                        else if (EmpReader.GetValue(9).ToString().ToUpper().Trim().Equals("IN"))
                        {
                            txtUpdateEmpNo.Clear();
                            lblUpdateEmpName.Text = "";
                            MessageBox.Show("Employee is currently inactive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    DBConnection.Close();

                    if (empApprFlag)
                    {
                        txtUpdateEmpNo.Clear();
                        txtUpdateEmpNo.Focus();
                        MessageBox.Show("Employee Name not on Record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        public double calculateBonus(string empNo)
        {

            DateTime hireDate;
            double yearsService = 0;


            DBConnection.Open();
            string command = "Select * from EMPLOYEEFILE WHERE EMPNO='" + empNo + "'";
            OleDbCommand cmdH = new OleDbCommand(command, DBConnection);
            OleDbDataReader HReader = cmdH.ExecuteReader();

            while (HReader.Read())
            {
                if (HReader.GetValue(0).ToString().ToUpper().Trim().Equals(empNo))
                {
                    hireDate = Convert.ToDateTime(HReader.GetValue(7));
                    //currentDate = dateTimePicker1.Value;

                    //yearsService = (currentDate - hireDate).TotalDays;
                }
            }

            DBConnection.Close();

            if (yearsService / 365 <= 1)
                bonus = 25;

            else
            {
                for (int temp = (int)yearsService / 365; temp >= 1; temp--)
                {
                    bonus = bonus + 10;

                }
                bonus = bonus + 25;
            }

            return bonus;

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
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateAll();
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBConnection.Close();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void UpdateAll()
        {
            //ADD CHECK TO TEXTFIELDS
            
            if (remittanceNo != txtRemittanceNo.Text.ToUpper().Trim() || remittanceNo.Trim().Equals(""))
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
                        findRowRemDetail.BeginEdit();
                        findRowRemDetail["REMDETEXPSALES"] = Convert.ToDouble(dataGridView1.Rows[n].Cells[5].Value); ;
                        findRowRemDetail["REMDETAMTREMITTED"] = Convert.ToDouble(dataGridView1.Rows[n].Cells[6].Value);
                        findRowRemDetail.EndEdit();

                    }


                    thisAdapterRemDetail.Update(thisDataSetRemDetail, "REMITTANCEDETAILFILE");
                    thisAdapterRemDetail.AcceptChangesDuringUpdate = true;
                    DBConnection.Close();
                }

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

                if (findRowRemHeader!=null)
                {

                    totalBalance = expectedSale - totalAmountRemit;

                    //do a while loop to read the missing amount
                    //open returnfile - opendrugcode


                    findRowRemHeader.BeginEdit();

                    findRowRemHeader[4] = totalAmountRemit;
                    findRowRemHeader[5] = brandedSales;
                    findRowRemHeader[6] = genericSales;
                    findRowRemHeader[7] = totalComission;
                    findRowRemHeader[8] = expectedSale;
                    findRowRemHeader[9] = totalNetSales;
                    findRowRemHeader[10] = totalBalance;
                    findRowRemHeader[12] = txtApprEmpCode.Text;
                    findRowRemHeader[13] = txtRemarks.Text.ToString().ToUpper();
                    
                    findRowRemHeader.EndEdit();



                }
                thisAdapterRemHeader.Update(thisDataSetRemHeader, "REMITTANCEHEADERFILE");
                thisAdapterRemHeader.AcceptChangesDuringUpdate = true;
                DBConnection.Close();


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

                        returnBalance = (price * qtyReturned);

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

                            string sqlDetail = "Select * from RETURNDETAILFILE where RETDETTRANSNO='" + returnNo + "'And RETDETDRUGCODE = '" + drugCode + "'";
                            OleDbDataAdapter thisAdapter1 = new OleDbDataAdapter(sqlDetail, DBConnection);
                            OleDbCommandBuilder thisBuilder1 = new OleDbCommandBuilder(thisAdapter1);
                            DataSet thisDataSet1 = new DataSet();
                            thisAdapter1.Fill(thisDataSet1, "RETURNDETAILFILE");

                            foreach (DataRow thisDetailRow in thisDataSet1.Tables["RETURNDETAILFILE"].Rows)
                            {
                                                            
                                    thisDetailRow["RETDETSTATUS"] = "OP";
                                
                            }
                            thisAdapter1.Update(thisDataSet1, "RETURNDETAILFILE");
                            thisAdapter1.AcceptChangesDuringUpdate = true;
                        }
                        else
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
                {

                    string sqlHeader = "Select * from RETURNHEADERFILE where RETHEADCODE = '" + returnNo + "'";
                    OleDbDataAdapter thisAdapter = new OleDbDataAdapter(sqlHeader, DBConnection);
                    OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);
                    DataSet thisDataSet = new DataSet();
                    thisAdapter.Fill(thisDataSet, "RETURNHEADERFILE");

                    foreach (DataRow thisHeaderRow in thisDataSet.Tables["RETURNHEADERFILE"].Rows)
                    {

                            thisHeaderRow["RETHEADSTATUS"] = "OP";
                        
                    }
                    thisAdapter.Update(thisDataSet, "RETURNHEADERFILE");
                    thisAdapter.AcceptChangesDuringUpdate = true;

                    MessageBox.Show("Data successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                DBConnection.Close();

                clearAll();

            }

        }


        public void clearAll()
        {

            returnNo = "";
            remittanceNo = "";
            empNo ="";
            checkEmp= "";
            empSame = "";
            updateEmp ="";

            indexer = 0;
            rowDuplicate = 0;
            countDuplicate = 0;

            bonus = 0;
            genericSales = 0;
            brandedSales = 0;
            expectedSale = 0;
            totalAmountRemit = 0;
            genericCom = 0;
            brandedCom = 0;
            totalNetSales = 0;
            totalComission = 0;
            totalBalance = 0;

            txtRemittanceNo.Clear();
            txtRemarks.Clear();
            txtDrugCode.Clear();
            txtRemAmount.Clear();
            txtRemarks.Clear();
            txtApprEmpCode.Clear();

            lblRetNo.Text = "";
            lblApprvByName.Text = "";

            lblCheckEmp.Text = "";
            lblBonus.Text = "";
            lblBrandedCom.Text = "";
            lblBrandedSales.Text = "";
            lblEmpName.Text = "";
            lblEmpNo.Text = "";
            lblExpectedAmt.Text = "";
            lblUpdateEmp.Text = "";
            lblGenCom.Text = "";
            lblGenSales.Text = "";
            lblNetSales.Text = "";
            lblTotalRemAmt.Text = "";
            lblTotalCom.Text = "";
            lblDate.Text = "";
            dataGridView1.Rows.Clear();
        }

       
    }
}
