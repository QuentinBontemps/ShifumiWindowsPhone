using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Shifumi.Core
{
    [DataContract]
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region event

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        ///     Assigne une valeur à un champ privé et déclenché l'évènement PropertyChanged.
        /// </summary>
        /// <typeparam name="T">Type du champ.</typeparam>
        /// <param name="propertyName">Nom de la propriété.</param>
        /// <param name="field">Champ à mettre à jour.</param>
        /// <param name="value">Nouvelle valeur du champ.</param>
        protected void Assign<T>(string propertyName, ref T field, T value)
        {
            if (field == null || !field.Equals(value))
            {
                ThreadSafePropertyChanging(propertyName);
                field = value;
                ThreadSafePropertyChanged(propertyName);
            }
        }

        /// <summary>
        ///     Déclenche l'évènement PropertyChanged sur le Thread UI
        /// </summary>
        /// <param name="propertyName">Nom de la propriété à mettre à jour.</param>
        protected void ThreadSafePropertyChanged(string propertyName)
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OnPropertyChanged(propertyName));
        }

        /// <summary>
        ///     Déclenche l'évènement PropertyChanging sur le Thread UI
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui va être mettre à jour.</param>
        protected void ThreadSafePropertyChanging(string propertyName)
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OnPropertyChanging(propertyName));
        }


        /// <summary>
        ///     Déclenche l'évènement PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changé.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        ///     Déclenche l'évènement PropertyChanging.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui va changé.</param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler handler = PropertyChanging;

            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
