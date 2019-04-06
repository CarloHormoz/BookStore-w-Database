using System;

namespace Lab1
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
            this.CancelOrderButton = new System.Windows.Forms.Button();
            this.ConfirmOrderButton = new System.Windows.Forms.Button();
            this.AuthorTextBox = new System.Windows.Forms.TextBox();
            this.TaxTextBox = new System.Windows.Forms.TextBox();
            this.TotalTextBox = new System.Windows.Forms.TextBox();
            this.SubtotalTextBox = new System.Windows.Forms.TextBox();
            this.QuantityTextBox = new System.Windows.Forms.TextBox();
            this.PriceTextBox = new System.Windows.Forms.TextBox();
            this.ISBNTextBox = new System.Windows.Forms.TextBox();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.ISBNLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.SubtotalLabel = new System.Windows.Forms.Label();
            this.TaxLabel = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderSummaryData = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookSelectionBox = new System.Windows.Forms.ComboBox();
            this.AddTitleButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.OrderSummaryData)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelOrderButton
            // 
            this.CancelOrderButton.BackColor = System.Drawing.Color.GhostWhite;
            this.CancelOrderButton.Enabled = false;
            this.CancelOrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelOrderButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CancelOrderButton.Location = new System.Drawing.Point(439, 428);
            this.CancelOrderButton.Name = "CancelOrderButton";
            this.CancelOrderButton.Size = new System.Drawing.Size(143, 23);
            this.CancelOrderButton.TabIndex = 1;
            this.CancelOrderButton.Text = "Cancel Order";
            this.CancelOrderButton.UseVisualStyleBackColor = false;
            this.CancelOrderButton.Click += new System.EventHandler(this.CancelOrderButton_Click);
            // 
            // ConfirmOrderButton
            // 
            this.ConfirmOrderButton.BackColor = System.Drawing.Color.GhostWhite;
            this.ConfirmOrderButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfirmOrderButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ConfirmOrderButton.Location = new System.Drawing.Point(273, 428);
            this.ConfirmOrderButton.Name = "ConfirmOrderButton";
            this.ConfirmOrderButton.Size = new System.Drawing.Size(147, 23);
            this.ConfirmOrderButton.TabIndex = 2;
            this.ConfirmOrderButton.Text = "Confirm Order";
            this.ConfirmOrderButton.UseVisualStyleBackColor = false;
            this.ConfirmOrderButton.Click += new System.EventHandler(this.ConfirmOrderButton_Click);
            // 
            // AuthorTextBox
            // 
            this.AuthorTextBox.Location = new System.Drawing.Point(160, 66);
            this.AuthorTextBox.MaxLength = 0;
            this.AuthorTextBox.Name = "AuthorTextBox";
            this.AuthorTextBox.ReadOnly = true;
            this.AuthorTextBox.Size = new System.Drawing.Size(204, 20);
            this.AuthorTextBox.TabIndex = 3;
            // 
            // TaxTextBox
            // 
            this.TaxTextBox.Location = new System.Drawing.Point(366, 392);
            this.TaxTextBox.Name = "TaxTextBox";
            this.TaxTextBox.ReadOnly = true;
            this.TaxTextBox.Size = new System.Drawing.Size(125, 20);
            this.TaxTextBox.TabIndex = 4;
            this.TaxTextBox.Text = "$0.00";
            // 
            // TotalTextBox
            // 
            this.TotalTextBox.Location = new System.Drawing.Point(541, 392);
            this.TotalTextBox.Name = "TotalTextBox";
            this.TotalTextBox.ReadOnly = true;
            this.TotalTextBox.Size = new System.Drawing.Size(128, 20);
            this.TotalTextBox.TabIndex = 5;
            this.TotalTextBox.Text = "$0.00";
            // 
            // SubtotalTextBox
            // 
            this.SubtotalTextBox.Location = new System.Drawing.Point(195, 392);
            this.SubtotalTextBox.Name = "SubtotalTextBox";
            this.SubtotalTextBox.ReadOnly = true;
            this.SubtotalTextBox.Size = new System.Drawing.Size(131, 20);
            this.SubtotalTextBox.TabIndex = 6;
            this.SubtotalTextBox.Text = "$0.00";
            // 
            // QuantityTextBox
            // 
            this.QuantityTextBox.Location = new System.Drawing.Point(380, 158);
            this.QuantityTextBox.MaxLength = 3;
            this.QuantityTextBox.Name = "QuantityTextBox";
            this.QuantityTextBox.Size = new System.Drawing.Size(91, 20);
            this.QuantityTextBox.TabIndex = 7;
            // 
            // PriceTextBox
            // 
            this.PriceTextBox.Location = new System.Drawing.Point(352, 107);
            this.PriceTextBox.MaxLength = 0;
            this.PriceTextBox.Name = "PriceTextBox";
            this.PriceTextBox.ReadOnly = true;
            this.PriceTextBox.Size = new System.Drawing.Size(156, 20);
            this.PriceTextBox.TabIndex = 8;
            // 
            // ISBNTextBox
            // 
            this.ISBNTextBox.Location = new System.Drawing.Point(513, 66);
            this.ISBNTextBox.MaxLength = 0;
            this.ISBNTextBox.Name = "ISBNTextBox";
            this.ISBNTextBox.ReadOnly = true;
            this.ISBNTextBox.Size = new System.Drawing.Size(188, 20);
            this.ISBNTextBox.TabIndex = 9;
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(114, 68);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(41, 13);
            this.AuthorLabel.TabIndex = 10;
            this.AuthorLabel.Text = "Author:";
            // 
            // ISBNLabel
            // 
            this.ISBNLabel.AutoSize = true;
            this.ISBNLabel.Location = new System.Drawing.Point(476, 68);
            this.ISBNLabel.Name = "ISBNLabel";
            this.ISBNLabel.Size = new System.Drawing.Size(35, 13);
            this.ISBNLabel.TabIndex = 11;
            this.ISBNLabel.Text = "ISBN:";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(312, 110);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(34, 13);
            this.PriceLabel.TabIndex = 12;
            this.PriceLabel.Text = "Price:";
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.AutoSize = true;
            this.QuantityLabel.Location = new System.Drawing.Point(402, 142);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(46, 13);
            this.QuantityLabel.TabIndex = 13;
            this.QuantityLabel.Text = "Quantity";
            // 
            // SubtotalLabel
            // 
            this.SubtotalLabel.AutoSize = true;
            this.SubtotalLabel.BackColor = System.Drawing.Color.FloralWhite;
            this.SubtotalLabel.Location = new System.Drawing.Point(140, 395);
            this.SubtotalLabel.Name = "SubtotalLabel";
            this.SubtotalLabel.Size = new System.Drawing.Size(49, 13);
            this.SubtotalLabel.TabIndex = 14;
            this.SubtotalLabel.Text = "Subtotal:";
            // 
            // TaxLabel
            // 
            this.TaxLabel.AutoSize = true;
            this.TaxLabel.Location = new System.Drawing.Point(332, 395);
            this.TaxLabel.Name = "TaxLabel";
            this.TaxLabel.Size = new System.Drawing.Size(28, 13);
            this.TaxLabel.TabIndex = 15;
            this.TaxLabel.Text = "Tax:";
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(501, 395);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(34, 13);
            this.TotalLabel.TabIndex = 16;
            this.TotalLabel.Text = "Total:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(362, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Order Summary";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderSummaryData
            // 
            this.OrderSummaryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrderSummaryData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Column1,
            this.QuantityColumn,
            this.LineTotalColumn});
            this.OrderSummaryData.Location = new System.Drawing.Point(160, 252);
            this.OrderSummaryData.Name = "OrderSummaryData";
            this.OrderSummaryData.ReadOnly = true;
            this.OrderSummaryData.Size = new System.Drawing.Size(542, 134);
            this.OrderSummaryData.TabIndex = 18;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Price";
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // QuantityColumn
            // 
            this.QuantityColumn.HeaderText = "QTY";
            this.QuantityColumn.Name = "QuantityColumn";
            // 
            // LineTotalColumn
            // 
            this.LineTotalColumn.HeaderText = "Line Total";
            this.LineTotalColumn.Name = "LineTotalColumn";
            this.LineTotalColumn.Width = 125;
            // 
            // BookSelectionBox
            // 
            this.BookSelectionBox.FormattingEnabled = true;
            this.BookSelectionBox.Location = new System.Drawing.Point(116, 25);
            this.BookSelectionBox.Name = "BookSelectionBox";
            this.BookSelectionBox.Size = new System.Drawing.Size(586, 21);
            this.BookSelectionBox.Sorted = true;
            this.BookSelectionBox.TabIndex = 19;
            this.BookSelectionBox.SelectedIndexChanged += new System.EventHandler(this.BookSelectionBox_SelectedIndexChanged);
            this.BookSelectionBox.Click += new System.EventHandler(this.BookSelectionBox_Click);
            // 
            // AddTitleButton
            // 
            this.AddTitleButton.BackColor = System.Drawing.Color.GhostWhite;
            this.AddTitleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddTitleButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddTitleButton.Location = new System.Drawing.Point(352, 194);
            this.AddTitleButton.Name = "AddTitleButton";
            this.AddTitleButton.Size = new System.Drawing.Size(156, 23);
            this.AddTitleButton.TabIndex = 0;
            this.AddTitleButton.Text = "Add Title";
            this.AddTitleButton.UseVisualStyleBackColor = false;
            this.AddTitleButton.Click += new System.EventHandler(this.AddTitleButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(917, 476);
            this.Controls.Add(this.BookSelectionBox);
            this.Controls.Add(this.OrderSummaryData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.TaxLabel);
            this.Controls.Add(this.SubtotalLabel);
            this.Controls.Add(this.QuantityLabel);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.ISBNLabel);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.ISBNTextBox);
            this.Controls.Add(this.PriceTextBox);
            this.Controls.Add(this.QuantityTextBox);
            this.Controls.Add(this.SubtotalTextBox);
            this.Controls.Add(this.TotalTextBox);
            this.Controls.Add(this.TaxTextBox);
            this.Controls.Add(this.AuthorTextBox);
            this.Controls.Add(this.ConfirmOrderButton);
            this.Controls.Add(this.CancelOrderButton);
            this.Controls.Add(this.AddTitleButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.OrderSummaryData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CancelOrderButton;
        private System.Windows.Forms.Button ConfirmOrderButton;
        private System.Windows.Forms.TextBox AuthorTextBox;
        private System.Windows.Forms.TextBox TaxTextBox;
        private System.Windows.Forms.TextBox TotalTextBox;
        private System.Windows.Forms.TextBox SubtotalTextBox;
        private System.Windows.Forms.TextBox QuantityTextBox;
        private System.Windows.Forms.TextBox PriceTextBox;
        private System.Windows.Forms.TextBox ISBNTextBox;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label ISBNLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label SubtotalLabel;
        private System.Windows.Forms.Label TaxLabel;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView OrderSummaryData;
        private System.Windows.Forms.ComboBox BookSelectionBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineTotalColumn;
        private System.Windows.Forms.Button AddTitleButton;
    }
}

