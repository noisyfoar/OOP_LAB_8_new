using OOP_LAB_8.figures;

namespace OOP_LAB_8.Decorators
{
    public class Marked : Decorator
    {
        public Marked(Shape new_shape) : base(new_shape)
        {
            name = CONST_SHAPE.Marked;
        }

        public override void Draw(Graphics g)
        {
            decoratedShape.Draw(g);
            Rectangle rect = new Rectangle(p0, shapeSize);
            Pen pen = new Pen(Color.Red);
            g.DrawRectangle(pen, rect);
            base.Draw(g);
        }
    }
}
