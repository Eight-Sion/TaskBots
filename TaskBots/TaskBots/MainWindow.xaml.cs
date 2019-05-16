using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
using TaskBots.Model.Control;
using TaskBots.Model.Control.CustomControls;

namespace TaskBots
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //canvas.Children.Add(new TaskItem());
        }
        Canvas canvas;
        List<TaskItem> TaskData = new List<TaskItem>();
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            canvas = sender as Canvas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = TaskData.Count;
            TaskData.Add(new TaskItem());
            canvas.Children.Add(TaskData[index]);
        }
    }
}
