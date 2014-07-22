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

namespace Shifumi.Core
{
    /// <summary>
    ///     Représente une command. Implémente ICommand.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Events

        /// <summary>
        ///     Evenement à déclencher lorsque la fonction qui détermine si la commande
        ///     peut être exécuté doit être réévalué.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Fields

        //Référence de méthode (delegate) qui attend un object en paramètre.
        private Action<object> _ExecuteMethodReference;

        //Référence de méthode (delegate) qui attend un object en paramètre 
        //et retourne un bool
        private Func<object, bool> _CanExecuteMethodReference;

        #endregion

        #region Constructors

        /// <summary>
        ///     Constructeur avec une action associé à la commande.
        /// </summary>
        /// <param name="executeAction">Action de la commande.</param>
        public DelegateCommand(Action<object> executeAction)
            : this(executeAction, null) //this permet de rappeler un autre constructeur de la classe.
        {

        }

        /// <summary>
        ///     Constructeur avec une action associé à la commande et une fonction
        ///     pour vérifier si la commande peut être exécuté.
        /// </summary>
        /// <param name="executeAction">Action de la commande.</param>
        /// <param name="canExecuteFunc">Fonction de teste de la commande.</param>
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }
            this._ExecuteMethodReference = executeAction;
            this._CanExecuteMethodReference = canExecuteFunc;
        }

        #endregion

        #region Methds

        /// <summary>
        ///     Méthode qui détermine si la commande peut être exécuté.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        /// <returns>Détermine si la commande peut être exécuté.</returns>
        public bool CanExecute(object parameter)
        {
            bool result = true;

            if (_CanExecuteMethodReference != null)
            {
                result = _CanExecuteMethodReference(parameter);
            }

            return result;
        }

        /// <summary>
        ///     Exécute la commande.
        /// </summary>
        /// <param name="parameter">Paramètre de la commande.</param>
        public void Execute(object parameter)
        {
            _ExecuteMethodReference(parameter);
        }

        /// <summary>
        ///     Déclenche l'évènement CanExecuteChanged.
        ///     Doit être appelé lorsque la fonction qui détermine si la commande
        ///     peut être exécuté doit être réévalué par le contrôle graphique.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        #endregion


    }
}
