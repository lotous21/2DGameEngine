using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class TreeDataStruct : IEnumerable<Node>
    {
        public Node Root;
        public TreeDataStruct()
        {
            GameObjects gameobject = new GameObjects();
            gameobject.SetName("Root");
            Root = new Node(gameobject, null);

        }

        public IEnumerator<Node> GetEnumerator()
        {
            return new BreadthEnum(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return  this.GetEnumerator();
        }
    }

    class BreadthEnum : IEnumerator<Node>
    {

        TreeDataStruct _tree;
        private Node _current = null;
        public Node Current => _current;

        Stack<Node> _currentCache = new Stack<Node>();
        Stack<Node> _nextCache = new Stack<Node>();

        object System.Collections.IEnumerator.Current => Current;

        public BreadthEnum(TreeDataStruct tree)
        {
            _tree = tree;
        }

        public void Dispose() { }

        bool _reachedEnd = false;
        public bool MoveNext()
        {
            if (_reachedEnd)
                return false;

            // Start of tree
            if(Current == null)
            {
                _current = _tree.Root;
                foreach (var child in _current.Children)
                    _currentCache.Push(child);           
                return true;
            }

            // Current level
            if(_currentCache.Count > 0)
            {
                _current = _currentCache.Pop();
                foreach (var child in _current.Children)
                    _nextCache.Push(child);
                return true;
            }

            // Next level
            else if(_nextCache.Count > 0)
            {
                while ( _nextCache.Count > 0)
                {
                    _currentCache.Push(_nextCache.Pop());
                }
                _nextCache.Clear();

                _current = _currentCache.Pop();
                foreach (var child in _current.Children)
                    _nextCache.Push(child);
                return true;
            }

            // END
            else
            {
                _reachedEnd = true;
                _current = null;
            }
            return false;
        }

        // YOU DONT HAVE TO IMPLEMENT
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    class Node
    {
        public bool IsLeaf => Children.Count == 0;
        public GameObjects Value;
        public List<Node> Children = new List<Node>();
        public Node Parent;
        public Node(GameObjects go , Node parent)
        {
            Value = go;
            if(parent != null)
            {
                Parent = parent;
                Parent.Children.Add(this);
            }
        }
    }

}
