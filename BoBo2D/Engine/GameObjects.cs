using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class GameObjects
    {
        private List<Componenet> Components;
        private bool isEnabled;
        private string name;
        private Callbacks callbacks;

        public Vector2 Position { get; set; }
        public Texture2D Image { get; set; }
        public Rectangle Bounds { get; set; }
        public Color ImageColor { get; set; }

        public GameObjects()
        {
            Components = new List<Componenet>();
            isEnabled = true;
            callbacks = new Callbacks();
        }

        public List<Componenet> GetComponentList()
        {
            return Components;
        }

        public void SetComponentList(Componenet compo)
        {
            Components.Add(compo);
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void Disable()
        {
            isEnabled = false;

            foreach (Componenet c in Components)
            {
                c.Disable();
            }
        }
        public void Enable()
        {
            isEnabled = true;

            foreach (Componenet c in Components)
            {
                c.Enable();
            }
        }
        public bool IsEnable()
        {
            if (isEnabled)
            {
                return true;
            }
            else return false;
        }

        public void Destroy()
        {
            callbacks.OnDisable();
        }

        public void AddComponenet(Componenet componenet)
        {
            Components.Add(componenet);
            componenet.Enable();
        }

        public void RemoveComponenet(Componenet componenet)
        {
            Components.Remove(componenet);
            componenet.Disable();
        }

        public Componenet GetComponenet(int index)
        {
            return Components[index];
        }

        public bool IsCollision(GameObjects g)
        {
            if (this.Bounds.Intersects(g.Bounds))
            {
                return true;
            }
            else return false;
        }
        public bool IsCollisionList(List<GameObjects> g)
        {
            foreach(GameObjects a in g)
            {
                if (this.Bounds.Intersects(a.Bounds))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
