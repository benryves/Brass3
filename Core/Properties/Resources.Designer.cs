﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Core.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 	&lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        internal static string HtmlListFooter {
            get {
                return ResourceManager.GetString("HtmlListFooter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!doctype&gt;
        ///&lt;html&gt;
        ///	&lt;head&gt;
        ///		&lt;style&gt;
        ///			* {
        ///				font-size: 8pt;
        ///			}
        ///			pre.source {
        ///				padding: 0px;
        ///				margin: 0px;
        ///				font-family: consolas, &quot;lucida console&quot;, monospace;
        ///			}
        ///			pre.source span.comment {
        ///				color: #060;
        ///			}
        ///			pre.source span.string {
        ///				color: #900;
        ///			}
        ///			pre.source span.directive {
        ///				color: #009;
        ///			}
        ///			pre.source span.function {
        ///				color: #069;
        ///			}
        ///			pre.source span.label {
        ///				color: #663;
        ///			}
        ///			pre.source.disabled, pre.source.disabled * {
        ///	 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HtmlListHeader {
            get {
                return ResourceManager.GetString("HtmlListHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap VeraDoc_Icon_Error_PNG {
            get {
                object obj = ResourceManager.GetObject("VeraDoc_Icon_Error_PNG", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap VeraDoc_Icon_File_PNG {
            get {
                object obj = ResourceManager.GetObject("VeraDoc_Icon_File_PNG", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap VeraDoc_Icon_Folder_PNG {
            get {
                object obj = ResourceManager.GetObject("VeraDoc_Icon_Folder_PNG", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; ?&gt;
        ///&lt;!DOCTYPE html PUBLIC &quot;-//W3C//DTD XHTML 1.0 Frameset//EN&quot; &quot;XHTML1-f.dtd&quot; &gt;
        ///&lt;html xmlns=&quot;http://www.w3.org/TR/1999/REC-html-in-xml&quot; xml:lang=&quot;en&quot; lang=&quot;en&quot; &gt;
        ///
        ///	&lt;head&gt;
        ///		&lt;title&gt;Vera Source Documentation&lt;/title&gt;
        ///	&lt;/head&gt;
        ///	
        ///	&lt;frameset cols=&quot;220px, *&quot;&gt;
        ///		&lt;frame src =&quot;tree.xml&quot; /&gt;
        ///		&lt;frame src =&quot;$(main)&quot; name=&quot;file&quot;/&gt;
        ///	&lt;/frameset&gt;
        ///
        ///&lt;/html&gt;
        ///.
        /// </summary>
        internal static string VeraDoc_Index_HTML {
            get {
                return ResourceManager.GetString("VeraDoc_Index_HTML", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /**
        /// * Page
        /// */
        ///
        ///html,body {
        ///	width: 100%;
        ///	padding: 0px;
        ///	margin: 0px;
        ///}
        ///
        ////**
        /// * Header
        /// */
        ///
        ///h1 {
        ///	font: bold 13px verdana,Tahoma,Arial,sans-serif;
        ///	border-bottom: 2px dotted black;
        ///	color: #003399;
        ///	background-color: #B5C3FF;
        ///	border-bottom: 1px solid #A8ADBE;
        ///	padding: 10px;
        ///	margin: 0px;
        ///	
        ///}
        ///
        ////**
        /// * Sub header (File description, Routines etc)
        /// */
        ///
        ///h2 {
        ///	font: bold 12px verdana,Tahoma,Arial,sans-serif;
        ///	color: #003399;
        ///	margin: 25px 0px 20px 0px;
        ///	background-color: #D4DFFF;
        ///	padding: 5px;
        ///	padding-left:  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string VeraDoc_VeraDoc_CSS {
            get {
                return ResourceManager.GetString("VeraDoc_VeraDoc_CSS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;ISO-8859-1&quot;?&gt;
        ///&lt;xsl:stylesheet version=&quot;1.0&quot;
        ///xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot;&gt;
        ///
        ///&lt;xsl:template match=&quot;/file&quot;&gt;
        ///	&lt;html&gt;
        ///	&lt;head&gt;
        ///		&lt;title&gt;Documentation of &lt;xsl:value-of select=&quot;name&quot;/&gt;&lt;/title&gt;
        ///		&lt;link href=&quot;veradoc.css&quot; rel=&quot;stylesheet&quot; type=&quot;text/css&quot;/&gt;
        ///	&lt;/head&gt;
        ///	&lt;body&gt;
        ///		&lt;h1&gt;Documentation of source file &lt;xsl:value-of select=&quot;name&quot;/&gt;&lt;/h1&gt;
        ///		&lt;xsl:if test=&quot;description/p&quot;&gt;
        ///			&lt;h2&gt;File description&lt;/h2&gt;
        ///			&lt;xsl:for-each select=&quot;description/p&quot;&gt;
        ///				&lt;p&gt;&lt;xsl:value-of select [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string VeraDoc_VeraDoc_XSL {
            get {
                return ResourceManager.GetString("VeraDoc_VeraDoc_XSL", resourceCulture);
            }
        }
    }
}
