namespace GooManGame.States {
    /// <summary>
    /// Base class for all states of the game.
    /// </summary>
    public abstract class GameState {
        public abstract string Name { get; }
        public GameState NextState { get; set; }
        public bool loaded = false;

        public UI ui;
        public Camera camera;
        public EntityCollection entities;
        public Level level;

        public bool HasUI => ui != null;
        public bool HasCamera => camera != null;
        public bool HasEntities => entities != null;
        public bool HasLevel => level != null;

        public GameState(bool hasUI, bool hasCamera, bool hasEntities, bool hasLevel) {
            ui = hasUI ? new UI() : null;
            camera = hasCamera ? new Camera() : null;
            entities = hasEntities ? new EntityCollection() : null;
            level = hasLevel ? new Level() : null;

            ui?.SetOwner(this);
            camera?.SetOwner(this);
            entities?.SetOwner(this);
            level?.SetOwner(this);
        }

        public virtual void OnLoad() {
            Debug.Raise($"{Name} loading...");
        }
        public virtual void OnUnload() {
            Debug.Raise($"{Name} unloading...");
        }

        public virtual void Update(float elapsed) {
            ui?.Update();
            camera?.Update();
            entities?.Update();
        }

        public virtual void Draw() {
            camera?.Draw();
            if (!HasCamera)
                Game.Instance.Clear(PixelEngine.Pixel.Presets.Black);
            ui?.Draw();
        }
    }
}
