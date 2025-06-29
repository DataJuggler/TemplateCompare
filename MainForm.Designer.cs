namespace TemplateCompare
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            CompareButton = new DataJuggler.Win.Controls.Button();
            TemplateFolderControl = new DataJuggler.Win.Controls.LabelTextBoxBrowserControl();
            ComponentsFolderControl = new DataJuggler.Win.Controls.LabelTextBoxBrowserControl();
            Graph = new DataJuggler.Win.Controls.ProgressBar();
            StatusLabel = new Label();
            ShowOnlyDifferencesCheckBox = new DataJuggler.Win.Controls.LabelCheckBoxControl();
            SuspendLayout();
            // 
            // CompareButton
            // 
            CompareButton.BackColor = Color.Transparent;
            CompareButton.ButtonText = "Compare";
            CompareButton.FlatStyle = FlatStyle.Flat;
            CompareButton.ForeColor = Color.LemonChiffon;
            CompareButton.Location = new Point(628, 156);
            CompareButton.Margin = new Padding(5);
            CompareButton.Name = "CompareButton";
            CompareButton.Size = new Size(116, 55);
            CompareButton.TabIndex = 0;
            CompareButton.Theme = DataJuggler.Win.Controls.Enumerations.ThemeEnum.Dark;
            CompareButton.UseMnemonic = true;
            CompareButton.Click += CompareButton_Click;
            // 
            // TemplateFolderControl
            // 
            TemplateFolderControl.BackColor = Color.Transparent;
            TemplateFolderControl.BrowseType = DataJuggler.Win.Controls.Enumerations.BrowseTypeEnum.Folder;
            TemplateFolderControl.ButtonColor = Color.LemonChiffon;
            TemplateFolderControl.ButtonImage = (Image)resources.GetObject("TemplateFolderControl.ButtonImage");
            TemplateFolderControl.ButtonWidth = 48;
            TemplateFolderControl.DarkMode = false;
            TemplateFolderControl.DisabledLabelColor = Color.Empty;
            TemplateFolderControl.Editable = true;
            TemplateFolderControl.EnabledLabelColor = Color.Empty;
            TemplateFolderControl.Filter = null;
            TemplateFolderControl.Font = new Font("Calibri", 16F);
            TemplateFolderControl.HideBrowseButton = false;
            TemplateFolderControl.LabelBottomMargin = 0;
            TemplateFolderControl.LabelColor = SystemColors.ControlText;
            TemplateFolderControl.LabelFont = new Font("Calibri", 16F, FontStyle.Bold);
            TemplateFolderControl.LabelText = "Template Folder:";
            TemplateFolderControl.LabelTopMargin = 0;
            TemplateFolderControl.LabelWidth = 200;
            TemplateFolderControl.Location = new Point(33, 31);
            TemplateFolderControl.Name = "TemplateFolderControl";
            TemplateFolderControl.OnTextChangedListener = null;
            TemplateFolderControl.OpenCallback = null;
            TemplateFolderControl.ScrollBars = ScrollBars.None;
            TemplateFolderControl.SelectedPath = null;
            TemplateFolderControl.Size = new Size(711, 32);
            TemplateFolderControl.StartPath = null;
            TemplateFolderControl.TabIndex = 1;
            TemplateFolderControl.TextBoxBottomMargin = 0;
            TemplateFolderControl.TextBoxDisabledColor = Color.Empty;
            TemplateFolderControl.TextBoxEditableColor = Color.Empty;
            TemplateFolderControl.TextBoxFont = new Font("Calibri", 16F);
            TemplateFolderControl.TextBoxTopMargin = 0;
            TemplateFolderControl.Theme = DataJuggler.Win.Controls.Enumerations.ThemeEnum.Glass;
            // 
            // ComponentsFolderControl
            // 
            ComponentsFolderControl.BackColor = Color.Transparent;
            ComponentsFolderControl.BrowseType = DataJuggler.Win.Controls.Enumerations.BrowseTypeEnum.Folder;
            ComponentsFolderControl.ButtonColor = Color.LemonChiffon;
            ComponentsFolderControl.ButtonImage = (Image)resources.GetObject("ComponentsFolderControl.ButtonImage");
            ComponentsFolderControl.ButtonWidth = 48;
            ComponentsFolderControl.DarkMode = false;
            ComponentsFolderControl.DisabledLabelColor = Color.Empty;
            ComponentsFolderControl.Editable = true;
            ComponentsFolderControl.EnabledLabelColor = Color.Empty;
            ComponentsFolderControl.Filter = null;
            ComponentsFolderControl.Font = new Font("Calibri", 16F);
            ComponentsFolderControl.HideBrowseButton = false;
            ComponentsFolderControl.LabelBottomMargin = 0;
            ComponentsFolderControl.LabelColor = SystemColors.ControlText;
            ComponentsFolderControl.LabelFont = new Font("Calibri", 16F, FontStyle.Bold);
            ComponentsFolderControl.LabelText = "Component Folder:";
            ComponentsFolderControl.LabelTopMargin = 0;
            ComponentsFolderControl.LabelWidth = 200;
            ComponentsFolderControl.Location = new Point(33, 85);
            ComponentsFolderControl.Name = "ComponentsFolderControl";
            ComponentsFolderControl.OnTextChangedListener = null;
            ComponentsFolderControl.OpenCallback = null;
            ComponentsFolderControl.ScrollBars = ScrollBars.None;
            ComponentsFolderControl.SelectedPath = null;
            ComponentsFolderControl.Size = new Size(711, 32);
            ComponentsFolderControl.StartPath = null;
            ComponentsFolderControl.TabIndex = 2;
            ComponentsFolderControl.TextBoxBottomMargin = 0;
            ComponentsFolderControl.TextBoxDisabledColor = Color.Empty;
            ComponentsFolderControl.TextBoxEditableColor = Color.Empty;
            ComponentsFolderControl.TextBoxFont = new Font("Calibri", 16F);
            ComponentsFolderControl.TextBoxTopMargin = 0;
            ComponentsFolderControl.Theme = DataJuggler.Win.Controls.Enumerations.ThemeEnum.Glass;
            // 
            // Graph
            // 
            Graph.BackColor = Color.DarkGray;
            Graph.BackgroundColor = Color.DarkGray;
            Graph.BorderStyle = BorderStyle.FixedSingle;
            Graph.ForeColor = Color.DodgerBlue;
            Graph.Location = new Point(34, 393);
            Graph.Maximum = 100;
            Graph.Name = "Graph";
            Graph.SetOverflowToMax = true;
            Graph.Size = new Size(732, 22);
            Graph.TabIndex = 3;
            Graph.Value = 0;
            Graph.Visible = false;
            // 
            // StatusLabel
            // 
            StatusLabel.ForeColor = Color.LemonChiffon;
            StatusLabel.Location = new Point(32, 361);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(728, 27);
            StatusLabel.TabIndex = 4;
            StatusLabel.Text = "Status:";
            StatusLabel.Visible = false;
            // 
            // ShowOnlyDifferencesCheckBox
            // 
            ShowOnlyDifferencesCheckBox.BackColor = Color.Transparent;
            ShowOnlyDifferencesCheckBox.CheckBoxHorizontalOffSet = 0;
            ShowOnlyDifferencesCheckBox.CheckBoxVerticalOffSet = 4;
            ShowOnlyDifferencesCheckBox.CheckChangedListener = null;
            ShowOnlyDifferencesCheckBox.Checked = true;
            ShowOnlyDifferencesCheckBox.Editable = true;
            ShowOnlyDifferencesCheckBox.Font = new Font("Calibri", 16F);
            ShowOnlyDifferencesCheckBox.LabelColor = Color.LemonChiffon;
            ShowOnlyDifferencesCheckBox.LabelFont = new Font("Calibri", 16F, FontStyle.Bold);
            ShowOnlyDifferencesCheckBox.LabelText = "Only Show Differences:";
            ShowOnlyDifferencesCheckBox.LabelWidth = 260;
            ShowOnlyDifferencesCheckBox.Location = new Point(302, 169);
            ShowOnlyDifferencesCheckBox.Name = "ShowOnlyDifferencesCheckBox";
            ShowOnlyDifferencesCheckBox.Size = new Size(295, 28);
            ShowOnlyDifferencesCheckBox.TabIndex = 5;
            ShowOnlyDifferencesCheckBox.Theme = DataJuggler.Win.Controls.Enumerations.ThemeEnum.Dark;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Controls.Add(ShowOnlyDifferencesCheckBox);
            Controls.Add(StatusLabel);
            Controls.Add(Graph);
            Controls.Add(ComponentsFolderControl);
            Controls.Add(TemplateFolderControl);
            Controls.Add(CompareButton);
            Font = new Font("Calibri", 16F);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private DataJuggler.Win.Controls.Button CompareButton;
        private DataJuggler.Win.Controls.LabelTextBoxBrowserControl TemplateFolderControl;
        private DataJuggler.Win.Controls.LabelTextBoxBrowserControl ComponentsFolderControl;
        private DataJuggler.Win.Controls.ProgressBar Graph;
        private Label StatusLabel;
        private DataJuggler.Win.Controls.LabelCheckBoxControl ShowOnlyDifferencesCheckBox;
    }
}
