using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlappyBird.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Model.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void CheckIfPlayerNameExistsTest()
        {
            //Arrange
            List<Player> playerList = new List<Player>();
            Player player = new Player("Test", "test", 0);
            Player player2 = new Player("Unit", "unit", 1);
            playerList.Add(player);
            playerList.Add(player2);
            bool test = false;
            //Act
            test = player.CheckIfPlayerNameExists("Test", playerList);

            //Assert
            Assert.IsTrue(test);
        }
    }
}