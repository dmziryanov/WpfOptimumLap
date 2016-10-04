
using System;
using System.Windows;

namespace MobileRibbonMVVMSample
{
    public struct ListDescription
    {
        public static readonly ListDescription None = new ListDescription((TextMarkerStyle)999, 0);

        public ListDescription(TextMarkerStyle markerStyle, float markerOffset = 10) : this()
        {
            MarkerStyle = markerStyle;
            MarkerOffset = markerOffset;
        }

        public TextMarkerStyle MarkerStyle { get; set; }
        public double MarkerOffset { get; set; }

        public bool IsBullet
        {
            get { return MarkerStyle == TextMarkerStyle.Circle || 
                    MarkerStyle == TextMarkerStyle.Box || 
                    MarkerStyle == TextMarkerStyle.Disc || 
                    MarkerStyle == TextMarkerStyle.Square; }
        }

        public override bool Equals(object obj)
        {
            if(!(obj is ListDescription))
                return false;            
            return ((ListDescription)obj).MarkerStyle == MarkerStyle;
        }

        public override int GetHashCode()
        {
            return MarkerStyle.GetHashCode();
        }
    }
}
