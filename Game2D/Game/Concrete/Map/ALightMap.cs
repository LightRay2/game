using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGeneration
{
    class ADirection
    {
        public const int Top = 0;
        public const int Right = 1;
        public const int Bottomn = 2;
        public const int Left = 3;

        public static char direction_to_char(int direction)
        {
            switch (direction)
            {
                case 0:
                    return 'u';
                case 1:
                    return 'r';
                case 2:
                    return 'd';
                case 3:
                    return 'l';
                default:
                    return '-';
            }
        }
    }

    class ALight
    {
        public const double ground = 1;
        public const double sun = 1.4;
        public const double almost_sun = 1.2;
        public const double rib = 0.9;  //todo realistic light
        public const double almost_shadow = 0.8;
        public const double shadow = 0.6;
    }

    class APolygon
    {
        APoint3[] coordinates;
        AVector3 normal;
        public double light { get; set; }

        public APolygon(APoint3 p1, APoint3 p2, APoint3 p3)
        {
            coordinates = new APoint3[3] { p1, p2, p3 };

            calculate_normal_from_coordinates();
            calculate_light_from_normal_old();
        }

        void calculate_normal_from_coordinates()
        {
            AVector3 v1 = new AVector3(coordinates[0], coordinates[1]);
            AVector3 v2 = new AVector3(coordinates[0], coordinates[2]);

            AVector3 result = AVector3.perpendicular(v1, v2);
            result.turn_up();

            normal = result;
        }

        void calculate_light_from_normal_experimental() //todo dobit- ili udalit-
        {
            if (normal.x == 0 && normal.y == 0) //up
            {
                light = 1;
                return;
            }

            double k = Math.Atan2(normal.y, normal.x) / (2 * Math.PI);
            k = k > 1 ? 2 - k : k;
            light = k + 0.5;
        }

        void calculate_light_from_normal_old()
        {
            if (normal.x == 0 && normal.y == 0) { light = ALight.ground; }  //up
            if (normal.x < 0 && normal.y < 0) { light = ALight.sun; }  //sun
            if ((normal.x == 0 && normal.y < 0) || (normal.x < 0 && normal.y == 0)) { light = ALight.almost_sun; }    //almost sun
            if ((normal.x < 0 && normal.y > 0) || (normal.x > 0 && normal.y < 0)) { light = ALight.rib; }  //rib
            if ((normal.x == 0 && normal.y > 0) || (normal.x > 0 && normal.y == 0)) { light = ALight.almost_shadow; }    //almost shadow
            if (normal.x > 0 && normal.y > 0) { light = ALight.shadow; }  //shadow
        }

        public Point[] to_point_array(int cell_width, int cell_height)
        {
            return new Point[3] {
                new Point(Convert.ToInt32(coordinates[0].x * cell_width), Convert.ToInt32(coordinates[0].y * cell_height)),
                new Point(Convert.ToInt32(coordinates[1].x * cell_width), Convert.ToInt32(coordinates[1].y * cell_height)),
                new Point(Convert.ToInt32(coordinates[2].x * cell_width), Convert.ToInt32(coordinates[2].y * cell_height))
            };
        }

    }

    class ALightMap
    {
        public int width;
        public int height;
        APolygon[] polygons;

        AVertexMap v_map;

        public ALightMap(AVertexMap v_map)
        {
            this.v_map = v_map;
            this.width = v_map.width - 1;
            this.height = v_map.height - 1;
            this.polygons = new APolygon[width * height * 4];

            generate_polygons();
        }

        public Bitmap to_image(int cell_width, int cell_height)
        {
            Bitmap b = new Bitmap(width * cell_width, height * cell_width);
            Graphics g = Graphics.FromImage(b);

            for (int i = 0; i < polygons.Length; i++ )
            {
                int c = 255 - (int)Math.Round(100 * (2 - polygons[i].light));
                g.FillPolygon(new SolidBrush(Color.FromArgb(255, c, c, c)), polygons[i].to_point_array(cell_width, cell_height));
            }

            g.Dispose();
            return b;
        }

        void generate_polygons()
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    APoint3 mid = new APoint3(i + 0.5, j + 0.5,
                        (v_map.get(i, j).Value +
                        v_map.get(i + 1, j).Value +
                        v_map.get(i, j + 1).Value +
                        v_map.get(i + 1, j + 1).Value ) / 4);

                    mid.z = mid.z % 0.5 == 0 ? mid.z : Math.Round(mid.z);

                    set(i, j, ADirection.Top, new APolygon(
                        new APoint3(i, j, v_map.get(i, j).Value),
                        new APoint3(i + 1, j, v_map.get(i + 1, j).Value),
                        mid));

                    set(i, j, ADirection.Right, new APolygon(
                        new APoint3(i + 1, j, v_map.get(i + 1, j).Value),
                        new APoint3(i + 1, j + 1, v_map.get(i + 1, j + 1).Value),
                        mid));

                    set(i, j, ADirection.Bottomn, new APolygon(
                        new APoint3(i + 1, j + 1, v_map.get(i + 1, j + 1).Value),
                        new APoint3(i, j + 1, v_map.get(i, j + 1).Value),
                        mid));

                    set(i, j, ADirection.Left, new APolygon(
                        new APoint3(i, j + 1, v_map.get(i, j + 1).Value),
                        new APoint3(i, j, v_map.get(i, j).Value),
                        mid));
                }
        }

        void set(int x, int y, int direction, APolygon polygon)
        {
            polygons[(x * height + y) * 4 + direction] = polygon;
        }

        public APolygon get(int x, int y, int direction)
        {
            return polygons[(x * height + y) * 4 + direction];
        }
    }
}
