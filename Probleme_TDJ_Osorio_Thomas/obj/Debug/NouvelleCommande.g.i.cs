﻿#pragma checksum "..\..\NouvelleCommande.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0FAFE93CF330CBBB89A9A13AEAF8F501C9D169A6CF85A10D53C5FF1A9EAF757F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Probleme_TDJ_Osorio_Thomas;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Probleme_TDJ_Osorio_Thomas {
    
    
    /// <summary>
    /// NouvelleCommande
    /// </summary>
    public partial class NouvelleCommande : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Facture;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Pizza;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Boisson;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Commis;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoisson;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSlider1;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider Slider1;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\NouvelleCommande.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider Slider2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Probleme_TDJ_Osorio_Thomas;component/nouvellecommande.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NouvelleCommande.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\NouvelleCommande.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Nouvelle_Pizza);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 11 "..\..\NouvelleCommande.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Nouvelle_Boisson);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Facture = ((System.Windows.Controls.ListView)(target));
            
            #line 12 "..\..\NouvelleCommande.xaml"
            this.Facture.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Facture_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 23 "..\..\NouvelleCommande.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Retirer);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Pizza = ((System.Windows.Controls.ListView)(target));
            
            #line 24 "..\..\NouvelleCommande.xaml"
            this.Pizza.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Pizza_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Boisson = ((System.Windows.Controls.ListView)(target));
            
            #line 31 "..\..\NouvelleCommande.xaml"
            this.Boisson.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Boisson_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 38 "..\..\NouvelleCommande.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Envoyer);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Commis = ((System.Windows.Controls.ComboBox)(target));
            
            #line 39 "..\..\NouvelleCommande.xaml"
            this.Commis.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Commis_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtBoisson = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.txtSlider1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.Slider1 = ((System.Windows.Controls.Slider)(target));
            
            #line 52 "..\..\NouvelleCommande.xaml"
            this.Slider1.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Slider2 = ((System.Windows.Controls.Slider)(target));
            
            #line 53 "..\..\NouvelleCommande.xaml"
            this.Slider2.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider2_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

