using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public class Plane
    {
        #region FIELDS
        public int SummOfPenalties { get; private set; }
        static List<Dispatcher> dispatchersList;
        public event EventHandler<FlightEventArgs> ChangingIndicators;
        #endregion
        #region CTOR
        public Plane()
        {
            dispatchersList = new List<Dispatcher>();
        }
        #endregion
        #region EVENT
        /// <summary>
        /// Защищённый виртуальный метод, который вызывает 
        /// делегаты сохранённые в событии. Для безопасности потоков создаётся временная копия <temp></temp>
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ChangeIndicators(FlightEventArgs e)
        {
            EventHandler<FlightEventArgs> temp = ChangingIndicators;
            temp?.Invoke(this, e);
        }
        /// <summary>
        /// Метод проверяет скорость самолёта, если скорость больше, либо равна 50, 
        /// то заставляет диспетчеров из списка отписаться от события.
        /// Если скорость более 50 то запускает метод вызывающий собыие.
        /// </summary>
        /// <param name="e"></param>
        public void ReportToDispatcher(FlightEventArgs e)
        {
            if (e.Speed <= 50 && !e.Start)
            {
                foreach(Dispatcher dispetcher in dispatchersList)
                {
                    dispetcher.UnregisterDispatcher(this);
                }
            }
            else
                ChangeIndicators(e);
        }
        #endregion
        #region METHODS
        /// <summary>
        /// Добавляет диспетчера в список
        /// </summary>
        /// <param name="dispatcher"></param>
        public static void AddDispatcher(Dispatcher dispatcher)
        {
            dispatchersList.Add(dispatcher); 
        }
        /// <summary>
        /// Возвращает колво диспетчеров в списке.
        /// </summary>
        /// <returns>int</returns>
        public static int CountDispatchers => dispatchersList.Count;
        /// <summary>
        /// Удаляет диспетчера из списка. Предварительно записав насчитанные им штрафные очки.
        /// </summary>
        /// <param name="name"></param>
        public void DeleteDispatcher(string name)
        {
            if (dispatchersList.Count == 2) { Console.WriteLine("You cant delete dispatcher. Flight must controle minimum 2 dispatchers."); }
            else
            {
                for (int index = 0; index < dispatchersList.Count; index++)
                {
                    if (dispatchersList[index].Name == name)
                    {
                        SummOfPenalties += dispatchersList[index].Penalties;
                        dispatchersList[index].UnregisterDispatcher(this);
                        dispatchersList.Remove(dispatchersList[index]);
                    }
                }
            }
        }
        /// <summary>
        /// Суммирует штрафные очки всех диспетчеров из списка.
        /// </summary>
        public void CountAllPenaltiesPoints()
        {
            foreach(Dispatcher dispatcher in dispatchersList)
            {
                SummOfPenalties += dispatcher.Penalties;
            }
            Console.WriteLine($"Summ of your penalties:{SummOfPenalties}");
        }
        #endregion
    }
}
