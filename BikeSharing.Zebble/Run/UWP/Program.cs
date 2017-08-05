using System;

namespace UWP
{
    class Program
    {
        public static void Main()
        {
            Zebble.UIRuntime.GetEntryAssembly = () => typeof(Program).GetAssembly();

            Windows.UI.Xaml.Application.Start((p) => new App());
        }
    }
}