using System.Collections.Generic;

namespace GooManGame.code.Base {
	/// <summary>
	/// Discrete state of the game IE main menu, settings menu, play.
	/// </summary>
	public abstract class Scene : IUpdateable{
		#region updateables
		List<IUpdateable> updateables;
		public void AddUpdateable(IUpdateable ie) =>
			updateables.Add(ie);
		public void RemoveUpdateable(IUpdateable ie) =>
			updateables.Remove(ie);
		#endregion updateables

		public virtual void Update(float elapsed) {
			foreach (IUpdateable ie in updateables)
				ie.Update(elapsed);
		}

		public Scene()
        {
			updateables = new List<IUpdateable>();
        }
	}
}
