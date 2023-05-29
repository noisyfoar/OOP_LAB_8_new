using OOP_LAB_8.Decorators;
using OOP_LAB_8.figures;
using OOP_LAB_8.Observer;

namespace OOP_LAB_8.factory
{
    public abstract class ShapeArray : Element
    {
        public List<Shape> shapes = new List<Shape>();

        public void loadShapes(string filename, ShapeFactory factory)
        {

            if (File.Exists(filename))
            {
                using StreamReader reader = new StreamReader(filename);


                Shape shape;
                string line;
                int count;
                count = Convert.ToInt32(reader.ReadLine());

                for (int i = 0; i < count; ++i)
                {
                    line = reader.ReadLine();
                    shape = factory.create(Convert.ToChar(line));
                    if (shape != null)
                    {
                        shape.load(reader, factory);
                        shapes.Add(shape);
                    }
                }
            }
        }

        public void saveShapes(string filename)
        {
            using StreamWriter writer = new StreamWriter(filename);
            writer.WriteLine(shapes.Count);
            for (int i = 0; i < shapes.Count; ++i)
            {
                shapes[i].save(writer);
            }
        }
        public override void addObserver(Element observer)
        {
            _observers.Add(observer);
        }
        public override void removeObserver(Element observer)
        {
            _observers.Remove(observer);
        }
        public override void notifyObservers()
        {
            foreach(Element observer in _observers)
            {
                observer.update(this);
            }
        }
    }
}
