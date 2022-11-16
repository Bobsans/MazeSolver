namespace MazeSolver; 

public static class InfoChannel {
    static readonly object _lock = new();
    static string _info;

    public static string Info {
        get {
            lock (_lock) {
                Changed = false;
                return _info;
            }
        }
        private set {
            lock (_lock) {
                _info = value;
            }
        }
    }

    public static bool Changed { get; private set; }

    public static void Publish(string info) {
        lock (_lock) {
            Info = info;
            Changed = true;
        }
    }
}
