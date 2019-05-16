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

namespace TaskBots.Model.Control.CustomControls
{
    /// <summary>
    /// TaskItem.xaml の相互作用ロジック
    /// </summary>
    public partial class TaskItem : UserControl
    {
        public TaskItem(double locationX = 0, double locationY = 0) {
            InitializeComponent();
            Canvas.SetLeft(this, locationX);
            Canvas.SetTop(this, locationY);
        }
        Point moveStartMousePosition;
        Point moveIncrimentalValue;
        Point moveStartPosition;
        bool IsMoving = false;
        public bool IsCommentEditing {
            get {
                return !CommentTextBox.IsReadOnly;
            }
            set {
                CommentTextBox.IsReadOnly = !value;
                CommentTextBox.Visibility = value? Visibility.Visible : Visibility.Hidden;
                if (value) CommentTextBox.Focus();
                
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            IsCommentEditing = false;
        }
        private void TextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(textBlock.Text, out int inputValue))
            {
                TaskProgressBar.Value = inputValue;
            }
            else {
                MessageBox.Show("有効な値を入力してください。");
                TaskProgressBar.Value = 0;
                textBlock.Text = "0";
            }
        }
        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CommentTextBlock.Text = CommentTextBox.Text;
        }
        private void CommentTextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoving)
            {
                Point nowMousePosition = e.GetPosition(Application.Current.MainWindow);
                moveIncrimentalValue = new Point(nowMousePosition.X - moveStartMousePosition.X, nowMousePosition.Y - moveStartMousePosition.Y);
                Canvas.SetLeft(this, moveStartPosition.X + moveIncrimentalValue.X);
                Canvas.SetTop(this, moveStartPosition.Y + moveIncrimentalValue.Y);
            }
        }
        private void CommentTextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMoving) {
                Canvas.SetLeft(this, moveStartPosition.X);
                Canvas.SetTop(this, moveStartPosition.Y);
                IsMoving = false;
            }
        }
        private void CommentTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMoving)
            {
                moveStartMousePosition = e.GetPosition(Application.Current.MainWindow);
                moveStartPosition.X = (int)Canvas.GetLeft(this);
                moveStartPosition.Y = (int)Canvas.GetTop(this);
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }
        }
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IsCommentEditing = true;
        }
        private void CommentTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsCommentEditing) {
                if (e.Key == Key.Enter) {
                    IsCommentEditing = false;
                }
            }
        }
    }
}
