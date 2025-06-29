

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.UltimateHelper;

#endregion

namespace TemplateCompare
{

    #region class TemplateManager
    /// <summary>
    /// This class creates the TemplateMaps then starts the Comparison
    /// </summary>
    public class TemplateManager
    {
        
        #region Private Variables
        private string componentsFolder;
        private List<TemplateMap> templates;
        private string templatesFolder;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'TemplateManager' object.
        /// </summary>
        public TemplateManager(string templatesFolder, string componentsFolder)
        {
            // store
            TemplatesFolder = templatesFolder;
            ComponentsFolder = componentsFolder;
        }
        #endregion
        
        #region Events
            
        #endregion
        
        #region Methods

            #region Compare()
            /// <summary>
            /// Compare
            /// </summary>
            public void Compare()
            {
                // If the Templates collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(Templates))
                {
                    // Iterate the collection of TemplateMap objects
                    foreach (TemplateMap template in Templates)
                    {
                        // Compare
                        template.Compare();
                    }
                }
            }
            #endregion
            
            #region MapTemplates()
            /// <summary>
            /// Map Templates
            /// </summary>
            public void MapTemplates()
            {
                // locals
                string componentPath = "";
                TemplateMap templateMap = null;

                // We must parse the GlobalDefaults first
                string globalDefaultsPath = Path.Combine(ComponentsFolder, "GlobalDefaults.cs");

                // Parse the GlobalDefaultProperteis
                List<ComponentProperty> globalDefaults = TemplateMap.ParseGlobalDefaultProperties(globalDefaultsPath);

                // Create a new collection of 'TemplateMap' objects.
                Templates = new List<TemplateMap>();

                // If both folders are set
                if ((HasComponentsFolder) && (HasTemplatesFolder))
                {
                    // Get the files
                    List<string> files = FileHelper.GetFiles(TemplatesFolder, ".txt");

                    // If the files collection exists and has one or more items
                    if (ListHelper.HasOneOrMoreItems(files))
                    {
                        // Iterate the collection of string objects
                        foreach (string filePath in files)
                        {
                            // reset
                            componentPath = "";

                            // Create a new instance of a 'FileInfo' object.
                            FileInfo file = new FileInfo(filePath);

                            // determine the action by the name
                            switch (file.Name)
                            {
                                case "CalendarComponent.txt":
                                case "CalendarSlim.txt":

                                    // Maps to the CalendarComponent
                                    componentPath = Path.Combine(ComponentsFolder, "CalendarComponent.razor.cs");

                                    // If the componentPath Exists On Disk
                                    if (FileHelper.Exists(componentPath))
                                    {
                                        // Create a TemplateMap
                                        templateMap = new TemplateMap(filePath, componentPath, globalDefaults);

                                        // Add this item
                                        Templates.Add(templateMap);
                                    }

                                    // required
                                    break;

                                case "CheckBoxComponent.txt":
                                case "CheckBoxSlim.txt":

                                    // Maps to the CheckBoxComponent
                                    componentPath = Path.Combine(ComponentsFolder, "CheckBoxComponent.razor.cs");

                                    if (FileHelper.Exists(componentPath))
                                    {
                                        templateMap = new TemplateMap(filePath, componentPath, globalDefaults);
                                        Templates.Add(templateMap);
                                    }

                                    break;

                                case "CheckedListBoxComponent.txt":
                                case "CheckedListBoxSlim.txt":

                                    // Maps to the CheckedListBoxComponent
                                    componentPath = Path.Combine(ComponentsFolder, "CheckedListBox.razor.cs");

                                    if (FileHelper.Exists(componentPath))
                                    {
                                        templateMap = new TemplateMap(filePath, componentPath, globalDefaults);
                                        Templates.Add(templateMap);
                                    }

                                    break;

                                case "ComboBoxCheckListComponent.txt":
                                case "ComboBoxCheckListSlim.txt":
                                case "ComboBoxComponent.txt":
                                case "ComboBoxSlim.txt":

                                    // Maps to the ComboBoxCheckListComponent
                                    componentPath = Path.Combine(ComponentsFolder, "ComboBox.razor.cs");

                                    if (FileHelper.Exists(componentPath))
                                    {
                                        templateMap = new TemplateMap(filePath, componentPath, globalDefaults);
                                        Templates.Add(templateMap);
                                    }

                                    break;

                                case "TextBoxComponent.txt":
                                case "TextBoxSlim.txt":

                                    // Maps to the TextBoxComponent
                                    componentPath = Path.Combine(ComponentsFolder, "TextBoxComponent.razor.cs");

                                    if (FileHelper.Exists(componentPath))
                                    {
                                        templateMap = new TemplateMap(filePath, componentPath, globalDefaults);
                                        Templates.Add(templateMap);
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region ComponentsFolder
            /// <summary>
            /// This property gets or sets the value for 'ComponentsFolder'.
            /// </summary>
            public string ComponentsFolder
            {
                get { return componentsFolder; }
                set { componentsFolder = value; }
            }
            #endregion
            
            #region HasComponentsFolder
            /// <summary>
            /// This property returns true if the 'ComponentsFolder' exists.
            /// </summary>
            public bool HasComponentsFolder
            {
                get
                {
                    // initial value
                    bool hasComponentsFolder = (!String.IsNullOrEmpty(ComponentsFolder));

                    // return value
                    return hasComponentsFolder;
                }
            }
            #endregion
            
            #region HasTemplatesFolder
            /// <summary>
            /// This property returns true if the 'TemplatesFolder' exists.
            /// </summary>
            public bool HasTemplatesFolder
            {
                get
                {
                    // initial value
                    bool hasTemplatesFolder = (!String.IsNullOrEmpty(TemplatesFolder));

                    // return value
                    return hasTemplatesFolder;
                }
            }
            #endregion
            
            #region Templates
            /// <summary>
            /// This property gets or sets the value for 'Templates'.
            /// </summary>
            public List<TemplateMap> Templates
            {
                get { return templates; }
                set { templates = value; }
            }
            #endregion
                       
            #region TemplatesFolder
            /// <summary>
            /// This property gets or sets the value for 'TemplatesFolder'.
            /// </summary>
            public string TemplatesFolder
            {
                get { return templatesFolder; }
                set { templatesFolder = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
