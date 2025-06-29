using DataJuggler.UltimateHelper;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.IO;

namespace TemplateCompare
{
    public static class ReportWriter
    {
        private const int Margin = 40;
        private const int LineHeight = 20;
        private const int SectionSpacing = 30;
        private const int PageHeight = 960; // US Letter height in points
        private const int BottomBuffer = 80;

        public static bool WritePdfReport(TemplateManager manager, string outputPath, bool showOnlyDifferences)
        {
            // initial value
            bool written = false;

            // ensure the font resolver is set once
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new FontResolver("calibri");
            }

            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.Letter;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Font setup
                XFont boldFont = new XFont("calibri", 14, XFontStyleEx.Bold);
                XFont contentFont = new XFont("calibri", 14, XFontStyleEx.Regular);

                int y = Margin;

                // Column positions (Code first, Razor second)
                int xName = Margin;
                int xFound = xName + 140;
                int xEqual = xFound + 50;
                int xRazor = xEqual + 45;
                int xCode = xRazor + 140;

                foreach (var map in manager.Templates)
                {
                    int estimatedHeight = (map.Results.Count + 6) * LineHeight + SectionSpacing;

                    if (y + estimatedHeight > PageHeight - BottomBuffer)
                    {
                        page = document.AddPage();
                        page.Size = PdfSharp.PageSize.Letter;
                        gfx = XGraphics.FromPdfPage(page);
                        y = Margin;
                    }

                    // Section header
                    DrawLine(gfx, "Template Comparison Report", boldFont, ref y);
                    DrawLine(gfx, $"Component: {Path.GetFileName(map.ComponentPath)}", boldFont, ref y);
                    DrawLine(gfx, $"Template:  {Path.GetFileName(map.TemplatePath)}", boldFont, ref y);
                    DrawLine(gfx, " ", contentFont, ref y); // blank line for spacing

                    if (map.IsValid)
                    {
                        DrawLine(gfx, "This component is up to date!", contentFont, ref y);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            Properties.Resources.Smiley.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            ms.Position = 0;

                            XImage smiley = XImage.FromStream(ms);
                            gfx.DrawImage(smiley, Margin, y, 32, 32); // Draw at left margin
                            y += 40;
                        }
                    }
                    else
                    {
                        // Column headers
                        gfx.DrawString("Property", boldFont, XBrushes.Black, new XPoint(xName, y));
                        gfx.DrawString("Found", boldFont, XBrushes.Black, new XPoint(xFound, y));
                        gfx.DrawString("Equal", boldFont, XBrushes.Black, new XPoint(xEqual, y));
                        gfx.DrawString("Code Value", boldFont, XBrushes.Black, new XPoint(xCode, y));
                        gfx.DrawString("Razor Value", boldFont, XBrushes.Black, new XPoint(xRazor, y));
                        y += LineHeight;

                        // Comparison rows
                        foreach (var result in map.Results)
                        {
                            if (!showOnlyDifferences || !result.IsEqual || !result.Found)
                            {
                                gfx.DrawString(Trunc(result.Name, 40), contentFont, XBrushes.Black, new XPoint(xName, y));
                                gfx.DrawString(result.Found.ToString(), contentFont, XBrushes.Black, new XPoint(xFound, y));
                                gfx.DrawString(result.IsEqual.ToString(), contentFont, XBrushes.Black, new XPoint(xEqual, y));                                
                                gfx.DrawString(Trunc(result.CodeValue, 35), contentFont, XBrushes.Black, new XPoint(xCode, y));
                                gfx.DrawString(Trunc(result.RazorValue, 35), contentFont, XBrushes.Black, new XPoint(xRazor, y));                                

                                y += LineHeight;

                                if (y > PageHeight - BottomBuffer)
                                {
                                    page = document.AddPage();
                                    page.Size = PdfSharp.PageSize.Letter;
                                    gfx = XGraphics.FromPdfPage(page);
                                    y = Margin;
                                }
                            }
                        }

                        y += SectionSpacing;
                    }
                }

                document.Save(outputPath);
                written = File.Exists(outputPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error generating PDF: " + ex.Message);
            }

            return written;
        }

        private static int DrawLine(XGraphics gfx, string text, XFont font, ref int y)
        {
            gfx.DrawString(text, font, XBrushes.Black, new XRect(Margin, y, 612, LineHeight), XStringFormats.TopLeft);
            y += LineHeight;
            return y;
        }

        private static string Trunc(string value, int maxLength)
        {
            string truncatedValue = value;

            if (TextHelper.Exists(value) && value.Length > maxLength)
            {
                truncatedValue = value.Substring(0, maxLength - 3) + "...";
            }

            return truncatedValue;
        }
    }
}