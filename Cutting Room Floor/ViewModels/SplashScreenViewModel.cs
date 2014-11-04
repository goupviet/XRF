using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Xrf.ViewModels
{
    public class SplashScreenViewModel
    {
        private List<string> SplashLines;
        private Random Rng;

        public string RandomSplashLine
        {
            get
            {
                var maxIndex = SplashLines.Count - 1;
                return SplashLines[Rng.Next(0, maxIndex)];
            }
        }

        public SplashScreenViewModel()
        {
            string resource_data = Properties.Resources.Splash;
            SplashLines = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Rng = new Random();
        }
    }
}
