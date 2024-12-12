namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class JPXResolutionPrecinctComponentLayerPacketProgressionOrder : JPXPacketProgressionOrder
    {
        public JPXResolutionPrecinctComponentLayerPacketProgressionOrder(JPXImage image, JPXTile tile) : base(image, tile)
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
            public JPXResolutionPrecinctComponentLayerPacketProgressionOrder <>4__this;
            private JPXCodingStyleComponent[] <codingStyleComponents>5__1;
            private int <resolution>5__2;
            private int <precinct>5__3;
            private JPXTileComponent[] <components>5__4;
            private int <component>5__5;
            private int <layer>5__6;
            private int <layersCount>5__7;
            private int <componentsCount>5__8;
            private int <maxPrecinct>5__9;
            private int <maxResolutionLevel>5__10;

            [DebuggerHidden]
            public <GetEnumerator>d__1(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num4;
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<maxPrecinct>5__9 = 0;
                    this.<maxResolutionLevel>5__10 = this.<>4__this.MaxResolutionLevel;
                    JPXImage image = this.<>4__this.Image;
                    this.<codingStyleComponents>5__1 = image.CodingStyleComponents;
                    this.<componentsCount>5__8 = this.<codingStyleComponents>5__1.Length;
                    this.<components>5__4 = this.<>4__this.Tile.Components;
                    int index = 0;
                    while (true)
                    {
                        if (index >= this.<componentsCount>5__8)
                        {
                            this.<layersCount>5__7 = image.CodingStyleDefault.NumberOfLayers;
                            this.<resolution>5__2 = 0;
                            break;
                        }
                        JPXCodingStyleComponent component = this.<codingStyleComponents>5__1[index];
                        int resolutionLevelIndex = 0;
                        while (true)
                        {
                            if (resolutionLevelIndex >= this.<maxResolutionLevel>5__10)
                            {
                                index++;
                                break;
                            }
                            if (resolutionLevelIndex <= component.DecompositionLevelCount)
                            {
                                this.<maxPrecinct>5__9 = Math.Max(this.<maxPrecinct>5__9, this.<components>5__4[index].GetResolutionLevel(resolutionLevelIndex).PrecinctCount);
                            }
                            resolutionLevelIndex++;
                        }
                    }
                    goto TR_0003;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    num4 = this.<layer>5__6;
                    this.<layer>5__6 = num4 + 1;
                }
                goto TR_0012;
            TR_0003:
                if (this.<resolution>5__2 >= this.<maxResolutionLevel>5__10)
                {
                    return false;
                }
                this.<precinct>5__3 = 0;
            TR_0007:
                while (true)
                {
                    if (this.<precinct>5__3 < this.<maxPrecinct>5__9)
                    {
                        this.<component>5__5 = 0;
                    }
                    else
                    {
                        num4 = this.<resolution>5__2;
                        this.<resolution>5__2 = num4 + 1;
                        goto TR_0003;
                    }
                    break;
                }
            TR_000D:
                while (true)
                {
                    if (this.<component>5__5 < this.<componentsCount>5__8)
                    {
                        JPXCodingStyleComponent component2 = this.<codingStyleComponents>5__1[this.<component>5__5];
                        if ((this.<resolution>5__2 > component2.DecompositionLevelCount) || (this.<precinct>5__3 >= this.<components>5__4[this.<component>5__5].GetResolutionLevel(this.<resolution>5__2).PrecinctCount))
                        {
                            goto TR_000F;
                        }
                        else
                        {
                            this.<layer>5__6 = 0;
                        }
                    }
                    else
                    {
                        num4 = this.<precinct>5__3;
                        this.<precinct>5__3 = num4 + 1;
                        goto TR_0007;
                    }
                    break;
                }
                goto TR_0012;
            TR_000F:
                while (true)
                {
                    num4 = this.<component>5__5;
                    this.<component>5__5 = num4 + 1;
                    break;
                }
                goto TR_000D;
            TR_0012:
                while (true)
                {
                    if (this.<layer>5__6 >= this.<layersCount>5__7)
                    {
                        break;
                    }
                    this.<>2__current = new JPXPacketPosition(this.<component>5__5, this.<layer>5__6, this.<resolution>5__2, this.<precinct>5__3);
                    this.<>1__state = 1;
                    return true;
                }
                goto TR_000F;
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

