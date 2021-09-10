
namespace WSPTools
{
    partial class SpaceTag_Form
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
            this.SpaceListView = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSpaceNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSpaceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTagId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Spaces = new System.Windows.Forms.Label();
            this.EquipmentListView = new System.Windows.Forms.ListView();
            this.colTermId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFamily = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colParam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.TagsComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.colViewId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // SpaceListView
            // 
            this.SpaceListView.BackColor = System.Drawing.SystemColors.Info;
            this.SpaceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colSpaceNumber,
            this.colSpaceName,
            this.colLevel,
            this.colTagId,
            this.colViewId});
            this.SpaceListView.FullRowSelect = true;
            this.SpaceListView.GridLines = true;
            this.SpaceListView.HideSelection = false;
            this.SpaceListView.Location = new System.Drawing.Point(26, 108);
            this.SpaceListView.Name = "SpaceListView";
            this.SpaceListView.Size = new System.Drawing.Size(528, 320);
            this.SpaceListView.TabIndex = 0;
            this.SpaceListView.UseCompatibleStateImageBehavior = false;
            this.SpaceListView.View = System.Windows.Forms.View.Details;
            this.SpaceListView.SelectedIndexChanged += new System.EventHandler(this.SpaceListView_SelectedIndexChanged);
            // 
            // colId
            // 
            this.colId.Text = "Id";
            this.colId.Width = 70;
            // 
            // colSpaceNumber
            // 
            this.colSpaceNumber.Text = "Number";
            this.colSpaceNumber.Width = 70;
            // 
            // colSpaceName
            // 
            this.colSpaceName.Text = "Space Name";
            this.colSpaceName.Width = 100;
            // 
            // colLevel
            // 
            this.colLevel.Text = "Level Name";
            this.colLevel.Width = 120;
            // 
            // colTagId
            // 
            this.colTagId.Text = "TagId";
            this.colTagId.Width = 90;
            // 
            // Spaces
            // 
            this.Spaces.AutoSize = true;
            this.Spaces.Location = new System.Drawing.Point(26, 89);
            this.Spaces.Name = "Spaces";
            this.Spaces.Size = new System.Drawing.Size(43, 13);
            this.Spaces.TabIndex = 1;
            this.Spaces.Text = "Spaces";
            // 
            // EquipmentListView
            // 
            this.EquipmentListView.BackColor = System.Drawing.Color.Azure;
            this.EquipmentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTermId,
            this.colFamily,
            this.colType,
            this.colParam});
            this.EquipmentListView.GridLines = true;
            this.EquipmentListView.HideSelection = false;
            this.EquipmentListView.Location = new System.Drawing.Point(584, 111);
            this.EquipmentListView.Name = "EquipmentListView";
            this.EquipmentListView.Size = new System.Drawing.Size(387, 317);
            this.EquipmentListView.TabIndex = 2;
            this.EquipmentListView.UseCompatibleStateImageBehavior = false;
            this.EquipmentListView.View = System.Windows.Forms.View.Details;
            // 
            // colTermId
            // 
            this.colTermId.Text = "Id";
            // 
            // colFamily
            // 
            this.colFamily.Text = "FamilyName";
            this.colFamily.Width = 120;
            // 
            // colType
            // 
            this.colType.Text = "Type Name";
            this.colType.Width = 120;
            // 
            // colParam
            // 
            this.colParam.Text = "Air Flow";
            this.colParam.Width = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(581, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Equipment in Selected Space";
            // 
            // TagsComboBox
            // 
            this.TagsComboBox.FormattingEnabled = true;
            this.TagsComboBox.Location = new System.Drawing.Point(29, 46);
            this.TagsComboBox.Name = "TagsComboBox";
            this.TagsComboBox.Size = new System.Drawing.Size(189, 21);
            this.TagsComboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tags";
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(584, 458);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(165, 28);
            this.CreateButton.TabIndex = 6;
            this.CreateButton.Text = "Update Space Tags";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CloseButton.Location = new System.Drawing.Point(866, 458);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(105, 28);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(755, 458);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(105, 28);
            this.Cancel.TabIndex = 8;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Active View";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(231, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 20);
            this.textBox1.TabIndex = 10;
            // 
            // colViewId
            // 
            this.colViewId.Text = "View Id";
            this.colViewId.Width = 70;
            // 
            // SpaceTag_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 515);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TagsComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EquipmentListView);
            this.Controls.Add(this.Spaces);
            this.Controls.Add(this.SpaceListView);
            this.Name = "SpaceTag_Form";
            this.Text = "Space Tag Data [{0} : {1}]";
            this.Load += new System.EventHandler(this.SpaceTag_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView SpaceListView;
        private System.Windows.Forms.ColumnHeader colSpaceNumber;
        private System.Windows.Forms.ColumnHeader colSpaceName;
        private System.Windows.Forms.ColumnHeader colLevel;
        private System.Windows.Forms.Label Spaces;
        private System.Windows.Forms.ListView EquipmentListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colTermId;
        private System.Windows.Forms.ColumnHeader colFamily;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colParam;
        private System.Windows.Forms.ComboBox TagsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader colTagId;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ColumnHeader colViewId;
    }
}