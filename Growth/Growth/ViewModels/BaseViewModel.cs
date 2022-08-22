using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Growth.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == null) return;
        VerifyPropertyName(e.PropertyName);
        PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Verify that the property name matches an existing public member property on this object.
    /// </summary>
    /// <param name="propertyName"></param>
    [Conditional("Debug")]
    [DebuggerStepThrough]
    public void VerifyPropertyName(string propertyName)
    {
        if (TypeDescriptor.GetProperties(this)[propertyName] != null) return;

        string msg = "Invalid property name: " + propertyName;
        throw new ArgumentException(msg);
    }
}
