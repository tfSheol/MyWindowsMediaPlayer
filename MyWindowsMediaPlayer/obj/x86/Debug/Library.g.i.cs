﻿#pragma checksum "..\..\..\Library.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3009184FE7957C04804B9B118630DB8C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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


namespace MyWindowsMediaPlayer {
    
    
    /// <summary>
    /// Library
    /// </summary>
    public partial class Library : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Library.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView libList;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Library.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button musics;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Library.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button movies;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Library.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pictures;
        
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
            System.Uri resourceLocater = new System.Uri("/MyWindowsMediaPlayer;component/library.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Library.xaml"
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
            this.libList = ((System.Windows.Controls.ListView)(target));
            
            #line 6 "..\..\..\Library.xaml"
            this.libList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Select_Element);
            
            #line default
            #line hidden
            return;
            case 2:
            this.musics = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Library.xaml"
            this.musics.Click += new System.Windows.RoutedEventHandler(this.Click_Musics);
            
            #line default
            #line hidden
            return;
            case 3:
            this.movies = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Library.xaml"
            this.movies.Click += new System.Windows.RoutedEventHandler(this.Click_Movies);
            
            #line default
            #line hidden
            return;
            case 4:
            this.pictures = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Library.xaml"
            this.pictures.Click += new System.Windows.RoutedEventHandler(this.Click_Pictures);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

