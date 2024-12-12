namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TokenEditorSelection
    {
        private Locker selectionChangedLocker = new Locker();
        private Locker syncSelectionLocker = new Locker();

        public TokenEditorSelection(TokenEditor owner)
        {
            this.Owner = owner;
            this.SelectedTokensIndexes = new List<int>();
        }

        public void ResetSelection()
        {
            this.selectionChangedLocker.DoLockedAction(delegate {
                this.SelectedTokensIndexes = new List<int>();
                if (!this.syncSelectionLocker.IsLocked)
                {
                    this.Owner.SelectedTokens.Clear();
                }
                this.UpdateVisibleSelection();
            });
        }

        public void SelectToken(TokenEditorPresenter token)
        {
            int item = this.Owner.EditableIndexOfToken(token);
            if (this.SelectedTokensIndexes.Contains(item))
            {
                this.UnselectTokenByIndex(item);
            }
            else
            {
                this.SelectTokenByIndex(item);
            }
        }

        public void SelectTokenByIndex(int index)
        {
            this.selectionChangedLocker.DoLockedAction(delegate {
                if (!this.SelectedTokensIndexes.Contains(index))
                {
                    this.SelectedTokensIndexes.Add(index);
                    this.SyncOwnerWithSelection(index, true);
                    this.UpdateVisibleSelection();
                }
            });
        }

        public void SetStartSelectionIndex(int index)
        {
            this.StartSelectionIndex = index;
        }

        private void SyncOwnerWithSelection(int index, bool isAdd)
        {
            if (!this.syncSelectionLocker.IsLocked)
            {
                object obj2;
                if (!this.Owner.ProvideValue(index, out obj2))
                {
                    this.Owner.SelectedTokens.Clear();
                }
                else if (obj2 != null)
                {
                    if (isAdd)
                    {
                        this.Owner.SelectedTokens.Add(obj2);
                    }
                    else if (this.Owner.SelectedTokens.Contains(obj2))
                    {
                        this.Owner.SelectedTokens.Remove(obj2);
                    }
                }
            }
        }

        public void SyncSelectionWithOwner()
        {
            this.syncSelectionLocker.DoLockedAction(new Action(this.SyncSelectionWithOwnerCore));
        }

        private void SyncSelectionWithOwnerCore()
        {
            if (!this.IsSelectionChanging)
            {
                this.ResetSelection();
                foreach (object obj2 in this.Owner.SelectedTokens)
                {
                    int index = this.Owner.ProvideIndex(obj2);
                    if (index != -1)
                    {
                        this.SelectTokenByIndex(index);
                    }
                }
            }
        }

        public void UnselectTokenByIndex(int index)
        {
            this.selectionChangedLocker.DoLockedAction(delegate {
                if (this.SelectedTokensIndexes.Contains(index))
                {
                    this.SelectedTokensIndexes.Remove(index);
                    this.SyncOwnerWithSelection(index, false);
                    this.UpdateVisibleSelection();
                }
            });
        }

        public void UpdateVisibleSelection()
        {
            Dictionary<int, UIElement> visibleTokens = this.Owner.GetVisibleTokens();
            if (visibleTokens != null)
            {
                foreach (int num in visibleTokens.Keys)
                {
                    TokenEditorPresenter presenter = visibleTokens[num] as TokenEditorPresenter;
                    presenter.IsSelected = this.SelectedTokensIndexes.Contains(num);
                }
            }
        }

        public List<int> SelectedTokensIndexes { get; private set; }

        public int StartSelectionIndex { get; private set; }

        public bool HasSelectedTokens =>
            this.SelectedTokensIndexes.Count > 0;

        private bool IsSelectionChanging =>
            this.selectionChangedLocker.IsLocked;

        private TokenEditor Owner { get; set; }
    }
}

