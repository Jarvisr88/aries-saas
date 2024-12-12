namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EmfPlusGraphicsStateStack
    {
        private readonly Stack<EmfPlusGraphicsState> graphicsStateStack = new Stack<EmfPlusGraphicsState>();
        private EmfPlusGraphicsState current = new EmfPlusGraphicsState();

        public EmfPlusGraphicsStateStack()
        {
            this.PageTransformationMatrix = PdfTransformationMatrix.Identity;
        }

        public void Pop()
        {
            this.current = this.graphicsStateStack.Pop();
        }

        public void Push(int id)
        {
            this.graphicsStateStack.Push(this.current.Clone(id));
        }

        public EmfPlusClip Clip
        {
            get => 
                this.current.Clip;
            set => 
                this.current.Clip = value;
        }

        public int? CurrentId =>
            this.current.StateId;

        public PdfTransformationMatrix Transform
        {
            get => 
                this.current.Transform;
            set => 
                this.current.Transform = value;
        }

        public PdfTransformationMatrix PageTransformationMatrix { get; set; }

        public PdfTransformationMatrix PageUnitTransformationMatrix
        {
            get => 
                this.current.PageUnitTransformationMatrix;
            set => 
                this.current.PageUnitTransformationMatrix = value;
        }

        private class EmfPlusGraphicsState
        {
            private int? stateId;
            private EmfPlusClip clip;
            private PdfTransformationMatrix transform = new PdfTransformationMatrix();

            public EmfPlusGraphicsState()
            {
                this.PageUnitTransformationMatrix = PdfTransformationMatrix.Identity;
            }

            public EmfPlusGraphicsStateStack.EmfPlusGraphicsState Clone(int newId) => 
                new EmfPlusGraphicsStateStack.EmfPlusGraphicsState { 
                    clip = this.clip,
                    transform = this.transform,
                    stateId = new int?(newId),
                    PageUnitTransformationMatrix = this.PageUnitTransformationMatrix
                };

            public EmfPlusClip Clip
            {
                get => 
                    this.clip;
                set => 
                    this.clip = value;
            }

            public int? StateId =>
                this.stateId;

            public PdfTransformationMatrix Transform
            {
                get => 
                    this.transform;
                set => 
                    this.transform = value;
            }

            public PdfTransformationMatrix PageUnitTransformationMatrix { get; set; }
        }
    }
}

