// Updated by XamlIntelliSenseFileGenerator 19.05.2024 10:28:59
#pragma checksum "..\..\..\..\View\EditPage\CarCheckEdit.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "92F7B9B3952CABB7520F70C5AE45B2E6FCDF45C8513580D92CE020B8B4225B96"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using Warehouse.View.EditPage;


namespace Warehouse.View.EditPage
{


    /// <summary>
    /// CarCheckEdit
    /// </summary>
    public partial class CarCheckEdit : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Warehouse;component/view/editpage/carcheckedit.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\View\EditPage\CarCheckEdit.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Image imageControl;
        internal System.Windows.Controls.ComboBox DriverComboBox;
        internal System.Windows.Controls.ComboBox AccountComboBox;
        internal System.Windows.Controls.DatePicker Date;
        internal System.Windows.Controls.DatePicker AdmissionСarDate;
        internal System.Windows.Controls.TextBox DetergentBox;
        internal System.Windows.Controls.ComboBox CarTtemperatureBox;
        internal System.Windows.Controls.Button Return;
        internal System.Windows.Controls.Button ToSecondPage;
        internal System.Windows.Controls.Button BackToFirstPage;
        internal System.Windows.Controls.Button Confirm;
    }
}
