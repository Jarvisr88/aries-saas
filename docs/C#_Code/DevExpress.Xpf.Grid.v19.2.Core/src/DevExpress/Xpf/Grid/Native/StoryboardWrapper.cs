namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Animation;

    internal class StoryboardWrapper : IStoryboardWrapper
    {
        private readonly Storyboard storyboard;

        event EventHandler IStoryboardWrapper.Completed
        {
            add
            {
                this.storyboard.Completed += value;
            }
            remove
            {
                this.storyboard.Completed -= value;
            }
        }

        public StoryboardWrapper(Storyboard storyboard)
        {
            if (storyboard == null)
            {
                throw new ArgumentNullException();
            }
            this.storyboard = storyboard;
        }

        public void Begin()
        {
            this.storyboard.Begin();
        }

        public IEnumerable<Timeline> GetChildren() => 
            this.storyboard.Children;

        public void Seek(TimeSpan offset)
        {
            this.storyboard.Seek(offset);
        }

        public void Stop()
        {
            this.storyboard.Stop();
        }
    }
}

