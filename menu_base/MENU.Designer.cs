
namespace menu_base
{
    partial class MENU
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
            this.panel_menu = new System.Windows.Forms.Panel();
            this.panel_menu_view = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_selection = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_option_name = new System.Windows.Forms.Label();
            this.panel_title = new System.Windows.Forms.Panel();
            this.label_menu_title = new System.Windows.Forms.Label();
            this.panel_menu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_title.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_menu
            // 
            this.panel_menu.Controls.Add(this.panel_menu_view);
            this.panel_menu.Controls.Add(this.panel2);
            this.panel_menu.Controls.Add(this.panel1);
            this.panel_menu.Controls.Add(this.panel_title);
            this.panel_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_menu.Location = new System.Drawing.Point(0, 0);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Padding = new System.Windows.Forms.Padding(3);
            this.panel_menu.Size = new System.Drawing.Size(284, 540);
            this.panel_menu.TabIndex = 0;
            this.panel_menu.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_menu_Paint);
            // 
            // panel_menu_view
            // 
            this.panel_menu_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_menu_view.Location = new System.Drawing.Point(3, 133);
            this.panel_menu_view.Name = "panel_menu_view";
            this.panel_menu_view.Size = new System.Drawing.Size(278, 374);
            this.panel_menu_view.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_selection);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 507);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 30);
            this.panel2.TabIndex = 3;
            // 
            // label_selection
            // 
            this.label_selection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_selection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_selection.ForeColor = System.Drawing.Color.Red;
            this.label_selection.Location = new System.Drawing.Point(0, 0);
            this.label_selection.Name = "label_selection";
            this.label_selection.Size = new System.Drawing.Size(278, 30);
            this.label_selection.TabIndex = 0;
            this.label_selection.Text = "0/0";
            this.label_selection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_option_name);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 50);
            this.panel1.TabIndex = 2;
            // 
            // label_option_name
            // 
            this.label_option_name.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_option_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_option_name.ForeColor = System.Drawing.Color.Red;
            this.label_option_name.Location = new System.Drawing.Point(0, 0);
            this.label_option_name.Name = "label_option_name";
            this.label_option_name.Size = new System.Drawing.Size(278, 50);
            this.label_option_name.TabIndex = 6;
            this.label_option_name.Text = "Main";
            this.label_option_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_title
            // 
            this.panel_title.Controls.Add(this.label_menu_title);
            this.panel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_title.Location = new System.Drawing.Point(3, 3);
            this.panel_title.Name = "panel_title";
            this.panel_title.Size = new System.Drawing.Size(278, 80);
            this.panel_title.TabIndex = 0;
            // 
            // label_menu_title
            // 
            this.label_menu_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_menu_title.Location = new System.Drawing.Point(0, 0);
            this.label_menu_title.Name = "label_menu_title";
            this.label_menu_title.Size = new System.Drawing.Size(278, 80);
            this.label_menu_title.TabIndex = 0;
            this.label_menu_title.Text = "TITLE";
            this.label_menu_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MENU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 540);
            this.Controls.Add(this.panel_menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MENU";
            this.Load += new System.EventHandler(this.menu_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MENU_Paint);
            this.panel_menu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel_title.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.Panel panel_title;
        private System.Windows.Forms.Label label_menu_title;
        private System.Windows.Forms.Panel panel_menu_view;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_option_name;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_selection;
    }
}

