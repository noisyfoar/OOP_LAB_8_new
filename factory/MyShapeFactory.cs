using OOP_LAB_8.figures;

namespace OOP_LAB_8.factory
{
    public class MyShapeFactory : ShapeFactory
    {
        public override Shape create(char type)
        {
            switch (type)
            {
                case (char)CONST_SHAPE.Circle:
                    return new CCircle();

                case (char)CONST_SHAPE.Rectangle:
                    return new CRectangle();

                case (char)CONST_SHAPE.Triangle:
                    return new CTriangle();

                case (char)CONST_SHAPE.Group:
                    return new CGroup();

                default:
                    return new CCircle();
            }
        }
    }
}
