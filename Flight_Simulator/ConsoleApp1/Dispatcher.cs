using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public class Dispatcher
    {
        #region FIELDS
        private const int MAX_SPEED = 1000;
        public int Penalties{ get; private set; }
        public string Name{ get; }
        private int weatherAdjustment;
        private Random rand;
        #endregion
        #region CTORS
        public Dispatcher()
        {
            Plane.AddDispatcher(this);
            rand = new Random(DateTime.Now.Millisecond);
            weatherAdjustment = rand.Next(-200, 200);
        }
        public Dispatcher(Plane plane, string name): this()
        {
            Name = name;
            plane.ChangingIndicators += HeightAdjustment;
        }
        #endregion
        #region METHODS
        /// <summary>
        /// Метод соответствующий делегату события.
        /// Расчитывает рекоммендуемую высоту полёта.
        /// </summary>
        /// <param name="plane">Sender</param>
        /// <param name="e">EventArgs</param>
        private void HeightAdjustment(Object plane, FlightEventArgs e)
        {
            int recommendedHight = 7 * e.Speed - weatherAdjustment;
            CheckingFlightParams(e, recommendedHight);
            Console.WriteLine($"Dispather: {Name}\tRecommended Hight: {recommendedHight}\tPenalties: {Penalties}");
        }
        /// <summary>
        /// Отписывает диспетчера от уведомлений изменения скорости, высоты самолёта.
        /// </summary>
        /// <param name="plane"></param>
        public void UnregisterDispatcher(Plane plane)
        {
            plane.ChangingIndicators -= HeightAdjustment;
        }
        /// <summary>
        /// Проверка на различные условия и начисление штрафных очков.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="recommendedHight"></param>
        private void CheckingFlightParams(FlightEventArgs e, int recommendedHight)
        {
            int difference = e.Hight - recommendedHight;
            if(difference < 0) { difference *= -1; }
            if(difference >= 300 && difference <= 600) { Penalties += 25; }
            else if(difference >= 600 && difference < 1000 ) { Penalties += 50; }
            if(difference > 1000) { throw new PlaneCrushException(message: "Plane has been crushed."); }
            if(Penalties >= 1000) { throw new UnsuitableForFlightsException(message: "Unfortunatly your failure exam."); }
            if(e.Speed == 0 && e.Hight == 0 && e.Start) { throw new PlaneCrushException(message: "Plane has been crushed."); }
            if(e.Speed > MAX_SPEED) { Penalties += 100; Console.WriteLine("Immediately slow down the speed!"); }
        }
        #endregion
    }
}
