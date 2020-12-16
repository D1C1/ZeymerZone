using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

public interface INavigationService
{
    void Navigate(Type sourcePage);
    void Navigate(Type sourcePage, object parameter);
    void GoBack();
}

//Source https://stackoverflow.com/questions/26816264/page-navigation-using-mvvm-in-store-app
namespace ZeymerZoneUWP
{
    public sealed class NavigationService : INavigationService
    {
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }
    }
}
