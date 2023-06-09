﻿using OOP_LAB_8.Base;
using OOP_LAB_8.Decorators;
using OOP_LAB_8.factory;
using OOP_LAB_8.figures;

namespace OOP_LAB_8.Observer
{

    public class TreeObserver : Element
    {
        private TreeView tv;


        public TreeObserver(TreeView treeView)
        {
            tv = treeView;
        }


        public void processNode(TreeNode tn, Shape shape)
        {
            if (shape.getName() == CONST_SHAPE.Group)
            {
                foreach (Shape obj in ((CGroup)((Decorator)shape).getShape()).shapes)
                {
                    if(obj.getName() == CONST_SHAPE.Group)
                    {
                        TreeNode new_node = new TreeNode(CONST_SHAPE.Group.ToString());
                        tn.Nodes.Add(new_node);
                        processNode(new_node, obj);
                    }
                    else
                    {
                        processNode(tn, obj);
                    }
                }
            }
            else
            {
                tn.Nodes.Add(shape.getName().ToString());
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
        public TreeNode getNode(int index)
        {
            return tv.Nodes[index];
        }
        public override void update(Element subject)
        {
            tv.Nodes.Clear();
            tv.Text = "ShapeArray";
            foreach (Shape shape in ((ShapeArray)subject).shapes)
            {
                TreeNode new_node = new TreeNode(shape.getName().ToString());
                if (shape is Marked)
                {
                    new_node.Checked = true;
                }
                else
                {
                    new_node.Collapse();
                }
                if (shape.getName() == CONST_SHAPE.Group)
                {
                    processNode(new_node, shape);
                }
                tv.Nodes.Add(new_node);
            }
        }
    }
}