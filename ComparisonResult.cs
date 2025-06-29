

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace TemplateCompare
{

    #region class ComparisonResult
    /// <summary>
    /// This class is used to compare the results of two properties with the same name
    /// </summary>
    public class ComparisonResult
    {
        
        #region Private Variables
        private string codeValue;
        private bool found;
        private string name;
        private string razorValue;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'ComparisonResult' object.
        /// </summary>
        public ComparisonResult(string name, string razorValue, string codeValue)
        {
            // store
            Name = name;
            RazorValue = razorValue;
            CodeValue = codeValue;            
        }
        #endregion

        #region Events

        #endregion

        #region Methods

            #region ToString()
            /// <summary>
            /// method returns the String
            /// </summary>
            public override string ToString()
            {
                return Name + " Found = " + Found + " IsEqual = " + IsEqual;
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region CodeValue
            /// <summary>
            /// This property gets or sets the value for 'CodeValue'.
            /// </summary>
            public string CodeValue
            {
                get { return codeValue; }
                set { codeValue = value; }
            }
            #endregion
            
            #region Found
            /// <summary>
            /// This property gets or sets the value for 'Found'.
            /// </summary>
            public bool Found
            {
                get { return found; }
                set { found = value; }
            }
            #endregion
            
            #region IsEqual
            /// <summary>
            /// This read only property returns true if the ComponentValue matches the TemplateValue
            /// </summary>
            public bool IsEqual
            {

                get
                {
                    // initial value
                    bool isEqual = RazorValue.ToLower().Trim() == CodeValue.ToLower().Trim();

                    // return value
                    return isEqual;
                }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion
            
            #region RazorValue
            /// <summary>
            /// This property gets or sets the value for 'RazorValue'.
            /// </summary>
            public string RazorValue
            {
                get { return razorValue; }
                set { razorValue = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
