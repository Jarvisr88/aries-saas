namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Text;

    [SvgElementNameAlias("path")]
    public class SvgPath : SvgElement
    {
        private SvgPathSegmentCollection segmentsCore = new SvgPathSegmentCollection();

        public SvgPath()
        {
            this.segmentsCore.CollectionChanged += new CollectionChangeEventHandler(this.OnSegmentsCollectionChanged);
        }

        public static SvgPath Create(SvgElementProperties properties, SvgPathSegmentCollection segments)
        {
            SvgPath path = new SvgPath {
                Segments = segments
            };
            path.Assign(properties);
            return path;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null)
        {
            SvgPath result = this.DeepCopy<SvgPath>(updateStyle);
            result.Segments.BeginUpdate();
            this.Segments.ForEach(delegate (SvgPathSegment x) {
                result.Segments.Add(x.DeepCopy());
            });
            result.Segments.EndUpdate();
            return result;
        }

        private void OnPathDataChanged()
        {
            this.Segments.BeginUpdate();
            this.Segments.Clear();
            SVGPathParser.Parse(this.PathData, this.Segments);
            this.Segments.CancelUpdate();
        }

        private void OnSegmentsCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            StringBuilder sb = new StringBuilder(this.Segments.Count);
            this.Segments.ForEach(x => sb.Append(x.ToString()));
            string str = sb.ToString();
            this.SetValueCore<string>("PathData", str);
        }

        [SvgPropertyNameAlias("d")]
        public string PathData
        {
            get => 
                this.GetValueCore<string>("PathData", false);
            internal set
            {
                value = value.Replace('\t', ' ');
                value = value.Replace('\r', ' ');
                value = value.Replace('\n', ' ');
                this.SetValueCore<string>("PathData", value, new Action(this.OnPathDataChanged));
            }
        }

        public SvgPathSegmentCollection Segments
        {
            get => 
                this.segmentsCore;
            internal set
            {
                if (!ReferenceEquals(this.segmentsCore, value))
                {
                    if (this.segmentsCore != null)
                    {
                        this.segmentsCore.CollectionChanged -= new CollectionChangeEventHandler(this.OnSegmentsCollectionChanged);
                    }
                    this.segmentsCore = value;
                    if (this.segmentsCore != null)
                    {
                        this.segmentsCore.CollectionChanged += new CollectionChangeEventHandler(this.OnSegmentsCollectionChanged);
                        this.OnSegmentsCollectionChanged(null, null);
                    }
                }
            }
        }
    }
}

