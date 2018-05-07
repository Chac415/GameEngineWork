using System.Collections.Generic;
using System.Linq;
using Engine.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Buttons
{
    /// <summary>
    /// Class to store buttons within a list
    /// 
    /// Author: Carl Chalmers
    /// Date of change: 04/03/18
    /// Version 0.5
    /// 
    /// </summary>
    public class ButtonManager: IProvider
    {
        #region Variables

        public IDictionary<string, IButton> Buttons { get; private set; }

        #endregion

        #region Methods

        public ButtonManager()
        {
            Buttons = new Dictionary<string, IButton>();
        }

        public void AddButton(string name, IButton button)
        {
            Buttons.Add(name, button);
        }

        public void RemoveButton(string name)
        {
            Buttons.Remove(name);
        }

        public void Update()
        {
            IList<IButton> buttons = Buttons.Values.ToList();
            foreach (IButton butt in buttons)
            {
                butt.update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            IList<IButton> buttons = Buttons.Values.ToList();
            foreach (IButton butt in buttons)
            {
                butt.Draw(spriteBatch);
            }
        }

        #endregion

    }
}
