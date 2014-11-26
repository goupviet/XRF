namespace XRFGDITest
{
    partial class Form1
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
            this.imgs = new System.Windows.Forms.TabControl();
            this.tab_orig = new System.Windows.Forms.TabPage();
            this.pb_orig = new System.Windows.Forms.PictureBox();
            this.tab_sepia = new System.Windows.Forms.TabPage();
            this.pb_sepia = new System.Windows.Forms.PictureBox();
            this.tab_trans = new System.Windows.Forms.TabPage();
            this.pb_trans = new System.Windows.Forms.PictureBox();
            this.tab_gscale = new System.Windows.Forms.TabPage();
            this.pb_gscale = new System.Windows.Forms.PictureBox();
            this.tab_neg = new System.Windows.Forms.TabPage();
            this.pb_neg = new System.Windows.Forms.PictureBox();
            this.tab_sub = new System.Windows.Forms.TabPage();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.TrackBar();
            this.pnlrplclr = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlsubclr = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pb_postsub = new System.Windows.Forms.PictureBox();
            this.pb_presub = new System.Windows.Forms.PictureBox();
            this.tab_arith = new System.Windows.Forms.TabPage();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_open = new System.Windows.Forms.ToolStripButton();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.cld = new System.Windows.Forms.ColorDialog();
            this.tab_cust_mat = new System.Windows.Forms.TabPage();
            this.pb_custmat = new System.Windows.Forms.PictureBox();
            this.imgs.SuspendLayout();
            this.tab_orig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_orig)).BeginInit();
            this.tab_sepia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_sepia)).BeginInit();
            this.tab_trans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_trans)).BeginInit();
            this.tab_gscale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_gscale)).BeginInit();
            this.tab_neg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_neg)).BeginInit();
            this.tab_sub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_postsub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_presub)).BeginInit();
            this.ToolStrip1.SuspendLayout();
            this.tab_cust_mat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_custmat)).BeginInit();
            this.SuspendLayout();
            // 
            // imgs
            // 
            this.imgs.Controls.Add(this.tab_orig);
            this.imgs.Controls.Add(this.tab_sepia);
            this.imgs.Controls.Add(this.tab_trans);
            this.imgs.Controls.Add(this.tab_gscale);
            this.imgs.Controls.Add(this.tab_neg);
            this.imgs.Controls.Add(this.tab_cust_mat);
            this.imgs.Controls.Add(this.tab_sub);
            this.imgs.Controls.Add(this.tab_arith);
            this.imgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgs.Location = new System.Drawing.Point(0, 25);
            this.imgs.Name = "imgs";
            this.imgs.SelectedIndex = 0;
            this.imgs.Size = new System.Drawing.Size(689, 388);
            this.imgs.TabIndex = 2;
            // 
            // tab_orig
            // 
            this.tab_orig.Controls.Add(this.pb_orig);
            this.tab_orig.Location = new System.Drawing.Point(4, 22);
            this.tab_orig.Name = "tab_orig";
            this.tab_orig.Padding = new System.Windows.Forms.Padding(3);
            this.tab_orig.Size = new System.Drawing.Size(681, 362);
            this.tab_orig.TabIndex = 0;
            this.tab_orig.Text = "Original";
            this.tab_orig.UseVisualStyleBackColor = true;
            // 
            // pb_orig
            // 
            this.pb_orig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_orig.Location = new System.Drawing.Point(3, 3);
            this.pb_orig.Name = "pb_orig";
            this.pb_orig.Size = new System.Drawing.Size(675, 356);
            this.pb_orig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_orig.TabIndex = 0;
            this.pb_orig.TabStop = false;
            // 
            // tab_sepia
            // 
            this.tab_sepia.Controls.Add(this.pb_sepia);
            this.tab_sepia.Location = new System.Drawing.Point(4, 22);
            this.tab_sepia.Name = "tab_sepia";
            this.tab_sepia.Padding = new System.Windows.Forms.Padding(3);
            this.tab_sepia.Size = new System.Drawing.Size(681, 362);
            this.tab_sepia.TabIndex = 1;
            this.tab_sepia.Text = "Sepia";
            this.tab_sepia.UseVisualStyleBackColor = true;
            // 
            // pb_sepia
            // 
            this.pb_sepia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_sepia.Location = new System.Drawing.Point(3, 3);
            this.pb_sepia.Name = "pb_sepia";
            this.pb_sepia.Size = new System.Drawing.Size(675, 356);
            this.pb_sepia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_sepia.TabIndex = 0;
            this.pb_sepia.TabStop = false;
            // 
            // tab_trans
            // 
            this.tab_trans.Controls.Add(this.pb_trans);
            this.tab_trans.Location = new System.Drawing.Point(4, 22);
            this.tab_trans.Name = "tab_trans";
            this.tab_trans.Size = new System.Drawing.Size(681, 362);
            this.tab_trans.TabIndex = 2;
            this.tab_trans.Text = "Transparency";
            this.tab_trans.UseVisualStyleBackColor = true;
            // 
            // pb_trans
            // 
            this.pb_trans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_trans.Location = new System.Drawing.Point(0, 0);
            this.pb_trans.Name = "pb_trans";
            this.pb_trans.Size = new System.Drawing.Size(681, 362);
            this.pb_trans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_trans.TabIndex = 0;
            this.pb_trans.TabStop = false;
            // 
            // tab_gscale
            // 
            this.tab_gscale.Controls.Add(this.pb_gscale);
            this.tab_gscale.Location = new System.Drawing.Point(4, 22);
            this.tab_gscale.Name = "tab_gscale";
            this.tab_gscale.Size = new System.Drawing.Size(681, 362);
            this.tab_gscale.TabIndex = 3;
            this.tab_gscale.Text = "Grayscale";
            this.tab_gscale.UseVisualStyleBackColor = true;
            // 
            // pb_gscale
            // 
            this.pb_gscale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_gscale.Location = new System.Drawing.Point(0, 0);
            this.pb_gscale.Name = "pb_gscale";
            this.pb_gscale.Size = new System.Drawing.Size(681, 362);
            this.pb_gscale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_gscale.TabIndex = 0;
            this.pb_gscale.TabStop = false;
            // 
            // tab_neg
            // 
            this.tab_neg.Controls.Add(this.pb_neg);
            this.tab_neg.Location = new System.Drawing.Point(4, 22);
            this.tab_neg.Name = "tab_neg";
            this.tab_neg.Padding = new System.Windows.Forms.Padding(3);
            this.tab_neg.Size = new System.Drawing.Size(681, 362);
            this.tab_neg.TabIndex = 4;
            this.tab_neg.Text = "Negative";
            this.tab_neg.UseVisualStyleBackColor = true;
            // 
            // pb_neg
            // 
            this.pb_neg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_neg.Location = new System.Drawing.Point(3, 3);
            this.pb_neg.Name = "pb_neg";
            this.pb_neg.Size = new System.Drawing.Size(675, 356);
            this.pb_neg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_neg.TabIndex = 0;
            this.pb_neg.TabStop = false;
            // 
            // tab_sub
            // 
            this.tab_sub.Controls.Add(this.btnFilter);
            this.tab_sub.Controls.Add(this.label3);
            this.tab_sub.Controls.Add(this.threshold);
            this.tab_sub.Controls.Add(this.pnlrplclr);
            this.tab_sub.Controls.Add(this.label2);
            this.tab_sub.Controls.Add(this.pnlsubclr);
            this.tab_sub.Controls.Add(this.label1);
            this.tab_sub.Controls.Add(this.pb_postsub);
            this.tab_sub.Controls.Add(this.pb_presub);
            this.tab_sub.Location = new System.Drawing.Point(4, 22);
            this.tab_sub.Name = "tab_sub";
            this.tab_sub.Padding = new System.Windows.Forms.Padding(3);
            this.tab_sub.Size = new System.Drawing.Size(681, 362);
            this.tab_sub.TabIndex = 5;
            this.tab_sub.Text = "Substitution";
            this.tab_sub.UseVisualStyleBackColor = true;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(270, 205);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(122, 23);
            this.btnFilter.TabIndex = 8;
            this.btnFilter.Text = "Apply Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "with Threshold:";
            // 
            // threshold
            // 
            this.threshold.Location = new System.Drawing.Point(270, 154);
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(122, 45);
            this.threshold.TabIndex = 6;
            this.threshold.Value = 5;
            // 
            // pnlrplclr
            // 
            this.pnlrplclr.BackColor = System.Drawing.Color.Black;
            this.pnlrplclr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlrplclr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlrplclr.Location = new System.Drawing.Point(273, 96);
            this.pnlrplclr.Name = "pnlrplclr";
            this.pnlrplclr.Size = new System.Drawing.Size(122, 36);
            this.pnlrplclr.TabIndex = 5;
            this.pnlrplclr.Click += new System.EventHandler(this.pnlrplclr_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "for:";
            // 
            // pnlsubclr
            // 
            this.pnlsubclr.BackColor = System.Drawing.Color.White;
            this.pnlsubclr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlsubclr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlsubclr.Location = new System.Drawing.Point(273, 40);
            this.pnlsubclr.Name = "pnlsubclr";
            this.pnlsubclr.Size = new System.Drawing.Size(122, 36);
            this.pnlsubclr.TabIndex = 3;
            this.pnlsubclr.Click += new System.EventHandler(this.pnlsubclr_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Substitute:";
            // 
            // pb_postsub
            // 
            this.pb_postsub.Dock = System.Windows.Forms.DockStyle.Right;
            this.pb_postsub.Location = new System.Drawing.Point(401, 3);
            this.pb_postsub.Name = "pb_postsub";
            this.pb_postsub.Size = new System.Drawing.Size(277, 356);
            this.pb_postsub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_postsub.TabIndex = 1;
            this.pb_postsub.TabStop = false;
            // 
            // pb_presub
            // 
            this.pb_presub.Dock = System.Windows.Forms.DockStyle.Left;
            this.pb_presub.Location = new System.Drawing.Point(3, 3);
            this.pb_presub.Name = "pb_presub";
            this.pb_presub.Size = new System.Drawing.Size(264, 356);
            this.pb_presub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_presub.TabIndex = 0;
            this.pb_presub.TabStop = false;
            // 
            // tab_arith
            // 
            this.tab_arith.Location = new System.Drawing.Point(4, 22);
            this.tab_arith.Name = "tab_arith";
            this.tab_arith.Padding = new System.Windows.Forms.Padding(3);
            this.tab_arith.Size = new System.Drawing.Size(681, 362);
            this.tab_arith.TabIndex = 7;
            this.tab_arith.Text = "Arithmetic Blending";
            this.tab_arith.UseVisualStyleBackColor = true;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_open});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(689, 25);
            this.ToolStrip1.TabIndex = 3;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // btn_open
            // 
            this.btn_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(76, 22);
            this.btn_open.Text = "Open Image";
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "OpenFileDialog1";
            this.ofd.Title = "Open file to test";
            // 
            // cld
            // 
            this.cld.AnyColor = true;
            this.cld.FullOpen = true;
            this.cld.SolidColorOnly = true;
            // 
            // tab_cust_mat
            // 
            this.tab_cust_mat.Controls.Add(this.pb_custmat);
            this.tab_cust_mat.Location = new System.Drawing.Point(4, 22);
            this.tab_cust_mat.Name = "tab_cust_mat";
            this.tab_cust_mat.Padding = new System.Windows.Forms.Padding(3);
            this.tab_cust_mat.Size = new System.Drawing.Size(681, 362);
            this.tab_cust_mat.TabIndex = 8;
            this.tab_cust_mat.Text = "Custom Matrix";
            this.tab_cust_mat.UseVisualStyleBackColor = true;
            // 
            // pb_custmat
            // 
            this.pb_custmat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_custmat.Location = new System.Drawing.Point(3, 3);
            this.pb_custmat.Name = "pb_custmat";
            this.pb_custmat.Size = new System.Drawing.Size(675, 356);
            this.pb_custmat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_custmat.TabIndex = 0;
            this.pb_custmat.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 413);
            this.Controls.Add(this.imgs);
            this.Controls.Add(this.ToolStrip1);
            this.Name = "Form1";
            this.Text = "XRF GDI/HLSL Filter + Shader Test";
            this.imgs.ResumeLayout(false);
            this.tab_orig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_orig)).EndInit();
            this.tab_sepia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_sepia)).EndInit();
            this.tab_trans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_trans)).EndInit();
            this.tab_gscale.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_gscale)).EndInit();
            this.tab_neg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_neg)).EndInit();
            this.tab_sub.ResumeLayout(false);
            this.tab_sub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_postsub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_presub)).EndInit();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.tab_cust_mat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_custmat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TabControl imgs;
        internal System.Windows.Forms.TabPage tab_orig;
        internal System.Windows.Forms.PictureBox pb_orig;
        internal System.Windows.Forms.TabPage tab_sepia;
        internal System.Windows.Forms.TabPage tab_trans;
        internal System.Windows.Forms.TabPage tab_gscale;
        internal System.Windows.Forms.TabPage tab_neg;
        internal System.Windows.Forms.TabPage tab_sub;
        internal System.Windows.Forms.TabPage tab_arith;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripButton btn_open;
        internal System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.PictureBox pb_sepia;
        private System.Windows.Forms.PictureBox pb_trans;
        private System.Windows.Forms.PictureBox pb_gscale;
        private System.Windows.Forms.PictureBox pb_neg;
        private System.Windows.Forms.PictureBox pb_postsub;
        private System.Windows.Forms.PictureBox pb_presub;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar threshold;
        private System.Windows.Forms.Panel pnlrplclr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlsubclr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog cld;
        private System.Windows.Forms.TabPage tab_cust_mat;
        private System.Windows.Forms.PictureBox pb_custmat;
    }
}

