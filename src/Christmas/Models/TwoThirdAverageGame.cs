using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Christmas.Models
{
    public class TwoThirdAverageGame
    {
        public enum Status
        {
            IN_PROGRESS,
            STOPPED
        }

        private static List<Submission> playersAndTheirNumbers = new List<Submission>();
        private static readonly double MIN_VALUE = 0.0;
        private static readonly double MAX_VALUE = 100.0;
        private static Status GAME_STATUS = Status.IN_PROGRESS;

        public static void Submit(string name, double submission)
        {
            if (GAME_STATUS == Status.IN_PROGRESS && name != null && isWithinValidRange(submission))
            {
                string trimmedName = System.Text.RegularExpressions.Regex.Replace(name.Trim(), @"\s+", " ");

                if (!string.IsNullOrEmpty(trimmedName))
                {
                    playersAndTheirNumbers.Remove(playersAndTheirNumbers.Where(x => x.Name.Equals(trimmedName)).FirstOrDefault());
                    playersAndTheirNumbers.Add(new Submission { Name = trimmedName, Number = submission });
                }
            }
        }

        public static List<Submission> GetSubmissionsAsAdmin()
        {
            return playersAndTheirNumbers.ToList();
        }

        public static List<Submission> GetSubmissions()
        {
            if (GAME_STATUS == Status.IN_PROGRESS)
                return new List<Submission>();

            return playersAndTheirNumbers.ToList();
        }

        private static bool isWithinValidRange(double submission)
        {
            return (submission >= MIN_VALUE && submission <= MAX_VALUE);
        }

        public static double GetTwoThirdOfAverage()
        {
            if (GAME_STATUS == Status.IN_PROGRESS || playersAndTheirNumbers == null || playersAndTheirNumbers.Count == 0)
                return 0;

            return playersAndTheirNumbers.Average(x => x.Number) * 2.0 / 3.0;
        }

        public static string GetWinner()
        {
            if (GAME_STATUS == Status.IN_PROGRESS)
                return "No one";

            double currentSmallestDelta = double.MaxValue;
            string currentLeader = "No one";
            double answer = GetTwoThirdOfAverage();

            foreach (var submission in playersAndTheirNumbers)
            {
                double delta = Math.Abs(submission.Number - answer);
                if (delta < currentSmallestDelta)
                {
                    currentLeader = submission.Name;
                    currentSmallestDelta = delta;
                }
            }
            return currentLeader;
        }

        public static string GetSecond()
        {
            if (GAME_STATUS == Status.IN_PROGRESS)
                return "No one";

            double currentSmallestDelta = double.MaxValue;
            string currentSecond = "No one";
            double answer = GetTwoThirdOfAverage();

            foreach (var submission in playersAndTheirNumbers)
            {
                double delta = Math.Abs(submission.Number - answer);
                if (delta < currentSmallestDelta && submission.Name != GetWinner())
                {
                    currentSecond = submission.Name;
                    currentSmallestDelta = delta;
                }
            }
            return currentSecond;
        }

        public static string GetThird()
        {
            if (GAME_STATUS == Status.IN_PROGRESS)
                return "No one";

            double currentSmallestDelta = double.MaxValue;
            string currentThird = "No one";
            double answer = GetTwoThirdOfAverage();

            foreach (var submission in playersAndTheirNumbers)
            {
                double delta = Math.Abs(submission.Number - answer);
                if (delta < currentSmallestDelta && submission.Name != GetWinner() && submission.Name != GetSecond())
                {
                    currentThird = submission.Name;
                    currentSmallestDelta = delta;
                }
            }
            return currentThird;
        }

        public static int GetNumberOfSubmissions()
        {
            if (GAME_STATUS == Status.IN_PROGRESS)
                return 0;
            return playersAndTheirNumbers.Count();
        }

        public static double GetValueThisPersonSubmitted(string v)
        {
            return playersAndTheirNumbers.Where(x => x.Name.Equals(v)).FirstOrDefault().Number;
        }

        public static void Reset()
        {
            playersAndTheirNumbers.Clear();
            GAME_STATUS = Status.IN_PROGRESS;
        }

        public static List<string> GetPlayerList()
        {
            return playersAndTheirNumbers.Select(x => x.Name).ToList();
        }

        public static void ReleaseResults()
        {
            GAME_STATUS = Status.STOPPED;
        }

        public static Status GetGameStatus()
        {
            return GAME_STATUS;
        }
    }
}