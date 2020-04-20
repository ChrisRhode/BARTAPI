namespace PollWebServiceTest
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
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnFixJSON = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lbStationPicker = new System.Windows.Forms.ComboBox();
            this.lbDestinationPicker = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(50, 164);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(677, 134);
            this.tbOutput.TabIndex = 1;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(50, 304);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(677, 89);
            this.tbResult.TabIndex = 3;
            // 
            // btnFixJSON
            // 
            this.btnFixJSON.Location = new System.Drawing.Point(196, 31);
            this.btnFixJSON.Name = "btnFixJSON";
            this.btnFixJSON.Size = new System.Drawing.Size(113, 23);
            this.btnFixJSON.TabIndex = 4;
            this.btnFixJSON.Text = "Fix JSON";
            this.btnFixJSON.UseVisualStyleBackColor = true;
            this.btnFixJSON.Click += new System.EventHandler(this.btnFixJSON_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(47, 418);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(680, 23);
            this.lblMessage.TabIndex = 7;
            // 
            // lbStationPicker
            // 
            this.lbStationPicker.FormattingEnabled = true;
            this.lbStationPicker.Location = new System.Drawing.Point(366, 96);
            this.lbStationPicker.Name = "lbStationPicker";
            this.lbStationPicker.Size = new System.Drawing.Size(361, 24);
            this.lbStationPicker.TabIndex = 8;
            this.lbStationPicker.SelectedIndexChanged += new System.EventHandler(this.lbStationPicker_SelectedIndexChanged);
            // 
            // lbDestinationPicker
            // 
            this.lbDestinationPicker.FormattingEnabled = true;
            this.lbDestinationPicker.Location = new System.Drawing.Point(366, 133);
            this.lbDestinationPicker.Name = "lbDestinationPicker";
            this.lbDestinationPicker.Size = new System.Drawing.Size(361, 24);
            this.lbDestinationPicker.TabIndex = 9;
            this.lbDestinationPicker.SelectedIndexChanged += new System.EventHandler(this.lbDestinationPicker_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbDestinationPicker);
            this.Controls.Add(this.lbStationPicker);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnFixJSON);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbOutput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnFixJSON;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox lbStationPicker;
        private System.Windows.Forms.ComboBox lbDestinationPicker;
    }
}

