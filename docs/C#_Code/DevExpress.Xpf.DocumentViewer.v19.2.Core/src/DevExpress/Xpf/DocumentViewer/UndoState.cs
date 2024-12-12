namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Runtime.CompilerServices;

    public class UndoState
    {
        protected bool Equals(UndoState other) => 
            Equals(this.State, other.State);

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((UndoState) obj) : false) : true) : false;

        public override int GetHashCode() => 
            (this.State != null) ? this.State.GetHashCode() : 0;

        public UndoActionType ActionType { get; set; }

        public NavigationState State { get; set; }

        public Action<NavigationState> Perform { get; set; }
    }
}

