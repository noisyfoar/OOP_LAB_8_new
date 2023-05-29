using OOP_LAB_8.Base;
using OOP_LAB_8.figures;


namespace OOP_LAB_8.figures
{
    public class CTriangle : Shape
        {
            public Help_vector[] vectors { get; set; }
            public Point[] points { get; set; }

            public CTriangle() : base()
            {

                vectors = new Help_vector[6];
                points= new Point[3];
                name = CONST_SHAPE.Triangle;
            }

            public CTriangle(Shape shape) : base(shape)
            {
                vectors = new Help_vector[6];
                points = new Point[3];

                name = CONST_SHAPE.Triangle;
            }


            public override void Draw(Graphics g)
            {
                points[0] = new Point(p0.X + shapeSize.Width / 2, p0.Y); // A vertex
                points[1] = new Point(p0.X, p0.Y + shapeSize.Height); // B vertex
                points[2] = new Point(p0.X + shapeSize.Width, p0.Y + shapeSize.Height); // C vertex

                Pen pen = new Pen(Color.Black);
                Brush brush = new SolidBrush(color);
                g.DrawPolygon(pen, points);
                g.FillPolygon(brush, points);
            }
            public override bool inShape(int x, int y)
            {
                Point O = new Point(x, y); // Point O - into or out triangle

                vectors[0] = new Help_vector(O, points[0]);
                vectors[1] = new Help_vector(O, points[1]);
                vectors[2] = new Help_vector(O, points[2]);
                vectors[3] = new Help_vector(points[0], points[1]);
                vectors[4] = new Help_vector(points[1], points[2]);
                vectors[5] = new Help_vector(points[2], points[0]);

                int OA_AB = vectors[0] * vectors[3];
                int OB_BC = vectors[1] * vectors[4];
                int OC_CA = vectors[2] * vectors[5];

                if (((OA_AB < 0) && (OB_BC < 0) && (OC_CA < 0)) || ((OA_AB > 0) && (OB_BC >0) && (OC_CA > 0))) 
                    return true;
                return false;
            }
        }
    }

