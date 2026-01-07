using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace flappy
{
    public partial class MainWindow : Window
	{
		private DispatcherTimer gameTimer = new DispatcherTimer();
		RotateTransform rotateTransform = new RotateTransform(0);

		private double gravity = 0.5;
		private double velocity = 0;
		private double jumpStrength = -8;

		private double birdTopPosition;

		public MainWindow()
		{
			InitializeComponent();
			InitializeGame();
		}

		private void InitializeGame()
		{
			birdTopPosition = Canvas.GetTop(Bird);

			gameTimer.Tick += GameLoop;
			gameTimer.Interval = TimeSpan.FromMilliseconds(16); //60 FPS
			gameTimer.Start();

			Bird.RenderTransform = rotateTransform;
			Bird.RenderTransformOrigin = new Point(0.5, 0.5);

			this.Focus();
		}

		private void GameLoop(object sender, EventArgs e)
		{
			velocity += gravity;
			birdTopPosition += velocity;

			Canvas.SetTop(Bird, birdTopPosition);
			rotateTransform.Angle = velocity / jumpStrength * -30;


			DebugText.Text = $"Sebesség: {velocity:F2} | Y Pozíció: {birdTopPosition:F2}";


			CheckBoundaries();
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				velocity = jumpStrength;
			}
		}

		private void CheckBoundaries()
		{
			if (birdTopPosition > GameCanvas.ActualHeight-30)
			{
				GameOver();
			}
			
			else if (birdTopPosition < 0)
			{
				GameOver();
			}
		}

		private void GameOver()
		{
			gameTimer.Stop();
			//ide majd még logika
		}
	}
}