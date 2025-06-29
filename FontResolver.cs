

#region using statements

using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace TemplateCompare
{

    #region class FontResolver
    /// <summary>
    /// This class is used to create a FontResolver because PDFSharp doesn't include one
    /// </summary>
    public class FontResolver : IFontResolver
    {
        
        #region Private Variables
        private readonly Dictionary<string, string> fontMap;
        private readonly string fontFolder;
        private readonly string baseFontName;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'FontResolver' object.
        /// </summary>
        public FontResolver(string fontName, string fontFolder = @"C:\Windows\Fonts")
        {
            this.baseFontName = fontName.ToLower();
            this.fontFolder = fontFolder;

            // Define mapping of styles to TTF filenames
            fontMap = new Dictionary<string, string>
            {
                { "regular", $"{baseFontName}.ttf" },
                { "bold", $"{baseFontName}b.ttf" },
                { "italic", $"{baseFontName}i.ttf" },
                { "bolditalic", $"{baseFontName}z.ttf" } // adjust if needed for actual font file naming
            };
        }
        #endregion
        
        #region Events
            
        #endregion
        
        #region Methods
            
            #region GetFont(string faceName)
            /// <summary>
            /// method Get Font
            /// </summary>
            public byte[] GetFont(string faceName)
            {
                // initial value
                byte[] fontBytes = null;

                // if the face name starts with the base font name
                if (faceName.StartsWith(baseFontName))
                {
                    // get the style key (e.g. "bold", "italic", etc.)
                    string styleKey = faceName.Split('#')[1];

                    // if the fontMap contains the style key
                    if (fontMap.TryGetValue(styleKey, out var fileName))
                    {
                        // build the full path to the .ttf file
                        string path = Path.Combine(fontFolder, fileName);

                        // if the font file exists
                        if (File.Exists(path))
                        {
                            // load the font bytes
                            fontBytes = File.ReadAllBytes(path);
                        }
                        else
                        {
                            // throw file not found exception
                            throw new FileNotFoundException($"Font file not found: {path}");
                        }
                    }
                }

                // return value
                return fontBytes;
            }
            #endregion
            
            #region ResolveTypeface(string familyName, bool isBold, bool isItalic)
            /// <summary>
            /// method Resolve Typeface
            /// </summary>
            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                // only resolve our registered font
                if (familyName.ToLower() == baseFontName)
                {
                    string styleKey = isBold && isItalic ? "bolditalic"
                    : isBold ? "bold"
                    : isItalic ? "italic"
                    : "regular";

                    string faceId = $"{baseFontName}#{styleKey}";
                    return new FontResolverInfo(faceId);
                }

                // fallback to default platform font resolver
                return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
            }
            #endregion
            
        #endregion
        
        #region Properties
            
        #endregion
        
    }
    #endregion

}
