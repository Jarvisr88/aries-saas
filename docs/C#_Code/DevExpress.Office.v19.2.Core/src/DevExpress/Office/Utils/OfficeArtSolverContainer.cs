namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class OfficeArtSolverContainer : CompositeOfficeDrawingPartBase
    {
        public static OfficeArtSolverContainer FromStream(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtSolverContainer container = new OfficeArtSolverContainer();
            container.Read(reader, header);
            return container;
        }

        public OfficeArtFConnectionRule GetConnectionRule(int connectionShapeId)
        {
            OfficeArtFConnectionRule rule2;
            using (List<OfficeDrawingPartBase>.Enumerator enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        OfficeDrawingPartBase current = enumerator.Current;
                        OfficeArtFConnectionRule rule = current as OfficeArtFConnectionRule;
                        if ((rule == null) || (rule.ConnectionShapeId != connectionShapeId))
                        {
                            continue;
                        }
                        rule2 = rule;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return rule2;
        }

        protected internal void Read(BinaryReader reader, OfficeArtRecordHeader header)
        {
            for (int i = 0; i < header.InstanceInfo; i++)
            {
                OfficeArtRecordHeader header2 = OfficeArtRecordHeader.FromStream(reader);
                OfficeArtSolverContainerFileBlock item = this.ReadRule(reader, header2);
                if (item != null)
                {
                    base.Items.Add(item);
                }
            }
        }

        private OfficeArtSolverContainerFileBlock ReadRule(BinaryReader reader, OfficeArtRecordHeader header)
        {
            OfficeArtSolverContainerFileBlock block = OfficeArtSolverContainerFileBlockFactory.CreateInstance(header.TypeCode);
            if (block == null)
            {
                return null;
            }
            block.Read(reader);
            return block;
        }

        protected internal override bool ShouldWrite() => 
            base.Items.Count > 0;

        public override int HeaderVersion =>
            15;

        public override int HeaderInstanceInfo =>
            base.Items.Count;

        public override int HeaderTypeCode =>
            0xf005;
    }
}

