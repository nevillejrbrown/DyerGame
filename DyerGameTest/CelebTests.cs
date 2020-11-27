using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using DyerGame.Models;


namespace DyerGameTest
{
    [TestClass]
    public class TestCelebState
    {
        [TestMethod]
        public void TestCelebInitialisedWithCorrectState()
        {
            Celeb celeb = new Celeb("Danny Dyer");
            celeb.State.Should().Be(CelebState.IN_HAT);
        }

        [TestMethod]
        public void TestCelebGuessedGoesToCorrectState()
        {
            Celeb celeb = new Celeb("Danny Dyer");
            celeb.Guess();
            celeb.State.Should().Be(CelebState.GUESSED);
        }

        [TestMethod]
        public void TestCelebGuessedWhenStateInvalid()
        {
            Celeb celeb = new Celeb("Danny Dyer");
            celeb.Guess();
            celeb.Invoking(c => c.Guess())
                .Should().Throw<InvalidOperationException>();
                
        }
        [TestMethod]
        public void TestAttemptToThrowBurnedCelebBackIntoHat()
        {
            Celeb celeb = new Celeb("Danny Dyer");
            celeb.Burn();
            celeb.Invoking(c => c.PutBackIntoHat())
                .Should().Throw<InvalidOperationException>();
        }




    }
}
