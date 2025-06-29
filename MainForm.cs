

#region using statements

using DataJuggler.UltimateHelper;

#endregion

namespace TemplateCompare
{

    #region class MainForm
    /// <summary>
    /// This class is the Main Form for this app
    /// </summary>
    public partial class MainForm : Form
    {

        #region Private Variables

        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'MainForm' object.
        /// </summary>
        public MainForm()
        {
            // Create Controls
            InitializeComponent();

            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Events

            #region CompareButton_Click(object sender, EventArgs e)
            /// <summary>
            /// event is fired when the 'CompareButton' is clicked.
            /// </summary>
            private void CompareButton_Click(object sender, EventArgs e)
            {
                // verify both folders exist                
                if ((FolderHelper.Exists(ComponentsFolderControl.Text)) && (FolderHelper.Exists(TemplateFolderControl.Text)))
                {
                    // Create a new instance of a 'TemplateManager' object.
                    TemplateManager templateManager = new TemplateManager(TemplateFolderControl.Text, ComponentsFolderControl.Text);     

                    // Map the templates
                    templateManager.MapTemplates();

                    // Compare the Templates
                    templateManager.Compare();

                    // set the outPath
                    string outputPath = @"C:\Temp\ComparisongReport.pdf"; 

                    // Add some random characters to make it unique
                    outputPath = FileHelper.CreateFileNameWithPartialGuid(outputPath, 12);

                    // Write the Report
                    bool generated = ReportWriter.WritePdfReport(templateManager, outputPath, ShowOnlyDifferencesCheckBox.Checked);
                    
                    // if generated
                    if (generated)
                    {
                        // Create a StartInfo to launch the file
                        var startInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = outputPath,
                            UseShellExecute = true,
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                        };

                        // Start the process
                        System.Diagnostics.Process.Start(startInfo);
                    }
                    else
                    {
                        // Show a message for now
                        MessageBox.Show("Houston We Have A Problem", "Oops");
                    }
                }
            }
            #endregion
        
        #endregion

        #region Methods

            #region Init()
            /// <summary>
            ///  This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Defaults for faster testing - my machine only, remove this or change it
                TemplateFolderControl.Text = @"C:\Projects\GitHub\Regionizer2022\Regionizer\Regionizer\Templates";
                ComponentsFolderControl.Text = @"C:\Projects\GitHub\DataJuggler.Blazor.Components";

                // For some reason the colors are changing
                TemplateFolderControl.LabelColor = Color.LemonChiffon;
                ComponentsFolderControl.LabelColor = Color.LemonChiffon;
            }
            #endregion
            
        #endregion

        #region Properties

        #endregion
    }
    #endregion

}
