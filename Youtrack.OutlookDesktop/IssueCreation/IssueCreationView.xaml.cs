using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Input;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Expression.Interactivity.Media;
using System.Diagnostics;
using System.Windows.Automation;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Shell;
using Meissner.MicrosoftPlanner;

namespace Meissner.MicrosoftPlanner
{
    /// <summary>
    /// Interaktionslogik für IssueCreationView.xaml
    /// </summary>
    public partial class IssueCreationView
    {

        public IssueCreationView()
        {
            InitializeComponent();
        }

        public IIssueCreationViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
