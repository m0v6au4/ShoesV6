namespace ShoesV6
{
    partial class Formpathadj
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.textBox_Z = new System.Windows.Forms.TextBox();
            this.textBox_A = new System.Windows.Forms.TextBox();
            this.textBox_B = new System.Windows.Forms.TextBox();
            this.textBox_C = new System.Windows.Forms.TextBox();
            this.label1_X = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.label_Z = new System.Windows.Forms.Label();
            this.label_A = new System.Windows.Forms.Label();
            this.label_B = new System.Windows.Forms.Label();
            this.label_C = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Z = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_A = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_B = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_C = new System.Windows.Forms.NumericUpDown();
            this.btn_save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_index = new System.Windows.Forms.Label();
            this.numericUpDown_index = new System.Windows.Forms.NumericUpDown();
            this.button_RobotMove = new System.Windows.Forms.Button();
            this.checkBox_Auto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_index)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(12, 11);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series6.Legend = "Legend1";
            series6.MarkerColor = System.Drawing.Color.Gray;
            series6.MarkerSize = 7;
            series6.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series6.Name = "Series1";
            series6.YValuesPerPoint = 2;
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(581, 458);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            // 
            // textBox_X
            // 
            this.textBox_X.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_X.Location = new System.Drawing.Point(730, 62);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(122, 29);
            this.textBox_X.TabIndex = 1;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Y.Location = new System.Drawing.Point(730, 102);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(122, 29);
            this.textBox_Y.TabIndex = 2;
            // 
            // textBox_Z
            // 
            this.textBox_Z.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Z.Location = new System.Drawing.Point(730, 143);
            this.textBox_Z.Name = "textBox_Z";
            this.textBox_Z.Size = new System.Drawing.Size(122, 29);
            this.textBox_Z.TabIndex = 3;
            // 
            // textBox_A
            // 
            this.textBox_A.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_A.Location = new System.Drawing.Point(730, 183);
            this.textBox_A.Name = "textBox_A";
            this.textBox_A.Size = new System.Drawing.Size(122, 29);
            this.textBox_A.TabIndex = 4;
            // 
            // textBox_B
            // 
            this.textBox_B.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_B.Location = new System.Drawing.Point(730, 223);
            this.textBox_B.Name = "textBox_B";
            this.textBox_B.Size = new System.Drawing.Size(122, 29);
            this.textBox_B.TabIndex = 5;
            // 
            // textBox_C
            // 
            this.textBox_C.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_C.Location = new System.Drawing.Point(730, 264);
            this.textBox_C.Name = "textBox_C";
            this.textBox_C.Size = new System.Drawing.Size(122, 29);
            this.textBox_C.TabIndex = 6;
            // 
            // label1_X
            // 
            this.label1_X.AutoSize = true;
            this.label1_X.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1_X.Location = new System.Drawing.Point(694, 66);
            this.label1_X.Name = "label1_X";
            this.label1_X.Size = new System.Drawing.Size(19, 20);
            this.label1_X.TabIndex = 7;
            this.label1_X.Text = "X";
            // 
            // label_Y
            // 
            this.label_Y.AutoSize = true;
            this.label_Y.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Y.Location = new System.Drawing.Point(694, 106);
            this.label_Y.Name = "label_Y";
            this.label_Y.Size = new System.Drawing.Size(19, 20);
            this.label_Y.TabIndex = 8;
            this.label_Y.Text = "Y";
            // 
            // label_Z
            // 
            this.label_Z.AutoSize = true;
            this.label_Z.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Z.Location = new System.Drawing.Point(694, 147);
            this.label_Z.Name = "label_Z";
            this.label_Z.Size = new System.Drawing.Size(19, 20);
            this.label_Z.TabIndex = 9;
            this.label_Z.Text = "Z";
            // 
            // label_A
            // 
            this.label_A.AutoSize = true;
            this.label_A.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_A.Location = new System.Drawing.Point(694, 187);
            this.label_A.Name = "label_A";
            this.label_A.Size = new System.Drawing.Size(20, 20);
            this.label_A.TabIndex = 10;
            this.label_A.Text = "A";
            // 
            // label_B
            // 
            this.label_B.AutoSize = true;
            this.label_B.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_B.Location = new System.Drawing.Point(694, 227);
            this.label_B.Name = "label_B";
            this.label_B.Size = new System.Drawing.Size(19, 20);
            this.label_B.TabIndex = 11;
            this.label_B.Text = "B";
            // 
            // label_C
            // 
            this.label_C.AutoSize = true;
            this.label_C.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_C.Location = new System.Drawing.Point(694, 268);
            this.label_C.Name = "label_C";
            this.label_C.Size = new System.Drawing.Size(20, 20);
            this.label_C.TabIndex = 12;
            this.label_C.Text = "C";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(756, 380);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // numericUpDown_X
            // 
            this.numericUpDown_X.DecimalPlaces = 2;
            this.numericUpDown_X.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_X.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown_X.Location = new System.Drawing.Point(858, 62);
            this.numericUpDown_X.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_X.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_X.TabIndex = 14;
            this.numericUpDown_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.DecimalPlaces = 2;
            this.numericUpDown_Y.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_Y.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown_Y.Location = new System.Drawing.Point(858, 102);
            this.numericUpDown_Y.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_Y.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown_Y.Name = "numericUpDown_Y";
            this.numericUpDown_Y.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_Y.TabIndex = 15;
            this.numericUpDown_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_Z
            // 
            this.numericUpDown_Z.DecimalPlaces = 2;
            this.numericUpDown_Z.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_Z.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown_Z.Location = new System.Drawing.Point(858, 143);
            this.numericUpDown_Z.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_Z.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown_Z.Name = "numericUpDown_Z";
            this.numericUpDown_Z.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_Z.TabIndex = 16;
            this.numericUpDown_Z.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_A
            // 
            this.numericUpDown_A.DecimalPlaces = 2;
            this.numericUpDown_A.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_A.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown_A.Location = new System.Drawing.Point(858, 183);
            this.numericUpDown_A.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_A.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown_A.Name = "numericUpDown_A";
            this.numericUpDown_A.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_A.TabIndex = 17;
            this.numericUpDown_A.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_B
            // 
            this.numericUpDown_B.DecimalPlaces = 2;
            this.numericUpDown_B.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_B.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown_B.Location = new System.Drawing.Point(858, 223);
            this.numericUpDown_B.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_B.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown_B.Name = "numericUpDown_B";
            this.numericUpDown_B.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_B.TabIndex = 18;
            this.numericUpDown_B.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_C
            // 
            this.numericUpDown_C.DecimalPlaces = 2;
            this.numericUpDown_C.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_C.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown_C.Location = new System.Drawing.Point(858, 264);
            this.numericUpDown_C.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_C.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown_C.Name = "numericUpDown_C";
            this.numericUpDown_C.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_C.TabIndex = 19;
            this.numericUpDown_C.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_save.Location = new System.Drawing.Point(840, 320);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(93, 36);
            this.btn_save.TabIndex = 32;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(738, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Index :";
            // 
            // label_index
            // 
            this.label_index.AutoSize = true;
            this.label_index.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_index.Location = new System.Drawing.Point(793, 24);
            this.label_index.Name = "label_index";
            this.label_index.Size = new System.Drawing.Size(37, 20);
            this.label_index.TabIndex = 34;
            this.label_index.Text = "null";
            // 
            // numericUpDown_index
            // 
            this.numericUpDown_index.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_index.Location = new System.Drawing.Point(858, 22);
            this.numericUpDown_index.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_index.Name = "numericUpDown_index";
            this.numericUpDown_index.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_index.TabIndex = 35;
            this.numericUpDown_index.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_index.ValueChanged += new System.EventHandler(this.numericUpDown_index_ValueChanged);
            // 
            // button_RobotMove
            // 
            this.button_RobotMove.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_RobotMove.Location = new System.Drawing.Point(730, 320);
            this.button_RobotMove.Name = "button_RobotMove";
            this.button_RobotMove.Size = new System.Drawing.Size(93, 36);
            this.button_RobotMove.TabIndex = 36;
            this.button_RobotMove.Text = "RobotMove";
            this.button_RobotMove.UseVisualStyleBackColor = true;
            this.button_RobotMove.Click += new System.EventHandler(this.button_RobotMove_Click);
            // 
            // checkBox_Auto
            // 
            this.checkBox_Auto.AutoSize = true;
            this.checkBox_Auto.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_Auto.Location = new System.Drawing.Point(699, 445);
            this.checkBox_Auto.Name = "checkBox_Auto";
            this.checkBox_Auto.Size = new System.Drawing.Size(124, 24);
            this.checkBox_Auto.TabIndex = 37;
            this.checkBox_Auto.Text = "自動微調模式";
            this.checkBox_Auto.UseVisualStyleBackColor = true;
            this.checkBox_Auto.CheckedChanged += new System.EventHandler(this.checkBox_Auto_CheckedChanged);
            // 
            // Formpathadj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 504);
            this.Controls.Add(this.checkBox_Auto);
            this.Controls.Add(this.button_RobotMove);
            this.Controls.Add(this.numericUpDown_index);
            this.Controls.Add(this.label_index);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.numericUpDown_C);
            this.Controls.Add(this.numericUpDown_B);
            this.Controls.Add(this.numericUpDown_A);
            this.Controls.Add(this.numericUpDown_Z);
            this.Controls.Add(this.numericUpDown_Y);
            this.Controls.Add(this.numericUpDown_X);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_C);
            this.Controls.Add(this.label_B);
            this.Controls.Add(this.label_A);
            this.Controls.Add(this.label_Z);
            this.Controls.Add(this.label_Y);
            this.Controls.Add(this.label1_X);
            this.Controls.Add(this.textBox_C);
            this.Controls.Add(this.textBox_B);
            this.Controls.Add(this.textBox_A);
            this.Controls.Add(this.textBox_Z);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.chart1);
            this.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "Formpathadj";
            this.Text = "Formpathadj";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formpathadj_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_index)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.TextBox textBox_Z;
        private System.Windows.Forms.TextBox textBox_A;
        private System.Windows.Forms.TextBox textBox_B;
        private System.Windows.Forms.TextBox textBox_C;
        private System.Windows.Forms.Label label1_X;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label label_Z;
        private System.Windows.Forms.Label label_A;
        private System.Windows.Forms.Label label_B;
        private System.Windows.Forms.Label label_C;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_X;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z;
        private System.Windows.Forms.NumericUpDown numericUpDown_A;
        private System.Windows.Forms.NumericUpDown numericUpDown_B;
        private System.Windows.Forms.NumericUpDown numericUpDown_C;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_index;
        private System.Windows.Forms.NumericUpDown numericUpDown_index;
        private System.Windows.Forms.Button button_RobotMove;
        private System.Windows.Forms.CheckBox checkBox_Auto;
    }
}