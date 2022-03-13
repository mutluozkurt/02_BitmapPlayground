using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class MovingAverageFilter : IFilter
    {

        public Color[,] Apply(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            Color[,] result = new Color[width, height];

            Parallel.For(0, width, x =>
            {
                Parallel.For(0, height, y =>
                {
                    var p = input[x, y];
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        result[x, y] = Color.FromArgb(p.A, p.R, p.G, p.B);
                    }
                    else
                    {
                        result[x, y] = GetAverage(input, x, y);
                    }
                });
            });

            return result;
        }

        public Color GetAverage(Color[,] _input, int x, int y)
        {
            var p = _input[x, y];
            var pleft = _input[(x - 1), y];
            var pright = _input[(x + 1), y];
            var pabove = _input[x, (y - 1)];
            var pbellow = _input[x, (y + 1)];

            int avgr = (pleft.R + pright.R + pabove.R + pbellow.R) / 4;
            int avgg = (pleft.G + pright.G + pabove.G + pbellow.G) / 4;
            int avgb = (pleft.B + pright.B + pabove.B + pbellow.B) / 4;

            return Color.FromArgb(p.A, avgr, avgg, avgb);

        }

        public string Name => "Moving Average filter";

        public override string ToString()
            => Name;


    }
}
