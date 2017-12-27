// <copyright file="GameFrameworkElement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
            this.Loaded += this.GameFrameworkElement_Loaded;
            this.KeyDown += this.GameFrameworkElement_KeyDown;
            this.MouseMove += this.GameFrameworkElement_MouseMove;
            this.MouseLeftButtonUp += this.GameFrameworkElement_MouseLeftButtonUp;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Brushes.Silver, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));



            if (this.game != null)
            {
                drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.Black, 1), this.game.ThePlayer.Item);

                foreach (Bubble[] arritem in this.game.Bubbles)
                {
                    foreach (Bubble item in arritem)
                    {
                        this.DrawOutBubble(drawingContext, item);
                    }
                }

                foreach (Bubble item in game.ThePlayer.NextBullets)
                {
                    this.DrawOutBubble(drawingContext, item);
                }

                this.DrawOutBubble(drawingContext, this.game.ThePlayer.Bullet);



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
                this.game = new Game((int)this.ActualWidth, (int)this.ActualHeight);
                this.InvalidateVisual();

                this.Focusable = true;
                this.Focus();

                this.timer = new DispatcherTimer();
                this.timer.Interval = TimeSpan.FromMilliseconds(20);
                this.timer.Tick += this.Timer_Tick;
                this.timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.game.ThePlayer.Move(this.game.ThePlayer.Angle);
            this.InvalidateVisual();
        }

        private void GameFrameworkElement_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;

            if ((x >= 0 && x < this.ActualWidth) && (y >= 0 && y < this.ActualHeight - 30))
            {
                this.game.ThePlayer.CalcAngle(x, y);
                this.game.ThePlayer.Rotate();
            }
        }

        private void GameFrameworkElement_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.game.ThePlayer.Bullet.Velocity == 0)
            {
                this.game.ThePlayer.Bullet.Velocity = 5;
                this.game.ThePlayer.ShootingAngle = this.game.ThePlayer.Angle;
            }
        }

        private void DrawOutBubble(DrawingContext drawingContext, Bubble bubble)
        {
            switch (bubble.ColorNumber)
            {
                case 0:
                    drawingContext.DrawGeometry(Brushes.SteelBlue, new Pen(Brushes.SlateBlue, 1), bubble.Item);
                    break;
                case 1:
                    drawingContext.DrawGeometry(Brushes.LightCoral, new Pen(Brushes.DeepPink, 1), bubble.Item);
                    break;
                case 2:
                    drawingContext.DrawGeometry(Brushes.Aquamarine, new Pen(Brushes.LightSeaGreen, 1), bubble.Item);
                    break;
                case 3:
                    drawingContext.DrawGeometry(Brushes.Goldenrod, new Pen(Brushes.DarkGoldenrod, 1), bubble.Item);
                    break;
                case 4:
                    drawingContext.DrawGeometry(Brushes.Gray, new Pen(Brushes.DarkGray, 1), bubble.Item);
                    break;
                default:
                    break;
            }
        }

    }
}
