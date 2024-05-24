﻿#pragma checksum "..\..\..\..\View\AddPage\ProductAdd.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "88887D07DB9DD28DEAAB83C7A373A65576091615510DA7B4378F078A5CB84D0A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using Warehouse.View.AddPage;


namespace Warehouse.View.AddPage {
    
    
    /// <summary>
    /// ProductAdd
    /// </summary>
    public partial class ProductAdd : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageControl;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductTitleBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ProductTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductCost;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ProductDescriptionBox;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox OrderSuitabilityComboBox;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Confirm;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\View\AddPage\ProductAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Return;
        
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
            System.Uri resourceLocater = new System.Uri("/Warehouse;component/view/addpage/productadd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\AddPage\ProductAdd.xaml"
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
            this.imageControl = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.ProductTitleBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ProductTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.ProductCost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ProductDescriptionBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.OrderSuitabilityComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.Confirm = ((System.Windows.Controls.Button)(target));
            
            #line 121 "..\..\..\..\View\AddPage\ProductAdd.xaml"
            this.Confirm.Click += new System.Windows.RoutedEventHandler(this.Confirm_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Return = ((System.Windows.Controls.Button)(target));
            
            #line 138 "..\..\..\..\View\AddPage\ProductAdd.xaml"
            this.Return.Click += new System.Windows.RoutedEventHandler(this.Return_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

