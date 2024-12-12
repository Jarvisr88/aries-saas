namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class JPXResolutionLayerComponentPrecinctPacketProgressionOrder : JPXPacketProgressionOrder
    {
        public JPXResolutionLayerComponentPrecinctPacketProgressionOrder(JPXImage image, JPXTile tile) : base(image, tile)
        {
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__1))]
        public override IEnumerator<JPXPacketPosition> GetEnumerator()
        {
            <GetEnumerator>d__1 d__1 = new <GetEnumerator>d__1(0);
            d__1.<>4__this = this;
            return d__1;
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__1 : IEnumerator<JPXPacketPosition>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private JPXPacketPosition <>2__current;
            public JPXResolutionLayerComponentPrecinctPacketProgressionOrder <>4__this;
            private int <resolution>5__1;
            private JPXCodingStyleComponent[] <codingStyleComponents>5__2;
            private JPXTileComponent[] <components>5__3;
            private int <component>5__4;
            private int <layer>5__5;
            private int <precinct>5__6;
            private int <precinctCount>5__7;
            private int <codingStyleComponentsCount>5__8;
            private int <layersCount>5__9;
            private int <maxResoltuionLevel>5__10;

            [DebuggerHidden]
            public <GetEnumerator>d__1(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num2;
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<codingStyleComponents>5__2 = this.<>4__this.Image.CodingStyleComponents;
                    this.<maxResoltuionLevel>5__10 = this.<>4__this.MaxResolutionLevel;
                    this.<layersCount>5__9 = this.<>4__this.Image.CodingStyleDefault.NumberOfLayers;
                    this.<codingStyleComponentsCount>5__8 = this.<codingStyleComponents>5__2.Length;
                    this.<components>5__3 = this.<>4__this.Tile.Components;
                    this.<resolution>5__1 = 0;
                    goto TR_0003;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    num2 = this.<precinct>5__6;
                    this.<precinct>5__6 = num2 + 1;
                }
                goto TR_0011;
            TR_0003:
                if (this.<resolution>5__1 >= this.<maxResoltuionLevel>5__10)
                {
                    return false;
                }
                this.<layer>5__5 = 0;
            TR_0007:
                while (true)
                {
                    if (this.<layer>5__5 < this.<layersCount>5__9)
                    {
                        this.<component>5__4 = 0;
                    }
                    else
                    {
                        num2 = this.<resolution>5__1;
                        this.<resolution>5__1 = num2 + 1;
                        goto TR_0003;
                    }
                    break;
                }
            TR_000C:
                while (true)
                {
                    if (this.<component>5__4 < this.<codingStyleComponentsCount>5__8)
                    {
                        if (this.<resolution>5__1 > this.<codingStyleComponents>5__2[this.<component>5__4].DecompositionLevelCount)
                        {
                            goto TR_000E;
                        }
                        else
                        {
                            this.<precinctCount>5__7 = this.<components>5__3[this.<component>5__4].GetResolutionLevel(this.<resolution>5__1).PrecinctCount;
                            this.<precinct>5__6 = 0;
                        }
                    }
                    else
                    {
                        num2 = this.<layer>5__5;
                        this.<layer>5__5 = num2 + 1;
                        goto TR_0007;
                    }
                    break;
                }
                goto TR_0011;
            TR_000E:
                while (true)
                {
                    num2 = this.<component>5__4;
                    this.<component>5__4 = num2 + 1;
                    break;
                }
                goto TR_000C;
            TR_0011:
                while (true)
                {
                    if (this.<precinct>5__6 >= this.<precinctCount>5__7)
                    {
                        break;
                    }
                    this.<>2__current = new JPXPacketPosition(this.<component>5__4, this.<layer>5__5, this.<resolution>5__1, this.<precinct>5__6);
                    this.<>1__state = 1;
                    return true;
                }
                goto TR_000E;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            JPXPacketPosition IEnumerator<JPXPacketPosition>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

