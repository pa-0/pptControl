namespace Power_Control
{
    partial class MainForm
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
            this.Extremities = new System.Windows.Forms.CheckBox();
            this.Skeleton = new System.Windows.Forms.CheckBox();
            this.Joints = new System.Windows.Forms.CheckBox();
            this.Adaptive = new System.Windows.Forms.CheckBox();
            this.FullHandMode = new System.Windows.Forms.RadioButton();
            this.Cursor = new System.Windows.Forms.CheckBox();
            this.TrackingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.CursorMode = new System.Windows.Forms.RadioButton();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.Scale2 = new System.Windows.Forms.CheckBox();
            this.Mirror = new System.Windows.Forms.CheckBox();
            this.ImageGroupBox = new System.Windows.Forms.GroupBox();
            this.Depth = new System.Windows.Forms.RadioButton();
            this.Labelmap = new System.Windows.Forms.RadioButton();
            this.ContourCheckBox = new System.Windows.Forms.CheckBox();
            this.infoTextBox = new System.Windows.Forms.RichTextBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbGesturesList = new System.Windows.Forms.ComboBox();
            this.Panel2 = new System.Windows.Forms.PictureBox();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status2 = new System.Windows.Forms.StatusStrip();
            this.Record = new System.Windows.Forms.ToolStripMenuItem();
            this.Playback = new System.Windows.Forms.ToolStripMenuItem();
            this.Live = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.sourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Stop = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.TrackingModeGroupBox.SuspendLayout();
            this.OptionsGroupBox.SuspendLayout();
            this.ImageGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Panel2)).BeginInit();
            this.Status2.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Extremities
            // 
            this.Extremities.AutoSize = true;
            this.Extremities.Enabled = false;
            this.Extremities.Location = new System.Drawing.Point(20, 115);
            this.Extremities.Name = "Extremities";
            this.Extremities.Size = new System.Drawing.Size(76, 17);
            this.Extremities.TabIndex = 46;
            this.Extremities.Text = "Extremities";
            this.Extremities.UseVisualStyleBackColor = true;
            // 
            // Skeleton
            // 
            this.Skeleton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Skeleton.AutoSize = true;
            this.Skeleton.Enabled = false;
            this.Skeleton.Location = new System.Drawing.Point(20, 99);
            this.Skeleton.Name = "Skeleton";
            this.Skeleton.Size = new System.Drawing.Size(68, 17);
            this.Skeleton.TabIndex = 23;
            this.Skeleton.Text = "Skeleton";
            this.Skeleton.UseVisualStyleBackColor = true;
            // 
            // Joints
            // 
            this.Joints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Joints.AutoSize = true;
            this.Joints.Enabled = false;
            this.Joints.Location = new System.Drawing.Point(20, 84);
            this.Joints.Name = "Joints";
            this.Joints.Size = new System.Drawing.Size(53, 17);
            this.Joints.TabIndex = 19;
            this.Joints.Text = "Joints";
            this.Joints.UseVisualStyleBackColor = true;
            // 
            // Adaptive
            // 
            this.Adaptive.AutoSize = true;
            this.Adaptive.Location = new System.Drawing.Point(19, 47);
            this.Adaptive.Name = "Adaptive";
            this.Adaptive.Size = new System.Drawing.Size(68, 17);
            this.Adaptive.TabIndex = 47;
            this.Adaptive.Text = "Adaptive";
            this.Adaptive.UseVisualStyleBackColor = true;
            // 
            // FullHandMode
            // 
            this.FullHandMode.AutoSize = true;
            this.FullHandMode.Location = new System.Drawing.Point(6, 67);
            this.FullHandMode.Name = "FullHandMode";
            this.FullHandMode.Size = new System.Drawing.Size(115, 17);
            this.FullHandMode.TabIndex = 51;
            this.FullHandMode.Text = "Full Hand Tracking";
            this.FullHandMode.UseVisualStyleBackColor = true;
            // 
            // Cursor
            // 
            this.Cursor.AutoSize = true;
            this.Cursor.Checked = true;
            this.Cursor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cursor.Location = new System.Drawing.Point(19, 31);
            this.Cursor.Name = "Cursor";
            this.Cursor.Size = new System.Drawing.Size(56, 17);
            this.Cursor.TabIndex = 42;
            this.Cursor.Text = "Cursor";
            this.Cursor.UseVisualStyleBackColor = true;
            // 
            // TrackingModeGroupBox
            // 
            this.TrackingModeGroupBox.Controls.Add(this.Extremities);
            this.TrackingModeGroupBox.Controls.Add(this.Skeleton);
            this.TrackingModeGroupBox.Controls.Add(this.Joints);
            this.TrackingModeGroupBox.Controls.Add(this.Adaptive);
            this.TrackingModeGroupBox.Controls.Add(this.FullHandMode);
            this.TrackingModeGroupBox.Controls.Add(this.CursorMode);
            this.TrackingModeGroupBox.Controls.Add(this.Cursor);
            this.TrackingModeGroupBox.Location = new System.Drawing.Point(430, 165);
            this.TrackingModeGroupBox.Name = "TrackingModeGroupBox";
            this.TrackingModeGroupBox.Size = new System.Drawing.Size(128, 140);
            this.TrackingModeGroupBox.TabIndex = 61;
            this.TrackingModeGroupBox.TabStop = false;
            this.TrackingModeGroupBox.Text = "Tracking Type";
            // 
            // CursorMode
            // 
            this.CursorMode.AutoSize = true;
            this.CursorMode.Checked = true;
            this.CursorMode.Location = new System.Drawing.Point(6, 13);
            this.CursorMode.Name = "CursorMode";
            this.CursorMode.Size = new System.Drawing.Size(100, 17);
            this.CursorMode.TabIndex = 51;
            this.CursorMode.TabStop = true;
            this.CursorMode.Text = "Cursor Tracking";
            this.CursorMode.UseVisualStyleBackColor = true;
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.Scale2);
            this.OptionsGroupBox.Controls.Add(this.Mirror);
            this.OptionsGroupBox.Location = new System.Drawing.Point(430, 109);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(128, 50);
            this.OptionsGroupBox.TabIndex = 60;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // Scale2
            // 
            this.Scale2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Scale2.AutoSize = true;
            this.Scale2.Checked = true;
            this.Scale2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Scale2.Location = new System.Drawing.Point(11, 15);
            this.Scale2.Name = "Scale2";
            this.Scale2.Size = new System.Drawing.Size(53, 17);
            this.Scale2.TabIndex = 26;
            this.Scale2.Text = "Scale";
            this.Scale2.UseVisualStyleBackColor = true;
            // 
            // Mirror
            // 
            this.Mirror.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Mirror.AutoSize = true;
            this.Mirror.Checked = true;
            this.Mirror.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Mirror.Location = new System.Drawing.Point(11, 31);
            this.Mirror.Name = "Mirror";
            this.Mirror.Size = new System.Drawing.Size(52, 17);
            this.Mirror.TabIndex = 30;
            this.Mirror.Text = "Mirror";
            this.Mirror.UseVisualStyleBackColor = true;
            // 
            // ImageGroupBox
            // 
            this.ImageGroupBox.Controls.Add(this.Depth);
            this.ImageGroupBox.Controls.Add(this.Labelmap);
            this.ImageGroupBox.Controls.Add(this.ContourCheckBox);
            this.ImageGroupBox.Location = new System.Drawing.Point(430, 40);
            this.ImageGroupBox.Name = "ImageGroupBox";
            this.ImageGroupBox.Size = new System.Drawing.Size(128, 63);
            this.ImageGroupBox.TabIndex = 59;
            this.ImageGroupBox.TabStop = false;
            this.ImageGroupBox.Text = "Image";
            // 
            // Depth
            // 
            this.Depth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Depth.AutoSize = true;
            this.Depth.Checked = true;
            this.Depth.Enabled = false;
            this.Depth.Location = new System.Drawing.Point(12, 12);
            this.Depth.Name = "Depth";
            this.Depth.Size = new System.Drawing.Size(54, 17);
            this.Depth.TabIndex = 20;
            this.Depth.TabStop = true;
            this.Depth.Text = "Depth";
            this.Depth.UseVisualStyleBackColor = true;
            // 
            // Labelmap
            // 
            this.Labelmap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Labelmap.AutoSize = true;
            this.Labelmap.Enabled = false;
            this.Labelmap.Location = new System.Drawing.Point(12, 27);
            this.Labelmap.Name = "Labelmap";
            this.Labelmap.Size = new System.Drawing.Size(95, 17);
            this.Labelmap.TabIndex = 21;
            this.Labelmap.Text = "Labeled Image";
            this.Labelmap.UseVisualStyleBackColor = true;
            // 
            // ContourCheckBox
            // 
            this.ContourCheckBox.AutoSize = true;
            this.ContourCheckBox.Checked = true;
            this.ContourCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ContourCheckBox.Enabled = false;
            this.ContourCheckBox.Location = new System.Drawing.Point(13, 43);
            this.ContourCheckBox.Name = "ContourCheckBox";
            this.ContourCheckBox.Size = new System.Drawing.Size(92, 17);
            this.ContourCheckBox.TabIndex = 41;
            this.ContourCheckBox.Text = "Hand Contour";
            this.ContourCheckBox.UseVisualStyleBackColor = true;
            // 
            // infoTextBox
            // 
            this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoTextBox.Location = new System.Drawing.Point(24, 386);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(400, 136);
            this.infoTextBox.TabIndex = 58;
            this.infoTextBox.Text = "";
            // 
            // labelFPS
            // 
            this.labelFPS.AutoSize = true;
            this.labelFPS.Location = new System.Drawing.Point(473, 490);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(0, 13);
            this.labelFPS.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(445, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Gesture:";
            // 
            // cmbGesturesList
            // 
            this.cmbGesturesList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGesturesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbGesturesList.FormattingEnabled = true;
            this.cmbGesturesList.Location = new System.Drawing.Point(445, 353);
            this.cmbGesturesList.Name = "cmbGesturesList";
            this.cmbGesturesList.Size = new System.Drawing.Size(94, 17);
            this.cmbGesturesList.TabIndex = 55;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel2.ErrorImage = null;
            this.Panel2.InitialImage = null;
            this.Panel2.Location = new System.Drawing.Point(24, 31);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(400, 349);
            this.Panel2.TabIndex = 54;
            this.Panel2.TabStop = false;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(23, 15);
            this.StatusLabel.Text = "OK";
            // 
            // Status2
            // 
            this.Status2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.Status2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.Status2.Location = new System.Drawing.Point(0, 540);
            this.Status2.Name = "Status2";
            this.Status2.Size = new System.Drawing.Size(601, 20);
            this.Status2.SizingGrip = false;
            this.Status2.TabIndex = 53;
            this.Status2.Text = "Status2";
            // 
            // Record
            // 
            this.Record.Name = "Record";
            this.Record.Size = new System.Drawing.Size(121, 22);
            this.Record.Text = "Record";
            // 
            // Playback
            // 
            this.Playback.Name = "Playback";
            this.Playback.Size = new System.Drawing.Size(121, 22);
            this.Playback.Text = "Playback";
            // 
            // Live
            // 
            this.Live.Checked = true;
            this.Live.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Live.Name = "Live";
            this.Live.Size = new System.Drawing.Size(121, 22);
            this.Live.Text = "Live";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Live,
            this.Playback,
            this.Record});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenu.Size = new System.Drawing.Size(601, 24);
            this.MainMenu.TabIndex = 50;
            this.MainMenu.Text = "MainMenu";
            // 
            // sourceToolStripMenuItem
            // 
            this.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem";
            this.sourceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.sourceToolStripMenuItem.Text = "Device";
            // 
            // Stop
            // 
            this.Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Stop.Enabled = false;
            this.Stop.Location = new System.Drawing.Point(452, 458);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(80, 27);
            this.Stop.TabIndex = 52;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.Location = new System.Drawing.Point(452, 426);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(80, 28);
            this.Start.TabIndex = 51;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 560);
            this.Controls.Add(this.TrackingModeGroupBox);
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.ImageGroupBox);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbGesturesList);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Status2);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Name = "MainForm";
            this.Text = "Power Control";
            this.TrackingModeGroupBox.ResumeLayout(false);
            this.TrackingModeGroupBox.PerformLayout();
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            this.ImageGroupBox.ResumeLayout(false);
            this.ImageGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Panel2)).EndInit();
            this.Status2.ResumeLayout(false);
            this.Status2.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Extremities;
        private System.Windows.Forms.CheckBox Skeleton;
        private System.Windows.Forms.CheckBox Joints;
        private System.Windows.Forms.CheckBox Adaptive;
        private System.Windows.Forms.RadioButton FullHandMode;
        private System.Windows.Forms.CheckBox Cursor;
        private System.Windows.Forms.GroupBox TrackingModeGroupBox;
        private System.Windows.Forms.RadioButton CursorMode;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        private System.Windows.Forms.CheckBox Scale2;
        private System.Windows.Forms.CheckBox Mirror;
        private System.Windows.Forms.GroupBox ImageGroupBox;
        private System.Windows.Forms.RadioButton Depth;
        private System.Windows.Forms.RadioButton Labelmap;
        private System.Windows.Forms.CheckBox ContourCheckBox;
        private System.Windows.Forms.RichTextBox infoTextBox;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbGesturesList;
        private System.Windows.Forms.PictureBox Panel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.StatusStrip Status2;
        private System.Windows.Forms.ToolStripMenuItem Record;
        private System.Windows.Forms.ToolStripMenuItem Playback;
        private System.Windows.Forms.ToolStripMenuItem Live;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem sourceToolStripMenuItem;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Start;
    }
}

