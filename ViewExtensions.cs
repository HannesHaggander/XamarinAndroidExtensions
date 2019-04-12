using System;
using Android.Content.Res;
using Android.Views;

public static class ViewExtensions
{
    #region ClickActions

    public static View SetClickAction(this View view, Action action)
    {
        view.SetOnClickListener(new OnClickJava { Click = action });
        return view;
    }

    public static View SetLongClickAction(this View view, Action action)
    {
        view.SetOnLongClickListener(new OnLongClickJava{ Click = action});
        return view;
    }

    #endregion

    public static T FindViewOrThrow<T>(this View view, int resId) where T : View
    {
        if(view == null) { throw new ArgumentNullException(nameof(view)); }
        return (T) view.FindViewById(resId) 
               ?? throw new Resources.NotFoundException($"Failed to find [{resId}] in view: [{view}]");
    }
}

internal class OnClickJava : Java.Lang.Object, View.IOnClickListener
{
    public void OnClick(View v) => Click?.Invoke();
    public Action Click { get; set; }
}

internal class OnLongClickJava : Java.Lang.Object, View.IOnLongClickListener
{
    public bool OnLongClick(View v)
    {
        Click?.Invoke();
        return true;
    }

    public Action Click;
}
