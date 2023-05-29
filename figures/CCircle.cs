namespace OOP_LAB_8.figures
{
    public class CCircle : Shape
    {
        public CCircle() : base()
        {
            name = CONST_SHAPE.Circle;
        }
        public CCircle(Shape shape) : base(shape)
        {
            name = CONST_SHAPE.Circle;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Brush brush= new SolidBrush(color);

            g.FillEllipse(brush, p0.X, p0.Y, shapeSize.Width, shapeSize.Height);
            g.DrawEllipse(pen, p0.X, p0.Y, shapeSize.Width, shapeSize.Height);
        }
        public override bool inShape(int x, int y)
        {
            double p = ((double)Math.Pow(p0.X + shapeSize.Width/2 - x, 2) / (double)Math.Pow(shapeSize.Width/2, 2)) +
                ((double)Math.Pow(p0.Y + shapeSize.Height/2 - y, 2) / (double)Math.Pow(shapeSize.Height/2, 2));

            if (p <= 1)
                return true;

            return false;
        }
    }
}
