namespace ChatBot
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
            btnSend = new Button();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            txtUserInput = new TextBox();
            btnClear = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.ForeColor = Color.Black;
            btnSend.Location = new Point(408, 350);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 29);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Cyan;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(251, 20);
            label1.TabIndex = 1;
            label1.Text = "CYBERSECURITY AWARENESS BOT";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.AppWorkspace;
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Location = new Point(12, 37);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(599, 307);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // txtUserInput
            // 
            txtUserInput.Location = new Point(12, 350);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(381, 27);
            txtUserInput.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(517, 350);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 4;
            btnClear.Text = "Clear chat";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.ForeColor = Color.Black;
            btnExit.Location = new Point(12, 415);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 29);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(832, 500);
            Controls.Add(btnExit);
            Controls.Add(btnClear);
            Controls.Add(txtUserInput);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(btnSend);
            ForeColor = Color.Cyan;
            Name = "Form1";
            Text = "Cyber Security Awareness Bot";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private Label label1;
        private RichTextBox richTextBox1;
        private TextBox txtUserInput;
        private Button btnClear;
        private Button btnExit;
    }
}
