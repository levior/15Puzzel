using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App6
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly int[] _bordersNums = { 0, 4, 8, 12, 3, 7, 11, 15 };
        private readonly Random _rnd;

        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime _startTime;
        private int _stepMove;
        private int _startApplication;
        //int TTTTTTTTTTTTT;


        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            _startApplication = 0;
            _rnd = new Random();
            //_timer = new DispatcherTimer();
            ////_timer.Tick += new Windows.UI.Xaml.EventHandler(_timer_Tick);
            ////_timer.Tick += new System.EventHandler(_timer_Tick);
            //_timer.Interval = new TimeSpan(0, 0, 0, 1);
            //// 

            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();
            //timer.Start();
            _stepMove = 0;
            //TTTTTTTTTTTTT = 1;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        void timer_Tick(object sender, object e)
        {
            var time = DateTime.Now - _startTime;
            TextBlockTime.Text = string.Format("{0:00}:", time.Hours) + string.Format("{0:00}:", time.Minutes) + string.Format("{0:00}", time.Seconds);
            //TextBlockTime.Text = string.Format(Const.TimeFormat, time.Hours, time.Minutes, time.Seconds);
            //TextBlockTime.Text = TTTTTTTTTTTTT.ToString();
            //TTTTTTTTTTTTT++;

            //void _timer_Tick(object sender, object e)
            //{
            //    var time = DateTime.Now - _startTime;
            //    TextBlockTime.Text = string.Format(Const.TimeFormat, time.Hours, time.Minutes, time.Seconds);
            //}
        }


        public void NewGame()
        {
            
            

            Scrambles();
            while (!CheckIfSolvable())
            {
                Scrambles();
            }
            

            TextBlockStep.Text = "0";
            TextBlockTime.Text = "00:00:00";
            _stepMove = 0;
            _startTime = DateTime.Now;
            timer.Start();


        }

        // Обработчик события
        private void  Border_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            
            Border bd = sender as Border;
            // Ищем пустую ячейку
            int a = Convert.ToInt32(bd.Tag);
            int b = FindEmptyItemPosition();
            if (CanMove(a, b) == true)
            {
                var stack1 = FindBorderByTagId(a);
                var stack2 = FindBorderByTagId(b);
                var image1 = stack1.Child;
                stack1.Child = null;
                stack2.Child = image1;
                _stepMove++;
                TextBlockStep.Text = _stepMove.ToString();
                // проверка условия победы
                if (GameWin())
                {

                    timer.Stop();
                    //ShowWin();
                    String str = "\n\n\nВы победили!\n" + "Время: " + TextBlockTime.Text + "\nКоличество шагов: " + TextBlockStep.Text;
                    _R_End.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    _TextBlockEnd.Text = str;
                    _TextBlockEnd.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    _Burron_End.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    var stack3 = FindBorderByTagId(15);
                    var stack4 = FindBorderByTagId(16);
                    var image3 = stack1.Child;
                    stack3.Child = null;
                    stack4.Child = image1;
                    

                    //TextBlockStep.Text = "WIN";();
                    /*
                    foreach (Border bbb in ContentPanel.Children)
                    {
                        bbb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }*/
                }

                
            }
            else
            {
                //TextBoxOut.Text = "FALSE";
            }
        }

        /*
        void ShowWin()
        {
            Popup popup = new Popup();
            popup.IsLightDismissEnabled = true;
            Grid panel = new Grid();
            panel.Background = bottomAppBar.Background;
            panel.Height = 700;
            panel.Width = 700;
            panel.Transitions = new TransitionCollection();
            panel.Transitions.Add(new PopupThemeTransition());


            TextBlock txtBlock = new TextBlock();
            txtBlock.FontSize = 48;
            txtBlock.Text = "Статистика:";
            //txtBlock.VerticalAlignment = 40;
            txtBlock.Height = 100;
            txtBlock.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.FontSize = 48;
            txtBlock1.Text = "Время: " + TextBlockTime.Text;
            //txtBlock1.VerticalAlignment = 100;
            txtBlock1.Height = 200;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;


            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.FontSize = 48;
            txtBlock2.Text = "Количество ходов: " + TextBlockStep.Text;
            txtBlock2.Height = 300;
            //txtBlock2.VerticalAlignment = 200;
            txtBlock2.HorizontalAlignment = HorizontalAlignment.Center;


            Button btnMain = new Button();


            btnMain.Content = "Начать новую игру";
            btnMain.FontSize = 48;
            btnMain.VerticalAlignment = VerticalAlignment.Bottom;
            btnMain.HorizontalAlignment = HorizontalAlignment.Center;
            btnMain.Click += (s, e) => { NewGame(); //popup.IsOpen = false; 
            
            };
            panel.Children.Add(btnMain);
            panel.Children.Add(txtBlock);
            panel.Children.Add(txtBlock1);
            panel.Children.Add(txtBlock2);
            popup.Child = panel;
            
            //var button = (Button) sender;
            //var transform = button.TransformToVisual(this);
            //var point = transform.TransformPoint(new Point());
            popup.HorizontalOffset = 340;
            popup.VerticalOffset = 20;
            popup.IsOpen = true;
        }
         */


        // Метод определния координат по тэгу
        PositionXY GetPositionXYbyBorder(int tag)
        {
            PositionXY pxy = new PositionXY();
            int n = 1;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (tag == n)
                    {
                        pxy.SetX(i);
                        pxy.SetY(j);
                        return pxy;
                    }
                    n++;
                }
            pxy.SetX(-1);
            pxy.SetY(-1);
            return pxy;
        }

        // Метод определения тэга по позиции
        int GetTagPositionXYbyBorder(PositionXY pxy)
        {
            int n = 1;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (pxy.GetX() == i && pxy.GetY() == j)
                    {
                        return n;
                    }
                    n++;
                }
            return -1;
        }

        // Метод определяет возможность перехода
        bool CanMove(int pushTag, int voidTag)
        {
            PositionXY pos1 = new PositionXY();
            PositionXY pos2 = new PositionXY();

            pos1 = GetPositionXYbyBorder(pushTag);
            pos2 = GetPositionXYbyBorder(voidTag);

            // Проверяем возможность
            int i, j;
            i = j = 0;
            i = pos1.GetX();
            j = pos1.GetY();

            if ((i - 1) == pos2.GetX() && (j) == pos2.GetY()) return true;
            if ((i + 1) == pos2.GetX() && (j) == pos2.GetY()) return true;
            if ((i) == pos2.GetX() && (j - 1) == pos2.GetY()) return true;
            if ((i) == pos2.GetX() && (j + 1) == pos2.GetY()) return true;


            return false;
        }

        // Возвражает объект указанного типа
        Border FindBorderByTagId(int tag)
        {
            foreach (Border b in ContentPanel.Children)
            {
                if (tag == Convert.ToInt32(b.Tag))
                    return b;
            }
            return null;
        }

        // метод ищет пустую позицию
        int FindEmptyItemPosition()
        {
            foreach (Border b in ContentPanel.Children)
            {
                if (null == b.Child)
                {
                    return Convert.ToInt32(b.Tag);
                }
            }
            return 0;
        }

        // Метод на определение победы
        bool GameWin()
        {
            int n = 1;
            int k = 0;
            foreach (Border b in ContentPanel.Children)
            {

                if (b.Child != null)
                {
                    if (((Image)b.Child).Tag.ToString() == b.Tag.ToString())
                    {
                        n++;
                    }
                }
                else
                {
                    k = Convert.ToInt32(b.Tag);
                }
            }
            if (n == 16 && k == 16) return true;
            return false;
        }

        
        int FindItemValueByPosition(int position)
        {
            return ((StackPanel)ContentPanel.Children[position]).Children.Count > 0 ?
                Convert.ToInt32(((Image)((StackPanel)ContentPanel.Children[position]).Children[0]).Tag) : 16;
        }

        // Метод автоматического перемешивания пазлов
        void Scrambles()
        {
            var count = 0;
            while (count < 25)
            {
                var a = _rnd.Next(1, 16);
                var b = _rnd.Next(1, 16);


                if (a == b) continue;

                var stack1 = FindBorderByTagId(a);
                var stack2 = FindBorderByTagId(b);

                if (a == 16)
                {
                    var image2 = stack2.Child;
                    stack2.Child = null;
                    stack1.Child = image2;
                }
                else if (b == 16)
                {
                    var image1 = stack1.Child;
                    stack1.Child = null;
                    stack2.Child = image1;
                }
                else
                {
                    var image1 = stack1.Child;
                    var image2 = stack2.Child;

                    stack1.Child = null;
                    stack2.Child = null;

                    stack1.Child = image2;
                    stack2.Child = image1;
                }

                count++;
            }
        }


        // Проверка решаемости данной растановки пазлов
        bool CheckIfSolvable()
        {
            var n = 1;
            for (var i = 1; i <= 16; i++)
            {
                if (!(ContentPanel.Children[i] is StackPanel)) continue;

                var num1 = FindItemValueByPosition(i);
                var num2 = FindItemValueByPosition(i - 1);

                if (num1 > num2)
                {
                    n++;
                }
            }

            var emptyPos = FindEmptyItemPosition();
            return n % 2 == (emptyPos + emptyPos / 4) % 2 ? true : false;
        }

        // Метод выполняется при загрузки приложения
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            // грубо, но не без этого
            if (_startApplication == 0)
            {
                NewGame();
                _startApplication = 1;
            }

        }

        private void PointerPressed_NewGame(object sender, RoutedEventArgs e)
        {
            _R_End.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _TextBlockEnd.Text = "";
            _TextBlockEnd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _Burron_End.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            NewGame();
        }

        private void PointerPressed_Help(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpPage));
        }

        private void PointerPressed_About(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }

        private void _Burron_End_Click_1(object sender, RoutedEventArgs e)
        {
            _R_End.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _TextBlockEnd.Text = "";
            _TextBlockEnd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _Burron_End.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            NewGame();
        }


    }
}
