

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.UltimateHelper;
using DataJuggler.UltimateHelper.Objects;

#endregion

namespace TemplateCompare
{

    #region class TemplateMap
    /// <summary>
    /// This class is used to map a Template to a A BlazorComponent.
    /// </summary>
    public class TemplateMap
    {
        
        #region Private Variables        
        private string componentPath;        
        private List<ComponentProperty> templateProperties;
        private List<ComponentProperty> codeProperties;
        private List<ComponentProperty> globalDefaultsProperties;
        private List<ComparisonResult> results;
        private List<string> ignoreList;
        private string templatePath;
        private const string InitMethodDeclaration = "public void Init()";
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a TemplateMap object
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="componentPath"></param>
        public TemplateMap(string templatePath, string componentPath, List<ComponentProperty> globalDefaults)
        {
            // store the args
            TemplatePath = templatePath;
            ComponentPath = componentPath;
            GlobalDefaultsProperties = globalDefaults;

            // Create
            TemplateProperties = new List<ComponentProperty>();
            CodeProperties = new List<ComponentProperty>();

            // Create a list of field names to ignore
            IgnoreList = new List<string>();

            // Ignore these fields
            IgnoreList.Add("Name");
            IgnoreList.Add("Parent");
            IgnoreList.Add("Caption");
            IgnoreList.Add("LabelColor");
            IgnoreList.Add("ButtonBorderColor");
        }
        #endregion
        
        #region Methods
            
            #region Compare()
            /// <summary>
            /// This method compares the CodeProperties verse the RazorProperties to produce a report
            /// </summary>
            public void Compare()
            {
                // Create a new collection of 'ComparisonResult' objects.
                Results = new List<ComparisonResult>();

                // locals
                string codeValue = "";
                bool found = false;
                bool skip = false;

                // Parse all the properties from the Razor component
                ParseTemplateProperties();

                // Parse the properties from the component.razor.cs
                ParseCodeProperties();

                // If the TemplateProperties collection and the CodeProperties collection both exist and each have one or more items
                if (ListHelper.HasOneOrMoreItems(TemplateProperties, CodeProperties))
                {
                    // Iterate the collection of ComponentProperty objects
                    foreach (ComponentProperty property in TemplateProperties)
                    {
                        // reset
                        skip = false;

                        // Attempt to find the matching property
                        ComponentProperty codeProperty = CodeProperties.FirstOrDefault(x => x.Name == property.Name);

                        // set the value
                        if (NullHelper.Exists(codeProperty))
                        {
                            // Set the value
                            codeValue = codeProperty.Value;

                            // was found
                            found = true;
                        }
                        else
                        {
                            // erase
                            codeValue = "";

                            // was not found
                            found = false;
                        }

                        // Check if this property is in the ignore list
                        if (HasIgnoreList)
                        {   
                            // set skip to true
                            skip = IgnoreList.Contains(property.Name);
                        }
                        
                        // if the value for skip is false
                        if (!skip)
                        {
                            // Create the result
                            ComparisonResult result = new ComparisonResult(property.Name, property.Value, codeValue);
                            result.Found = found;

                            // if not equal
                            if (!result.IsEqual)
                            {
                                // if this is a GlobalDefaults Value
                                if ((codeValue.Contains("GlobalDefaults")) && (HasGlobalDefaultsProperties))
                                {
                                    // Find the value
                                    ComponentProperty globalDefaultValue = GlobalDefaultsProperties.FirstOrDefault(x => x.Name == property.Name);

                                    // If the globalDefaultValue object exists
                                    if (NullHelper.Exists(globalDefaultValue))
                                    {
                                        // recreate
                                        result = new ComparisonResult(property.Name, property.Value, globalDefaultValue.Value);
                                        result.Found = found;
                                    }
                                }
                            }

                            // Add this result
                            Results.Add(result);
                        }
                    }
                }
            }
            #endregion
            
            #region GetInitMethodLines()
            /// <summary>
            /// returns a list of Init Method Lines
            /// </summary>
            public List<TextLine> GetInitMethodLines()
            {
                // initial value
                List<TextLine> lines = new List<TextLine>();

                List<TextLine> tempLines = TextHelper.GetTextLinesFromFile(ComponentPath);

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(tempLines))
                {
                    // Get the index
                    int index = tempLines.FirstOrDefault(x => x.Text.Contains(InitMethodDeclaration)).Index;

                    // if the line was found
                    if (index > 0)
                    {
                        // iterate the lines
                        for(int x = index; x < tempLines.Count;x++)
                        {
                            // if the endregion was encountered
                            if (tempLines[x].Text.Contains("#endregion"))
                            {
                                // break out of loop
                                break;
                            }
                            else
                            {
                                // Add this line
                                lines.Add(tempLines[x]);
                            }
                        }
                    }
                }

                // return value
                return lines;
            }
            #endregion
            
            #region ParseCodeProperties()
            /// <summary>
            /// Parse Code Properties
            /// </summary>
            public void ParseCodeProperties()
            {
                // recreate
                CodeProperties = new List<ComponentProperty>();

                // Get the lines that make up the Init method
                List<TextLine> lines = GetInitMethodLines();

                // Get the words
                List<Word> words = new List<Word>();

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // if this is not a blank line
                        if (TextHelper.Exists(line.Text.Trim()))
                        {
                            // if this is not a comment
                            if (!line.Text.Trim().StartsWith("//"))
                            {
                                // parse only on space
                                char[] delimiter = new char[] { '=' };

                                // Get the words on either side of the equal sign
                                List<Word> tempWords = TextHelper.GetWords(line.Text, delimiter);

                                // if ther are exacly two words
                                if ((ListHelper.HasXOrMoreItems(tempWords, 2) && (tempWords.Count == 2)))
                                {
                                    // Set the name value pair
                                    string propertyName = tempWords[0].Text.Trim();
                                    string propertyValue = tempWords[1].Text.Trim();

                                    // If the strings propertyName and propertyValue both exist
                                    if (TextHelper.Exists(propertyName, propertyValue))
                                    {
                                        // Create the property
                                        ComponentProperty property = new ComponentProperty();

                                         // Set the name
                                        property.Name = tempWords[0].Text.Trim();
                                        property.Value = tempWords[1].Text.Replace("\"", "").Replace(";","").Trim();                                        

                                        // Now trim out any comments out of value
                                        property.Value = StripComments(property.Value);

                                        // Add this property
                                        CodeProperties.Add(property);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region ParseGlobalDefaultProperties(string globalDefaultsPath)
            /// <summary>
            /// Parse Global Default Properties
            /// </summary>
            public static List<ComponentProperty> ParseGlobalDefaultProperties(string globalDefaultsPath)
            {
                // Create a new collection of 'ComponentProperty' objects.
                List<ComponentProperty> properties = new List<ComponentProperty>();

                // Load the TestLines from Path
                List<TextLine> lines = TextHelper.GetTextLinesFromFile(globalDefaultsPath);

                // parse on equal sign
                char[] delimiter = new char[] { '=' };

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // if this is a line with values
                        if (line.Text.Contains("="))
                        {
                            // Get the words on either side of the equal sign
                            List<Word> tempWords = TextHelper.GetWords(line.Text, delimiter);

                            // if ther are exacly two words
                            if ((ListHelper.HasXOrMoreItems(tempWords, 2) && (tempWords.Count == 2)))
                            {
                                // Set the name value pair
                                string propertyName = tempWords[0].Text.Trim();
                                string propertyValue = tempWords[1].Text.Trim();

                                // If the strings propertyName and propertyValue both exist
                                if (TextHelper.Exists(propertyName, propertyValue))
                                {
                                    // Create the property
                                    ComponentProperty property = new ComponentProperty();

                                        // Set the name
                                    property.Name = tempWords[0].Text.Replace("public const", "").Replace("string", "").Replace("int","").Replace("double","").Trim();
                                    property.Value = tempWords[1].Text.Replace("\"", "").Replace(";","").Trim();

                                    // Add this property
                                    properties.Add(property);
                                }
                            }
                        }
                    }
                }

                // return value
                return properties;
            }
            #endregion
            
            #region ParseTemplateProperties()
            /// <summary>
            /// Parse the Razor Template to parse out the words
            /// </summary>
            public void ParseTemplateProperties()
            {
                TemplateProperties = new List<ComponentProperty>();

                List<TextLine> lines = TextHelper.GetTextLinesFromFile(TemplatePath);
                char[] spaceDelimiter = new char[] { ' ' };

                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    List<Word> words = new List<Word>();

                    foreach (TextLine line in lines)
                    {
                        List<Word> lineWords = TextHelper.GetWords(line.Text, spaceDelimiter);

                        if (ListHelper.HasOneOrMoreItems(lineWords))
                        {
                            for (int x = 0; x < lineWords.Count; x++)
                            {
                                Word word = lineWords[x];

                                if (word.Text.Contains("<"))
                                {
                                    continue;
                                }

                                if (x == lineWords.Count - 1)
                                {
                                    word.Text = word.Text.Replace(">", "").Trim();
                                }

                                words.Add(word);
                            }
                        }
                    }

                    ComponentProperty currentProperty = null;

                    foreach (Word word in words)
                    {
                        if (word.Text.Contains("="))
                        {
                            List<Word> parts = TextHelper.GetWords(word.Text, new char[] { '=' });

                            if (parts.Count >= 2)
                            {
                                currentProperty = new ComponentProperty
                                {
                                    Name = parts[0].Text.Trim(),
                                    Value = parts[1].Text.Replace("\"", "").Trim()
                                };

                                TemplateProperties.Add(currentProperty);
                            }
                        }
                        else
                        {
                            if (currentProperty != null && currentProperty.Name.EndsWith("ClassName"))
                            {
                                string extra = word.Text.Replace("\"", "").Trim();

                                if (!string.IsNullOrWhiteSpace(extra))
                                {
                                    currentProperty.Value += " " + extra;
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region StripComments(string source)
            /// <summary>
            /// returns the Comments
            /// </summary>
            public string StripComments(string source)
            {
                // initial value
                string stripped = source;

                // If the source string exists
                if (TextHelper.Exists(source))
                {
                    // Get the index
                    int index = source.IndexOf("//");
                    
                    // if the index was found
                    if (index > 0)
                    {
                        // Set the return value
                        stripped = source.Substring(0, index -1).Trim();
                    }
                }

                // return value
                return stripped;
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region CodeProperties
            /// <summary>
            /// This property gets or sets the value for 'CodeProperties'.
            /// </summary>
            public List<ComponentProperty> CodeProperties
            {
                get { return codeProperties; }
                set { codeProperties = value; }
            }
            #endregion
            
            #region ComponentPath
            /// <summary>
            /// This property gets or sets the value for 'ComponentPath'.
            /// </summary>
            public string ComponentPath
            {
                get { return componentPath; }
                set { componentPath = value; }
            }
            #endregion
                     
            #region GlobalDefaultsProperties
            /// <summary>
            /// This property gets or sets the value for 'GlobalDefaultsProperties'.
            /// </summary>
            public List<ComponentProperty> GlobalDefaultsProperties
            {
                get { return globalDefaultsProperties; }
                set { globalDefaultsProperties = value; }
            }
            #endregion
            
            #region HasCodeProperties
            /// <summary>
            /// This property returns true if this object has a 'CodeProperties'.
            /// </summary>
            public bool HasCodeProperties
            {
                get
                {
                    // initial value
                    bool hasCodeProperties = (CodeProperties != null);

                    // return value
                    return hasCodeProperties;
                }
            }
            #endregion
            
            #region HasGlobalDefaultsProperties
            /// <summary>
            /// This property returns true if this object has a 'GlobalDefaultsProperties'.
            /// </summary>
            public bool HasGlobalDefaultsProperties
            {
                get
                {
                    // initial value
                    bool hasGlobalDefaultsProperties = (GlobalDefaultsProperties != null);

                    // return value
                    return hasGlobalDefaultsProperties;
                }
            }

            #region HasIgnoreList
            /// <summary>
            /// This property returns true if this object has an 'IgnoreList'.
            /// </summary>
            public bool HasIgnoreList
            {
                get
                {
                    // initial value
                    bool hasIgnoreList = (IgnoreList != null);

                    // return value
                    return hasIgnoreList;
                }
            }
            #endregion

            #region HasTemplateProperties
            /// <summary>
            /// This property returns true if this object has a 'TemplateProperties'.
            /// </summary>
            public bool HasTemplateProperties
            {
                get
                {
                    // initial value
                    bool hasTemplateProperties = (TemplateProperties != null);

                    // return value
                    return hasTemplateProperties;
                }
            }
            #endregion

            #region IgnoreList
            /// <summary>
            /// This property gets or sets the value for 'IgnoreList'.
            /// </summary>
            public List<string> IgnoreList
            {
                get { return ignoreList; }
                set { ignoreList = value; }
            }
            #endregion
            
            #region TemplateProperties
            /// <summary>
            /// This property gets or sets the value for 'TemplateProperties'.
            /// </summary>
            public List<ComponentProperty> TemplateProperties
            {
                get { return templateProperties; }
                set { templateProperties = value; }
            }
            #endregion
            
            #region Results
            /// <summary>
            /// This property gets or sets the value for 'Results'.
            /// </summary>
            public List<ComparisonResult> Results
            {
                get { return results; }
                set { results = value; }
            }
            #endregion
            
            #region TemplatePath
            /// <summary>
            /// This property gets or sets the value for 'TemplatePath'.
            /// </summary>
            public string TemplatePath
            {
                get { return templatePath; }
                set { templatePath = value; }
            }
            #endregion
            
        #endregion
        
            #region IsValid
            /// <summary>
            /// This read only property returns true if there are no Results with Found = false or Equal = false
            /// </summary>
            public bool IsValid
            {
                get
                {
                    // initial value
                    bool isValid = false;

                    // if Results exists
                    if (Results != null)
                    {
                        ComparisonResult result = Results.FirstOrDefault(x => !x.Found);
                        ComparisonResult result2 = Results.FirstOrDefault(x => !x.IsEqual);

                        // If both of these are null this is valid
                        isValid = (NullHelper.IsNull(result)) && (NullHelper.IsNull(result2));
                    }

                    // return value
                    return isValid;
                }
            }
            #endregion

        #endregion
        
    }
    #endregion

}
