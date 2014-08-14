using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGeneration
{
    class AVertexMap
    {
        public double?[,] map { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        double noise;
        int seed;
        int smooth;
        int levels;


        public AVertexMap(int width, int heigth, double noise, int seed, int smooth, int levels)
        {
            this.width = width;
            this.height = heigth;
            map = new double?[width, heigth];

            this.noise = noise;
            this.seed = seed;
            this.smooth = smooth;
            this.levels = levels;

            generate();
        }

        void generate()
        {
            clear();
            diamond_square();
            simple_smooth(smooth);
            translate_range(0, levels - 1);
            round();
        }

        void clear()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = null;
                }
            }
        }

        public double? get(int x, int y)
        {
            int nx = x;
            nx = x >= width ? 2 * (width - 1) - x : nx;
            nx = x < 0 ? Math.Abs(x) : nx;

            int ny = y;
            ny = y >= height ? 2 * (height - 1) - y : ny;
            ny = y < 0 ? Math.Abs(y) : ny;

            return map[nx, ny];  //return point to avalibale area
        }

        public double? get(Point p)
        {
            return get(p.X, p.Y);
        }

        void set(int x, int y, double? value)
        {
            map[x, y] = value;
        }

        void set(Point p, double? value)
        {
            set(p.X, p.Y, value);
        }

        void find_range(ref double min, ref double max)
        {
            min = map[0, 0].Value;
            max = map[0, 0].Value;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j].Value < min)
                    {
                        min = map[i, j].Value;
                    }
                    if (map[i, j].Value > max)
                    {
                        max = map[i, j].Value;
                    }
                }
        }

        public void translate_range(double new_min, double new_max)
        {
            double min = 0, max = 0;
            find_range(ref min, ref max);

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = (map[i, j].Value - min) / (max - min) * (new_max - new_min) + new_min;
                }
        }

        void round()
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = Math.Round(map[i, j].Value);
                }
        }

        void simple_smooth(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                simple_smooth_iteration();
            }
        }

        void simple_smooth_iteration()
        {
            double?[,] smooth_map = new double?[width, height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    double tmp = 0;
                    for (int a = -1; a < 2; a++)
                        for (int b = -1; b < 2; b++)
                        {
                            tmp += get(i + a, j + b).Value;
                        }
                    tmp /= 9;
                    smooth_map[i, j] = tmp;
                }

            map = smooth_map;
        }

        public Bitmap to_image()
        {
            Bitmap img = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    int c = map[i, j] == null ? 255 : 255 - (int)map[i, j].Value;
                    img.SetPixel(i, j, Color.FromArgb(c, c, c));
                }

            return img;
        }

        void diamond_square()
        {

            map[0, 0] = 0;
            map[width - 1, 0] = 0;
            map[0, height - 1] = 0;
            map[width - 1, height - 1] = 0;

            Random rand = new Random(seed);

            for (int i = (width - 1) / 2; i != 0; i /= 2)
            {
                deamond_square_iteration(i, rand);
            }

        }

        void deamond_square_iteration(int step, Random rand)
        {
            for (int i = 0; i < width; i += step)   //step 1, mid of cell generation
                for (int j = 0; j < height; j += step)
                    if ((i + j) % (2 * step) == 0 && get(i, j) == null)
                    {
                        double? value =
                            (get(i - step, j - step) +
                            get(i - step, j + step) +
                            get(i + step, j - step) +
                            get(i + step, j + step)) / 4 + (rand.NextDouble() * 2 - 1) * noise;

                        set(i, j, value);
                    }

            for (int i = 0; i < width; i += step)   //step 2, mids of edges generation
                for (int j = 0; j < height; j += step)
                    if ((i + j) % (2 * step) != 0 && get(i, j) == null)
                    {
                        double? value =
                            (get(i, j - step) +
                            get(i, j + step) +
                            get(i + step, j) +
                            get(i - step, j)) / 4 + (rand.NextDouble() * 2 - 1) * noise;

                        set(i, j, value);
                    }
        }

    }

}
