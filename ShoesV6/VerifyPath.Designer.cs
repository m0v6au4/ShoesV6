namespace ShoesV6
{
    partial class VerifyPath
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.checkBox_Auto = new System.Windows.Forms.CheckBox();
            this.button_RobotMove = new System.Windows.Forms.Button();
            this.numericUpDown_index = new System.Windows.Forms.NumericUpDown();
            this.label_index = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.numericUpDown_C = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_B = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_A = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Z = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label_C = new System.Windows.Forms.Label();
            this.label_B = new System.Windows.Forms.Label();
            this.label_A = new System.Windows.Forms.Label();
            this.label_Z = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.label1_X = new System.Windows.Forms.Label();
            this.textBox_C = new System.Windows.Forms.TextBox();
            this.textBox_B = new System.Windows.Forms.TextBox();
            this.textBox_A = new System.Windows.Forms.TextBox();
            this.textBox_Z = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_C)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_Auto
            // 
            this.checkBox_Auto.AutoSize = true;
            this.checkBox_Auto.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_Auto.Location = new System.Drawing.Point(699, 457);
            this.checkBox_Auto.Name = "checkBox_Auto";
            this.checkBox_Auto.Size = new System.Drawing.Size(124, 24);
            this.checkBox_Auto.TabIndex = 63;
            this.checkBox_Auto.Text = "自動微調模式";
            this.checkBox_Auto.UseVisualStyleBackColor = true;
            // 
            // button_RobotMove
            // 
            this.button_RobotMove.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_RobotMove.Location = new System.Drawing.Point(730, 332);
            this.button_RobotMove.Name = "button_RobotMove";
            this.button_RobotMove.Size = new System.Drawing.Size(93, 36);
            this.button_RobotMove.TabIndex = 62;
            this.button_RobotMove.Text = "RobotMove";
            this.button_RobotMove.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_index
            // 
            this.numericUpDown_index.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDown_index.Location = new System.Drawing.Point(858, 34);
            this.numericUpDown_index.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_index.Name = "numericUpDown_index";
            this.numericUpDown_index.Size = new System.Drawing.Size(75, 29);
            this.numericUpDown_index.TabIndex = 61;
            this.numericUpDown_index.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_index.ValueChanged += new System.EventHandler(this.numericUpDown_index_ValueChanged);
            // 
            // label_index
            // 
            this.label_index.AutoSize = true;
            this.label_index.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_index.Location = new System.Drawing.Point(793, 36);
            this.label_index.Name = "label_index";
            this.label_index.Size = new System.Drawing.Size(37, 20);
            this.label_index.TabIndex = 60;
            this.label_index.Text = "null";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(738, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 59;
            this.label2.Text = "Index :";
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_save.Location = new System.Drawing.Point(840, 332);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(93, 36);
            this.btn_save.TabIndex = 58;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
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
            this.numericUpDown_C.Location = new System.Drawing.Point(858, 276);
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
            this.numericUpDown_C.TabIndex = 57;
            this.numericUpDown_C.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            this.numericUpDown_B.Location = new System.Drawing.Point(858, 235);
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
            this.numericUpDown_B.TabIndex = 56;
            this.numericUpDown_B.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            this.numericUpDown_A.Location = new System.Drawing.Point(858, 195);
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
            this.numericUpDown_A.TabIndex = 55;
            this.numericUpDown_A.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            this.numericUpDown_Z.Location = new System.Drawing.Point(858, 155);
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
            this.numericUpDown_Z.TabIndex = 54;
            this.numericUpDown_Z.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            this.numericUpDown_Y.Location = new System.Drawing.Point(858, 114);
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
            this.numericUpDown_Y.TabIndex = 53;
            this.numericUpDown_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            this.numericUpDown_X.Location = new System.Drawing.Point(858, 74);
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
            this.numericUpDown_X.TabIndex = 52;
            this.numericUpDown_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(756, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 51;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // label_C
            // 
            this.label_C.AutoSize = true;
            this.label_C.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_C.Location = new System.Drawing.Point(694, 280);
            this.label_C.Name = "label_C";
            this.label_C.Size = new System.Drawing.Size(20, 20);
            this.label_C.TabIndex = 50;
            this.label_C.Text = "C";
            // 
            // label_B
            // 
            this.label_B.AutoSize = true;
            this.label_B.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_B.Location = new System.Drawing.Point(694, 239);
            this.label_B.Name = "label_B";
            this.label_B.Size = new System.Drawing.Size(19, 20);
            this.label_B.TabIndex = 49;
            this.label_B.Text = "B";
            // 
            // label_A
            // 
            this.label_A.AutoSize = true;
            this.label_A.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_A.Location = new System.Drawing.Point(694, 199);
            this.label_A.Name = "label_A";
            this.label_A.Size = new System.Drawing.Size(20, 20);
            this.label_A.TabIndex = 48;
            this.label_A.Text = "A";
            // 
            // label_Z
            // 
            this.label_Z.AutoSize = true;
            this.label_Z.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Z.Location = new System.Drawing.Point(694, 159);
            this.label_Z.Name = "label_Z";
            this.label_Z.Size = new System.Drawing.Size(19, 20);
            this.label_Z.TabIndex = 47;
            this.label_Z.Text = "Z";
            // 
            // label_Y
            // 
            this.label_Y.AutoSize = true;
            this.label_Y.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Y.Location = new System.Drawing.Point(694, 118);
            this.label_Y.Name = "label_Y";
            this.label_Y.Size = new System.Drawing.Size(19, 20);
            this.label_Y.TabIndex = 46;
            this.label_Y.Text = "Y";
            // 
            // label1_X
            // 
            this.label1_X.AutoSize = true;
            this.label1_X.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1_X.Location = new System.Drawing.Point(694, 78);
            this.label1_X.Name = "label1_X";
            this.label1_X.Size = new System.Drawing.Size(19, 20);
            this.label1_X.TabIndex = 45;
            this.label1_X.Text = "X";
            // 
            // textBox_C
            // 
            this.textBox_C.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_C.Location = new System.Drawing.Point(730, 276);
            this.textBox_C.Name = "textBox_C";
            this.textBox_C.Size = new System.Drawing.Size(122, 29);
            this.textBox_C.TabIndex = 44;
            // 
            // textBox_B
            // 
            this.textBox_B.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_B.Location = new System.Drawing.Point(730, 235);
            this.textBox_B.Name = "textBox_B";
            this.textBox_B.Size = new System.Drawing.Size(122, 29);
            this.textBox_B.TabIndex = 43;
            // 
            // textBox_A
            // 
            this.textBox_A.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_A.Location = new System.Drawing.Point(730, 195);
            this.textBox_A.Name = "textBox_A";
            this.textBox_A.Size = new System.Drawing.Size(122, 29);
            this.textBox_A.TabIndex = 42;
            // 
            // textBox_Z
            // 
            this.textBox_Z.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Z.Location = new System.Drawing.Point(730, 155);
            this.textBox_Z.Name = "textBox_Z";
            this.textBox_Z.Size = new System.Drawing.Size(122, 29);
            this.textBox_Z.TabIndex = 41;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Y.Location = new System.Drawing.Point(730, 114);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(122, 29);
            this.textBox_Y.TabIndex = 40;
            // 
            // textBox_X
            // 
            this.textBox_X.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_X.Location = new System.Drawing.Point(730, 74);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(122, 29);
            this.textBox_X.TabIndex = 39;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 23);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.Gray;
            series1.MarkerSize = 7;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(581, 458);
            this.chart1.TabIndex = 38;
            this.chart1.Text = "chart1";
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            // 
            // VerifyPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
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
            this.Name = "VerifyPath";
            this.Text = "VerifyPath";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_C)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_Auto;
        private System.Windows.Forms.Button button_RobotMove;
        private System.Windows.Forms.NumericUpDown numericUpDown_index;
        private System.Windows.Forms.Label label_index;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.NumericUpDown numericUpDown_C;
        private System.Windows.Forms.NumericUpDown numericUpDown_B;
        private System.Windows.Forms.NumericUpDown numericUpDown_A;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_X;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_C;
        private System.Windows.Forms.Label label_B;
        private System.Windows.Forms.Label label_A;
        private System.Windows.Forms.Label label_Z;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label label1_X;
        private System.Windows.Forms.TextBox textBox_C;
        private System.Windows.Forms.TextBox textBox_B;
        private System.Windows.Forms.TextBox textBox_A;
        private System.Windows.Forms.TextBox textBox_Z;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}