

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace TemplateCompare
{

    #region class ComponentProperty
    /// <summary>
    /// This class represents one Property and its value
    /// </summary>
    public class ComponentProperty
    {
        
        #region Private Variables
        private string name;
        private string _value;
        #endregion

        #region Events

        #endregion

        #region Methods

            #region ToString()
            /// <summary>
            /// method returns the Name = Value when ToString is called
            /// </summary>
            public override string ToString()
            {
                return "Name: " + Name + " = " + Value;
            }
            #endregion
            
        #endregion
        
        #region Properties
            
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
            
            #region Value
            /// <summary>
            /// This property gets or sets the value for 'Value'.
            /// </summary>
            public string Value
            {
                get { return _value; }
                set 
                {
                    _value = value;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
