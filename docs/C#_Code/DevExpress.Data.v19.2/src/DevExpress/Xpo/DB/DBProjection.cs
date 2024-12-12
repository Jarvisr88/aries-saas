namespace DevExpress.Xpo.DB
{
    using DevExpress.Utils;
    using System;

    [Serializable]
    public class DBProjection : DBTable
    {
        public SelectStatement Projection;
        private static int HashSeed = HashCodeHelper.StartGeneric<string>(typeof(DBProjection).Name);

        public DBProjection()
        {
        }

        public DBProjection(SelectStatement projection)
        {
            this.Projection = projection;
        }

        public override bool Equals(object obj)
        {
            DBProjection projection = obj as DBProjection;
            return ((projection != null) ? Equals(this.Projection, projection.Projection) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.FinishGeneric<SelectStatement>(HashSeed, this.Projection);

        public override string ToString() => 
            (this.Projection == null) ? "null" : this.Projection.ToString();
    }
}

