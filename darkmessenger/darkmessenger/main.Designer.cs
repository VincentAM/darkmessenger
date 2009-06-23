namespace darkmessenger
{
    partial class main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_message = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_pseudo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_connexion = new System.Windows.Forms.Button();
            this.tb_adressip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtb_allmessage = new System.Windows.Forms.RichTextBox();
            this.lb_client = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_message
            // 
            this.tb_message.Enabled = false;
            this.tb_message.Location = new System.Drawing.Point(82, 4);
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(284, 20);
            this.tb_message.TabIndex = 1;
            this.tb_message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_message_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Adresse :";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tb_pseudo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.bt_connexion);
            this.panel1.Controls.Add(this.tb_adressip);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(7, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 32);
            this.panel1.TabIndex = 3;
            // 
            // tb_pseudo
            // 
            this.tb_pseudo.Location = new System.Drawing.Point(272, 6);
            this.tb_pseudo.Name = "tb_pseudo";
            this.tb_pseudo.Size = new System.Drawing.Size(104, 20);
            this.tb_pseudo.TabIndex = 6;
            this.tb_pseudo.Text = "Sleepy";
            this.tb_pseudo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_pseudo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(203, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pseudo :";
            // 
            // bt_connexion
            // 
            this.bt_connexion.Location = new System.Drawing.Point(387, 4);
            this.bt_connexion.Name = "bt_connexion";
            this.bt_connexion.Size = new System.Drawing.Size(127, 23);
            this.bt_connexion.TabIndex = 4;
            this.bt_connexion.Text = "Connexion";
            this.bt_connexion.UseVisualStyleBackColor = true;
            this.bt_connexion.Click += new System.EventHandler(this.bt_connexion_Click);
            // 
            // tb_adressip
            // 
            this.tb_adressip.Location = new System.Drawing.Point(70, 6);
            this.tb_adressip.Name = "tb_adressip";
            this.tb_adressip.Size = new System.Drawing.Size(127, 20);
            this.tb_adressip.TabIndex = 3;
            this.tb_adressip.Text = "192.168.0.3:9999";
            this.tb_adressip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_adressip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_adressip_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Message :";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rtb_allmessage);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tb_message);
            this.panel2.Location = new System.Drawing.Point(7, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 237);
            this.panel2.TabIndex = 5;
            // 
            // rtb_allmessage
            // 
            this.rtb_allmessage.Location = new System.Drawing.Point(6, 34);
            this.rtb_allmessage.Name = "rtb_allmessage";
            this.rtb_allmessage.ReadOnly = true;
            this.rtb_allmessage.Size = new System.Drawing.Size(360, 195);
            this.rtb_allmessage.TabIndex = 5;
            this.rtb_allmessage.Text = "";
            // 
            // lb_client
            // 
            this.lb_client.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_client.Enabled = false;
            this.lb_client.FormattingEnabled = true;
            this.lb_client.Location = new System.Drawing.Point(5, 5);
            this.lb_client.Name = "lb_client";
            this.lb_client.Size = new System.Drawing.Size(125, 225);
            this.lb_client.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lb_client);
            this.panel3.Location = new System.Drawing.Point(390, 46);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(137, 237);
            this.panel3.TabIndex = 7;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 288);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "main";
            this.Text = "DarkMessenger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bt_connexion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtb_allmessage;
        private System.Windows.Forms.ListBox lb_client;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tb_pseudo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_adressip;
    }
}

