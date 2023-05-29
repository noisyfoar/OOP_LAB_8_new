namespace OOP_LAB_8.figures
{
    public class CRectangle : Shape
    {

        public CRectangle() : base()
        {
            name = CONST_SHAPE.Rectangle;
        }
        public CRectangle(Shape shape) : base(shape)
        {
            name = CONST_SHAPE.Rectangle;
        }


        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(color);

            Rectangle rect = new Rectangle(p0, shapeSize);

            g.DrawRectangle(pen, rect);
            g.FillRectangle(brush, rect);
        }
        public override bool inShape(int x, int y)
        {
            if((x >= p0.X) && ( y >= p0.Y))
                if((x <= p0.X + shapeSize.Width ) && (y <= p0.Y + shapeSize.Height))
                    return true;
            return false;
        }
    }
}
