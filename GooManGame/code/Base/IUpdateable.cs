namespace GooManGame.code.Base {
	/// <summary>
	/// Object which is contained within a <see cref="Scene"/> which is updated each frame.
	/// </summary>
	interface IUpdateable {
		void Update(float elapsed);
	}
}
