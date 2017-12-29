// <copyright file="GameFrameworkElement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Threading;


    internal class GameFrameworkElement : FrameworkElement
    {
        private Game game;
        private DispatcherTimer timer;

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
            drawingContext.DrawRectangle(Brushes.DarkSlateGray, null, new Rect(0, 400, this.ActualWidth, 100));



            if (this.game != null)
            {


                foreach (Bubble[] arritem in this.game.Bubbles)
                {
                    foreach (Bubble item in arritem)
                    {
                        if (item != default(Bubble))
                        {
                            this.DrawOutBubble(drawingContext, item);
                        }
                    }
                }

                foreach (Bubble item in this.game.ThePlayer.NextBullets)
                {
                    if (item != default(Bubble))
                    {
                        this.DrawOutBubble(drawingContext, item);
                    }
                }

                drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.Black, 1), this.game.ThePlayer.Item);

                this.DrawOutBubble(drawingContext, this.game.ThePlayer.Bullet);

                if (this.game.CheckIfLoose())
                {
                    this.DrawGameOver(drawingContext);
                }
                if (this.game.CheckIfWin())
                {
                    this.DrawNextLevel(drawingContext);
                }

                this.DrawLevel(drawingContext);
                this.DrawScores(drawingContext);
            }
        }

        private void GameFrameworkElement_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (this.game.CheckIfLoose())
                {
                    this.timer.Start();
                    this.game.Level.ResetLevel();
                    this.game.Scores.ResetScore();
                    this.game.ToStartingState();
                }
                else if (this.game.CheckIfWin())
                {
                    this.timer.Start();
                    this.game.Level.LevelUp();
                    this.game.ToStartingState();
                }
            }
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
            if (this.game.CheckIfLoose())
            {
                this.timer.Stop();
            }
            else if (this.game.CheckIfWin())
            {
                this.timer.Stop();

            }
            else
            {
                this.game.DoTurn();
            }
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
                this.game.ThePlayer.Bullet.Velocity = 10;
                this.game.ThePlayer.ShootingAngle = this.game.ThePlayer.Angle;
            }
        }

        private void DrawOutBubble(DrawingContext drawingContext, Bubble bubble)
        {
            switch (bubble.ColorNumber)
            {
                case -1:
                    drawingContext.DrawGeometry(Brushes.Silver, null, bubble.Item);
                    break;
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
                case 5:
                    drawingContext.DrawGeometry(Brushes.Brown, new Pen(Brushes.DarkRed, 1), bubble.Item);
                    break;
                case 6:
                    drawingContext.DrawGeometry(Brushes.ForestGreen, new Pen(Brushes.DarkGreen, 1), bubble.Item);
                    break;

                default:
                    break;
            }
        }

        private void DrawGameOver(DrawingContext drawingContext)
        {
            FormattedText text1 = new FormattedText(
                "Game over",
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Calibry"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal),
                32,
                Brushes.White);

            FormattedText text2 = new FormattedText(
                "\nPlease press ENTER to restart game \nPlease press ESC to exit game",
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Calibri"),
                18,
                Brushes.White);

            drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            drawingContext.DrawText(text1, new Point(this.ActualWidth / 2 - text1.Width / 2, this.ActualHeight / 2 - (2* text1.Height)));
            drawingContext.DrawText(text2, new Point(this.ActualWidth / 2 - text2.Width / 2, this.ActualHeight / 2 + text1.Height));

        }

        private void DrawNextLevel(DrawingContext drawingContext)
        {
            FormattedText text1 = new FormattedText(
                "Level " + this.game.Level.Level.ToString() + " completed" ,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Calibry"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal),
                26,
                Brushes.White);

            FormattedText text2 = new FormattedText(
                "\nPlease press ENTER to continue game \nPlease press ESC to exit game",
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Calibri"),
                18,
                Brushes.White);

            drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            drawingContext.DrawText(text1, new Point(this.ActualWidth / 2 - text1.Width / 2, this.ActualHeight / 2 - (2* text1.Height)));
            drawingContext.DrawText(text2, new Point(this.ActualWidth / 2 - text2.Width / 2, this.ActualHeight / 2 + text1.Height));

        }


        private void DrawLevel(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText(
                "Level " + this.game.Level.Level.ToString(),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Calibry"), FontStyles.Normal, FontWeights.SemiBold, FontStretches.Normal),
                16,
                Brushes.White);

            if (this.game.Level.Level > 0)
            {
                drawingContext.DrawText(text, new Point(10, 440));
            }
            
        }

        private void DrawScores(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText(
                "Your score: " + this.game.Scores.Score.ToString(),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(new FontFamily("Calibry"), FontStyles.Normal, FontWeights.SemiBold, FontStretches.Normal),
                16,
                Brushes.White);

            if (this.game.Level.Level > 0)
            {
                drawingContext.DrawText(text, new Point(this.ActualWidth - text.Width - 10, 440));
            }

        }
    }
}
