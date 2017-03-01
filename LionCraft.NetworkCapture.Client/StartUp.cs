using LionCraft.NetworkCapture.Core.Models.Managers;
using System;

namespace LionCraft.NetworkCapture.Client
{
    public class StartUp
    {
        static void Main()
        {
            new CaptureManager().Capture(Console.WriteLine);
        }
    }
}
