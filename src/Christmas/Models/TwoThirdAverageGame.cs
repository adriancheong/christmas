using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Christmas.Models
{
    public class TwoThirdAverageGame
    {
        private static List<Submission> playersAndTheirNumbers = new List<Submission>();
        private static readonly double MIN_VALUE = 0.0;
        private static readonly double MAX_VALUE = 100.0;
        private static bool RESULTS_LOCKED = true;

        public static void Submit(string name, double submission)
        {
            if (name != null && isWithinValidRange(submission))
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
            if (RESULTS_LOCKED)
                return new List<Submission>();

            return playersAndTheirNumbers.ToList();
        }

        private static bool isWithinValidRange(double submission)
        {
            return (submission >= MIN_VALUE && submission <= MAX_VALUE);
        }

        public static double GetTwoThirdOfAverage()
        {
            if (RESULTS_LOCKED || playersAndTheirNumbers == null || playersAndTheirNumbers.Count == 0)
                return 0;

            return playersAndTheirNumbers.Average(x => x.Number) * 2.0 / 3.0;
        }

        public static string GetWinner()
        {
            if (RESULTS_LOCKED)
                return "No one";

            double currentSmallestDelta = double.MaxValue;
            string currentLeader = "No one";
            double answer = GetTwoThirdOfAverage();

            foreach (var submission in playersAndTheirNumbers)
            {
                double delta = Math.Abs(submission.Number - answer);
                if (delta < currentSmallestDelta)
                {
                    currentSmallestDelta = delta;
                    currentLeader = submission.Name;
                }
            }
            return currentLeader;
        }

        public static int GetNumberOfSubmissions()
        {
            if (RESULTS_LOCKED)
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
            RESULTS_LOCKED = true;
        }

        public static List<string> GetPlayerList()
        {
            return playersAndTheirNumbers.Select(x => x.Name).ToList();
        }

        public static void ReleaseResults()
        {
            RESULTS_LOCKED = false;
        }
    }
}