namespace ShoesV6
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.Scan = new System.Windows.Forms.TabPage();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_IOConnect = new System.Windows.Forms.Button();
            this.btn_TC = new System.Windows.Forms.Button();
            this.btn_VerifyPath = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox_saveimage = new System.Windows.Forms.CheckBox();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.btn_RobConnect = new System.Windows.Forms.Button();
            this.btn_CamConnect = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.TabPage();
            this.EM = new System.Windows.Forms.TabPage();
            this.btn_removecamerarecipe = new System.Windows.Forms.Button();
            this.btn_savecamrecipe = new System.Windows.Forms.Button();
            this.comboBox_Cameramodel = new System.Windows.Forms.ComboBox();
            this.label_camerarecipe = new System.Windows.Forms.Label();
            this.label_shiftdatarecipe = new System.Windows.Forms.Label();
            this.btn_resample = new System.Windows.Forms.Button();
            this.btn_startsample = new System.Windows.Forms.Button();
            this.btn_pathadj = new System.Windows.Forms.Button();
            this.comboBox_model = new System.Windows.Forms.ComboBox();
            this.comboBox_size = new System.Windows.Forms.ComboBox();
            this.comboBox_direction = new System.Windows.Forms.ComboBox();
            this.label_exposure = new System.Windows.Forms.Label();
            this.checkBox_TriggerMode = new System.Windows.Forms.CheckBox();
            this.textBox_Exposure = new System.Windows.Forms.TextBox();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_addSample = new System.Windows.Forms.Button();
            this.text_angle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.text_shiftY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.text_shiftHeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.text_shiftX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_shrink = new System.Windows.Forms.TextBox();
            this.btn_SavePara = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.text_Erode = new System.Windows.Forms.TextBox();
            this.text_Dilate = new System.Windows.Forms.TextBox();
            this.text_Threshold = new System.Windows.Forms.TextBox();
            this.label_KUKA = new System.Windows.Forms.Label();
            this.label_Cam = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox_IOConnect = new System.Windows.Forms.PictureBox();
            this.pictureBox_Cam = new System.Windows.Forms.PictureBox();
            this.pictureBox_KUKA = new System.Windows.Forms.PictureBox();
            this.comboBox_ModelSize = new System.Windows.Forms.ComboBox();
            this.checkBox_PathCorrection = new System.Windows.Forms.CheckBox();
            this.Heartbeat_Timer = new System.Windows.Forms.Timer(this.components);
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl2.SuspendLayout();
            this.Scan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.EM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IOConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_KUKA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.Scan);
            this.tabControl2.Controls.Add(this.Connect);
            this.tabControl2.Controls.Add(this.EM);
            this.tabControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl2.Location = new System.Drawing.Point(1, 1);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(730, 490);
            this.tabControl2.TabIndex = 3;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // Scan
            // 
            this.Scan.BackColor = System.Drawing.SystemColors.Control;
            this.Scan.Controls.Add(this.btn_upload);
            this.Scan.Controls.Add(this.btn_IOConnect);
            this.Scan.Controls.Add(this.btn_TC);
            this.Scan.Controls.Add(this.btn_VerifyPath);
            this.Scan.Controls.Add(this.pictureBox1);
            this.Scan.Controls.Add(this.checkBox_saveimage);
            this.Scan.Controls.Add(this.btn_Scan);
            this.Scan.Controls.Add(this.btn_RobConnect);
            this.Scan.Controls.Add(this.btn_CamConnect);
            this.Scan.Location = new System.Drawing.Point(4, 25);
            this.Scan.Name = "Scan";
            this.Scan.Padding = new System.Windows.Forms.Padding(3);
            this.Scan.Size = new System.Drawing.Size(722, 461);
            this.Scan.TabIndex = 0;
            this.Scan.Text = "Scan";
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(623, 346);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(93, 36);
            this.btn_upload.TabIndex = 36;
            this.btn_upload.Text = "Upload";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // btn_IOConnect
            // 
            this.btn_IOConnect.Location = new System.Drawing.Point(108, 388);
            this.btn_IOConnect.Name = "btn_IOConnect";
            this.btn_IOConnect.Size = new System.Drawing.Size(93, 36);
            this.btn_IOConnect.TabIndex = 35;
            this.btn_IOConnect.Text = "IO";
            this.btn_IOConnect.UseVisualStyleBackColor = true;
            this.btn_IOConnect.Click += new System.EventHandler(this.btn_IOConnect_Click);
            // 
            // btn_TC
            // 
            this.btn_TC.Location = new System.Drawing.Point(9, 388);
            this.btn_TC.Name = "btn_TC";
            this.btn_TC.Size = new System.Drawing.Size(93, 36);
            this.btn_TC.TabIndex = 34;
            this.btn_TC.Text = "TC connect";
            this.btn_TC.UseVisualStyleBackColor = true;
            this.btn_TC.Click += new System.EventHandler(this.btn_TC_Click);
            // 
            // btn_VerifyPath
            // 
            this.btn_VerifyPath.Location = new System.Drawing.Point(527, 419);
            this.btn_VerifyPath.Name = "btn_VerifyPath";
            this.btn_VerifyPath.Size = new System.Drawing.Size(93, 36);
            this.btn_VerifyPath.TabIndex = 32;
            this.btn_VerifyPath.Text = "VerifyPath";
            this.btn_VerifyPath.UseVisualStyleBackColor = true;
            this.btn_VerifyPath.Click += new System.EventHandler(this.btn_VerifyPath_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(9, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(656, 290);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox_saveimage
            // 
            this.checkBox_saveimage.AutoSize = true;
            this.checkBox_saveimage.Checked = true;
            this.checkBox_saveimage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveimage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_saveimage.Location = new System.Drawing.Point(9, 312);
            this.checkBox_saveimage.Name = "checkBox_saveimage";
            this.checkBox_saveimage.Size = new System.Drawing.Size(105, 28);
            this.checkBox_saveimage.TabIndex = 27;
            this.checkBox_saveimage.Text = "儲存圖片";
            this.checkBox_saveimage.UseVisualStyleBackColor = true;
            // 
            // btn_Scan
            // 
            this.btn_Scan.Enabled = false;
            this.btn_Scan.Location = new System.Drawing.Point(207, 346);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(93, 36);
            this.btn_Scan.TabIndex = 5;
            this.btn_Scan.Text = "Scan";
            this.btn_Scan.UseVisualStyleBackColor = true;
            this.btn_Scan.Click += new System.EventHandler(this.btn_Scan_Click);
            // 
            // btn_RobConnect
            // 
            this.btn_RobConnect.Location = new System.Drawing.Point(9, 346);
            this.btn_RobConnect.Name = "btn_RobConnect";
            this.btn_RobConnect.Size = new System.Drawing.Size(93, 36);
            this.btn_RobConnect.TabIndex = 4;
            this.btn_RobConnect.Text = "RobConnect";
            this.btn_RobConnect.UseVisualStyleBackColor = true;
            this.btn_RobConnect.Click += new System.EventHandler(this.btn_RobConnect_Click);
            // 
            // btn_CamConnect
            // 
            this.btn_CamConnect.Location = new System.Drawing.Point(108, 346);
            this.btn_CamConnect.Name = "btn_CamConnect";
            this.btn_CamConnect.Size = new System.Drawing.Size(93, 36);
            this.btn_CamConnect.TabIndex = 3;
            this.btn_CamConnect.Text = "CamConnect";
            this.btn_CamConnect.UseVisualStyleBackColor = true;
            this.btn_CamConnect.Click += new System.EventHandler(this.btn_CamConnect_Click);
            // 
            // Connect
            // 
            this.Connect.BackColor = System.Drawing.SystemColors.Control;
            this.Connect.BackgroundImage = global::ShoesV6.Properties.Resources._180809_ukiyo_0;
            this.Connect.Location = new System.Drawing.Point(4, 25);
            this.Connect.Name = "Connect";
            this.Connect.Padding = new System.Windows.Forms.Padding(3);
            this.Connect.Size = new System.Drawing.Size(722, 461);
            this.Connect.TabIndex = 1;
            this.Connect.Text = "Connect";
            // 
            // EM
            // 
            this.EM.BackColor = System.Drawing.SystemColors.Control;
            this.EM.Controls.Add(this.btn_removecamerarecipe);
            this.EM.Controls.Add(this.btn_savecamrecipe);
            this.EM.Controls.Add(this.comboBox_Cameramodel);
            this.EM.Controls.Add(this.label_camerarecipe);
            this.EM.Controls.Add(this.label_shiftdatarecipe);
            this.EM.Controls.Add(this.btn_resample);
            this.EM.Controls.Add(this.btn_startsample);
            this.EM.Controls.Add(this.btn_pathadj);
            this.EM.Controls.Add(this.comboBox_model);
            this.EM.Controls.Add(this.comboBox_size);
            this.EM.Controls.Add(this.comboBox_direction);
            this.EM.Controls.Add(this.label_exposure);
            this.EM.Controls.Add(this.checkBox_TriggerMode);
            this.EM.Controls.Add(this.textBox_Exposure);
            this.EM.Controls.Add(this.btn_Remove);
            this.EM.Controls.Add(this.btn_addSample);
            this.EM.Controls.Add(this.text_angle);
            this.EM.Controls.Add(this.label5);
            this.EM.Controls.Add(this.label8);
            this.EM.Controls.Add(this.text_shiftY);
            this.EM.Controls.Add(this.label7);
            this.EM.Controls.Add(this.text_shiftHeight);
            this.EM.Controls.Add(this.label6);
            this.EM.Controls.Add(this.text_shiftX);
            this.EM.Controls.Add(this.label4);
            this.EM.Controls.Add(this.text_shrink);
            this.EM.Controls.Add(this.btn_SavePara);
            this.EM.Controls.Add(this.label3);
            this.EM.Controls.Add(this.label2);
            this.EM.Controls.Add(this.label1);
            this.EM.Controls.Add(this.text_Erode);
            this.EM.Controls.Add(this.text_Dilate);
            this.EM.Controls.Add(this.text_Threshold);
            this.EM.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EM.Location = new System.Drawing.Point(4, 25);
            this.EM.Name = "EM";
            this.EM.Padding = new System.Windows.Forms.Padding(3);
            this.EM.Size = new System.Drawing.Size(722, 461);
            this.EM.TabIndex = 2;
            this.EM.Text = "EM";
            // 
            // btn_removecamerarecipe
            // 
            this.btn_removecamerarecipe.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_removecamerarecipe.Location = new System.Drawing.Point(545, 92);
            this.btn_removecamerarecipe.Name = "btn_removecamerarecipe";
            this.btn_removecamerarecipe.Size = new System.Drawing.Size(93, 36);
            this.btn_removecamerarecipe.TabIndex = 63;
            this.btn_removecamerarecipe.Text = "Remove";
            this.btn_removecamerarecipe.UseVisualStyleBackColor = true;
            this.btn_removecamerarecipe.Click += new System.EventHandler(this.btn_removecamerarecipe_Click);
            // 
            // btn_savecamrecipe
            // 
            this.btn_savecamrecipe.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_savecamrecipe.Location = new System.Drawing.Point(418, 92);
            this.btn_savecamrecipe.Name = "btn_savecamrecipe";
            this.btn_savecamrecipe.Size = new System.Drawing.Size(121, 36);
            this.btn_savecamrecipe.TabIndex = 62;
            this.btn_savecamrecipe.Text = "SaveCamRecipe";
            this.btn_savecamrecipe.UseVisualStyleBackColor = true;
            this.btn_savecamrecipe.Click += new System.EventHandler(this.btn_savecamrecipe_Click);
            // 
            // comboBox_Cameramodel
            // 
            this.comboBox_Cameramodel.FormattingEnabled = true;
            this.comboBox_Cameramodel.Location = new System.Drawing.Point(476, 63);
            this.comboBox_Cameramodel.Name = "comboBox_Cameramodel";
            this.comboBox_Cameramodel.Size = new System.Drawing.Size(121, 23);
            this.comboBox_Cameramodel.TabIndex = 61;
            this.comboBox_Cameramodel.SelectedIndexChanged += new System.EventHandler(this.comboBox_Cameramodel_SelectedIndexChanged);
            // 
            // label_camerarecipe
            // 
            this.label_camerarecipe.AutoSize = true;
            this.label_camerarecipe.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_camerarecipe.Location = new System.Drawing.Point(472, 28);
            this.label_camerarecipe.Name = "label_camerarecipe";
            this.label_camerarecipe.Size = new System.Drawing.Size(144, 24);
            this.label_camerarecipe.TabIndex = 59;
            this.label_camerarecipe.Text = "CameraRecipe";
            // 
            // label_shiftdatarecipe
            // 
            this.label_shiftdatarecipe.AutoSize = true;
            this.label_shiftdatarecipe.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_shiftdatarecipe.Location = new System.Drawing.Point(414, 193);
            this.label_shiftdatarecipe.Name = "label_shiftdatarecipe";
            this.label_shiftdatarecipe.Size = new System.Drawing.Size(158, 24);
            this.label_shiftdatarecipe.TabIndex = 58;
            this.label_shiftdatarecipe.Text = "ShiftDataRecipe";
            // 
            // btn_resample
            // 
            this.btn_resample.Enabled = false;
            this.btn_resample.Location = new System.Drawing.Point(251, 28);
            this.btn_resample.Name = "btn_resample";
            this.btn_resample.Size = new System.Drawing.Size(93, 36);
            this.btn_resample.TabIndex = 57;
            this.btn_resample.Text = "ReSample";
            this.btn_resample.UseVisualStyleBackColor = true;
            this.btn_resample.Click += new System.EventHandler(this.btn_resample_Click);
            // 
            // btn_startsample
            // 
            this.btn_startsample.Location = new System.Drawing.Point(418, 265);
            this.btn_startsample.Name = "btn_startsample";
            this.btn_startsample.Size = new System.Drawing.Size(93, 36);
            this.btn_startsample.TabIndex = 56;
            this.btn_startsample.Text = "StartSample";
            this.btn_startsample.UseVisualStyleBackColor = true;
            this.btn_startsample.Click += new System.EventHandler(this.btn_startsample_Click);
            // 
            // btn_pathadj
            // 
            this.btn_pathadj.Location = new System.Drawing.Point(591, 275);
            this.btn_pathadj.Name = "btn_pathadj";
            this.btn_pathadj.Size = new System.Drawing.Size(93, 36);
            this.btn_pathadj.TabIndex = 55;
            this.btn_pathadj.Text = "PathAdj";
            this.btn_pathadj.UseVisualStyleBackColor = true;
            this.btn_pathadj.Click += new System.EventHandler(this.btn_pathadj_Click);
            // 
            // comboBox_model
            // 
            this.comboBox_model.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_model.FormattingEnabled = true;
            this.comboBox_model.Location = new System.Drawing.Point(309, 231);
            this.comboBox_model.Name = "comboBox_model";
            this.comboBox_model.Size = new System.Drawing.Size(121, 24);
            this.comboBox_model.TabIndex = 54;
            this.comboBox_model.SelectedIndexChanged += new System.EventHandler(this.comboBox_model_SelectedIndexChanged);
            // 
            // comboBox_size
            // 
            this.comboBox_size.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBox_size.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_size.FormattingEnabled = true;
            this.comboBox_size.Items.AddRange(new object[] {
            "6",
            "6.5",
            "7",
            "7.5",
            "8",
            "8.5",
            "9",
            "9.5",
            "10",
            "10.5",
            "11",
            "11.5",
            "12",
            "12.5",
            "13",
            "13.5",
            "14",
            "14.5",
            "15"});
            this.comboBox_size.Location = new System.Drawing.Point(437, 232);
            this.comboBox_size.Name = "comboBox_size";
            this.comboBox_size.Size = new System.Drawing.Size(121, 24);
            this.comboBox_size.TabIndex = 24;
            this.comboBox_size.SelectedIndexChanged += new System.EventHandler(this.comboBox_model_SelectedIndexChanged);
            // 
            // comboBox_direction
            // 
            this.comboBox_direction.Enabled = false;
            this.comboBox_direction.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_direction.FormattingEnabled = true;
            this.comboBox_direction.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBox_direction.Location = new System.Drawing.Point(563, 232);
            this.comboBox_direction.Name = "comboBox_direction";
            this.comboBox_direction.Size = new System.Drawing.Size(121, 24);
            this.comboBox_direction.TabIndex = 25;
            this.comboBox_direction.SelectedIndexChanged += new System.EventHandler(this.comboBox_model_SelectedIndexChanged);
            // 
            // label_exposure
            // 
            this.label_exposure.AutoSize = true;
            this.label_exposure.BackColor = System.Drawing.Color.Transparent;
            this.label_exposure.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_exposure.Location = new System.Drawing.Point(3, 330);
            this.label_exposure.Name = "label_exposure";
            this.label_exposure.Size = new System.Drawing.Size(92, 24);
            this.label_exposure.TabIndex = 53;
            this.label_exposure.Text = "Exposure";
            // 
            // checkBox_TriggerMode
            // 
            this.checkBox_TriggerMode.AutoSize = true;
            this.checkBox_TriggerMode.Checked = true;
            this.checkBox_TriggerMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_TriggerMode.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_TriggerMode.Location = new System.Drawing.Point(15, 298);
            this.checkBox_TriggerMode.Name = "checkBox_TriggerMode";
            this.checkBox_TriggerMode.Size = new System.Drawing.Size(121, 23);
            this.checkBox_TriggerMode.TabIndex = 52;
            this.checkBox_TriggerMode.Text = "TriggerMode";
            this.checkBox_TriggerMode.UseVisualStyleBackColor = true;
            this.checkBox_TriggerMode.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox_Exposure
            // 
            this.textBox_Exposure.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Exposure.Location = new System.Drawing.Point(110, 327);
            this.textBox_Exposure.Name = "textBox_Exposure";
            this.textBox_Exposure.Size = new System.Drawing.Size(100, 33);
            this.textBox_Exposure.TabIndex = 51;
            this.textBox_Exposure.Text = "0";
            this.textBox_Exposure.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Exposure_KeyDown);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Remove.Location = new System.Drawing.Point(309, 307);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(93, 36);
            this.btn_Remove.TabIndex = 50;
            this.btn_Remove.Text = "Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_addSample
            // 
            this.btn_addSample.Location = new System.Drawing.Point(309, 265);
            this.btn_addSample.Name = "btn_addSample";
            this.btn_addSample.Size = new System.Drawing.Size(93, 36);
            this.btn_addSample.TabIndex = 49;
            this.btn_addSample.Text = "AddSample";
            this.btn_addSample.UseVisualStyleBackColor = true;
            this.btn_addSample.Click += new System.EventHandler(this.btn_addSample_Click);
            // 
            // text_angle
            // 
            this.text_angle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_angle.Location = new System.Drawing.Point(110, 223);
            this.text_angle.Name = "text_angle";
            this.text_angle.Size = new System.Drawing.Size(100, 33);
            this.text_angle.TabIndex = 47;
            this.text_angle.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(11, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 24);
            this.label5.TabIndex = 46;
            this.label5.Text = "偏移角度";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(11, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 24);
            this.label8.TabIndex = 45;
            this.label8.Text = "Y偏移距離";
            // 
            // text_shiftY
            // 
            this.text_shiftY.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_shiftY.Location = new System.Drawing.Point(110, 145);
            this.text_shiftY.Name = "text_shiftY";
            this.text_shiftY.Size = new System.Drawing.Size(100, 33);
            this.text_shiftY.TabIndex = 44;
            this.text_shiftY.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(11, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 24);
            this.label7.TabIndex = 43;
            this.label7.Text = "偏移高度";
            // 
            // text_shiftHeight
            // 
            this.text_shiftHeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_shiftHeight.Location = new System.Drawing.Point(110, 184);
            this.text_shiftHeight.Name = "text_shiftHeight";
            this.text_shiftHeight.Size = new System.Drawing.Size(100, 33);
            this.text_shiftHeight.TabIndex = 42;
            this.text_shiftHeight.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(11, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 24);
            this.label6.TabIndex = 41;
            this.label6.Text = "X偏移距離";
            // 
            // text_shiftX
            // 
            this.text_shiftX.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_shiftX.Location = new System.Drawing.Point(110, 106);
            this.text_shiftX.Name = "text_shiftX";
            this.text_shiftX.Size = new System.Drawing.Size(100, 33);
            this.text_shiftX.TabIndex = 40;
            this.text_shiftX.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(11, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 24);
            this.label4.TabIndex = 39;
            this.label4.Text = "內縮距離";
            // 
            // text_shrink
            // 
            this.text_shrink.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_shrink.Location = new System.Drawing.Point(110, 67);
            this.text_shrink.Name = "text_shrink";
            this.text_shrink.Size = new System.Drawing.Size(100, 33);
            this.text_shrink.TabIndex = 38;
            this.text_shrink.Text = "0";
            // 
            // btn_SavePara
            // 
            this.btn_SavePara.Location = new System.Drawing.Point(623, 419);
            this.btn_SavePara.Name = "btn_SavePara";
            this.btn_SavePara.Size = new System.Drawing.Size(93, 36);
            this.btn_SavePara.TabIndex = 32;
            this.btn_SavePara.Text = "SavePara";
            this.btn_SavePara.UseVisualStyleBackColor = true;
            this.btn_SavePara.Click += new System.EventHandler(this.btn_SavePara_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(40, 408);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 31;
            this.label3.Text = "Erode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(40, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 24);
            this.label2.TabIndex = 30;
            this.label2.Text = "Dilate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Threshold";
            // 
            // text_Erode
            // 
            this.text_Erode.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_Erode.Location = new System.Drawing.Point(110, 405);
            this.text_Erode.Name = "text_Erode";
            this.text_Erode.Size = new System.Drawing.Size(100, 33);
            this.text_Erode.TabIndex = 28;
            this.text_Erode.Text = "0";
            // 
            // text_Dilate
            // 
            this.text_Dilate.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_Dilate.Location = new System.Drawing.Point(110, 366);
            this.text_Dilate.Name = "text_Dilate";
            this.text_Dilate.Size = new System.Drawing.Size(100, 33);
            this.text_Dilate.TabIndex = 27;
            this.text_Dilate.Text = "0";
            // 
            // text_Threshold
            // 
            this.text_Threshold.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_Threshold.Location = new System.Drawing.Point(110, 28);
            this.text_Threshold.Name = "text_Threshold";
            this.text_Threshold.Size = new System.Drawing.Size(100, 33);
            this.text_Threshold.TabIndex = 26;
            this.text_Threshold.Text = "0";
            this.text_Threshold.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_Threshold_KeyDown);
            // 
            // label_KUKA
            // 
            this.label_KUKA.AutoSize = true;
            this.label_KUKA.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_KUKA.Location = new System.Drawing.Point(767, 29);
            this.label_KUKA.Name = "label_KUKA";
            this.label_KUKA.Size = new System.Drawing.Size(109, 19);
            this.label_KUKA.TabIndex = 16;
            this.label_KUKA.Text = "KUKA Connect";
            // 
            // label_Cam
            // 
            this.label_Cam.AutoSize = true;
            this.label_Cam.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Cam.Location = new System.Drawing.Point(767, 59);
            this.label_Cam.Name = "label_Cam";
            this.label_Cam.Size = new System.Drawing.Size(102, 19);
            this.label_Cam.TabIndex = 18;
            this.label_Cam.Text = "Cam Connect";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(767, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "IO Connect";
            // 
            // pictureBox_IOConnect
            // 
            this.pictureBox_IOConnect.BackgroundImage = global::ShoesV6.Properties.Resources.icons8_red_circle_16;
            this.pictureBox_IOConnect.Location = new System.Drawing.Point(745, 96);
            this.pictureBox_IOConnect.Name = "pictureBox_IOConnect";
            this.pictureBox_IOConnect.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_IOConnect.TabIndex = 19;
            this.pictureBox_IOConnect.TabStop = false;
            // 
            // pictureBox_Cam
            // 
            this.pictureBox_Cam.BackgroundImage = global::ShoesV6.Properties.Resources.icons8_red_circle_16;
            this.pictureBox_Cam.Location = new System.Drawing.Point(745, 62);
            this.pictureBox_Cam.Name = "pictureBox_Cam";
            this.pictureBox_Cam.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_Cam.TabIndex = 17;
            this.pictureBox_Cam.TabStop = false;
            // 
            // pictureBox_KUKA
            // 
            this.pictureBox_KUKA.BackgroundImage = global::ShoesV6.Properties.Resources.icons8_red_circle_16;
            this.pictureBox_KUKA.Location = new System.Drawing.Point(745, 32);
            this.pictureBox_KUKA.Name = "pictureBox_KUKA";
            this.pictureBox_KUKA.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_KUKA.TabIndex = 15;
            this.pictureBox_KUKA.TabStop = false;
            // 
            // comboBox_ModelSize
            // 
            this.comboBox_ModelSize.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_ModelSize.FormattingEnabled = true;
            this.comboBox_ModelSize.Location = new System.Drawing.Point(748, 217);
            this.comboBox_ModelSize.Name = "comboBox_ModelSize";
            this.comboBox_ModelSize.Size = new System.Drawing.Size(121, 24);
            this.comboBox_ModelSize.TabIndex = 21;
            this.comboBox_ModelSize.SelectedIndexChanged += new System.EventHandler(this.comboBox_ModelSize_SelectedIndexChanged);
            // 
            // checkBox_PathCorrection
            // 
            this.checkBox_PathCorrection.AutoSize = true;
            this.checkBox_PathCorrection.Checked = true;
            this.checkBox_PathCorrection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_PathCorrection.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBox_PathCorrection.Location = new System.Drawing.Point(748, 256);
            this.checkBox_PathCorrection.Name = "checkBox_PathCorrection";
            this.checkBox_PathCorrection.Size = new System.Drawing.Size(58, 23);
            this.checkBox_PathCorrection.TabIndex = 22;
            this.checkBox_PathCorrection.Text = "校正";
            this.checkBox_PathCorrection.UseVisualStyleBackColor = true;
            // 
            // Heartbeat_Timer
            // 
            this.Heartbeat_Timer.Interval = 500;
            this.Heartbeat_Timer.Tick += new System.EventHandler(this.Heartbeat_Timer_Tick);
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView.Location = new System.Drawing.Point(760, 288);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(146, 203);
            this.dataGridView.TabIndex = 24;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(814, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "label10";
            this.label10.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBox1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "6",
            "6.5",
            "7",
            "7.5",
            "8",
            "8.5",
            "9",
            "9.5",
            "10",
            "10.5",
            "11",
            "11.5",
            "12",
            "12.5",
            "13",
            "13.5",
            "14",
            "14.5",
            "15"});
            this.comboBox1.Location = new System.Drawing.Point(796, 132);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 58;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(753, 174);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 33);
            this.textBox1.TabIndex = 64;
            this.textBox1.Text = "0";
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(942, 502);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.checkBox_PathCorrection);
            this.Controls.Add(this.comboBox_ModelSize);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox_IOConnect);
            this.Controls.Add(this.label_Cam);
            this.Controls.Add(this.pictureBox_Cam);
            this.Controls.Add(this.label_KUKA);
            this.Controls.Add(this.pictureBox_KUKA);
            this.Controls.Add(this.tabControl2);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl2.ResumeLayout(false);
            this.Scan.ResumeLayout(false);
            this.Scan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.EM.ResumeLayout(false);
            this.EM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IOConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_KUKA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage Scan;
        private System.Windows.Forms.TabPage Connect;
        private System.Windows.Forms.Button btn_CamConnect;
        private System.Windows.Forms.Label label_KUKA;
        private System.Windows.Forms.PictureBox pictureBox_KUKA;
        private System.Windows.Forms.Button btn_RobConnect;
        private System.Windows.Forms.Label label_Cam;
        private System.Windows.Forms.PictureBox pictureBox_Cam;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.TabPage EM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_Erode;
        private System.Windows.Forms.TextBox text_Dilate;
        private System.Windows.Forms.TextBox text_Threshold;
        private System.Windows.Forms.CheckBox checkBox_saveimage;
        private System.Windows.Forms.Button btn_SavePara;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox text_shiftY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox text_shiftHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_shiftX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_shrink;
        private System.Windows.Forms.TextBox text_angle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox_IOConnect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_VerifyPath;
        private System.Windows.Forms.ComboBox comboBox_ModelSize;
        private System.Windows.Forms.CheckBox checkBox_PathCorrection;
        private System.Windows.Forms.Button btn_addSample;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_TC;
        private System.Windows.Forms.Timer Heartbeat_Timer;
        private System.Windows.Forms.Button btn_IOConnect;
        private System.Windows.Forms.TextBox textBox_Exposure;
        private System.Windows.Forms.CheckBox checkBox_TriggerMode;
        private System.Windows.Forms.Label label_exposure;
        private System.Windows.Forms.ComboBox comboBox_size;
        private System.Windows.Forms.ComboBox comboBox_direction;
        private System.Windows.Forms.ComboBox comboBox_model;
        private System.Windows.Forms.Button btn_pathadj;
        private System.Windows.Forms.Button btn_startsample;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_resample;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox_Cameramodel;
        private System.Windows.Forms.Label label_camerarecipe;
        private System.Windows.Forms.Label label_shiftdatarecipe;
        private System.Windows.Forms.Button btn_savecamrecipe;
        private System.Windows.Forms.Button btn_removecamerarecipe;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_upload;
    }
}

