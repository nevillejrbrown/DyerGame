using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using DyerGame.Models;
using System.Linq;

namespace DyerGameTest

{
    [TestClass]
    public class TestGame
    {

        [TestMethod]
        public void TestGameStateInitialiseGame()
        {
            Game game = new Game();
            game.AddCeleb(new Celeb("Danny Dyer"));
            game.AddCeleb(new Celeb("Geoff Capes"));
            game.State.Should().Be(GameState.ROUND_READY_TO_START);
        }

        [TestMethod]
        public void TestGameStateCelebGuessedGame()
        {
            Game game = new Game();
            var geezer = new Celeb("Danny Dyer");
            game.AddCeleb(geezer);
            game.AddCeleb(new Celeb("Geoff Capes"));
            game.State.Should().Be(GameState.ROUND_READY_TO_START);
            geezer.Guess();
            game.State.Should().Be(GameState.ROUND_IN_PROGRESS);
        }
        
        [TestMethod]
        public void TestGameStateAllCelebsGuessedGame()
        {
            Game game = new Game();
            var geezer = new Celeb("Danny Dyer");
            game.AddCeleb(geezer);
            var geoff= new Celeb("Geoff Capes");
            game.AddCeleb(geoff);
            game.State.Should().Be(GameState.ROUND_READY_TO_START);
            geezer.Guess();
            geoff.Guess();
            game.State.Should().Be(GameState.ROUND_COMPLETE);
        }


        [TestMethod]
        public void TestGetRandomCeleb()
        {
            Game game = new Game();
            var geezer = new Celeb("Danny Dyer");
            game.AddCeleb(geezer);
            var geoff = new Celeb("Geoff Capes");
            game.AddCeleb(geoff);
            var john = new Celeb("John Peel");
            game.AddCeleb(john);
            var bill = new Celeb("Bill Bailey");
            game.AddCeleb(bill);
            var jake= new Celeb("Jake Gyllenhaal");
            game.AddCeleb(jake);
            geezer.Guess();
            geoff.Guess();
            john.Guess();
            jake.Guess();
            game.GetRandomCelebFromHat().Should().Be(bill);
        }
        [TestMethod]
        public void TestPutGuessedCelebsBackInHat()
        {
            Game game = new Game();
            
            var geezer = new Celeb("Danny Dyer");
            game.AddCeleb(geezer);

            var geoff = new Celeb("Geoff Capes");
            game.AddCeleb(geoff);

            var john = new Celeb("John Peel");
            game.AddCeleb(john);

            var bill = new Celeb("Bill Bailey");
            game.AddCeleb(bill);

            var jake = new Celeb("Jake Gyllenhaal");
            game.AddCeleb(jake);

            geezer.Guess();
            geoff.Guess();
            john.Guess();
            bill.Burn();
            jake.Burn();

            game.getCelebsInHat().Count().Should().Be(0);

            game.PutAllGuessedCelebsBackInHat();

            game.getCelebsInHat().Count().Should().Be(3);
        }

        [TestMethod]
        public void TestGetNullBackIfNoneInHat()
        {
            Game game = new Game();
            
            var geezer = new Celeb("Danny Dyer");
            game.AddCeleb(geezer);

            var geoff = new Celeb("Geoff Capes");
            game.AddCeleb(geoff);

            geezer.Guess();
            geoff.Burn();

            game.GetRandomCelebFromHat().Should().BeNull();
        }
    }


}
