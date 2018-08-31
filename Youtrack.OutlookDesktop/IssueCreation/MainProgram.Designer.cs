//using Youtrack.Common;

using System.Windows.Forms;

namespace Meissner.MicrosoftPlanner
{
    partial class MainProgram
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button.AllowDrop = true;
            this.button.BackgroundImage = global::Meissner.MicrosoftPlanner.Properties.Resources.shinchan1;
            this.button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button.Location = new System.Drawing.Point(21, -1);
            this.button.Name = "button1";
            this.button.Size = new System.Drawing.Size(76, 69);
            this.button.TabIndex = 0;
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button1_Click);
            this.button.DragDrop += new System.Windows.Forms.DragEventHandler(this.button_DragDrop);
            this.button.DragEnter += new System.Windows.Forms.DragEventHandler(this.button_DragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 71);
            this.Controls.Add(this.button);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(136, 110);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(136, 110);
            this.Name = "Form1";
            this.Opacity = 0.7D;
            this.ShowIcon = false;
            this.Text = "Planner";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button;
    }
}

