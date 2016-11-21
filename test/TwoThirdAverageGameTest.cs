﻿using System;
using Xunit;
using Christmas.Models;
using System.Collections.Generic;

namespace Tests
{
    public class TwoThirdAverageGameTest
    {
        [Fact]
        public void ResetWorks()
        {
            TwoThirdAverageGame.Reset();
            Assert.Equal(0, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.Equal(0, TwoThirdAverageGame.GetTwoThirdOfAverage());
            Assert.Equal("No one", TwoThirdAverageGame.GetWinner());

            TwoThirdAverageGame.Submit("Adrian", 25);

            Assert.Equal(1, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.True(TwoThirdAverageGame.GetTwoThirdOfAverage() > 0);
            Assert.Equal("Adrian", TwoThirdAverageGame.GetWinner());

            TwoThirdAverageGame.Reset();
            Assert.Equal(0, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.Equal(0, TwoThirdAverageGame.GetTwoThirdOfAverage());
            Assert.Equal("No one", TwoThirdAverageGame.GetWinner());
        }

        [Fact]
        public void SamePlayerSubmittingShouldHaveHisValueUpdated()
        {
            double expected = 21;
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Adrian", expected);
            double actual = TwoThirdAverageGame.GetValueThisPersonSubmitted("Adrian");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldHaveHisValueUpdated2()
        {
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Cheong", 50);
            TwoThirdAverageGame.Submit("Adrian", 15);
            double actual = TwoThirdAverageGame.GetValueThisPersonSubmitted("Adrian");

            Assert.Equal(15, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldHaveHisValueUpdated3()
        {
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Cheong", 50);
            TwoThirdAverageGame.Submit("Adrian", 15);
            TwoThirdAverageGame.Submit("Cheong", 11);
            double actual = TwoThirdAverageGame.GetValueThisPersonSubmitted("Cheong");

            Assert.Equal(11, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldHaveHisValueUpdated4()
        {
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Cheong", 50);
            TwoThirdAverageGame.Submit("Adrian", 15);
            TwoThirdAverageGame.Submit("Cheong", 11);
            TwoThirdAverageGame.Submit("Adrian", 11);
            double actual = TwoThirdAverageGame.GetValueThisPersonSubmitted("Adrian");

            Assert.Equal(11, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldHaveHisValueUpdated5()
        {
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Adrian", 15);
            TwoThirdAverageGame.Submit("Adrian", 10);
            double actual = TwoThirdAverageGame.GetValueThisPersonSubmitted("Adrian");

            Assert.Equal(10, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldNotIncreaseTotalCount()
        {
            double expected = 1;
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Adrian", 21);
            double actual = TwoThirdAverageGame.GetNumberOfSubmissions();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldNotIncreaseTotalCount2()
        {
            double expected = 2;
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Cheong", 50);
            TwoThirdAverageGame.Submit("Adrian", 21);
            double actual = TwoThirdAverageGame.GetNumberOfSubmissions();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SamePlayerSubmittingShouldNotIncreaseTotalCount3()
        {
            double expected = 2;
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit("Adrian", 25);
            TwoThirdAverageGame.Submit("Cheong", 50);
            TwoThirdAverageGame.Submit("Adrian", 21);
            TwoThirdAverageGame.Submit("Cheong", 20);
            double actual = TwoThirdAverageGame.GetNumberOfSubmissions();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Adrian")]
        [InlineData(" Adrian")]
        [InlineData("Adrian ")]
        [InlineData(" Adrian ")]
        [InlineData("   Adrian")]
        [InlineData("Adrian   ")]
        [InlineData("   Adrian   ")]
        public void PlayerNamesShouldBeTrimmedOnSubmission(string name)
        {
            string expected = "Adrian";
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(name, 25);
            List<string> players = TwoThirdAverageGame.GetPlayerList();
            var actual = players[0];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Adrian Cheong")]
        [InlineData(" Adrian Cheong")]
        [InlineData("Adrian Cheong ")]
        [InlineData(" Adrian Cheong ")]
        [InlineData("Adrian  Cheong")]
        [InlineData(" Adrian  Cheong ")]
        [InlineData("   Adrian Cheong")]
        [InlineData("Adrian Cheong   ")]
        [InlineData("   Adrian   Cheong   ")]
        public void PlayerNamesShouldBeTrimmedOnSubmission2(string name)
        {
            string expected = "Adrian Cheong";
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(name, 25);
            List<string> players = TwoThirdAverageGame.GetPlayerList();
            var actual = players[0];

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Adrian", -1)]
        [InlineData("Adrian", 101)]
        [InlineData("Adrian", -0.00000001)]
        [InlineData("Adrian", 100.00000001)]
        [InlineData("Adrian", double.MaxValue)]
        [InlineData("Adrian", double.MinValue)]
        [InlineData("Adrian", double.NaN)]
        [InlineData("Adrian", double.NegativeInfinity)]
        [InlineData(null, 25)]
        [InlineData("", 25)]
        [InlineData(" ", 25)]
        public void PlayersDoingStupidThingsShouldBeIgnored(string name, double number)
        {
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(name, number);

            Assert.Equal(0, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.Equal(0, TwoThirdAverageGame.GetTwoThirdOfAverage());
            Assert.Equal("No one", TwoThirdAverageGame.GetWinner());
        }

        [Theory]
        [InlineData("Adrian", -1)]
        [InlineData("Adrian", 101)]
        [InlineData("Adrian", -0.00000001)]
        [InlineData("Adrian", 100.00000001)]
        [InlineData("Adrian", double.MaxValue)]
        [InlineData("Adrian", double.MinValue)]
        [InlineData("Adrian", double.NaN)]
        [InlineData("Adrian", double.NegativeInfinity)]
        [InlineData(null, 25)]
        [InlineData("", 25)]
        [InlineData(" ", 25)]
        public void PlayersDoingStupidThingsShouldBeIgnoredWhenTheGameHasAlreadyBegun(string stupidName, double stupidNumber)
        {
            TwoThirdAverageGame.Reset();
            TwoThirdAverageGame.Submit("Proper Person", 24);

            TwoThirdAverageGame.Submit(stupidName, stupidNumber);

            Assert.Equal(1, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.Equal(16, TwoThirdAverageGame.GetTwoThirdOfAverage());
            Assert.Equal("Proper Person", TwoThirdAverageGame.GetWinner());
        }

        [Theory]
        [InlineData(25)]
        [InlineData(25.0)]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(100.0)]
        [InlineData(100)]
        [InlineData(12.34)]
        [InlineData(34.56)]
        public void IfThereAreTwoWinnersTheFirstPersonWhoSubmittedShouldWin(double value)
        {
            string firstPerson = "First Person";
            string secondPerson = "Second Person";

            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(firstPerson, value);
            TwoThirdAverageGame.Submit(secondPerson, value);

            string actual = TwoThirdAverageGame.GetWinner();

            Assert.Equal(firstPerson, actual);
        }

        [Theory]
        [InlineData(25)]
        [InlineData(25.0)]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(100.0)]
        [InlineData(100)]
        [InlineData(12.34)]
        [InlineData(34.56)]
        public void IfThereAreThreeWinnersTheFirstPersonWhoSubmittedShouldWin(double value)
        {
            string firstPerson = "First Person";
            string secondPerson = "Second Person";
            string thirdPerson = "Third Person";

            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(firstPerson, value);
            TwoThirdAverageGame.Submit(secondPerson, value);
            TwoThirdAverageGame.Submit(thirdPerson, value);

            string actual = TwoThirdAverageGame.GetWinner();

            Assert.Equal(firstPerson, actual);
        }

        [Fact]
        public void IfAPersonChangesHisValueHeShouldBeConsideredLastOfQueue()
        {
            string firstPerson = "First Person";
            string secondPerson = "Second Person";
            double winningValue = 25;

            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(firstPerson, 50);
            TwoThirdAverageGame.Submit(secondPerson, winningValue);
            TwoThirdAverageGame.Submit(firstPerson, winningValue);

            string actual = TwoThirdAverageGame.GetWinner();

            Assert.Equal(secondPerson, actual);
        }

        [Fact]
        public void IfAPersonChangesHisValueHeShouldBeConsideredLastOfQueue2()
        {
            string firstPerson = "First Person";
            string secondPerson = "Second Person";
            string thirdPerson = "Third Person";
            double winningValue = 25;
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(firstPerson, 0);
            TwoThirdAverageGame.Submit(secondPerson, winningValue);
            TwoThirdAverageGame.Submit(thirdPerson, winningValue);
            TwoThirdAverageGame.Submit(firstPerson, winningValue);

            string actual = TwoThirdAverageGame.GetWinner();
            Assert.Equal(secondPerson, actual);
        }

        [Fact]
        public void IfAPersonChangesHisValueHeShouldBeConsideredLastOfQueue3()
        {
            string firstPerson = "First Person";
            string secondPerson = "Second Person";
            string thirdPerson = "Third Person";
            double winningValue = 25;
            TwoThirdAverageGame.Reset();

            TwoThirdAverageGame.Submit(firstPerson, 0);
            TwoThirdAverageGame.Submit(secondPerson, 0);
            TwoThirdAverageGame.Submit(thirdPerson, winningValue);
            TwoThirdAverageGame.Submit(secondPerson, winningValue);
            TwoThirdAverageGame.Submit(firstPerson, winningValue);

            string actual = TwoThirdAverageGame.GetWinner();
            Assert.Equal(thirdPerson, actual);
        }

        [Fact]
        public void testMassiveRandomSpamUseCaseWithMultipleSubmissions()
        {
            TwoThirdAverageGame.Reset();
            TwoThirdAverageGame.Submit("Adrian", 73.43242379);
            TwoThirdAverageGame.Submit("Eileen", 24.1992526401822);
            TwoThirdAverageGame.Submit("Lui Hock", 12.562050468151);
            TwoThirdAverageGame.Submit("Yee Pey", 6.54877570622227);
            TwoThirdAverageGame.Submit("Jia Sin", 42.2141806342241);
            TwoThirdAverageGame.Submit("Hao Quan", 19.7893892710047);
            TwoThirdAverageGame.Submit("Jing Yuan", 88.8368410869358);
            TwoThirdAverageGame.Submit("Allen", 3.55457923765942);
            TwoThirdAverageGame.Submit("Alice", 14.3036154114645);
            TwoThirdAverageGame.Submit("Eunice", 0);
            TwoThirdAverageGame.Submit("Qi Yang", 5.19015232061224);
            TwoThirdAverageGame.Submit("Adrian", 46.85738196);
            TwoThirdAverageGame.Submit("Eileen", 3.6763183989933);
            TwoThirdAverageGame.Submit("Lui Hock", 43.2801036536813);
            TwoThirdAverageGame.Submit("Jing Yuan", 81.1472941516023);
            TwoThirdAverageGame.Submit("Allen", 19.5144175092296);
            TwoThirdAverageGame.Submit("Alice", 4.75170298386008);
            TwoThirdAverageGame.Submit("Eunice", 59.4291471599364);
            TwoThirdAverageGame.Submit("Adrian", 14.528706916404);
            TwoThirdAverageGame.Submit("Yee Pey", 2.88842300573672);
            TwoThirdAverageGame.Submit("Allen", 67.2416185679742);
            TwoThirdAverageGame.Submit("Alice", 31.042886259302);
            TwoThirdAverageGame.Submit("Eunice", 4.64297851);
            TwoThirdAverageGame.Submit("Jia Sin", 23.7674621195047);
            TwoThirdAverageGame.Submit("Jing Yuan", 58.6840246285958);
            TwoThirdAverageGame.Submit("Alice", 48.4684162480857);
            TwoThirdAverageGame.Submit("Qi Yang", 5.89387287022391);
            TwoThirdAverageGame.Submit("Adrian", 6.59055132251411);
            TwoThirdAverageGame.Submit("Yee Pey", 2.43624612573545);
            TwoThirdAverageGame.Submit("Adrian", 50.040706675074);
            TwoThirdAverageGame.Submit("Yee Pey", 63.9921404584476);
            TwoThirdAverageGame.Submit("Adrian", 13.4814486122107);
            TwoThirdAverageGame.Submit("Yee Pey", 37.0459822944279);

            Assert.Equal(11, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.Equal(19.75585547, TwoThirdAverageGame.GetTwoThirdOfAverage(), 8);
            Assert.Equal("Hao Quan", TwoThirdAverageGame.GetWinner());
        }

        [Fact]
        public void testMassiveRandomSpamUseCaseWithMultipleSubmissionsWithInvalidSubmissionsInBetween()
        {
            TwoThirdAverageGame.Reset();
            TwoThirdAverageGame.Submit("Punk", -0.01);
            TwoThirdAverageGame.Submit("Adrian", 73.43242379);
            TwoThirdAverageGame.Submit("Eileen", 24.1992526401822);
            TwoThirdAverageGame.Submit("Lui Hock", 12.562050468151);
            TwoThirdAverageGame.Submit("Yee Pey", 6.54877570622227);
            TwoThirdAverageGame.Submit("Punk", 100.01);
            TwoThirdAverageGame.Submit("Jia Sin", 42.2141806342241);
            TwoThirdAverageGame.Submit("Hao Quan", 19.7893892710047);
            TwoThirdAverageGame.Submit("Jing Yuan", 88.8368410869358);
            TwoThirdAverageGame.Submit("Allen", 3.55457923765942);
            TwoThirdAverageGame.Submit("Alice", 14.3036154114645);
            TwoThirdAverageGame.Submit("Punk", int.MinValue);
            TwoThirdAverageGame.Submit("Eunice", 0);
            TwoThirdAverageGame.Submit("Qi Yang", 5.19015232061224);
            TwoThirdAverageGame.Submit("Adrian", 46.85738196);
            TwoThirdAverageGame.Submit("Eileen", 3.6763183989933);
            TwoThirdAverageGame.Submit("Lui Hock", 43.2801036536813);
            TwoThirdAverageGame.Submit("Punk", int.MaxValue);
            TwoThirdAverageGame.Submit("Jing Yuan", 81.1472941516023);
            TwoThirdAverageGame.Submit("Allen", 19.5144175092296);
            TwoThirdAverageGame.Submit("Alice", 4.75170298386008);
            TwoThirdAverageGame.Submit("Eunice", 59.4291471599364);
            TwoThirdAverageGame.Submit("Adrian", 14.528706916404);
            TwoThirdAverageGame.Submit("Yee Pey", 2.88842300573672);
            TwoThirdAverageGame.Submit("Allen", 67.2416185679742);
            TwoThirdAverageGame.Submit("Punk", Double.MinValue);
            TwoThirdAverageGame.Submit("Alice", 31.042886259302);
            TwoThirdAverageGame.Submit("Eunice", 4.64297851);
            TwoThirdAverageGame.Submit("Jia Sin", 23.7674621195047);
            TwoThirdAverageGame.Submit("Jing Yuan", 58.6840246285958);
            TwoThirdAverageGame.Submit("Alice", 48.4684162480857);
            TwoThirdAverageGame.Submit("Qi Yang", 5.89387287022391);
            TwoThirdAverageGame.Submit("Adrian", 6.59055132251411);
            TwoThirdAverageGame.Submit("Punk", Double.MaxValue);
            TwoThirdAverageGame.Submit("Yee Pey", 2.43624612573545);
            TwoThirdAverageGame.Submit("Adrian", 50.040706675074);
            TwoThirdAverageGame.Submit("Yee Pey", 63.9921404584476);
            TwoThirdAverageGame.Submit(null, 50);
            TwoThirdAverageGame.Submit("Adrian", 13.4814486122107);
            TwoThirdAverageGame.Submit("Yee Pey", 37.0459822944279);
            TwoThirdAverageGame.Submit("", 50);

            Assert.Equal(11, TwoThirdAverageGame.GetNumberOfSubmissions());
            Assert.Equal(19.75585547, TwoThirdAverageGame.GetTwoThirdOfAverage(), 8);
            Assert.Equal("Hao Quan", TwoThirdAverageGame.GetWinner());
        }
    }
}
