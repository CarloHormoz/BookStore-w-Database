namespace Lab1
{
    partial class MainMenu
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
            this.CustomerButton = new System.Windows.Forms.Button();
            this.BookButton = new System.Windows.Forms.Button();
            this.OrderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CustomerButton
            // 
            this.CustomerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CustomerButton.Location = new System.Drawing.Point(299, 130);
            this.CustomerButton.Name = "CustomerButton";
            this.CustomerButton.Size = new System.Drawing.Size(162, 30);
            this.CustomerButton.TabIndex = 0;
            this.CustomerButton.Text = "Manage Customers";
            this.CustomerButton.UseVisualStyleBackColor = true;
            this.CustomerButton.Click += new System.EventHandler(this.CustomerButton_Click);
            // 
            // BookButton
            // 
            this.BookButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BookButton.Location = new System.Drawing.Point(299, 194);
            this.BookButton.Name = "BookButton";
            this.BookButton.Size = new System.Drawing.Size(162, 30);
            this.BookButton.TabIndex = 1;
            this.BookButton.Text = "Manage Books";
            this.BookButton.UseVisualStyleBackColor = true;
            this.BookButton.Click += new System.EventHandler(this.BookButton_Click);
            // 
            // OrderButton
            // 
            this.OrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OrderButton.Location = new System.Drawing.Point(299, 259);
            this.OrderButton.Name = "OrderButton";
            this.OrderButton.Size = new System.Drawing.Size(162, 30);
            this.OrderButton.TabIndex = 2;
            this.OrderButton.Text = "Place Order";
            this.OrderButton.UseVisualStyleBackColor = true;
            this.OrderButton.Click += new System.EventHandler(this.OrderButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OrderButton);
            this.Controls.Add(this.BookButton);
            this.Controls.Add(this.CustomerButton);
            this.Name = "MainMenu";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CustomerButton;
        private System.Windows.Forms.Button BookButton;
        private System.Windows.Forms.Button OrderButton;
    }
}