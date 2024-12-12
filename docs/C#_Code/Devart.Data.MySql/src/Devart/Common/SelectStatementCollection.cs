namespace Devart.Common
{
    using System;
    using System.Collections;

    public sealed class SelectStatementCollection
    {
        private readonly ArrayList a = new ArrayList();
        private readonly SelectStatementNode b = new SelectStatementNode();
        private string c;
        private readonly IList d;

        public SelectStatementCollection(IList collection, string sqlPrefix)
        {
            this.d = collection;
            this.c = sqlPrefix;
        }

        public void AddDeleted(int index)
        {
            if (((SelectStatementNode) this.d[index]).Binded)
            {
                SelectStatementNode node = new SelectStatementNode();
                int num = -1;
                int num2 = -1;
                int num3 = index + 1;
                while (true)
                {
                    if (num3 < this.d.Count)
                    {
                        if (!((SelectStatementNode) this.d[num3]).Binded)
                        {
                            num3++;
                            continue;
                        }
                        num = num3;
                    }
                    int num4 = index - 1;
                    while (true)
                    {
                        if (num4 > -1)
                        {
                            if (!((SelectStatementNode) this.d[num4]).Binded)
                            {
                                num4--;
                                continue;
                            }
                            num2 = num4;
                        }
                        if (num != -1)
                        {
                            node.a = ((SelectStatementNode) this.d[index]).a;
                            node.b = ((SelectStatementNode) this.d[num]).a;
                        }
                        else if (num2 != -1)
                        {
                            node.a = ((SelectStatementNode) this.d[num2]).b + 1;
                            node.b = ((SelectStatementNode) this.d[index]).b + 1;
                        }
                        else
                        {
                            node.a = ((SelectStatementNode) this.d[index]).a;
                            node.b = ((SelectStatementNode) this.d[index]).b + 1;
                        }
                        this.a.Add(node);
                        return;
                    }
                }
            }
        }

        public void ResetSqlPrefix()
        {
            this.c = string.Empty;
        }

        public IList List =>
            this.d;

        public SelectStatementNode Node =>
            this.b;

        public IList Removed =>
            this.a;

        public string SqlPrefix =>
            this.c;
    }
}

