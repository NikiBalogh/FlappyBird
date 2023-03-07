using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace FlappyBird.Model
{
    class DatabaseHandler
    {
        private string connectionString = "data source=CV-BB-5991;initial catalog=FlappyBox;trusted_connection=true";

        private DataSet Execute(string query)
        {

            DataSet resultSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection
            (connectionString))))
            {

                // Open conn, execute query, close conn, wrap result in DataSet:
                adapter.Fill(resultSet);
            }
            return resultSet;
        }

        public List<Player> GetPlayers()
        {
            List<Player> allPlayers = new List<Player>();
            string allPlayersQuery = "Select * From Table_Players";

            try
            {
                // Perform query and save result in variable:
                DataSet resultSet = Execute(allPlayersQuery);

                // Get the first table of the data set, and save in variable:
                DataTable playersTable = resultSet.Tables[0];

                // Iterate through the rows of the table, and extract the data,
                // and create a new person object each time, and add that to the list of persons.
                foreach (DataRow personRow in playersTable.Rows)
                {
                    Player player = new Player();
                    player.Name = (string)personRow["Name"];
                    player.PhoneNumber = (int)personRow["PhoneNumber"];
                    player.PlayCount = (int)personRow["PlayCount"];
                    player.Email = (string)personRow["Email"];
                    player.Score.PlayerId = (int)personRow["Id"];
                    allPlayers.Add(player);
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database.", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return allPlayers;
        }
        public List<Score> GetScores()
        {
            List<Score> allScores = new List<Score>();
            string allScoresQuery = "Select * From Table_Scores";

            try
            {
                // Perform query and save result in variable:
                DataSet resultSet = Execute(allScoresQuery);

                // Get the first table of the data set, and save in variable:
                DataTable scoresTable = resultSet.Tables[0];

                // Iterate through the rows of the table, and extract the data,
                // and create a new person object each time, and add that to the list of persons.
                foreach (DataRow personRow in scoresTable.Rows)
                {
                    Score score = new Score();
                    score.PlayerId = (int)personRow["PlayerId"];
                    score.Points = (int)personRow["Points"];
                    allScores.Add(score);
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database.", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return allScores;
        }
        public void UpdatePlayer(Player player)
        {
            string updatePlayerQuery =
            $"UPDATE Table_Players SET PlayCount = '{player.PlayCount}' WHERE Id = {player.Score.PlayerId}";
            try
            {
                Execute(updatePlayerQuery);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database and update data.", "Warning Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void InsertPlayer(Player player)
        {
            string updatePlayerQuery =
            $"INSERT INTO Table_Players (Name, PlayCount, PhoneNumber, Email) VALUES ('{player.Name}', {player.PlayCount}, {player.PhoneNumber}, '{player.Email}')";
            try
            {
                Execute(updatePlayerQuery);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database and update data.", "Warning Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void InsertScore(Score score)
        {
            string updatePlayerQuery =
            $"INSERT INTO Table_Scores (PlayerId, Points) VALUES ({score.PlayerId}, {score.Points})";
            try
            {
                Execute(updatePlayerQuery);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Program could not connect to database and update data.", "Warning Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
