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
using System.Windows.Shapes;

namespace NGPlusPlusStar.Windows
{
    /// <summary>
    /// Interaction logic for VisualTreeWindow.xaml
    /// </summary>
    public partial class VisualTreeWindow : Window
    {
        public VisualTreeWindow()
        {
            InitializeComponent();
        }
        public void ShowVisualTree(DependencyObject element)
        {
            this.VisualTreeElements.Items.Clear();
            this.ProcessElement(element, null);
        }
        private void ProcessElement(DependencyObject element, TreeViewItem previousItem)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = element.GetType().Name;
            item.IsExpanded = true;

            if (previousItem == null)
            {
                this.VisualTreeElements.Items.Add(item);
            }
            else
            {
                previousItem.Items.Add(item);
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                this.ProcessElement(VisualTreeHelper.GetChild(element, i), item);
            }
        }
    }
}
