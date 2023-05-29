using OOP_LAB_8.Base;
using OOP_LAB_8.Decorators;
using OOP_LAB_8.factory;
using OOP_LAB_8.figures;
using OOP_LAB_8.Observer;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OOP_LAB_8
{

    public partial class Form1 : Form
    {
        Shape createdShape;

        CONST_SHAPE selectedShape;
        ShapeFactory shapeFactory;
        TreeObserver treeObserver;

        Color color;
        Graphics g;


        int CursorX, CursorY;
        bool create;
        bool arrow;
        string filename = "";

        int index_arrow_from = 0;

        ShapeArray shapeArray;


        public Form1()
        {
            shapeArray = new MyShapeArray();
            shapeFactory = new MyShapeFactory();
            InitializeComponent();

            panel1.MouseUp += Form1_MouseUp;
            panel1.MouseDown += Form1_MouseDown;
            panel1.MouseMove+= Form1_MouseMove;


            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.Multiselect = false;

            color = Color.Black;
            pictureBox_color.Refresh();

            selectedShape = CONST_SHAPE.Circle;

            shapeArray = new MyShapeArray();
            treeObserver = new TreeObserver(treeView1);

            treeObserver.addObserver(shapeArray);
            shapeArray.addObserver(treeObserver);

            CursorX = 0;
            CursorY = 0;


            g = panel1.CreateGraphics();
            DoubleBuffered = true;
        }
        
        public void createShape(CONST_SHAPE choosenShape)
        {
            createdShape = shapeFactory.create((char)choosenShape);
            createdShape.setColor(color);
            createdShape.setImageSize(panel1.Size);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            CursorX = e.X; CursorY = e.Y;
            create = true;
            arrow = false;
            for(int i = 0; i < shapeArray.shapes.Count; ++i)
            {
                if (shapeArray.shapes[i].inShape(CursorX, CursorY))
                {
                    create = false;
                    if (Form.ModifierKeys == Keys.Shift)
                    {
                        index_arrow_from = i;
                        Shape temp = shapeArray.shapes[i];
                        CursorX = temp.getPoint().X + temp.getSize().Width/2; CursorY= temp.getPoint().Y + temp.getSize().Height/2;
                        arrow = true;
                    }
                    if (Form.ModifierKeys != Keys.Control)
                    {
                        for (int j = 0; j < shapeArray.shapes.Count; ++j)
                        {
                            shapeArray.shapes[j] = new UnMarked(shapeArray.shapes[j]);
                        }
                    }
                    if (shapeArray.shapes[i] is not Marked)
                    {
                        shapeArray.shapes[i] = new Marked(shapeArray.shapes[i]);
                    }
                }
            }
            if(create)
            {
                if (Form.ModifierKeys != Keys.Control)
                {
                    for (int j = 0; j < shapeArray.shapes.Count; ++j)
                    {
                        shapeArray.shapes[j] = new UnMarked(shapeArray.shapes[j]);
                    }
                }
                createShape(selectedShape);
                createdShape = new Marked(createdShape);
                shapeArray.shapes.Add(createdShape);
            }
            shapeArray.notifyObservers();
            panel1.Invalidate();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (create) 
            { 
                createdShape.resize(CursorX, CursorY, e.X, e.Y);
                panel1.Invalidate();
            }
            if (arrow)
            {
                drawLineTo(e.X, e.Y);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(create)
            {
                create = false;
            }
            if (arrow)
            {
                for(int i = 0; i < shapeArray.shapes.Count; ++i)
                {
                    if (!shapeArray.shapes[i].inShape(CursorX, CursorY) && shapeArray.shapes[i].inShape(e.X, e.Y) )
                    {
                        shapeArray.shapes[index_arrow_from].addObserver(shapeArray.shapes[i]);
                    }
                }

            }
            arrow = false;
            panel1.Invalidate();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int dx = 0, dy = 0, delta = 0;
            switch (e.KeyCode)
            {
                case Keys.W:
                    dy = -10;break;
                case Keys.S:
                    dy = 10;break;
                case Keys.D:
                    dx = 10;break;
                case Keys.A:
                    dx = -10;break;
                case Keys.G:
                    delta = 10;break;
                case Keys.L:
                    delta = -10;break;
                case Keys.Delete:
                    shapeArray.shapes.Clear();
                    shapeArray.notifyObservers();
                    break;
            }
            if(dx != 0 || dy != 0)
            {
                foreach (Shape temp in shapeArray.shapes)
                {
                    if(temp is Marked)
                        temp.move(dx, dy);
                }
                dx = 0; dy = 0;
            }
            if(delta != 0)
            {
                foreach (Shape temp in shapeArray.shapes)
                {
                    if(temp is Marked)
                        temp.resize(delta);
                }
                delta = 0;
            }

            panel1.Invalidate();
        }



        private void button_group_Click(object sender, EventArgs e)
        {
            List<Shape> to_keep = new List<Shape>();

            createShape(CONST_SHAPE.Group);
            createdShape = new Marked(createdShape);
            for(int i = 0; i < shapeArray.shapes.Count; i++)
            {
                if (shapeArray.shapes[i] is Marked)
                {
                    ((CGroup)((Decorator)createdShape).getShape()).add(panel1.Size,shapeArray.shapes[i]);
                }
                else
                {
                    shapeArray.shapes[i] = new UnMarked(shapeArray.shapes[i]);
                    to_keep.Add(shapeArray.shapes[i]);
                }
            }
            shapeArray.shapes.Clear();

            for (int i = 0; i < to_keep.Count; i++)
            {
                shapeArray.shapes.Add(to_keep[i]);
            }
            createdShape = new Marked(createdShape);
            shapeArray.shapes.Add(createdShape);
            to_keep.Clear();

            shapeArray.notifyObservers();
            panel1.Invalidate();
        }
        private void button_ungroup_Click(object sender, EventArgs e)
        {
            List<Shape> to_remove = new List<Shape>();
            List<Shape> to_add = new List<Shape>();
            for(int i = 0; i < shapeArray.shapes.Count; i++)
            {
                if (shapeArray.shapes[i].getName() == CONST_SHAPE.Group)
                {
                    to_remove.Add(shapeArray.shapes[i]);
                    for (int j = 0; j < ((CGroup)((Decorator)shapeArray.shapes[i]).getShape()).shapes.Count; ++j)
                    {
                        Shape temp = new Marked(((CGroup)((Decorator)shapeArray.shapes[i]).getShape()).shapes[j]);
                        to_add.Add(temp);
                    }
                    ((CGroup)((Decorator)shapeArray.shapes[i]).getShape()).shapes.Clear();

                }
            }
            foreach(Shape shape in to_remove)
            {
                shapeArray.shapes.Remove(shape);
            }
            foreach(Shape shape in to_add)
            {
                shapeArray.shapes.Add(shape);
            }

            shapeArray.notifyObservers();
            panel1.Invalidate();
        }


        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            pictureBox_color.Refresh();
        }
        private void button_setColor_Click(object sender, EventArgs e)
        {
            foreach (Shape temp in shapeArray.shapes)
            {
                if(temp is Marked)
                {
                    temp.setColor(color);
                }
            }
            panel1.Invalidate();
        }
        private void pictureBox_color_Paint(object sender, PaintEventArgs e)
        {
            pictureBox_color.BackColor = color;
        }
        private void toolStripTextBoxTriangle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Triangle;
        }
        private void toolStripTextBoxCircle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Circle;
        }
        private void toolStripTextBoxRectangle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Rectangle;
        }
        private void drawLineTo(int x1, int y1)
        {
            Pen pen = new Pen(Color.White);
            pen.CustomEndCap = new AdjustableArrowCap(6, 6);
            g.DrawLine(pen, CursorX, CursorY, x1, y1);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(SystemColors.GrayText);

            foreach (Shape c in shapeArray.shapes)
            {
                c.Draw(g);
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            foreach (Shape shape in shapeArray.shapes)
            {
                shape.setImageSize(panel1.Size);
                shape.move(0,0);
                shape.resize(0);
            }

            panel1.Invalidate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            shapeArray.shapes.Clear();
            filename = openFileDialog1.FileName;
            shapeArray.loadShapes(filename, shapeFactory);
            panel1.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(shapeArray.shapes.Count > 0)
            {
                if(filename == "")
                {
                    if(saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    filename = saveFileDialog1.FileName;
                }
                shapeArray.saveShapes(filename);
            }
        }
        
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            treeObserver.notifyObservers();
            panel1.Invalidate();
        }

        private void button_deleteSelected_Click(object sender, EventArgs e)
        {
            List<Shape> temp = new List<Shape>();

            foreach(Shape shape in shapeArray.shapes)
            {
                if(shape is UnMarked)
                {
                    temp.Add(shape);
                }
            }
            shapeArray.shapes = temp;
            shapeArray.notifyObservers();
            panel1.Invalidate();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}