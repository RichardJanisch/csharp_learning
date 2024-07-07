using System;
using System.Threading.Tasks;

namespace MeinErstesProjekt
{
    public class LongRunningOperation
    {
        public async Task<int> PerformCalculationAsync()
        {
            // Simuliere eine rechenintensive Berechnung
            return await Task.Run(() =>
            {
                int result = 0;
                for (int i = 0; i < 1000000000; i++)
                {
                    result += i;
                }
                return result;
            });
        }
    }
}
