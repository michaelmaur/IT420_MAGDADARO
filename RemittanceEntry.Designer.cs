namespace WindowsFormsApplication2
{
    partial class RemittanceEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemittanceNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEmpName = new System.Windows.Forms.Label();
            this.lblEmpNo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBonus = new System.Windows.Forms.Label();
            this.lblGenCom = new System.Windows.Forms.Label();
            this.lblBrandedCom = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblTotalCom = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblNetSales = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblTotalRemAmt = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblExpectedAmt = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBrandedSales = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblGenSales = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grdDrugCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRemAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDrugCode = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtApprEmpCode = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.lblApprvByName = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtReturnNo = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remittanceEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remittanceUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Return Number:";
            // 
            // txtRemittanceNo
            // 
            this.txtRemittanceNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemittanceNo.Location = new System.Drawing.Point(185, 110);
            this.txtRemittanceNo.Name = "txtRemittanceNo";
            this.txtRemittanceNo.Size = new System.Drawing.Size(100, 20);
            this.txtRemittanceNo.TabIndex = 3;
            this.txtRemittanceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemittanceNo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Remittance Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(459, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblEmpName);
            this.groupBox1.Controls.Add(this.lblEmpNo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(39, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 92);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Employee Information";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(165, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Employee Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Employee Number";
            // 
            // lblEmpName
            // 
            this.lblEmpName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblEmpName.Location = new System.Drawing.Point(165, 34);
            this.lblEmpName.MinimumSize = new System.Drawing.Size(150, 0);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(265, 17);
            this.lblEmpName.TabIndex = 1;
            // 
            // lblEmpNo
            // 
            this.lblEmpNo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblEmpNo.Location = new System.Drawing.Point(30, 34);
            this.lblEmpNo.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblEmpNo.Name = "lblEmpNo";
            this.lblEmpNo.Size = new System.Drawing.Size(113, 17);
            this.lblEmpNo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblBonus);
            this.groupBox2.Controls.Add(this.lblGenCom);
            this.groupBox2.Controls.Add(this.lblBrandedCom);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.lblTotalCom);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.lblNetSales);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.lblTotalRemAmt);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.lblExpectedAmt);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.lblBrandedSales);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblGenSales);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.txtRemAmount);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDrugCode);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(39, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 368);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sales Information";
            // 
            // lblBonus
            // 
            this.lblBonus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblBonus.Location = new System.Drawing.Point(623, 235);
            this.lblBonus.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblBonus.Name = "lblBonus";
            this.lblBonus.Size = new System.Drawing.Size(112, 17);
            this.lblBonus.TabIndex = 41;
            // 
            // lblGenCom
            // 
            this.lblGenCom.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblGenCom.Location = new System.Drawing.Point(505, 235);
            this.lblGenCom.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblGenCom.Name = "lblGenCom";
            this.lblGenCom.Size = new System.Drawing.Size(112, 17);
            this.lblGenCom.TabIndex = 40;
            // 
            // lblBrandedCom
            // 
            this.lblBrandedCom.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblBrandedCom.Location = new System.Drawing.Point(387, 235);
            this.lblBrandedCom.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblBrandedCom.Name = "lblBrandedCom";
            this.lblBrandedCom.Size = new System.Drawing.Size(112, 17);
            this.lblBrandedCom.TabIndex = 39;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(623, 252);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 13);
            this.label24.TabIndex = 38;
            this.label24.Text = "Bonus";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(510, 252);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(102, 13);
            this.label23.TabIndex = 37;
            this.label23.Text = "Generic Commission";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(387, 252);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(105, 13);
            this.label22.TabIndex = 36;
            this.label22.Text = "Branded Commission";
            // 
            // lblTotalCom
            // 
            this.lblTotalCom.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblTotalCom.Location = new System.Drawing.Point(623, 328);
            this.lblTotalCom.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblTotalCom.Name = "lblTotalCom";
            this.lblTotalCom.Size = new System.Drawing.Size(112, 17);
            this.lblTotalCom.TabIndex = 35;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(494, 328);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(92, 13);
            this.label19.TabIndex = 34;
            this.label19.Text = "Total Commission:";
            // 
            // lblNetSales
            // 
            this.lblNetSales.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblNetSales.Location = new System.Drawing.Point(623, 298);
            this.lblNetSales.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblNetSales.Name = "lblNetSales";
            this.lblNetSales.Size = new System.Drawing.Size(112, 17);
            this.lblNetSales.TabIndex = 33;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(514, 298);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 13);
            this.label21.TabIndex = 32;
            this.label21.Text = "Total Net Sales:";
            // 
            // lblTotalRemAmt
            // 
            this.lblTotalRemAmt.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblTotalRemAmt.Location = new System.Drawing.Point(204, 328);
            this.lblTotalRemAmt.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblTotalRemAmt.Name = "lblTotalRemAmt";
            this.lblTotalRemAmt.Size = new System.Drawing.Size(112, 17);
            this.lblTotalRemAmt.TabIndex = 31;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(41, 328);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(118, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Total Amount Remitted:";
            // 
            // lblExpectedAmt
            // 
            this.lblExpectedAmt.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblExpectedAmt.Location = new System.Drawing.Point(204, 298);
            this.lblExpectedAmt.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblExpectedAmt.Name = "lblExpectedAmt";
            this.lblExpectedAmt.Size = new System.Drawing.Size(112, 17);
            this.lblExpectedAmt.TabIndex = 29;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(68, 298);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Expected Amount:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Branded Sales";
            // 
            // lblBrandedSales
            // 
            this.lblBrandedSales.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblBrandedSales.Location = new System.Drawing.Point(122, 265);
            this.lblBrandedSales.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblBrandedSales.Name = "lblBrandedSales";
            this.lblBrandedSales.Size = new System.Drawing.Size(112, 17);
            this.lblBrandedSales.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 235);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Generic Sales";
            // 
            // lblGenSales
            // 
            this.lblGenSales.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblGenSales.Location = new System.Drawing.Point(122, 235);
            this.lblGenSales.MinimumSize = new System.Drawing.Size(60, 0);
            this.lblGenSales.Name = "lblGenSales";
            this.lblGenSales.Size = new System.Drawing.Size(112, 17);
            this.lblGenSales.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdDrugCode,
            this.Column2,
            this.Type,
            this.Sold,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(18, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(744, 150);
            this.dataGridView1.TabIndex = 14;
            // 
            // grdDrugCode
            // 
            this.grdDrugCode.HeaderText = "DrugCode";
            this.grdDrugCode.Name = "grdDrugCode";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Drug Name";
            this.Column2.Name = "Column2";
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Sold
            // 
            this.Sold.HeaderText = "Sold";
            this.Sold.Name = "Sold";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Price";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Sales";
            this.Column4.Name = "Column4";
            this.Column4.ToolTipText = "Expected Sales";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Amount Remitted";
            this.Column5.Name = "Column5";
            // 
            // txtRemAmount
            // 
            this.txtRemAmount.Location = new System.Drawing.Point(588, 32);
            this.txtRemAmount.Name = "txtRemAmount";
            this.txtRemAmount.Size = new System.Drawing.Size(100, 20);
            this.txtRemAmount.TabIndex = 13;
            this.txtRemAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemAmount_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Drug Code:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(446, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Remittance Amount:";
            // 
            // txtDrugCode
            // 
            this.txtDrugCode.Location = new System.Drawing.Point(113, 32);
            this.txtDrugCode.Name = "txtDrugCode";
            this.txtDrugCode.Size = new System.Drawing.Size(100, 20);
            this.txtDrugCode.TabIndex = 9;
            this.txtDrugCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDrugCode_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(43, 616);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(71, 13);
            this.label28.TabIndex = 8;
            this.label28.Text = "Approved By:";
            // 
            // txtApprEmpCode
            // 
            this.txtApprEmpCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApprEmpCode.Location = new System.Drawing.Point(137, 616);
            this.txtApprEmpCode.Name = "txtApprEmpCode";
            this.txtApprEmpCode.Size = new System.Drawing.Size(100, 20);
            this.txtApprEmpCode.TabIndex = 42;
            this.txtApprEmpCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApprEmpCode_KeyPress);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(261, 642);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(84, 13);
            this.label29.TabIndex = 44;
            this.label29.Text = "Employee Name";
            // 
            // lblApprvByName
            // 
            this.lblApprvByName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblApprvByName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApprvByName.Location = new System.Drawing.Point(261, 619);
            this.lblApprvByName.MinimumSize = new System.Drawing.Size(150, 0);
            this.lblApprvByName.Name = "lblApprvByName";
            this.lblApprvByName.Size = new System.Drawing.Size(419, 16);
            this.lblApprvByName.TabIndex = 43;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(137, 668);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(543, 20);
            this.txtRemarks.TabIndex = 46;
            this.txtRemarks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemarks_KeyPress);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(43, 668);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(52, 13);
            this.label31.TabIndex = 45;
            this.label31.Text = "Remarks:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(681, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(681, 195);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 28);
            this.btnClear.TabIndex = 48;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(215, 24);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(413, 28);
            this.label32.TabIndex = 49;
            this.label32.Text = "WelJane Pharma and Gen. Merchandise";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(330, 52);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(158, 16);
            this.label33.TabIndex = 50;
            this.label33.Text = "10A Brgy. San Jose Cebu City";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(521, 84);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(235, 20);
            this.dateTimePicker1.TabIndex = 51;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // txtReturnNo
            // 
            this.txtReturnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnNo.Location = new System.Drawing.Point(185, 84);
            this.txtReturnNo.Name = "txtReturnNo";
            this.txtReturnNo.Size = new System.Drawing.Size(100, 20);
            this.txtReturnNo.TabIndex = 1;
            this.txtReturnNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnNo_KeyPress_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 52;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remittanceEntryToolStripMenuItem,
            this.remittanceUpdateToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // remittanceEntryToolStripMenuItem
            // 
            this.remittanceEntryToolStripMenuItem.Name = "remittanceEntryToolStripMenuItem";
            this.remittanceEntryToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.remittanceEntryToolStripMenuItem.Text = "Remittance Entry";
            this.remittanceEntryToolStripMenuItem.Click += new System.EventHandler(this.remittanceEntryToolStripMenuItem_Click);
            // 
            // remittanceUpdateToolStripMenuItem
            // 
            this.remittanceUpdateToolStripMenuItem.Name = "remittanceUpdateToolStripMenuItem";
            this.remittanceUpdateToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.remittanceUpdateToolStripMenuItem.Text = "Remittance Update";
            this.remittanceUpdateToolStripMenuItem.Click += new System.EventHandler(this.remittanceUpdateToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // RemittanceEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(852, 702);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.lblApprvByName);
            this.Controls.Add(this.txtApprEmpCode);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRemittanceNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReturnNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RemittanceEntry";
            this.Text = "Remittance Entry";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemittanceNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEmpName;
        private System.Windows.Forms.Label lblEmpNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtRemAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDrugCode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBrandedSales;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblGenSales;
        private System.Windows.Forms.Label lblTotalRemAmt;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblExpectedAmt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotalCom;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblNetSales;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblBonus;
        private System.Windows.Forms.Label lblGenCom;
        private System.Windows.Forms.Label lblBrandedCom;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtApprEmpCode;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblApprvByName;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdDrugCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sold;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtReturnNo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remittanceEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remittanceUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

