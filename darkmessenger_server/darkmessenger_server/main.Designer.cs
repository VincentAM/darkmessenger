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
            this.label1 = new System.Windows.Forms.Label();
            this.p_etat_server = new System.Windows.Forms.Panel();
            this.rtb_console = new System.Windows.Forms.RichTextBox();
            this.lb_clients = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bt_start_listen = new System.Windows.Forms.Button();
            this.bt_stop_listen = new System.Windows.Forms.Button();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(414, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "État du server :";
            // 
            // p_etat_server
            // 
            this.p_etat_server.BackColor = System.Drawing.Color.Red;
            this.p_etat_server.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p_etat_server.Location = new System.Drawing.Point(510, 9);
            this.p_etat_server.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.p_etat_server.Name = "p_etat_server";
            this.p_etat_server.Size = new System.Drawing.Size(16, 16);
            this.p_etat_server.TabIndex = 1;
            // 
            // rtb_console
            // 
            this.rtb_console.BackColor = System.Drawing.Color.White;
            this.rtb_console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_console.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtb_console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_console.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_console.Location = new System.Drawing.Point(5, 5);
            this.rtb_console.Name = "rtb_console";
            this.rtb_console.ReadOnly = true;
            this.rtb_console.Size = new System.Drawing.Size(373, 224);
            this.rtb_console.TabIndex = 2;
            this.rtb_console.Text = "";
            // 
            // lb_clients
            // 
            this.lb_clients.BackColor = System.Drawing.Color.White;
            this.lb_clients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb_clients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_clients.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_clients.FormattingEnabled = true;
            this.lb_clients.ItemHeight = 16;
            this.lb_clients.Location = new System.Drawing.Point(5, 5);
            this.lb_clients.Name = "lb_clients";
            this.lb_clients.Size = new System.Drawing.Size(108, 224);
            this.lb_clients.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(138, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Console :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Clients :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rtb_console);
            this.panel2.Location = new System.Drawing.Point(141, 53);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(385, 236);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lb_clients);
            this.panel3.Location = new System.Drawing.Point(12, 53);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(120, 236);
            this.panel3.TabIndex = 7;
            // 
            // bt_start_listen
            // 
            this.bt_start_listen.Location = new System.Drawing.Point(124, 6);
            this.bt_start_listen.Name = "bt_start_listen";
            this.bt_start_listen.Size = new System.Drawing.Size(64, 25);
            this.bt_start_listen.TabIndex = 8;
            this.bt_start_listen.Text = "Start";
            this.bt_start_listen.UseVisualStyleBackColor = true;
            this.bt_start_listen.Click += new System.EventHandler(this.bt_start_listen_Click);
            // 
            // bt_stop_listen
            // 
            this.bt_stop_listen.Enabled = false;
            this.bt_stop_listen.Location = new System.Drawing.Point(194, 6);
            this.bt_stop_listen.Name = "bt_stop_listen";
            this.bt_stop_listen.Size = new System.Drawing.Size(71, 25);
            this.bt_stop_listen.TabIndex = 9;
            this.bt_stop_listen.Text = "Stop";
            this.bt_stop_listen.UseVisualStyleBackColor = true;
            this.bt_stop_listen.Click += new System.EventHandler(this.bt_stop_listen_Click);
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(60, 9);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(58, 20);
            this.tb_port.TabIndex = 10;
            this.tb_port.Text = "9999";
            this.tb_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Port : ";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 301);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_port);
            this.Controls.Add(this.bt_stop_listen);
            this.Controls.Add(this.bt_start_listen);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.p_etat_server);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "main";
            this.Text = "DarkMessenger - Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel p_etat_server;
        private System.Windows.Forms.RichTextBox rtb_console;
        private System.Windows.Forms.ListBox lb_clients;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bt_start_listen;
        private System.Windows.Forms.Button bt_stop_listen;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label4;

    }
}

