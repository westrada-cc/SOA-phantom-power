﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GIORP_TOTAL.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("localhost")]
        public string SOARegistryIp {
            get {
                return ((string)(this["SOARegistryIp"]));
            }
            set {
                this["SOARegistryIp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3128")]
        public int SOARegistryPort {
            get {
                return ((int)(this["SOARegistryPort"]));
            }
            set {
                this["SOARegistryPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("localhost")]
        public string ServicePublishIp {
            get {
                return ((string)(this["ServicePublishIp"]));
            }
            set {
                this["ServicePublishIp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15000")]
        public int ServicePublishPort {
            get {
                return ((int)(this["ServicePublishPort"]));
            }
            set {
                this["ServicePublishPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Phantom Power")]
        public string TeamName {
            get {
                return ((string)(this["TeamName"]));
            }
            set {
                this["TeamName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("PP-GIORP-TOTAL")]
        public string ServiceName {
            get {
                return ((string)(this["ServiceName"]));
            }
            set {
                this["ServiceName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("GIORP-TOTAL")]
        public string ServiceTagName {
            get {
                return ((string)(this["ServiceTagName"]));
            }
            set {
                this["ServiceTagName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string ServiceSecurityLevel {
            get {
                return ((string)(this["ServiceSecurityLevel"]));
            }
            set {
                this["ServiceSecurityLevel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Calculates taxes, net and total amount for certian Canadian province and amount.")]
        public string ServiceDescription {
            get {
                return ((string)(this["ServiceDescription"]));
            }
            set {
                this["ServiceDescription"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShouldValidateTeam {
            get {
                return ((bool)(this["ShouldValidateTeam"]));
            }
            set {
                this["ShouldValidateTeam"] = value;
            }
        }
    }
}
