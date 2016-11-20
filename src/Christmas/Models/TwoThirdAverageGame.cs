using System;
using System.Collections.Generic;
using System.Linq;

namespace Christmas.Models
{
    public class TwoThirdAverageGame
    {
        private static Dictionary<string, double> playersAndTheirNumbers = new Dictionary<string, double>();
        private static readonly double MIN_VALUE = 0.0;
        private static readonly double MAX_VALUE = 100.0;

        public static void Submit(string name, double submission)
        {
            if (name != null && isWithinValidRange(submission))
            {
                string trimmedName = System.Text.RegularExpressions.Regex.Replace(name.Trim(), @"\s+", " ");

                if (!string.IsNullOrEmpty(trimmedName))
                    playersAndTheirNumbers[trimmedName] = submission;
            }
        }

        public static object GetSubmissions()
        {
            return playersAndTheirNumbers;
        }

        private static bool isWithinValidRange(double submission)
        {
            return (submission >= MIN_VALUE && submission <= MAX_VALUE);
        }

        public static double GetTwoThirdOfAverage()
        {
            if (playersAndTheirNumbers == null || playersAndTheirNumbers.Count == 0)
                return 0;

            return playersAndTheirNumbers.Values.Average() * 2.0 / 3.0;
        }

        public static string GetWinner()
        {
            double currentSmallestDelta = double.MaxValue;
            string currentLeader = "No one";
            double answer = GetTwoThirdOfAverage();

            foreach (string name in playersAndTheirNumbers.Keys)
            {
                double delta = Math.Abs(playersAndTheirNumbers[name] - answer);
                if (delta < currentSmallestDelta)
                {
                    currentSmallestDelta = delta;
                    currentLeader = name;
                }
            }
            return currentLeader;
        }

        public static int GetNumberOfSubmissions()
        {
            return playersAndTheirNumbers.Count();
        }

        public static double GetValueThisPersonSubmitted(string v)
        {
            return playersAndTheirNumbers[v];
        }

        public static void Reset()
        {
            playersAndTheirNumbers = new Dictionary<string, double>();
        }

        public static List<string> GetPlayerList()
        {
            return playersAndTheirNumbers.Keys.ToList();
        }
    }
}