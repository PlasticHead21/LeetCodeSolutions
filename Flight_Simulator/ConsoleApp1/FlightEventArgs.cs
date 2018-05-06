using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    public class FlightEventArgs: EventArgs
    {
        #region FIELDS
        public bool Start { get; private set; }
        public int Hight { get; private set; }
        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
              if (value < 0)
              {
                throw new Exception(message: "Speed cant be negative.");
              }
              speed = value;  
            }
        }
        public bool IsPlaneLanded{ get; private set; }
        #endregion
        #region CTOR
        public FlightEventArgs(){}
        #endregion
        #region METHODS
        /// <summary>
        /// Проверяет нажата комбинация клавиш, или просто одна клавиша.
        /// Начиляет очки в соответствии с этим.
        /// </summary>
        /// <param name="indicator"></param>
        public void Higher(bool indicator)
        {
            Hight += indicator ? 500 : 250;
            ShowParams();
        }
        /// <summary>
        /// Проверяет нажата комбинация клавиш, или просто одна клавиша.
        /// Начиляет очки в соответствии с этим.
        /// </summary>
        /// <param name="indicator"></param>
        public void Lower(bool indicator)
        {
            Hight -= indicator ? 500 : 250;
            ShowParams();
        }
        /// <summary>
        /// Проверяет нажата комбинация клавиш, или просто одна клавиша.
        /// Начиляет очки в соответствии с этим.
        /// </summary>
        /// <param name="indicator"></param>
        public void Faster(bool indicator)
        {
            if(Speed == 0 && Hight == 0) { Start = true; }
            Speed += indicator ? 150 : 50;
            ShowParams();
        }
        /// <summary>
        /// Проверяет нажата комбинация клавиш, или просто одна клавиша.
        /// Начиляет очки в соответствии с этим.
        /// </summary>
        /// <param name="indicator"></param>
        public void Slower(bool indicator)
        {
            {
               Speed -= indicator ? 150 : 50;
               ShowParams();
            }
        }
        /// <summary>
        /// При достижении определённой высоты выводит сообщение, что самолёт начал снижение.
        /// И переводит свойство <Start>false</Start>
        /// </summary>
        public void Landing()
        {
            Start = false;
            Console.WriteLine("Plane start landing.");
        }
        /// <summary>
        /// Указывает, что самолёт приземлился.
        /// </summary>
        public void PlaneLanded()
        {
            IsPlaneLanded = true;
            Console.WriteLine("Plane has been landed.");
        }
        /// <summary>
        /// Показывает параметры полёта самолёта.
        /// </summary>
        public void ShowParams()
        {
            Console.WriteLine($"Hight of the plane: {Hight}");
            Console.WriteLine($"Speed of the plane: {Speed}");
        }
        #endregion
    }
}
