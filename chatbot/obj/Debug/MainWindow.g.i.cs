﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5DA05FCFA98EF8CF3824D619BDBC91AE854C67D7E88B0E690FABA4F27731DB0E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace chatbot {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ChatHistoryTextBlock;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserInputTextBox;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox TaskListBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskTitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ReminderDatePicker;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTaskButton;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CompleteTaskButton;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteTaskButton;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartQuizButton;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ShowLogButton;
        
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
            System.Uri resourceLocater = new System.Uri("/chatbot;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.ChatHistoryTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.UserInputTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\MainWindow.xaml"
            this.UserInputTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.UserInputTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 51 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SendButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TaskListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.TaskTitleTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\MainWindow.xaml"
            this.TaskTitleTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TaskTitleTextBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TaskDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\MainWindow.xaml"
            this.TaskDescriptionTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TaskDescriptionTextBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ReminderDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.AddTaskButton = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\MainWindow.xaml"
            this.AddTaskButton.Click += new System.Windows.RoutedEventHandler(this.AddTaskButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CompleteTaskButton = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\MainWindow.xaml"
            this.CompleteTaskButton.Click += new System.Windows.RoutedEventHandler(this.CompleteTaskButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.DeleteTaskButton = ((System.Windows.Controls.Button)(target));
            
            #line 107 "..\..\MainWindow.xaml"
            this.DeleteTaskButton.Click += new System.Windows.RoutedEventHandler(this.DeleteTaskButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.StartQuizButton = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\MainWindow.xaml"
            this.StartQuizButton.Click += new System.Windows.RoutedEventHandler(this.StartQuizButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ShowLogButton = ((System.Windows.Controls.Button)(target));
            
            #line 111 "..\..\MainWindow.xaml"
            this.ShowLogButton.Click += new System.Windows.RoutedEventHandler(this.ShowLogButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

