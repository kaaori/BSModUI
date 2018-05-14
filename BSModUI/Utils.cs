﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BSModUI
{
    internal class Utils
    {
        // VERY IMPORTANT DO NOT REMOVE
        public static void BogoSort<T>(ref List<T> data) where T : IComparable
        {
            while (!IsSorted(ref data))
                Shuffle(ref data);
        }

        // Useful Utils
        public static void Log(string message, Severity severity = Severity.Log)
        {
            switch (severity)
            {
                case Severity.Log:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Severity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Severity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine("[" + severity + "] Mod Menu: " + message);
            UnityEngine.Debug.Log("Mod Menu: " + message);
            Console.ResetColor();
        }

        public enum Severity
        {
            Log,
            Warning,
            Error
        }

        // Memes
        private static bool IsSorted<T>(ref List<T> data) where T : IComparable
        {
            if (data.Count <= 1)
                return true;
            for (int i = 1; i < data.Count; i++)
                if (data[i].CompareTo(data[i - 1]) < 0) return false;
            return true;
        }

        private static void Shuffle<T>(ref List<T> data)
        {
            var rand = new Random();

            for (int i = 0; i < data.Count; ++i)
            {
                var rnd = rand.Next(data.Count);
                var temp = data[i];
                data[i] = data[rnd];
                data[rnd] = temp;
            }
        }
    }
}
