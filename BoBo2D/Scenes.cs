using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class Scenes
    {
        private List<GameObjects> GameObjects;
        bool isActive;

        public Scenes()
        {
            GameObjects = new List<GameObjects>();
        }

        public void AddGameObject(GameObjects g)
        {
            GameObjects.Add(g);
            g.Enable();
        }
        public void RemoveGameObject(GameObjects g)
        {
            GameObjects.Remove(g);
            g.Disable();
        }
        public void RemoveGameObjectByName(string ObjectName)
        {
            foreach (GameObjects g in GameObjects)
            {
                if (g.GetName() == ObjectName)
                {
                    GameObjects.Remove(g);
                    g.Disable();
                }
            }
        }
        public bool IsSceneActive()
        {
            if (isActive)
            {
                return true;
            }

            return false;
        }
        public void ActivateScene()
        {
            isActive = true;
        }
        public void DeactivateScene()
        {
            isActive = false;
        }
        public void DrawAllObjects(SpriteBatch sb)
        {
            if (isActive)
            {
                for (int i = 0; i < GameObjects.Count; i++)
                {
                    for (int j = 0; j < GameObjects[i].GetComponentList().Count; j++)
                    {
                        GameObjects[i].GetComponenet(j).Draw(sb);
                    }
                }
            }
        }
        public void UpdateAllObjects(float elapsed)
        {
            if (isActive)
            {
                for (int i = 0; i < GameObjects.Count; i++)
                {
                    for (int j = 0; j < GameObjects[i].GetComponentList().Count; j++)
                    {
                        GameObjects[i].GetComponenet(j).Update(elapsed);
                    }
                }
            }
        }
        public void LoadScene(Scenes s)
        {
            s.ActivateScene();
        }
        public void UnloadScene(Scenes s)
        {
            s.DeactivateScene();
        }
    }
}
