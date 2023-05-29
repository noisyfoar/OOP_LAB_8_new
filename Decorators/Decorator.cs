using OOP_LAB_8.factory;
using OOP_LAB_8.figures;
using System.Drawing.Drawing2D;

namespace OOP_LAB_8.Decorators
{
    public abstract class Decorator : Shape
    {
        protected Shape decoratedShape;
        protected List<Shape> subjects;

        public Decorator(Shape new_shape) : base(new_shape)
        {
            subjects = new List<Shape>();
            if (new_shape is Decorator)
            {
                decoratedShape = ((Decorator)new_shape).decoratedShape;
                subjects = ((Decorator)(new_shape)).subjects;
            }
            else
            {
                decoratedShape = new_shape;
            }
            getInfoFromShape();
        }
        public override void Draw(Graphics g)
        {

            Pen pen1 = new Pen(Color.White);
            pen1.CustomEndCap = new AdjustableArrowCap(6, 6);
            Point _from, _to;
            
            foreach (Shape subject in subjects)
            {
                _to = new Point(decoratedShape.getPoint().X + decoratedShape.getSize().Width/2, decoratedShape.getPoint().Y + decoratedShape.getSize().Height/2);
                _from = new Point(subject.getPoint().X + subject.getSize().Width/2, subject.getPoint().Y + subject.getSize().Height/2);
                g.DrawLine(pen1, _from, _to);
            }
        }
        public override void addObserver(Element observer)
        {
            if (!_observers.Contains(observer))
            {
                decoratedShape.addObserver(observer);
            }
        }
        public override void removeObserver(Element observer)
        {
            if (!_observers.Contains(observer))
            {
                decoratedShape.removeObserver(observer);
            }
        }
        public override void notifyObservers()
        {
            decoratedShape.notifyObservers();
        }

        public override void update(Element subject)
        {
            if(subject is Shape)
            {

                decoratedShape.update(subject);
                if (!subjects.Contains(subject))
                {
                    subjects.Add(((Shape)(subject)));
                }
                getInfoFromShape();
            }
        }
        public Shape getShape()
        {
            return decoratedShape;
        }
        public override void load(StreamReader reader, ShapeFactory factory)
        {
            decoratedShape.load(reader, factory);
        }

        public override void save(StreamWriter writer)
        {
            decoratedShape.save(writer);
        }

        protected void getInfoFromShape()
        {
            shapeSize = decoratedShape.getSize();
            p0 = decoratedShape.getPoint();
        }
        public override void move(int dx, int dy)
        {
            decoratedShape.move(dx, dy);
            getInfoFromShape();
        }
        public override void resize(int x1, int y1, int x2, int y2)
        {
            decoratedShape.resize(x1, y1, x2, y2);
            getInfoFromShape();
        }

        public override void resize(int delta)
        {
            decoratedShape.resize(delta);
            getInfoFromShape();
        }
        public override void setColor(Color new_color)
        {
            decoratedShape.setColor(new_color);
        }

        public override CONST_SHAPE getName()
        {
            return decoratedShape.getName();
        }

        public override bool inShape(int x, int y)
        {
            return decoratedShape.inShape(x, y);
        }
    }
}
