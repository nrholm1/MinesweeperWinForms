namespace MinesweeperPart2
{
    partial class GameResultWindow
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
            this.GameResultMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameResultMessage
            // 
            this.GameResultMessage.AutoSize = true;
            this.GameResultMessage.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameResultMessage.Location = new System.Drawing.Point(183, 108);
            this.GameResultMessage.Name = "GameResultMessage";
            this.GameResultMessage.Size = new System.Drawing.Size(0, 28);
            this.GameResultMessage.TabIndex = 0;
            this.GameResultMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameResultWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.GameResultMessage);
            this.Name = "GameResultWindow";
            this.Text = "Game Result";
            this.Load += new System.EventHandler(this.GameResultWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameResultMessage;
    }
}