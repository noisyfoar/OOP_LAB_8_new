namespace OOP_LAB_8.Observer
{
    public abstract class Element
    {
        public List<Element> _observers = new List<Element>();
        public Element()
        {
            _observers = new List<Element>();
        }

        public abstract void addObserver(Element observer);
        public abstract void removeObserver(Element observer);
        public virtual void notifyObservers()
        {
            foreach (Element observer in _observers)
            {
                observer.update(this);
            }
        }
        public abstract void update(Element subject);
    }
}
