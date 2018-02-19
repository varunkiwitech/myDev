﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kauffman.Api.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ApiResponse {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApiResponse() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Kauffman.Api.Resources.ApiResponse", typeof(ApiResponse).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to Fetch User Info.
        /// </summary>
        public static string FethcUserInfoFailed {
            get {
                return ResourceManager.GetString("FethcUserInfoFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please use correct email format.
        /// </summary>
        public static string IncorrectEmail {
            get {
                return ResourceManager.GetString("IncorrectEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal Server Error.
        /// </summary>
        public static string InternalServerError {
            get {
                return ResourceManager.GetString("InternalServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot be blank.
        /// </summary>
        public static string InvalidSignUpInfo {
            get {
                return ResourceManager.GetString("InvalidSignUpInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User Registration Successful.
        /// </summary>
        public static string RegisterationSuccess {
            get {
                return ResourceManager.GetString("RegisterationSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User Registeration Failed.
        /// </summary>
        public static string RegistrationFailed {
            get {
                return ResourceManager.GetString("RegistrationFailed", resourceCulture);
            }
        }
    }
}
