﻿namespace PCI
{
    partial class PCISearcher
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PCIListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // PCIListBox
            // 
            this.PCIListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PCIListBox.FormattingEnabled = true;
            this.PCIListBox.ItemHeight = 16;
            this.PCIListBox.Location = new System.Drawing.Point(0, 0);
            this.PCIListBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PCIListBox.Name = "PCIListBox";
            this.PCIListBox.Size = new System.Drawing.Size(1343, 401);
            this.PCIListBox.TabIndex = 0;
            // 
            // PCISearcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 401);
            this.Controls.Add(this.PCIListBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PCISearcher";
            this.Text = "PCISearcher";
            this.Load += new System.EventHandler(this.PCISearcher_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PCIListBox;
    }
}

