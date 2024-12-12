namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data;
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public sealed class CustomExpressionNodeModel : NodeModelBase
    {
        private readonly CustomExpressionNodeClient nodeClient;
        private DelegateCommand removeCommandCore;

        private CustomExpressionNodeModel(CustomExpressionNodeClient nodeClient, CriteriaOperator filter)
        {
            Guard.ArgumentNotNull(nodeClient, "nodeClient");
            this.nodeClient = nodeClient;
            this.Filter = filter;
            this.<ShowExpressionEditorCommand>k__BackingField = new DelegateCommand(new Action(this.ShowExpressionEditor));
        }

        internal static CustomExpressionNodeModel Create(CustomExpressionNodeClient client, CriteriaOperator filter) => 
            new CustomExpressionNodeModel(client, filter);

        private IDataColumnInfo CreateColumn(string expression)
        {
            List<IDataColumnInfo> list = this.GetColumns().ToList<IDataColumnInfo>();
            Func<FilteringColumnDataInfoAdapter> fallback = <>c.<>9__15_1;
            if (<>c.<>9__15_1 == null)
            {
                Func<FilteringColumnDataInfoAdapter> local1 = <>c.<>9__15_1;
                fallback = <>c.<>9__15_1 = (Func<FilteringColumnDataInfoAdapter>) (() => null);
            }
            return list.FirstOrDefault<IDataColumnInfo>().Return<IDataColumnInfo, FilteringColumnDataInfoAdapter>(x => new FilteringColumnDataInfoAdapter(x.Caption, x.Name, x.FieldType, list, expression), fallback);
        }

        private IEnumerable<IDataColumnInfo> GetColumns() => 
            (IEnumerable<IDataColumnInfo>) this.nodeClient.GetColumns().Select<VisualFilteringColumn, FilteringColumnDataInfoAdapter>(delegate (VisualFilteringColumn x) {
                object obj2;
                HeaderAppearance appearance1 = x.GetHeaderAppearance();
                if (appearance1 == null)
                {
                    HeaderAppearance local1 = appearance1;
                    obj2 = null;
                }
                else
                {
                    object content = appearance1.Content;
                    if (content != null)
                    {
                        obj2 = content.ToString();
                    }
                    else
                    {
                        object local2 = content;
                        obj2 = null;
                    }
                }
                object local3 = obj2;
                object obj3 = local3;
                if (local3 == null)
                {
                    object local4 = local3;
                    obj3 = string.Empty;
                }
                return new FilteringColumnDataInfoAdapter((string) obj3, x.Name, this.nodeClient.GetColumnType(x.Name), null, null);
            });

        private string GetFilterText()
        {
            string text1;
            CriteriaOperator filter = this.Filter;
            if (filter != null)
            {
                text1 = filter.ToString();
            }
            else
            {
                CriteriaOperator local1 = filter;
                text1 = null;
            }
            string local2 = text1;
            string expression = local2;
            if (local2 == null)
            {
                string local3 = local2;
                expression = string.Empty;
            }
            return UnboundExpressionConvertHelper.ConvertToCaption(this.CreateColumn(null), expression);
        }

        private static string GetString(EditorStringId id) => 
            EditorLocalizer.GetString(id);

        private void OnFilterChanged()
        {
            if (this.removeCommandCore == null)
            {
                DelegateCommand removeCommandCore = this.removeCommandCore;
            }
            else
            {
                this.removeCommandCore.RaiseCanExecuteChanged();
            }
            base.RaisePropertyChanged("FilterText");
        }

        private void ShowExpressionEditor()
        {
            IDialogService service = this.GetService<IDialogService>();
            if (service != null)
            {
                ExpressionEditorModel viewModel = new ExpressionEditorModel(this.CreateColumn(this.FilterText));
                MessageBoxResult? defaultButton = null;
                defaultButton = null;
                List<UICommand> dialogCommands = UICommand.GenerateFromMessageBoxButton(MessageBoxButton.OKCancel, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, defaultButton);
                if (service.ShowDialog(dialogCommands, GetString(EditorStringId.ExpressionEditor_Title), viewModel) == dialogCommands[0])
                {
                    this.Filter = CriteriaOperator.Parse(viewModel.Expression, new object[0]);
                }
            }
        }

        public string FilterText =>
            this.GetFilterText();

        public CriteriaOperator Filter
        {
            get => 
                base.GetValue<CriteriaOperator>("Filter");
            internal set => 
                base.SetValue<CriteriaOperator>(value, new Action(this.OnFilterChanged), "Filter");
        }

        public ICommand ShowExpressionEditorCommand { get; }

        public override ICommand RemoveCommand
        {
            get
            {
                this.removeCommandCore ??= new DelegateCommand(delegate {
                    this.nodeClient.RemoveNode(this);
                }, () => this.nodeClient.CanExecuteRemoveAction(new Lazy<CriteriaOperator>(() => this.BuildEvaluableFilter(null))), false);
                return this.removeCommandCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomExpressionNodeModel.<>c <>9 = new CustomExpressionNodeModel.<>c();
            public static Func<CustomExpressionNodeModel.FilteringColumnDataInfoAdapter> <>9__15_1;

            internal CustomExpressionNodeModel.FilteringColumnDataInfoAdapter <CreateColumn>b__15_1() => 
                null;
        }

        private sealed class FilteringColumnDataInfoAdapter : IDataColumnInfo
        {
            private readonly string caption;
            private readonly string fieldName;
            private readonly Type type;
            private readonly List<IDataColumnInfo> columns;
            private readonly string unboundExpression;

            internal FilteringColumnDataInfoAdapter(string caption, string fieldName, Type type, List<IDataColumnInfo> columns, string unboundExpression)
            {
                this.caption = caption;
                this.fieldName = fieldName;
                this.type = type;
                this.columns = columns;
                this.unboundExpression = unboundExpression;
            }

            List<IDataColumnInfo> IDataColumnInfo.Columns =>
                this.columns;

            string IDataColumnInfo.UnboundExpression =>
                this.unboundExpression;

            string IDataColumnInfo.Caption =>
                this.caption;

            string IDataColumnInfo.FieldName =>
                this.fieldName;

            string IDataColumnInfo.Name =>
                string.Empty;

            Type IDataColumnInfo.FieldType =>
                this.type;

            DataControllerBase IDataColumnInfo.Controller =>
                null;
        }
    }
}

