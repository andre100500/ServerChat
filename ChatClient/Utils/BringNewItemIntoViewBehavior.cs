using System.Collections.Specialized;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System;

namespace ChatClient.Utils
{
    public class BringNewItemIntoViewBehavior : Behavior<ItemsControl>
    {
        private INotifyCollectionChanged notify;

        protected override void OnAttached()
        {
            base.OnAttached();
            notify = AssociatedObject.Items as INotifyCollectionChanged;
            notify.CollectionChanged += ItemsControl_CollectionChanged;
        }

        private void ItemsControl_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                var newIndex = e.NewStartingIndex;
                var newElement = AssociatedObject.ItemContainerGenerator.ContainerFromIndex(newIndex);
                var item = (FrameworkElement)newElement;
                item?.BringIntoView();
            }
        }
    }
}
