namespace Bebone.Sample
{
    class Program
    {
        public static void Main()
        {
            var app = new Application();

            app.PreInit();
            app.Init();
            app.Load();
            app.Run();
            app.Unload();
        }
    }
}