using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;

namespace AlfanousWP7.Helpers
{
    public static class HelperMethods
    {
        public static string Stringify(this IEnumerable<char> charEnumerable)
        {
            return new string(charEnumerable.ToArray());
        }
        public static string NumberToString(int number, string single, string couple, string plural)
        {
            return number == 1
                       ? String.Format("{0} واحدة", single)
                       : number == 2
                             ? couple
                             : number >= 3 && number <= 10
                                   ? String.Format("{0} {1}", number, plural)
                                   : String.Format("{0} {1}", number, single);
        }
        public static RotateTransition GetTransitionFrom(this PageOrientation currentOrientation, PageOrientation previousOrientation)
        {
            var transitionElement = new RotateTransition();
            switch (currentOrientation)
            {
                case PageOrientation.Landscape:
                case PageOrientation.LandscapeRight:
                    // Come here from PortraitUp (i.e. clockwise) or LandscapeLeft?
                    transitionElement.Mode = previousOrientation == PageOrientation.PortraitUp
                        ? RotateTransitionMode.In90Counterclockwise
                        : RotateTransitionMode.In180Clockwise;
                    break;
                case PageOrientation.LandscapeLeft:
                    // Come here from LandscapeRight or PortraitUp?
                    transitionElement.Mode = previousOrientation == PageOrientation.LandscapeRight
                        ? RotateTransitionMode.In180Counterclockwise
                        : RotateTransitionMode.In90Clockwise;
                    break;
                case PageOrientation.Portrait:
                case PageOrientation.PortraitUp:
                    // Come here from LandscapeLeft or LandscapeRight?
                    transitionElement.Mode = previousOrientation == PageOrientation.LandscapeLeft
                        ? RotateTransitionMode.In90Counterclockwise
                        : RotateTransitionMode.In90Clockwise;
                    break;
            }
            return transitionElement;
        }
    }
}