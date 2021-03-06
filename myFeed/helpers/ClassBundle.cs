﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace myFeed
{
    public static class Tools
    {
        public static void AnimateOpacity(DependencyObject target, int from, int to, double duration)
        {
            DoubleAnimation fade = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(duration),
                EnableDependentAnimation = true,
            };
            Storyboard.SetTarget(fade, target);
            Storyboard.SetTargetProperty(fade, "Opacity");

            DiscreteObjectKeyFrame dokf = new DiscreteObjectKeyFrame()
            {
                KeyTime = (from > to) ? TimeSpan.FromMilliseconds(duration) : TimeSpan.FromMilliseconds(0),
                Value = (from > to) ? Visibility.Collapsed : Visibility.Visible
            };
            ObjectAnimationUsingKeyFrames oaukf = new ObjectAnimationUsingKeyFrames() { EnableDependentAnimation = true };
            oaukf.KeyFrames.Add(dokf);
            Storyboard.SetTarget(oaukf, target);
            Storyboard.SetTargetProperty(oaukf, "Visibility");

            Storyboard openpane = new Storyboard();
            openpane.Children.Add(fade);
            openpane.Children.Add(oaukf);
            openpane.Begin();
        }
    }

    public class ListFeed
    {
        public string feedtitle { get; set; }
        public string feedsubtitle { get; set; }
        public string feedid { get; set; }
        public string feedimg { get; set; }
        [XmlIgnore]
        public string feedlink { get; set; } = string.Empty;
    }

    public class Bag
    {
        public string notification;
        public List<Website> list;
        public Frame frame;
        public Frame parentframe;
    }

    [DataContract]
    public class Categories
    {
        public List<Category> categories;
    }

    [DataContract]
    public class Category
    {
        public string title;
        public List<Website> websites;
    }

    [DataContract]
    public class Website
    {
        public string url;
        public bool notify = true;
    }

    [DataContract]
    public class PFeedItem
    {
        public string title { get; set; }

        public string feed { get; set; }

        public string link { get; set; }

        public string image { get; set; }

        public string content { get; set; }
        
        public string dateoffset { get; set; }
        
        public double opacity { get; set; }

        public int iconopacity { get; set; } = 1;
        
        [XmlIgnore]
        public DateTimeOffset PublishedDate { get; set; }

        [XmlElement("PublishedDate")]
        public string SomeDateString
        {
            get
            {
                return this.PublishedDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                this.PublishedDate = DateTime.Parse(value);
            }
        }
        
        public string GetTileId()
        {
            return + (int)this.title.First()
                   + PublishedDate.Second.ToString()
                   + PublishedDate.Minute.ToString()
                   + PublishedDate.Hour.ToString()
                   + PublishedDate.Day.ToString();
        }
    }
}
