namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public static class DragEventTestInitHelper
    {
        public static void SetAllowDrag(StartRecordDragEventArgs args, bool allowDrag)
        {
            args.AllowDrag = allowDrag;
        }

        public static void SetAllowedEffects(DragEventArgsBase args, DragDropEffects allowedEffects)
        {
            args.AllowedEffects = allowedEffects;
        }

        public static void SetCanceled(CompleteRecordDragDropEventArgs args, bool canceled)
        {
            args.Canceled = canceled;
        }

        public static void SetDragElement(StartRecordDragEventArgs args, object dragElement)
        {
            args.DragElement = dragElement;
        }

        public static void SetEffects(CompleteRecordDragDropEventArgs args, DragDropEffects effects)
        {
            args.Effects = effects;
        }

        public static void SetEffects(GiveRecordDragFeedbackEventArgs args, DragDropEffects effects)
        {
            args.Effects = effects;
        }

        public static void SetEscapePressed(ContinueRecordDragEventArgs args, bool escapePressed)
        {
            args.EscapePressed = escapePressed;
        }

        public static void SetIsFromOutside(DragEventArgsBase args, bool isFromOutside)
        {
            args.IsFromOutside = isFromOutside;
        }

        public static void SetKeyStates(ContinueRecordDragEventArgs args, DragDropKeyStates keyStates)
        {
            args.KeyStates = keyStates;
        }

        public static void SetKeyStates(DragEventArgsBase args, DragDropKeyStates keyStates)
        {
            args.KeyStates = keyStates;
        }

        public static void SetRecords(CompleteRecordDragDropEventArgs args, object[] records)
        {
            args.Records = records;
        }

        public static void SetRecords(StartRecordDragEventArgs args, object[] records)
        {
            args.Records = records;
        }

        public static void SetTargetRecord(DragEventArgsBase args, object targetRecord)
        {
            args.TargetRecord = targetRecord;
        }
    }
}

