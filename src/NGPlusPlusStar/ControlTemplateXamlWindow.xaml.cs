using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace NGPlusPlusStar.Windows
{
    /// <summary>
    /// Interaction logic for ControlTemplateXamlWindow.xaml
    /// </summary>
    public partial class ControlTemplateXamlWindow : Window
    {
        public ControlTemplateXamlWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var controlType = typeof(Control);
            var controls = new List<Type>();
            foreach (var t in Assembly.GetAssembly(controlType).GetTypes())
            {
                if (t.IsSubclassOf(controlType) && !t.IsAbstract && t.IsPublic)
                    controls.Add(t);
            }
            this.Controls.ItemsSource = controls;
        }

        private void Controls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var controlType = (Type)this.Controls.SelectedItem;
            var constructor = controlType.GetConstructors().First();
            var control = (Control)constructor.Invoke(null);
            control.Visibility = Visibility.Collapsed;
            this.Grid.Children.Add(control);

            var xaml = new StringBuilder();
            XamlWriter.Save(control.Template, XmlWriter.Create(xaml, new XmlWriterSettings { Indent = true }));

            this.TemplateXaml.Text = xaml.ToString();
            this.Grid.Children.Remove(control);
        }

    }
}
