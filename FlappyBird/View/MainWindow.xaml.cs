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
using System.Windows.Threading;
using FlappyBird.Controller;
using FlappyBird.Model;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller.Controller controller = new Controller.Controller();
        TimeSpan SpawnSpeed = new TimeSpan(30000000);
        TimeSpan ObstacleMoveSpeed = new TimeSpan(10000);
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        List<Label> highScoreLabelList = new List<Label>();
        public MainWindow()
        {
            InitializeComponent();
            HighScoreLabelListAdd();
            controller.PlayerList = controller.DatabaseHandler.GetPlayers();
            controller.ScoreList = controller.DatabaseHandler.GetScores();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = SpawnSpeed;
            timer2.Tick += new EventHandler(Timer2_Tick);
            timer2.Interval = ObstacleMoveSpeed;
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            if (controller.CurrentPlayer.CheckIfPlayerNameExists(textBoxUsername.Text, controller.PlayerList))
            {
                controller.GameStart();
                controller.CurrentPlayer = controller.FindCurrentPlayer(textBoxUsername.Text, controller.PlayerList);
                controller.CurrentPlayer.PlayCount++;
                controller.DatabaseHandler.UpdatePlayer(controller.CurrentPlayer);
                gridGame.Children.Add(controller.CurrentBox.Rec);
                timer.Start();
                timer2.Start();
                gridStartScreen.Visibility = Visibility.Collapsed;
                gridGame.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Insert a valid username into the username textbox", "Insert Name");
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            controller.CurrentBox.MoveDown(2);
            foreach (Obstacle obstacle in controller.ObstacleList)
            {
                obstacle.MoveLeft(2);
            }
            CheckIfScoring();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Obstacle obstacle = controller.GenerateObstacle();
            gridGame.Children.Add(obstacle.Rec);
            gridGame.Children.Add(obstacle.RecTop);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (controller.IfGameActive)
            {
                controller.PlayerInput();
            }
        }

        private void HighScoreLabelListAdd()
        {
            highScoreLabelList.Add(lblHighScore1);
            highScoreLabelList.Add(lblHighScore2);
            highScoreLabelList.Add(lblHighScore3);
            highScoreLabelList.Add(lblHighScore4);
            highScoreLabelList.Add(lblHighScore5);
            highScoreLabelList.Add(lblHighScore6);
            highScoreLabelList.Add(lblHighScore7);
            highScoreLabelList.Add(lblHighScore8);
            highScoreLabelList.Add(lblHighScore9);
            highScoreLabelList.Add(lblHighScore10);
        }

        private void RemoveObstacle()
        {
            //Change index under here to number of elements on screen + 1
            gridGame.Children.Remove(controller.ObstacleList[0].Rec);
            gridGame.Children.Remove(controller.ObstacleList[0].RecTop);
            controller.ObstacleList.RemoveAt(0);
            
        }

        private void CheckIfScoring()
        {
            if (controller.ObstacleList.Count == 0)
            {

            }
            else if (controller.ObstacleList[0].Coords.X < -650)
            {
                RemoveObstacle();
                controller.GotPoint = false;
            }
            else if (controller.CurrentBox.Coords.X + controller.CurrentBox.Rec.Width >= controller.ObstacleList[0].Coords.X - controller.ObstacleList[0].Rec.Width && controller.CurrentBox.Coords.Y + controller.CurrentBox.Rec.Height <= controller.ObstacleList[0].Coords.Y - controller.ObstacleList[0].Rec.Height && controller.CurrentBox.Coords.Y - controller.CurrentBox.Rec.Height >= controller.ObstacleList[0].Coords2.Y + controller.ObstacleList[0].RecTop.Height)
            {
                if (!controller.GotPoint)
                {
                    Scoring();
                    controller.GotPoint = true;
                }
            }
            else if ((controller.CurrentBox.Coords.X + controller.CurrentBox.Rec.Width >= controller.ObstacleList[0].Coords.X + controller.ObstacleList[0].Rec.Width && controller.CurrentBox.Coords.Y - controller.CurrentBox.Rec.Height >= controller.ObstacleList[0].Coords.Y - controller.ObstacleList[0].Rec.Height) || (controller.CurrentBox.Coords.X + controller.CurrentBox.Rec.Width >= controller.ObstacleList[0].Coords.X - controller.ObstacleList[0].Rec.Width && controller.CurrentBox.Coords.Y - controller.CurrentBox.Rec.Height <= controller.ObstacleList[0].Coords2.Y + controller.ObstacleList[0].RecTop.Height))
            {
                if (!controller.GotPoint)
                {
                    GameOver();
                }
            }
            else if (controller.CurrentBox.Coords.Y >= 450 || controller.CurrentBox.Coords.Y <= -450)
            {
                GameOver();
            }

        }
        private void Scoring()
        {
            controller.CurrentPlayer.Score.Points++;
            lblcurrentScore.Content = controller.CurrentPlayer.Score.Points;
        }
        private void ResetScore()
        {
            controller.CurrentPlayer.Score.Points = 0;
            lblcurrentScore.Content = controller.CurrentPlayer.Score.Points;
        }

        private void GameOver()
        {
            controller.IfGameActive = false;
            gridGame.Children.Remove(controller.CurrentBox.Rec);
            foreach (Obstacle obstacle in controller.ObstacleList)
            {
                gridGame.Children.Remove(obstacle.Rec);
                gridGame.Children.Remove(obstacle.RecTop);
            }
            controller.ObstacleList.Clear();
            timer.Stop();
            timer2.Stop();
            lblGameOverScore.Content = controller.CurrentPlayer.Score.Points;
            Score score = controller.CurrentPlayer.Score;
            controller.ScoreList.Add(score);
            controller.DatabaseHandler.InsertScore(score);
            gridGame.Visibility = Visibility.Collapsed;
            gridGameOver.Visibility = Visibility.Visible;
            ResetScore();
        }

        private void btnViewHighScores_Click(object sender, RoutedEventArgs e)
        {
            List<Score> scores = controller.ScoreList;
            scores.Sort((x, y) => y.Points.CompareTo(x.Points));
            if (scores.Count <= 10)
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    highScoreLabelList[i].Content = $"{i + 1}: {controller.PlayerList[scores[i].PlayerId - 1].Name} {scores[i].Points}";
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    highScoreLabelList[i].Content = $"{i + 1}: {controller.PlayerList[scores[i].PlayerId - 1].Name} {scores[i].Points}";
                }

            }
            gridStartScreen.Visibility = Visibility.Collapsed;
            gridHighScores.Visibility = Visibility.Visible;
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            gridHighScores.Visibility = Visibility.Collapsed;
            gridStartScreen.Visibility = Visibility.Visible;
        }

        private void btnNewPlayerConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNewPlayerName.Text) || string.IsNullOrEmpty(textBoxNewPlayerEmail.Text) || string.IsNullOrEmpty(textBoxNewPlayerPhone.Text))
            {

            }
            else
            {
                bool ifPhoneAInt = int.TryParse(textBoxNewPlayerPhone.Text, out int phoneNumber);
                if (ifPhoneAInt)
                {
                    controller.PlayerList = controller.CreateUser(textBoxNewPlayerName.Text, textBoxNewPlayerEmail.Text, phoneNumber, controller.PlayerList);
                    gridStartScreen.Visibility = Visibility.Visible;
                    gridNewPlayer.Visibility = Visibility.Collapsed;
                    textBoxNewPlayerName.Text = "";
                    textBoxNewPlayerPhone.Text = "";
                    textBoxNewPlayerEmail.Text = "";
                }
            }
        }

        private void btnNewPlayerCancel_Click(object sender, RoutedEventArgs e)
        {
            gridStartScreen.Visibility = Visibility.Visible;
            gridNewPlayer.Visibility = Visibility.Collapsed;
            textBoxNewPlayerName.Text = "";
            textBoxNewPlayerPhone.Text = "";
            textBoxNewPlayerEmail.Text = "";
        }

        private void btnNewPlayer_Click(object sender, RoutedEventArgs e)
        {
            gridStartScreen.Visibility = Visibility.Collapsed;
            gridNewPlayer.Visibility = Visibility.Visible;
        }

        private void btnBackToTitle_Click(object sender, RoutedEventArgs e)
        {
            gridGameOver.Visibility = Visibility.Collapsed;
            gridStartScreen.Visibility = Visibility.Visible;
        }
    }
}
