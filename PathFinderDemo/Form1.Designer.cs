namespace PathFinderDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            top_Panel = new Panel();
            start_TB = new TextBox();
            label2 = new Label();
            beam_CB = new CheckBox();
            hillClimb_CB = new CheckBox();
            pause_bttn = new Button();
            BFS_CB = new CheckBox();
            weight_TB = new TextBox();
            button_Clear = new Button();
            label1 = new Label();
            DFS_CB = new CheckBox();
            search_CB = new CheckBox();
            searchNode_bttn = new Button();
            connectBx = new CheckBox();
            addNode_Bttn = new Button();
            search_TB = new TextBox();
            fromLabel = new Label();
            weitght_tb = new Panel();
            sss = new Label();
            label3 = new Label();
            heuristics_lbl = new Label();
            stackLBL = new Label();
            panel1 = new Panel();
            Speed_Disp = new Label();
            speedUp_bttn = new Button();
            speedDown_bttn = new Button();
            w_TB = new TextBox();
            label4 = new Label();
            top_Panel.SuspendLayout();
            weitght_tb.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // top_Panel
            // 
            top_Panel.BackColor = SystemColors.ActiveBorder;
            top_Panel.Controls.Add(start_TB);
            top_Panel.Controls.Add(label2);
            top_Panel.Controls.Add(beam_CB);
            top_Panel.Controls.Add(hillClimb_CB);
            top_Panel.Controls.Add(pause_bttn);
            top_Panel.Controls.Add(BFS_CB);
            top_Panel.Controls.Add(weight_TB);
            top_Panel.Controls.Add(button_Clear);
            top_Panel.Controls.Add(label1);
            top_Panel.Controls.Add(DFS_CB);
            top_Panel.Controls.Add(search_CB);
            top_Panel.Controls.Add(searchNode_bttn);
            top_Panel.Controls.Add(connectBx);
            top_Panel.Controls.Add(addNode_Bttn);
            top_Panel.Controls.Add(search_TB);
            top_Panel.Controls.Add(fromLabel);
            top_Panel.Dock = DockStyle.Top;
            top_Panel.Location = new Point(0, 0);
            top_Panel.Name = "top_Panel";
            top_Panel.Size = new Size(1481, 78);
            top_Panel.TabIndex = 1;
            // 
            // start_TB
            // 
            start_TB.BorderStyle = BorderStyle.FixedSingle;
            start_TB.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            start_TB.Location = new Point(505, 19);
            start_TB.Name = "start_TB";
            start_TB.Size = new Size(100, 39);
            start_TB.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(442, 24);
            label2.Name = "label2";
            label2.Size = new Size(73, 32);
            label2.TabIndex = 15;
            label2.Text = "Start:";
            // 
            // beam_CB
            // 
            beam_CB.AutoSize = true;
            beam_CB.Location = new Point(1092, 49);
            beam_CB.Name = "beam_CB";
            beam_CB.Size = new Size(56, 19);
            beam_CB.TabIndex = 14;
            beam_CB.Text = "Beam";
            beam_CB.UseVisualStyleBackColor = true;
            beam_CB.CheckedChanged += beam_CB_CheckedChanged;
            // 
            // hillClimb_CB
            // 
            hillClimb_CB.AutoSize = true;
            hillClimb_CB.Location = new Point(1195, 15);
            hillClimb_CB.Name = "hillClimb_CB";
            hillClimb_CB.Size = new Size(96, 19);
            hillClimb_CB.TabIndex = 13;
            hillClimb_CB.Text = "Hill Climbing";
            hillClimb_CB.UseVisualStyleBackColor = true;
            hillClimb_CB.CheckedChanged += hillClimb_CB_CheckedChanged;
            // 
            // pause_bttn
            // 
            pause_bttn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            pause_bttn.Location = new Point(1297, 9);
            pause_bttn.Name = "pause_bttn";
            pause_bttn.Size = new Size(75, 37);
            pause_bttn.TabIndex = 6;
            pause_bttn.Text = "Pause";
            pause_bttn.UseVisualStyleBackColor = true;
            pause_bttn.Click += pause_bttn_Click;
            // 
            // BFS_CB
            // 
            BFS_CB.AutoSize = true;
            BFS_CB.Location = new Point(1144, 15);
            BFS_CB.Name = "BFS_CB";
            BFS_CB.Size = new Size(45, 19);
            BFS_CB.TabIndex = 12;
            BFS_CB.Text = "BFS";
            BFS_CB.UseVisualStyleBackColor = true;
            BFS_CB.CheckedChanged += BFS_CB_CheckedChanged;
            // 
            // weight_TB
            // 
            weight_TB.BorderStyle = BorderStyle.FixedSingle;
            weight_TB.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            weight_TB.Location = new Point(772, 17);
            weight_TB.Name = "weight_TB";
            weight_TB.Size = new Size(100, 39);
            weight_TB.TabIndex = 4;
            // 
            // button_Clear
            // 
            button_Clear.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button_Clear.Location = new Point(1394, 19);
            button_Clear.Name = "button_Clear";
            button_Clear.Size = new Size(75, 37);
            button_Clear.TabIndex = 11;
            button_Clear.Text = "Clear Table";
            button_Clear.UseVisualStyleBackColor = true;
            button_Clear.Click += button_Clear_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(611, 19);
            label1.Name = "label1";
            label1.Size = new Size(157, 32);
            label1.TabIndex = 3;
            label1.Text = "Input Weight";
            // 
            // DFS_CB
            // 
            DFS_CB.AutoSize = true;
            DFS_CB.Location = new Point(1092, 15);
            DFS_CB.Name = "DFS_CB";
            DFS_CB.Size = new Size(46, 19);
            DFS_CB.TabIndex = 10;
            DFS_CB.Text = "DFS";
            DFS_CB.UseVisualStyleBackColor = true;
            DFS_CB.CheckedChanged += DFS_CB_CheckedChanged;
            // 
            // search_CB
            // 
            search_CB.AutoSize = true;
            search_CB.Location = new Point(969, 29);
            search_CB.Name = "search_CB";
            search_CB.Size = new Size(93, 19);
            search_CB.TabIndex = 9;
            search_CB.Text = "Search Node";
            search_CB.UseVisualStyleBackColor = true;
            search_CB.CheckedChanged += search_CB_CheckedChanged;
            // 
            // searchNode_bttn
            // 
            searchNode_bttn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            searchNode_bttn.Location = new Point(266, 16);
            searchNode_bttn.Name = "searchNode_bttn";
            searchNode_bttn.Size = new Size(75, 37);
            searchNode_bttn.TabIndex = 8;
            searchNode_bttn.Text = "Search";
            searchNode_bttn.UseVisualStyleBackColor = true;
            searchNode_bttn.Click += searchNode_bttn_Click_1;
            // 
            // connectBx
            // 
            connectBx.AutoSize = true;
            connectBx.Location = new Point(879, 29);
            connectBx.Name = "connectBx";
            connectBx.Size = new Size(71, 19);
            connectBx.TabIndex = 7;
            connectBx.Text = "Connect";
            connectBx.UseVisualStyleBackColor = true;
            connectBx.CheckedChanged += connectBx_CheckedChanged;
            // 
            // addNode_Bttn
            // 
            addNode_Bttn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            addNode_Bttn.Location = new Point(361, 17);
            addNode_Bttn.Name = "addNode_Bttn";
            addNode_Bttn.Size = new Size(75, 37);
            addNode_Bttn.TabIndex = 4;
            addNode_Bttn.Text = "Add Node";
            addNode_Bttn.UseVisualStyleBackColor = true;
            addNode_Bttn.Click += addNode_Bttn_Click;
            // 
            // search_TB
            // 
            search_TB.BorderStyle = BorderStyle.FixedSingle;
            search_TB.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            search_TB.Location = new Point(160, 14);
            search_TB.Name = "search_TB";
            search_TB.Size = new Size(100, 39);
            search_TB.TabIndex = 2;
            // 
            // fromLabel
            // 
            fromLabel.AutoSize = true;
            fromLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            fromLabel.Location = new Point(12, 17);
            fromLabel.Name = "fromLabel";
            fromLabel.Size = new Size(153, 32);
            fromLabel.TabIndex = 0;
            fromLabel.Text = "Search Node";
            // 
            // weitght_tb
            // 
            weitght_tb.BackColor = SystemColors.ActiveBorder;
            weitght_tb.Controls.Add(sss);
            weitght_tb.Controls.Add(label3);
            weitght_tb.Controls.Add(heuristics_lbl);
            weitght_tb.Controls.Add(stackLBL);
            weitght_tb.Location = new Point(1192, 84);
            weitght_tb.Name = "weitght_tb";
            weitght_tb.Size = new Size(289, 635);
            weitght_tb.TabIndex = 2;
            // 
            // sss
            // 
            sss.AutoSize = true;
            sss.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            sss.Location = new Point(3, 423);
            sss.Name = "sss";
            sss.Size = new Size(92, 32);
            sss.TabIndex = 5;
            sss.Text = "Weight";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(3, 45);
            label3.Name = "label3";
            label3.Size = new Size(98, 32);
            label3.TabIndex = 4;
            label3.Text = "Display:";
            // 
            // heuristics_lbl
            // 
            heuristics_lbl.BackColor = SystemColors.ButtonHighlight;
            heuristics_lbl.Location = new Point(3, 455);
            heuristics_lbl.Name = "heuristics_lbl";
            heuristics_lbl.Size = new Size(283, 180);
            heuristics_lbl.TabIndex = 1;
            // 
            // stackLBL
            // 
            stackLBL.BackColor = SystemColors.ButtonHighlight;
            stackLBL.FlatStyle = FlatStyle.Flat;
            stackLBL.Location = new Point(3, 77);
            stackLBL.Name = "stackLBL";
            stackLBL.Size = new Size(283, 326);
            stackLBL.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Controls.Add(w_TB);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Speed_Disp);
            panel1.Controls.Add(speedUp_bttn);
            panel1.Controls.Add(speedDown_bttn);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 725);
            panel1.Name = "panel1";
            panel1.Size = new Size(1481, 95);
            panel1.TabIndex = 3;
            // 
            // Speed_Disp
            // 
            Speed_Disp.BackColor = SystemColors.ButtonHighlight;
            Speed_Disp.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            Speed_Disp.Location = new Point(266, 26);
            Speed_Disp.Name = "Speed_Disp";
            Speed_Disp.Size = new Size(75, 54);
            Speed_Disp.TabIndex = 6;
            Speed_Disp.Text = "10";
            Speed_Disp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // speedUp_bttn
            // 
            speedUp_bttn.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            speedUp_bttn.Location = new Point(347, 25);
            speedUp_bttn.Name = "speedUp_bttn";
            speedUp_bttn.Size = new Size(75, 58);
            speedUp_bttn.TabIndex = 5;
            speedUp_bttn.Text = "+";
            speedUp_bttn.UseVisualStyleBackColor = true;
            // 
            // speedDown_bttn
            // 
            speedDown_bttn.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            speedDown_bttn.Location = new Point(185, 25);
            speedDown_bttn.Name = "speedDown_bttn";
            speedDown_bttn.Size = new Size(75, 58);
            speedDown_bttn.TabIndex = 4;
            speedDown_bttn.Text = "-";
            speedDown_bttn.UseVisualStyleBackColor = true;
            // 
            // w_TB
            // 
            w_TB.BorderStyle = BorderStyle.FixedSingle;
            w_TB.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            w_TB.Location = new Point(724, 30);
            w_TB.Name = "w_TB";
            w_TB.Size = new Size(100, 39);
            w_TB.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(610, 30);
            label4.Name = "label4";
            label4.Size = new Size(108, 32);
            label4.TabIndex = 7;
            label4.Text = "Input W:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1481, 820);
            Controls.Add(panel1);
            Controls.Add(weitght_tb);
            Controls.Add(top_Panel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            top_Panel.ResumeLayout(false);
            top_Panel.PerformLayout();
            weitght_tb.ResumeLayout(false);
            weitght_tb.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel top_Panel;
        private Button addNode_Bttn;
        private TextBox search_TB;
        private Label fromLabel;
        private CheckBox connectBx;
        private Button searchNode_bttn;
        private CheckBox search_CB;
        private Panel weitght_tb;
        private Label stackLBL;
        private TextBox weight_TB;
        private Label label1;
        private CheckBox DFS_CB;
        private Button button_Clear;
        private Label sss;
        private Label label3;
        private Label heuristics_lbl;
        private CheckBox BFS_CB;
        private Button pause_bttn;
        private Panel panel1;
        private Button speedUp_bttn;
        private Button speedDown_bttn;
        private Label Speed_Disp;
        private CheckBox hillClimb_CB;
        private CheckBox beam_CB;
        private TextBox start_TB;
        private Label label2;
        private TextBox w_TB;
        private Label label4;
    }
}