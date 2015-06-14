﻿using System;
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

namespace NGPlusPlusStar.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.ScalarSlider.Maximum = double.MaxValue;
            this.RotationSlider.Maximum = 360;
            this.RotationSlider.Minimum = 0;
        }

        private bool middle_mouse_pressed = false;

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.middle_mouse_pressed = e.MiddleButton == MouseButtonState.Pressed;
        }
        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Released && this.middle_mouse_pressed)
            {
                this.middle_mouse_pressed = false;
                this.ScalarSlider.Value = 1.0;
                this.RotationSlider.Value = 0;
            }
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var rotate = (Keyboard.Modifiers & ModifierKeys.Shift) > 0;
            if (e.Delta > 0)
                if (rotate)
                    this.RotationSlider.Value = (this.RotationSlider.Value + 10) % this.RotationSlider.Maximum;
                else
                    this.ScalarSlider.Value *= 1.1;
            else
                if (rotate)
                {
                    var proposed_value = (this.RotationSlider.Value - 10);
                    if (proposed_value > 0)
                        this.RotationSlider.Value = proposed_value;
                    else
                        this.RotationSlider.Value = this.RotationSlider.Maximum + proposed_value;
                }
                else
                    this.ScalarSlider.Value *= 0.9;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var visualTreeWindow = new VisualTreeWindow();
            //visualTreeWindow.ShowVisualTree(this);
            //visualTreeWindow.Show();

            //var controlTemplateXamlWindow = new ControlTemplateXamlWindow();
            //controlTemplateXamlWindow.Show();
        }

    }
}
