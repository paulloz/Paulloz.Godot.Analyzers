//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paulloz.Godot.Analyzers.Resources {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Paulloz.Godot.Analyzers.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
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
        ///   Recherche une chaîne localisée semblable à Type Safety.
        /// </summary>
        internal static string CategoryTypeSafety {
            get {
                return ResourceManager.GetString("CategoryTypeSafety", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Usage.
        /// </summary>
        internal static string CategoryUsage {
            get {
                return ResourceManager.GetString("CategoryUsage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Only marshallable fields and properties can be exported..
        /// </summary>
        internal static string ExportIsMarshallableDiagnosticDescription {
            get {
                return ResourceManager.GetString("ExportIsMarshallableDiagnosticDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Type &apos;{0}&apos; is not marshallable and cannot be exported..
        /// </summary>
        internal static string ExportIsMarshallableDiagnosticMessageFormat {
            get {
                return ResourceManager.GetString("ExportIsMarshallableDiagnosticMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Export of unmarshallable type.
        /// </summary>
        internal static string ExportIsMarshallableDiagnosticTitle {
            get {
                return ResourceManager.GetString("ExportIsMarshallableDiagnosticTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à _.
        /// </summary>
        internal static string NeverAssignedSerializeFieldSuppressorJustification {
            get {
                return ResourceManager.GetString("NeverAssignedSerializeFieldSuppressorJustification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à  Usage of the generic form of GetNode is preferred for type safety..
        /// </summary>
        internal static string NonGenericGetComponentDiagnosticDescription {
            get {
                return ResourceManager.GetString("NonGenericGetComponentDiagnosticDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Method &apos;{0}&apos; has a preferred generic overload..
        /// </summary>
        internal static string NonGenericGetComponentDiagnosticMessageFormat {
            get {
                return ResourceManager.GetString("NonGenericGetComponentDiagnosticMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Usage of non generic GetNode.
        /// </summary>
        internal static string NonGenericGetNodeDiagnosticTitle {
            get {
                return ResourceManager.GetString("NonGenericGetNodeDiagnosticTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à _.
        /// </summary>
        internal static string ReadonlySerializeFieldSuppressorJustification {
            get {
                return ResourceManager.GetString("ReadonlySerializeFieldSuppressorJustification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à _.
        /// </summary>
        internal static string UnusedSerializeFieldSuppressorJustification {
            get {
                return ResourceManager.GetString("UnusedSerializeFieldSuppressorJustification", resourceCulture);
            }
        }
    }
}
