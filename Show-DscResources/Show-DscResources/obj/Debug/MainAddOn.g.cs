﻿#pragma checksum "..\..\MainAddOn.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D7BC30FA4604049F3BD2ECCB31337761"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Show_DscResources {
    
    
    /// <summary>
    /// DscResourceAddOn
    /// </summary>
    public partial class DscResourceAddOn : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\MainAddOn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HelpButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\MainAddOn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridObject;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\MainAddOn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView DscSampleCreatorTree;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\MainAddOn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InsertSampleButton;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\MainAddOn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CopySampleButton;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\MainAddOn.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridNameValue;
        
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
            System.Uri resourceLocater = new System.Uri("/Show-DscResources;component/mainaddon.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainAddOn.xaml"
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
            
            #line 23 "..\..\MainAddOn.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonRefresh_OnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.HelpButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\MainAddOn.xaml"
            this.HelpButton.Click += new System.Windows.RoutedEventHandler(this.HelpButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DataGridObject = ((System.Windows.Controls.DataGrid)(target));
            
            #line 33 "..\..\MainAddOn.xaml"
            this.DataGridObject.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DataGrid_OnMouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainAddOn.xaml"
            this.DataGridObject.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DataGrid_OnMouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DscSampleCreatorTree = ((System.Windows.Controls.TreeView)(target));
            return;
            case 5:
            this.InsertSampleButton = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\MainAddOn.xaml"
            this.InsertSampleButton.Click += new System.Windows.RoutedEventHandler(this.InsertSampleButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CopySampleButton = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\MainAddOn.xaml"
            this.CopySampleButton.Click += new System.Windows.RoutedEventHandler(this.CopySampleButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DataGridNameValue = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

