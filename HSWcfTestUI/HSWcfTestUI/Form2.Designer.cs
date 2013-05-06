namespace HSWcfTestUI
{
    partial class Form2
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
            this.cbx_idadeCrianca = new System.Windows.Forms.ComboBox();
            this.lst_idadeCrianca = new System.Windows.Forms.ListBox();
            this.btn_adicionar = new System.Windows.Forms.Button();
            this.btn_remover = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbx_idadeCrianca
            // 
            this.cbx_idadeCrianca.FormattingEnabled = true;
            this.cbx_idadeCrianca.Location = new System.Drawing.Point(24, 22);
            this.cbx_idadeCrianca.Name = "cbx_idadeCrianca";
            this.cbx_idadeCrianca.Size = new System.Drawing.Size(121, 21);
            this.cbx_idadeCrianca.TabIndex = 0;
            // 
            // lst_idadeCrianca
            // 
            this.lst_idadeCrianca.FormattingEnabled = true;
            this.lst_idadeCrianca.Location = new System.Drawing.Point(24, 66);
            this.lst_idadeCrianca.Name = "lst_idadeCrianca";
            this.lst_idadeCrianca.Size = new System.Drawing.Size(120, 95);
            this.lst_idadeCrianca.TabIndex = 1;
            // 
            // btn_adicionar
            // 
            this.btn_adicionar.Location = new System.Drawing.Point(179, 22);
            this.btn_adicionar.Name = "btn_adicionar";
            this.btn_adicionar.Size = new System.Drawing.Size(75, 23);
            this.btn_adicionar.TabIndex = 2;
            this.btn_adicionar.Text = "Adicionar";
            this.btn_adicionar.UseVisualStyleBackColor = true;
            
            // 
            // btn_remover
            // 
            this.btn_remover.Location = new System.Drawing.Point(179, 51);
            this.btn_remover.Name = "btn_remover";
            this.btn_remover.Size = new System.Drawing.Size(75, 23);
            this.btn_remover.TabIndex = 3;
            this.btn_remover.Text = "Remover";
            this.btn_remover.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 193);
            this.Controls.Add(this.btn_remover);
            this.Controls.Add(this.btn_adicionar);
            this.Controls.Add(this.lst_idadeCrianca);
            this.Controls.Add(this.cbx_idadeCrianca);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbx_idadeCrianca;
        private System.Windows.Forms.ListBox lst_idadeCrianca;
        private System.Windows.Forms.Button btn_adicionar;
        private System.Windows.Forms.Button btn_remover;

    }
}