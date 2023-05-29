using OOP_LAB_8.Decorators;
using OOP_LAB_8.Observer;
using System;

namespace OOP_LAB_8.factory
{
    public class MyShapeArray : ShapeArray
    {
        public override void update(Element subject)
        {
            TreeObserver treeObserver = (TreeObserver)subject;
            for (int i = 0; i < shapes.Count; ++i)
            {
                if (treeObserver.getNode(i).Checked)
                {
                    shapes[i] = new Marked(shapes[i]);
                }
                else
                {
                    shapes[i] = new UnMarked(shapes[i]);
                }
            }
        }
    }
}
