﻿namespace RaceGame2
{
    partial class Game
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Speler1Ronde = new System.Windows.Forms.Label();
            this.Speler2Ronde = new System.Windows.Forms.Label();
            this.Speler1Speed = new System.Windows.Forms.Label();
            this.Speler2Speed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(88)))), ((int)(((byte)(64)))));
            this.pictureBox2.Location = new System.Drawing.Point(503, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(512, 724);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(88)))), ((int)(((byte)(64)))));
            this.pictureBox1.Location = new System.Drawing.Point(-9, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 724);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Speler1Ronde
            // 
            this.Speler1Ronde.AutoSize = true;
            this.Speler1Ronde.BackColor = System.Drawing.Color.Black;
            this.Speler1Ronde.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Speler1Ronde.ForeColor = System.Drawing.Color.White;
            this.Speler1Ronde.Location = new System.Drawing.Point(0, 26);
            this.Speler1Ronde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Speler1Ronde.Name = "Speler1Ronde";
            this.Speler1Ronde.Size = new System.Drawing.Size(104, 36);
            this.Speler1Ronde.TabIndex = 2;
            this.Speler1Ronde.Text = "Ronde";
            // 
            // Speler2Ronde
            // 
            this.Speler2Ronde.AutoSize = true;
            this.Speler2Ronde.BackColor = System.Drawing.Color.Black;
            this.Speler2Ronde.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Speler2Ronde.ForeColor = System.Drawing.Color.White;
            this.Speler2Ronde.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Speler2Ronde.Location = new System.Drawing.Point(854, 26);
            this.Speler2Ronde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Speler2Ronde.Name = "Speler2Ronde";
            this.Speler2Ronde.Size = new System.Drawing.Size(104, 36);
            this.Speler2Ronde.TabIndex = 3;
            this.Speler2Ronde.Text = "Ronde";
            // 
            // Speler1Speed
            // 
            this.Speler1Speed.AutoSize = true;
            this.Speler1Speed.BackColor = System.Drawing.Color.Black;
            this.Speler1Speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Speler1Speed.ForeColor = System.Drawing.Color.White;
            this.Speler1Speed.Location = new System.Drawing.Point(0, 478);
            this.Speler1Speed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Speler1Speed.Name = "Speler1Speed";
            this.Speler1Speed.Size = new System.Drawing.Size(132, 36);
            this.Speler1Speed.TabIndex = 4;
            this.Speler1Speed.Text = "Snelheid";
            // 
            // Speler2Speed
            // 
            this.Speler2Speed.AutoSize = true;
            this.Speler2Speed.BackColor = System.Drawing.Color.Black;
            this.Speler2Speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Speler2Speed.ForeColor = System.Drawing.Color.White;
            this.Speler2Speed.Location = new System.Drawing.Point(826, 478);
            this.Speler2Speed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Speler2Speed.Name = "Speler2Speed";
            this.Speler2Speed.Size = new System.Drawing.Size(132, 36);
            this.Speler2Speed.TabIndex = 5;
            this.Speler2Speed.Text = "Snelheid";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(958, 548);
            this.Controls.Add(this.Speler2Speed);
            this.Controls.Add(this.Speler1Speed);
            this.Controls.Add(this.Speler2Ronde);
            this.Controls.Add(this.Speler1Ronde);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Game";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Game_FormClosed);
            this.Load += new System.EventHandler(this.Game_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label Speler1Ronde;
        private System.Windows.Forms.Label Speler2Ronde;
        private System.Windows.Forms.Label Speler1Speed;
        private System.Windows.Forms.Label Speler2Speed;
    }
}

