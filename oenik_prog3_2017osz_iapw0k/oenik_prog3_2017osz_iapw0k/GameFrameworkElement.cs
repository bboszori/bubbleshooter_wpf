namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;

    internal class GameFrameworkElement : FrameworkElement
    {
        private Game game;
        private DispatcherTimer timer;
        private int width;
        private int height;

        public GameFrameworkElement()
        {
            Loaded += GameFrameworkElement_Loaded;
            KeyDown += GameFrameworkElement_KeyDown;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size newSize = new Size();



            return newSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Brushes.Silver, null,
                new Rect(0, 0, this.ActualWidth, this.ActualHeight));

            if (game != null)
            {
                foreach (Bubble[] arritem in game.Bubbles)
                {
                    foreach (Bubble item in arritem)
                    {
                        switch (item.ColorNumber)
                        {
                            case 0:
                                drawingContext.DrawGeometry(Brushes.Blue, null, item.Ball);
                                break;
                            case 1:
                                drawingContext.DrawGeometry(Brushes.Red, null, item.Ball);
                                break;
                            case 2:
                                drawingContext.DrawGeometry(Brushes.MediumSeaGreen, null, item.Ball);
                                break;
                            case 3:
                                drawingContext.DrawGeometry(Brushes.Purple, null, item.Ball);
                                break;
                            case 4:
                                drawingContext.DrawGeometry(Brushes.Gray, null, item.Ball);
                                break;
                            default:
                                break;
                        }
                    }
                }

                //drawingContext.DrawGeometry(Brushes.Silver, null, this.game.Bullet.Ball);
            }
        }

        private void GameFrameworkElement_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        

        private void GameFrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                game = new Game((int)ActualWidth, (int)ActualHeight);
                InvalidateVisual();

                Focusable = true;
                Focus();

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(20);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // game.DoTurn();
            InvalidateVisual();
        }

        
    }
}
